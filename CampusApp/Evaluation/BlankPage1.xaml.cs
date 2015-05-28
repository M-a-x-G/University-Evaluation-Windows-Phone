using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics.Display;
using Windows.Storage;
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
    public sealed partial class BlankPage1 : Page
    {

        public struct sTextBox
        {
            public int _Page;
            public TextBox _TextBox;         

            public sTextBox(int aPage, TextBox aTextBox)
            {
                _Page = aPage;
                _TextBox = aTextBox;
            }
        }

        private List<sTextBox> tbl;
        private List<DataModel.DataSource> dsl;

        //test
        private DataModel.Questions dmq;

        public BlankPage1()
        {
            this.InitializeComponent();
                 
            dsl = new List<DataModel.DataSource>();

            // hier alle Textboxen speichern
            tbl = new List<sTextBox>();
            
            //sTextBox stb = new sTextBox(1, tbTest);
            //tbl.Add(stb);

           
            readJsonTest();
           
            


                /*TextBox.KeyDown += (s, a) => {
                if (a.Key == VirtualKey.Enter) {
                TextBox.IsEnabled = false;
                TextBox.IsEnabled = true;
                    }*/
                                                 
        }

        private async void readJsonTest()
        {
            string lAnswer1 = "";
            string lAnswer2 = "";
            string lAnswer3 = "";
            string lAnswer4 = "";
            string lAnswer5 = "";
            string lAnswer6 = "";
            
            // Json einlesen
            DataModel.Questions dmq = await GetSampleDataAsync();

            // auf das interne Datenmodel spiegeln
            foreach (DataModel.Choices item in dmq.multipleChoiceQuestions)
            {
                lAnswer1 = "";
                lAnswer2 = "";
                lAnswer3 = "";
                lAnswer4 = "";
                lAnswer5 = "";
                lAnswer6 = "";

                for (int j = 0; j < item.choices.Length; j++)
                {
                    if (j == 0) lAnswer1 = item.choices[j];
                    else if (j == 1) lAnswer2 = item.choices[j];
                    else if (j == 2) lAnswer3 = item.choices[j];
                    else if (j == 3) lAnswer4 = item.choices[j];
                    else if (j == 4) lAnswer5 = item.choices[j];
                    else if (j == 5) lAnswer6 = item.choices[j];
                }
          

                DataModel.DataSource ds = new DataModel.DataSource(item.text, lAnswer1, lAnswer2, lAnswer3, lAnswer4, lAnswer5, lAnswer6);
                dsl.Add(ds);
            }

            foreach (string value in dmq.textQuestions)
            {
                DataModel.DataSource ds = new DataModel.DataSource(value);
                dsl.Add(ds);
            }


            createPages();

        }

        private void createPages()
        {
            int max = dsl.Count;
            for (int i = 0; i < max; i++)
            {

                //Pivot
                PivotItem pi = new PivotItem();
                pi.Header = "Frage " + (i + 1) + " - " + max;

                this.pQuestions.Items.Add(pi);

                //ScrollViewer
                ScrollViewer sv = new ScrollViewer();
                sv.Height = 200;
                sv.VerticalAlignment = Windows.UI.Xaml.VerticalAlignment.Top;
                sv.VerticalScrollBarVisibility = ScrollBarVisibility.Visible;
                sv.VerticalScrollMode = ScrollMode.Enabled;
                sv.BringIntoViewOnFocusChange = true;
                sv.HorizontalScrollMode = ScrollMode.Disabled;
                sv.IsHoldingEnabled = true;

                //TextBlock - Question
                TextBlock tb = new TextBlock();
                tb.TextWrapping = TextWrapping.Wrap;
                tb.FontSize = 30;
                // set datacontext (binding)
                tb.DataContext = dsl[i];
                // binding
                Binding binding = new Binding() { Path = new PropertyPath("Question") };
                tb.SetBinding(TextBlock.TextProperty, binding);

                sv.Content = tb;


                //Grid
                Grid grid = new Grid();
                RowDefinition rd1 = new RowDefinition();
                RowDefinition rd2 = new RowDefinition();
                rd1.Height = new GridLength(200);
                rd2.Height = new GridLength(1, GridUnitType.Star);
                grid.RowDefinitions.Add(rd1);
                grid.RowDefinitions.Add(rd2);
                grid.Margin = new Thickness(0, 0, 0, 0);

                // set ScrollViewer to row 0
                Grid.SetRow(sv, 0);
                // add ScrollViewer to grid
                grid.Children.Add(sv);

                //StackPanel
                StackPanel sp = new StackPanel();
                sp.VerticalAlignment = Windows.UI.Xaml.VerticalAlignment.Bottom;
                sp.HorizontalAlignment = Windows.UI.Xaml.HorizontalAlignment.Center;

                if (!dsl[i].IsBigAnswer)
                {
                    string lAnswer = "";
                    
                    for (int j = 0; j < 3; j++)
                    {
                        
                        
                        
                        // 2. Grid (Buttons)
                        Grid grid2 = new Grid();
                        ColumnDefinition cd1 = new ColumnDefinition();
                        ColumnDefinition cd2 = new ColumnDefinition();
                        ColumnDefinition cd3 = new ColumnDefinition();
                        cd1.Width = new GridLength(1, GridUnitType.Auto);
                        cd2.Width = new GridLength(25);
                        cd3.Width = new GridLength(1, GridUnitType.Auto);
                        grid2.ColumnDefinitions.Add(cd1);
                        grid2.ColumnDefinitions.Add(cd2);
                        grid2.ColumnDefinitions.Add(cd3);
                        grid2.Margin = new Thickness(0, 0, 0, 0);

                      
                        Button bt1 = new Button();
                        bt1.Width = 155;
                        bt1.Height = 95;

                        TextBlock tbButton = new TextBlock();
                        tbButton.FontSize = 15;
                        tbButton.TextWrapping = TextWrapping.Wrap;
                        // set datacontext (binding)
                        tbButton.DataContext = dsl[i];
                        // binding

                        if (j == 0) lAnswer = "Answer1";
                        else if (j == 1) lAnswer = "Answer3";
                        else if (j == 2) lAnswer = "Answer5";

                        binding = new Binding() { Path = new PropertyPath(lAnswer) };
                        tbButton.SetBinding(TextBlock.TextProperty, binding);

                        bt1.Content = tbButton;

                        // set Button to column 0
                        Grid.SetColumn(bt1, 0);
                        // add Button to grid
                        grid2.Children.Add(bt1);

                        Button bt2 = new Button();
                        bt2.Width = 155;
                        bt2.Height = 95;

                        tbButton = new TextBlock();
                        tbButton.FontSize = 15;
                        tbButton.TextWrapping = TextWrapping.Wrap;
                        // set datacontext (binding)
                        tbButton.DataContext = dsl[i];
                        // binding

                        if (j == 0) lAnswer = "Answer2";
                        else if (j == 1) lAnswer = "Answer4";
                        else if (j == 2) lAnswer = "Answer6";

                        binding = new Binding() { Path = new PropertyPath(lAnswer) };
                        tbButton.SetBinding(TextBlock.TextProperty, binding);

                        bt2.Content = tbButton;

                        // set Button to column 2
                        Grid.SetColumn(bt2, 2);
                        // add Button to grid
                        grid2.Children.Add(bt2);

                        sp.Children.Add(grid2);
                    }

                }
                else
                {
                    // todo
                }







                // set StackPanel to row 1
                Grid.SetRow(sp, 1);
                // add StackPanel to grid
                grid.Children.Add(sp);

                pi.Content = grid;

                //CommandBar
                AppBarButton abb = new AppBarButton();

                // set datacontext (binding)
                abb.DataContext = dsl[i];
                // binding
                binding = new Binding() { Path = new PropertyPath("Question") };
                abb.SetBinding(AppBarButton.LabelProperty, binding);


                //abb.Label = "Frage " + (i + 1);
                

                this.cbar.SecondaryCommands.Add(abb);
            }
                
        }

        private async Task<DataModel.Questions> GetSampleDataAsync()
        {

            Uri dataUri = new Uri("ms-appx:///DataModel/Questions.json");           

            StorageFile file = await StorageFile.GetFileFromApplicationUriAsync(dataUri);
            
            using (var fileStream = await file.OpenStreamForReadAsync())
            {
                var memoryStream = new MemoryStream();
                await fileStream.CopyToAsync(memoryStream);

                memoryStream.Seek(0, SeekOrigin.Begin);
                

                DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(DataModel.Questions));
       
                DataModel.Questions dmq = (DataModel.Questions)ser.ReadObject(memoryStream);

                return dmq;
            }


        }
       
        /// <summary>
        /// Wird aufgerufen, wenn diese Seite in einem Frame angezeigt werden soll.
        /// </summary>
        /// <param name="e">Ereignisdaten, die beschreiben, wie diese Seite erreicht wurde.
        /// Dieser Parameter wird normalerweise zum Konfigurieren der Seite verwendet.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            
            
        }

       
        private void pQuestions_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (pQuestions.SelectedIndex == 1)
            {
                
                //tbTest.PreventKeyboardDisplayOnProgrammaticFocus = true;
                //tbTest.Focus(FocusState.Keyboard);

                

            }
        }

        private void tbTest_Loaded(object sender, RoutedEventArgs e)
        {
            // hier für alle Textboxen den Fokus setzen
            // Komponenten in einer Liste halten und hier setzen

            foreach (sTextBox i in tbl)
            {
                i._TextBox.PreventKeyboardDisplayOnProgrammaticFocus = true;
                i._TextBox.Focus(FocusState.Programmatic);
            }
            

            
            
        }
       

        private void PivotItem_GotFocus(object sender, RoutedEventArgs e)
        {
            // jedes PivotItem bekommt dieses Event
            // merken, welche Seite eine Textbox hat
            // und hier den Fokus von der Textbox, welche auf der Seite ist (merken), setzen
            foreach (sTextBox i in tbl)
            {
                if (pQuestions.SelectedIndex == i._Page)
                {
                    i._TextBox.Focus(FocusState.Programmatic);
                }
                            
            }
            
            
            
        }

        

        private void AppBarButton_GotFocus(object sender, RoutedEventArgs e)
        {
            pQuestions.Focus(FocusState.Pointer);
        }

        

        /*private DependencyObject FindChildControl<T>(DependencyObject control, string ctrlName)
        {
            int childNumber = VisualTreeHelper.GetChildrenCount(control);
            for (int i = 0; i < childNumber; i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(control, i);
                FrameworkElement fe = child as FrameworkElement;
                // Not a framework element or is null
                if (fe == null) return null;

                if (child is T && fe.Name == ctrlName)
                {
                    // Found the control so return
                    return child;
                }
                else
                {
                    // Not found it - search children
                    DependencyObject nextLevel = FindChildControl<T>(child, ctrlName);
                    if (nextLevel != null)
                        return nextLevel;
                }
            }
            return null;
        }

        static DependencyObject FindChildByName(DependencyObject from, string name)
        {
            int count = VisualTreeHelper.GetChildrenCount(from);

            for (int i = 0; i < count; i++)
            {
                var child = VisualTreeHelper.GetChild(from, i);
                if (child is FrameworkElement && ((FrameworkElement)child).Name == name)
                    return child;

                var result = FindChildByName(child, name);
                if (result != null)
                    return result;
            }

            return null;
        }*/     

       
    }
}
