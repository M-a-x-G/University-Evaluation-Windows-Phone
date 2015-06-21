using CampusApp.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// Die Elementvorlage "Leere Seite" ist unter http://go.microsoft.com/fwlink/?LinkID=390556 dokumentiert.

namespace CampusApp.Evaluation
{
    /// <summary>
    /// Eine leere Seite, die eigenständig verwendet werden kann oder auf die innerhalb eines Rahmens navigiert werden kann.
    /// </summary>
    public sealed partial class Course : Page
    {
        private DTO.QuestionsDTO qDTO;
        private List<DataModel.DataSource> dsl;
        private string url;
        public Course()
        {
            this.InitializeComponent();

            var appView = Windows.UI.ViewManagement.ApplicationView.GetForCurrentView();
            appView.SetDesiredBoundsMode(ApplicationViewBoundsMode.UseVisible);

            qDTO = new DTO.QuestionsDTO();
            url = "";
            
        }

        private void createButtons()
        {

            for (int i = 0; i < qDTO.innerSections.Length; i++)
            {
                Button btn = new Button();
                btn.Height = 80;
                btn.Click += Button_Click;
                btn.Foreground = new SolidColorBrush(Colors.Black);
                btn.HorizontalAlignment = Windows.UI.Xaml.HorizontalAlignment.Stretch;
                btn.Background = new SolidColorBrush(Colors.White);
                btn.Tag = i;

                TextBlock tbText = new TextBlock();
                tbText.FontSize = 20;
                tbText.Text = qDTO.innerSections[i];

                btn.Content = tbText;

                this.spButtons.Children.Add(btn);

            }

            
            
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            dsl = Helper.Functions.convertQuestionDTOToDataSource(qDTO, qDTO.innerSections[(int)btn.Tag], url);

            Frame.Navigate(typeof(Evaluation), dsl);
        }
       
        /// <summary>
        /// Wird aufgerufen, wenn diese Seite in einem Frame angezeigt werden soll.
        /// </summary>
        /// <param name="e">Ereignisdaten, die beschreiben, wie diese Seite erreicht wurde.
        /// Dieser Parameter wird normalerweise zum Konfigurieren der Seite verwendet.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            Helper.Functions.sendDataTOCourse help = (Helper.Functions.sendDataTOCourse)e.Parameter;
            qDTO = help.qDTO;
            url = help.url;

            // Test
            string[] test = { "Informatik", "Medieninformatik" };
            qDTO.innerSections = test;


            createButtons();
        }
    }
}
