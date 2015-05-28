using Lumia.Imaging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using VideoEffects;
using Windows.Devices.Enumeration;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Media.Capture;
using Windows.Media.Devices;
using Windows.Media.MediaProperties;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI.Core;
using Windows.UI.Popups;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using ZXing;
using ZXing.Common;

// Die Elementvorlage "Leere Seite" ist unter http://go.microsoft.com/fwlink/?LinkID=390556 dokumentiert.

namespace CampusApp.Evaluation
{
    /// <summary>
    /// Eine leere Seite, die eigenständig verwendet werden kann oder auf die innerhalb eines Rahmens navigiert werden kann.
    /// </summary>
    public sealed partial class QR_Code : Page
    {          
        MediaCapture mediaCapture;
        BarcodeReader reader;
        FocusControl focusControl;
        DispatcherTimer dispatcherTimer;

        public QR_Code()
        {
            this.InitializeComponent();

            var appView = Windows.UI.ViewManagement.ApplicationView.GetForCurrentView();
            appView.SetDesiredBoundsMode(ApplicationViewBoundsMode.UseCoreWindow);

            //Application.Current.Resuming += Application_Resuming;
            //Application.Current.Suspending += Application_Suspending;

            Application.Current.Suspending += new SuspendingEventHandler(App_Suspending);
            Application.Current.Resuming += new EventHandler<Object>(App_Resuming);
                              
        }

        void App_Resuming(object sender, object e)
        {
             createCamera();
             DispatcherTimerOn();
        }

        void App_Suspending(Object sender, Windows.ApplicationModel.SuspendingEventArgs e)
        {                   
            disposeCamera();
            DispatcherTimerOff();
        }

        async void disposeCamera()
        {
            
            // Release resources
            if (mediaCapture != null)
            {
                removeEffects();
                await mediaCapture.StopPreviewAsync();              
                captureElement.Source = null;
                mediaCapture.Dispose();
            }

        }

        async void createCamera()
        {
          
            CaptureUse primaryUse = CaptureUse.Video;

            mediaCapture = new MediaCapture();

            DeviceInformationCollection webcamList = await DeviceInformation.FindAllAsync(DeviceClass.VideoCapture);

            DeviceInformation backWebcam = (from webcam in webcamList
                                            where webcam.EnclosureLocation != null
                                            && webcam.EnclosureLocation.Panel == Windows.Devices.Enumeration.Panel.Back
                                            select webcam).FirstOrDefault();


            await mediaCapture.InitializeAsync(new MediaCaptureInitializationSettings
            {
                VideoDeviceId = backWebcam.Id,
                AudioDeviceId = "",
                StreamingCaptureMode = StreamingCaptureMode.Video,
                PhotoCaptureSource = PhotoCaptureSource.VideoPreview
            });

            mediaCapture.VideoDeviceController.PrimaryUse = primaryUse;

            mediaCapture.SetPreviewRotation(VideoRotation.Clockwise90Degrees);

            mediaCapture.VideoDeviceController.FlashControl.Enabled = false;

            setEffects();

            reader = new BarcodeReader
            {
                Options = new DecodingOptions
                {
                    PossibleFormats = new BarcodeFormat[] { BarcodeFormat.QR_CODE },
                    TryHarder = true
                }
            };


            captureElement.Source = mediaCapture;
            await mediaCapture.StartPreviewAsync();

            focusControl = mediaCapture.VideoDeviceController.FocusControl;

            if (focusControl.Supported == true)
            {
                FocusSettings focusSetting = new FocusSettings()
                {
                    AutoFocusRange = AutoFocusRange.FullRange,
                    Mode = FocusMode.Auto,
                    WaitForFocus = false,
                    DisableDriverFallback = false
                };

                focusControl.Configure(focusSetting);
                await mediaCapture.VideoDeviceController.ExposureControl.SetAutoAsync(true);
                //await focusControl.FocusAsync();


            }

            // zoom
            if (this.mediaCapture.VideoDeviceController.ZoomControl.Supported)
            {
                sliderZoom.Minimum = this.mediaCapture.VideoDeviceController.ZoomControl.Min;
                sliderZoom.Maximum = this.mediaCapture.VideoDeviceController.ZoomControl.Max;
                sliderZoom.StepFrequency = this.mediaCapture.VideoDeviceController.ZoomControl.Step;
            }
            
        }

        private void sliderZoom_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            if (this.mediaCapture.VideoDeviceController.ZoomControl.Supported)
            {
                this.mediaCapture.VideoDeviceController.ZoomControl.Value = (float)e.NewValue;
            }
        }

        
        void AnalyzeBitmap(Bitmap bitmap, TimeSpan time)
        {
            Result result = reader.Decode(
                bitmap.Buffers[0].Buffer.ToArray(),
                (int)bitmap.Buffers[0].Pitch,
                (int)bitmap.Dimensions.Height,
                BitmapFormat.Gray8
                );

            if (result != null)
            {
                var ignore = Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                {
                    DispatcherTimerOff();
                    removeEffects();

                    // check http url                  
                    if (Uri.IsWellFormedUriString(result.Text, UriKind.RelativeOrAbsolute))
                        mDialog(result.Text);
                    else
                        mDialog("Falscher QR-Code!" + Environment.NewLine + "Weiter scannen?");

                });
            }
        }

        async private void setEffects()
        {
            var definition = new LumiaAnalyzerDefinition(ColorMode.Yuv420Sp, 640, AnalyzeBitmap);

            await mediaCapture.AddEffectAsync(
                MediaStreamType.VideoPreview,
                definition.ActivatableClassId,
                definition.Properties
            );
        }

        async private void removeEffects()
        {            
            await mediaCapture.ClearEffectsAsync(
                MediaStreamType.VideoPreview              
            );
        }

        private void DispatcherTimerSetup()
        {
            dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += dispatcherTimer_Tick;
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            dispatcherTimer.Start();
        }

        async void dispatcherTimer_Tick(object sender, object e)
        {
            try
            {
                if (focusControl.Supported == true)
                {
                    await focusControl.FocusAsync();
                }
            }
            catch
            {
                //DisposeCapture();
            }
        }

        public void DispatcherTimerOff()
        {
            dispatcherTimer.Stop();
            dispatcherTimer.Tick -= dispatcherTimer_Tick;
        }

        public void DispatcherTimerOn()
        {
            dispatcherTimer.Tick += dispatcherTimer_Tick;   
            dispatcherTimer.Start();
            
        }


        /// <summary>
        /// Wird aufgerufen, wenn diese Seite in einem Frame angezeigt werden soll.
        /// </summary>
        /// <param name="e">Ereignisdaten, die beschreiben, wie diese Seite erreicht wurde.
        /// Dieser Parameter wird normalerweise zum Konfigurieren der Seite verwendet.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {

            createCamera();
            DispatcherTimerSetup();
                 
        }

        


        public async void mDialog(string text)
        {          

            MessageDialog msg = new MessageDialog(text);
            msg.Commands.Add(new UICommand("Ja", new UICommandInvokedHandler(CommandHandlers)));
            msg.Commands.Add(new UICommand("Nein", new UICommandInvokedHandler(CommandHandlers)));

            await msg.ShowAsync();
        }

        public void CommandHandlers(IUICommand commandLabel)
        {
            var Actions = commandLabel.Label;
            switch (Actions)
            {
                //Okay Button.
                case "Ja":
                    DispatcherTimerOn();
                    setEffects();

                    // ändern

                    //testtt();

                    break;
                //Quit Button.
                case "Nein":
                    disposeCamera();
                    DispatcherTimerOff();
                    // test
                    Application.Current.Exit();
                    break;
                //end.
            }
        }
  


        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {

            disposeCamera();
            DispatcherTimerOff();
        }

        

        

        

    }
}
