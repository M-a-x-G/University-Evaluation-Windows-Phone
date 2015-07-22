using Newtonsoft.Json;
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
using Windows.Phone.UI.Input;
using Windows.Storage;
using Windows.UI;
using Windows.UI.Popups;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// Die Elementvorlage "Leere Seite" ist unter http://go.microsoft.com/fwlink/?LinkID=390556 dokumentiert.

namespace CampusAppEvalWP
{
    /// <summary>
    /// Eine leere Seite, die eigenständig verwendet werden kann oder auf die innerhalb eines Rahmens navigiert werden kann.
    /// </summary>
    public sealed partial class Evaluation : Page
    {
       
        private List<AppBarButton> abbl;
        private List<DataModel.DataSource> dsl;
        private DTO.QRCodeDTO qrDTO;

        private ScrollViewer sv;
        private TextBlock tb;
        private Grid grid;
        private StackPanel sp;
        private Grid grid2;
        private Grid grid3;
        private Grid grid4;
        private Grid grid5;
        private Button bt1;
        private Button bt2;
        private Button bt3;
        private Button bt4;
        private Button bt5;
        private Button bt6;
        private TextBlock tbButton1;
        private TextBlock tbButton2;
        private TextBlock tbButton3;
        private TextBlock tbButton4;
        private TextBlock tbButton5;
        private TextBlock tbButton6;   
        private TextBox _textBox;

        private PivotItem piPage1;
        private PivotItem piPage2;
        private PivotItem piPage3;
        private PivotItem piPage4;
        private PivotItem piPage5;

        private Boolean closeApp;
        private Boolean OpenWindow;

        private string courseName;

        int maxItems;
        int lastPage;
        int counterPage;
        int buttonWidth;

        public Evaluation()
        {
            this.InitializeComponent();

            var appView = Windows.UI.ViewManagement.ApplicationView.GetForCurrentView();
            appView.SetDesiredBoundsMode(ApplicationViewBoundsMode.UseVisible);
       
            dsl = new List<DataModel.DataSource>();
            qrDTO = new DTO.QRCodeDTO();

            // hier alle AppBarButtons speichern
            abbl = new List<AppBarButton>();

            lastPage = -1;
            counterPage = 1;

            courseName = "";

            closeApp = false;
            OpenWindow = false;

            buttonWidth = (int)Window.Current.Bounds.Width / 2 - 28;

            // höhe = /2 / 3
            
            /*
            
            //Testdaten
            DataModel.DataSource dd = new DataModel.DataSource("Hallo, Frage 1");
            dsl.Add(dd);
            dd = new DataModel.DataSource("Hallo, Frage 2", "Antwort1", "Antwort2", "Antwort3", "Antwort4", "Antwort5", "Antwort6");
            dsl.Add(dd);
            dd = new DataModel.DataSource("Hallo, Frage 3");
            dsl.Add(dd);
            dd = new DataModel.DataSource("Hallo, Frage 4");
            dsl.Add(dd);
            dd = new DataModel.DataSource("Hallo, Frage 5", "Antwort1", "Antwort2", "Antwort3", "Antwort4", "Antwort5", "");
            dsl.Add(dd);
            dd = new DataModel.DataSource("Hallo, Frage 6", "Antwort1", "Antwort2", "Antwort3", "Antwort4", "", "");
            dsl.Add(dd);
            dd = new DataModel.DataSource("Hallo, Frage 7", "Antwort1", "Antwort2", "Antwort3", "Antwort4", "Antwort5", "Antwort6");
            dsl.Add(dd);
            dd = new DataModel.DataSource("Hallo, Frage 8", "Antwort1", "Antwort2", "Antwort3", "Antwort4", "Antwort5", "Antwort6");
            dsl.Add(dd);
            */
                                        
        }
      
        private void createTemplate()
        {
            
            //ScrollViewer
            sv = new ScrollViewer();
            sv.Height = 200;
            sv.VerticalAlignment = Windows.UI.Xaml.VerticalAlignment.Top;
            sv.VerticalScrollBarVisibility = ScrollBarVisibility.Visible;
            sv.VerticalScrollMode = ScrollMode.Enabled;
            sv.BringIntoViewOnFocusChange = true;
            sv.HorizontalScrollMode = ScrollMode.Disabled;
            sv.IsHoldingEnabled = true;
            sv.Tapped += AppBarButton_GotFocus;

            //TextBlock - Question
            tb = new TextBlock();
            tb.TextWrapping = TextWrapping.Wrap;
            tb.FontSize = 25;
            tb.Foreground = new SolidColorBrush(Colors.Black);
            tb.GotFocus += AppBarButton_GotFocus;

            Binding binding = new Binding() { Path = new PropertyPath("Question") };
            tb.SetBinding(TextBlock.TextProperty, binding);

            sv.Content = tb;

            //Grid
            grid = new Grid();     
            RowDefinition rd1 = new RowDefinition();
            RowDefinition rd2 = new RowDefinition();
            rd1.Height = new GridLength(170);
            rd2.Height = new GridLength(1, GridUnitType.Star);
            grid.RowDefinitions.Add(rd1);
            grid.RowDefinitions.Add(rd2);
            grid.Margin = new Thickness(0, 0, 0, 0);

            // set ScrollViewer to row 0
            Grid.SetRow(sv, 0);
            // add ScrollViewer to grid
            grid.Children.Add(sv);

            sp = new StackPanel();
            sp.VerticalAlignment = Windows.UI.Xaml.VerticalAlignment.Bottom;
            sp.HorizontalAlignment = Windows.UI.Xaml.HorizontalAlignment.Stretch;

            // buttons template
            //
            //
            // 2. Grid (Buttons)
            grid2 = new Grid();
            ColumnDefinition cd1 = new ColumnDefinition();
            ColumnDefinition cd2 = new ColumnDefinition();
            ColumnDefinition cd3 = new ColumnDefinition();
            cd1.Width = new GridLength(1, GridUnitType.Auto);
            cd2.Width = new GridLength(10);
            cd3.Width = new GridLength(1, GridUnitType.Auto);
            grid2.ColumnDefinitions.Add(cd1);
            grid2.ColumnDefinitions.Add(cd2);
            grid2.ColumnDefinitions.Add(cd3);
            grid2.Margin = new Thickness(0, 0, 0, 0);

            bt1 = new Button();
            bt1.Width = buttonWidth; // 165;
            bt1.Height = 95;          
            bt1.Click += Button_Click;
            bt1.Foreground = new SolidColorBrush(Colors.White);
            bt1.Tag = 1;

            tbButton1 = new TextBlock();
            tbButton1.FontSize = 15;
            tbButton1.TextWrapping = TextWrapping.Wrap;
                    
            // binding
            binding = new Binding() { Path = new PropertyPath("Answer1") };
            tbButton1.SetBinding(TextBlock.TextProperty, binding);

            binding = new Binding() { Path = new PropertyPath("Answer1Color") };
            bt1.SetBinding(Button.BackgroundProperty, binding);
            bt1.SetBinding(Button.BorderBrushProperty, binding);          
            
            bt1.Content = tbButton1;

            // set Button to column 0
            Grid.SetColumn(bt1, 0);
            // add Button to grid
            grid2.Children.Add(bt1);

            bt2 = new Button();
            bt2.Width = buttonWidth + 5; // 175;
            bt2.Height = 95;  
            bt2.Click += Button_Click;
            bt2.Foreground = new SolidColorBrush(Colors.White);
            bt2.Tag = 2;

            tbButton2 = new TextBlock();
            tbButton2.FontSize = 15;
            tbButton2.TextWrapping = TextWrapping.Wrap;

            // binding
            binding = new Binding() { Path = new PropertyPath("Answer2") };
            tbButton2.SetBinding(TextBlock.TextProperty, binding);

            binding = new Binding() { Path = new PropertyPath("Answer2Color") };
            bt2.SetBinding(Button.BackgroundProperty, binding);
            bt2.SetBinding(Button.BorderBrushProperty, binding);
            
            bt2.Content = tbButton2;

            // set Button to column 2
            Grid.SetColumn(bt2, 2);
            // add Button to grid
            grid2.Children.Add(bt2);

            sp.Children.Add(grid2);

            //////////////////////////////
            // 3. Grid (Buttons)
            grid3 = new Grid();
            cd1 = new ColumnDefinition();
            cd2 = new ColumnDefinition();
            cd3 = new ColumnDefinition();
            cd1.Width = new GridLength(1, GridUnitType.Auto);
            cd2.Width = new GridLength(10);
            cd3.Width = new GridLength(1, GridUnitType.Auto);
            grid3.ColumnDefinitions.Add(cd1);
            grid3.ColumnDefinitions.Add(cd2);
            grid3.ColumnDefinitions.Add(cd3);
            grid3.Margin = new Thickness(0, 0, 0, 0);

            bt3 = new Button();
            bt3.Width = buttonWidth; // 165;
            bt3.Height = 95;
            bt3.Click += Button_Click;
            bt3.Foreground = new SolidColorBrush(Colors.White);
            bt3.Tag = 3;

            tbButton3 = new TextBlock();
            tbButton3.FontSize = 15;
            tbButton3.TextWrapping = TextWrapping.Wrap;

            // binding
            binding = new Binding() { Path = new PropertyPath("Answer3") };
            tbButton3.SetBinding(TextBlock.TextProperty, binding);

            binding = new Binding() { Path = new PropertyPath("Answer3Color") };
            bt3.SetBinding(Button.BackgroundProperty, binding);
            bt3.SetBinding(Button.BorderBrushProperty, binding);

            bt3.Content = tbButton3;

            // set Button to column 0
            Grid.SetColumn(bt3, 0);
            // add Button to grid
            grid3.Children.Add(bt3);

            bt4 = new Button();
            bt4.Width = buttonWidth + 5; // 175;
            bt4.Height = 95;
            bt4.Click += Button_Click;
            bt4.Foreground = new SolidColorBrush(Colors.White);
            bt4.Tag = 4;

            tbButton4 = new TextBlock();
            tbButton4.FontSize = 15;
            tbButton4.TextWrapping = TextWrapping.Wrap;

            // binding
            binding = new Binding() { Path = new PropertyPath("Answer4") };
            tbButton4.SetBinding(TextBlock.TextProperty, binding);

            binding = new Binding() { Path = new PropertyPath("Answer4Color") };
            bt4.SetBinding(Button.BackgroundProperty, binding);
            bt4.SetBinding(Button.BorderBrushProperty, binding);

            bt4.Content = tbButton4;

            // set Button to column 2
            Grid.SetColumn(bt4, 2);
            // add Button to grid
            grid3.Children.Add(bt4);

            sp.Children.Add(grid3);

            //////////////////////////////
            // 4. Grid (Buttons)
            grid4 = new Grid();
            cd1 = new ColumnDefinition();
            cd2 = new ColumnDefinition();
            cd3 = new ColumnDefinition();
            cd1.Width = new GridLength(1, GridUnitType.Auto);
            cd2.Width = new GridLength(10);
            cd3.Width = new GridLength(1, GridUnitType.Auto);
            grid4.ColumnDefinitions.Add(cd1);
            grid4.ColumnDefinitions.Add(cd2);
            grid4.ColumnDefinitions.Add(cd3);
            grid4.Margin = new Thickness(0, 0, 0, 0);

            bt5 = new Button();
            bt5.Width = buttonWidth; // 165;
            bt5.Height = 95;
            bt5.Click += Button_Click;
            bt5.Foreground = new SolidColorBrush(Colors.White);
            bt5.Tag = 5;
            tbButton5 = new TextBlock();
            tbButton5.FontSize = 15;
            tbButton5.TextWrapping = TextWrapping.Wrap;

            // binding
            binding = new Binding() { Path = new PropertyPath("Answer5") };
            tbButton5.SetBinding(TextBlock.TextProperty, binding);

            binding = new Binding() { Path = new PropertyPath("Answer5Color") };
            bt5.SetBinding(Button.BackgroundProperty, binding);
            bt5.SetBinding(Button.BorderBrushProperty, binding);

            bt5.Content = tbButton5;

            // set Button to column 0
            Grid.SetColumn(bt5, 0);
            // add Button to grid
            grid4.Children.Add(bt5);

            bt6 = new Button();
            bt6.Width = buttonWidth + 5; // 175;
            bt6.Height = 95;
            bt6.Click += Button_Click;
            bt6.Foreground = new SolidColorBrush(Colors.White);
            bt6.Tag = 6;

            tbButton6 = new TextBlock();
            tbButton6.FontSize = 15;
            tbButton6.TextWrapping = TextWrapping.Wrap;

            // binding
            binding = new Binding() { Path = new PropertyPath("Answer6") };
            tbButton6.SetBinding(TextBlock.TextProperty, binding);

            binding = new Binding() { Path = new PropertyPath("Answer6Color") };
            bt6.SetBinding(Button.BackgroundProperty, binding);
            bt6.SetBinding(Button.BorderBrushProperty, binding);

            bt6.Content = tbButton6;

            // set Button to column 2
            Grid.SetColumn(bt6, 2);
            // add Button to grid
            grid4.Children.Add(bt6);

            sp.Children.Add(grid4);

            

            // Textbox template
            //
            //
            //

            // Textbox
            grid5 = new Grid();
            grid5.Margin = new Thickness(0, 0, 0, 0);

            _textBox = new TextBox();
            _textBox.MaxLength = 1000;
            _textBox.Margin = new Thickness(0, 0, 0, 0);
            _textBox.VerticalAlignment = Windows.UI.Xaml.VerticalAlignment.Stretch;
            _textBox.HorizontalAlignment = Windows.UI.Xaml.HorizontalAlignment.Stretch;
            //_textBox.Loaded += tb_Loaded;
            _textBox.TextWrapping = TextWrapping.Wrap;
            _textBox.AcceptsReturn = true;
            //_textBox.Background = new SolidColorBrush(Color.FromArgb(64,0,0,0));
            _textBox.Background = new SolidColorBrush(Colors.White);
            //_textBox.BorderBrush = new SolidColorBrush(Colors.Black);
            _textBox.BorderBrush = new SolidColorBrush(Color.FromArgb(255, 204, 10, 47));
            //_textBox.PreventKeyboardDisplayOnProgrammaticFocus = true;
            //_textBox.GotFocus += _textBox_GotFocus;
            _textBox.PlaceholderText = "Geben Sie hier Ihre Antwort ein";

            binding = new Binding() { Path = new PropertyPath("BigAnswer") };
            _textBox.SetBinding(TextBox.TextProperty, binding);

            

            // create CommandBar buttons
            //
            //

            for (int i = 0; i < maxItems; i++)
            {
                AppBarButton abb = new AppBarButton();
                abb.GotFocus += AppBarButton_GotFocus;
                abb.Click += AppBarButton_Click;
                abb.Tag = i+1;
                if (dsl[i].Question.Length > 30)
                    abb.Label = (i + 1) + ". " + dsl[i].Question.Substring(0,25) + "...";
                else
                    abb.Label = (i + 1) + ". " + dsl[i].Question;

                this.cbar.SecondaryCommands.Add(abb);

                this.abbl.Add(abb);
            }        
            
        }  

        private void setPage()
        {
            // question
            tb.DataContext = dsl[this.counterPage - 1];
            
            if (this.dsl[this.counterPage-1].IsBigAnswer)
            {
                _textBox.DataContext = dsl[this.counterPage - 1]; 
                
                if (this.grid.Children.Count > 1)
                    this.grid.Children.RemoveAt(1);

                // set TextBox to row 1
                Grid.SetRow(_textBox, 1);
                // add TextBox to grid
                grid.Children.Add(_textBox);
            }
            else
            {
                
                bt1.IsEnabled = true;
                bt2.IsEnabled = true;
                bt3.IsEnabled = true;
                bt4.IsEnabled = true;
                bt5.IsEnabled = true;
                bt6.IsEnabled = true; 
                
                
                // button1
                tbButton1.DataContext = dsl[this.counterPage - 1];
                bt1.DataContext = dsl[this.counterPage - 1];
                if (dsl[this.counterPage - 1].Answer1.Equals(""))
                {
                    bt1.IsEnabled = false;                  
                }

                // button2
                tbButton2.DataContext = dsl[this.counterPage - 1];
                bt2.DataContext = dsl[this.counterPage - 1];
                if (dsl[this.counterPage - 1].Answer2.Equals(""))
                {
                    bt2.IsEnabled = false; 
                }

                // butoon3
                tbButton3.DataContext = dsl[this.counterPage - 1];
                bt3.DataContext = dsl[this.counterPage - 1];
                if (dsl[this.counterPage - 1].Answer3.Equals(""))
                {
                    bt3.IsEnabled = false;
                }

                // button4
                tbButton4.DataContext = dsl[this.counterPage - 1];
                bt4.DataContext = dsl[this.counterPage - 1];
                if (dsl[this.counterPage - 1].Answer4.Equals(""))
                {
                    bt4.IsEnabled = false;
                }

                // button5
                tbButton5.DataContext = dsl[this.counterPage - 1];
                bt5.DataContext = dsl[this.counterPage - 1];
                if (dsl[this.counterPage - 1].Answer5.Equals(""))
                {
                    bt5.IsEnabled = false;
                }

                // button6
                tbButton6.DataContext = dsl[this.counterPage - 1];
                bt6.DataContext = dsl[this.counterPage - 1];
                if (dsl[this.counterPage - 1].Answer6.Equals(""))
                {
                    bt6.IsEnabled = false;
                }

                if (this.grid.Children.Count > 1)
                    this.grid.Children.RemoveAt(1);
                // set StackPanel to row 1
                Grid.SetRow(sp, 1);
                // add StackPanel to grid
                grid.Children.Add(sp);
            }


            // delete all contents
            piPage1.Content = null;
            piPage2.Content = null;
            piPage3.Content = null;
            piPage4.Content = null;
            piPage5.Content = null;

            if (pQuestions.SelectedIndex == 0)
                piPage1.Content = grid;
            else if (pQuestions.SelectedIndex == 1)
                piPage2.Content = grid;
            else if (pQuestions.SelectedIndex == 2)
                piPage3.Content = grid;
            else if (pQuestions.SelectedIndex == 3)
                piPage4.Content = grid;
            else if (pQuestions.SelectedIndex == 4)
                piPage5.Content = grid;
        }

        private void createPages()
        {
            
            
            piPage1 = new PivotItem();        
            piPage1.Header = "Frage 1 - " + maxItems;

            piPage2 = new PivotItem();
            piPage2.Header = "Frage 2 - " + maxItems;

            piPage3 = new PivotItem();
            piPage3.Header = "Frage 3 - " + maxItems;

            piPage4 = new PivotItem();
            piPage4.Header = "Frage 4 - " + maxItems;

            piPage5 = new PivotItem();
            piPage5.Header = "Frage 5 - " + maxItems;

            this.pQuestions.Items.Add(piPage1);
            this.pQuestions.Items.Add(piPage2);
            this.pQuestions.Items.Add(piPage3);
            this.pQuestions.Items.Add(piPage4);
            this.pQuestions.Items.Add(piPage5);
                                
        }

        /// <summary>
        /// Wird aufgerufen, wenn diese Seite in einem Frame angezeigt werden soll.
        /// </summary>
        /// <param name="e">Ereignisdaten, die beschreiben, wie diese Seite erreicht wurde.
        /// Dieser Parameter wird normalerweise zum Konfigurieren der Seite verwendet.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            //HardwareButtons.BackPressed += HardwareButtons_BackPressed;
            
            Helper.Functions.sendDataTOEvaluation help = (Helper.Functions.sendDataTOEvaluation)e.Parameter;
            dsl = help.dsl;
            qrDTO = help.qrDTO;
            courseName = help.course;
             
                    
            maxItems = dsl.Count;

            createPages();

            createTemplate();

            setPage();
            

        }

       
        private void pQuestions_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {                      
            
            pQuestions.Focus(FocusState.Pointer);
          
            if (lastPage != -1)
            {
                // save old datas in textbox
                if (this.dsl[this.counterPage - 1].IsBigAnswer)
                {                   
                    dsl[this.counterPage - 1].BigAnswer = _textBox.Text;
                }
                
                if (pQuestions.SelectedIndex == 1)
                {
                    if (lastPage == 2)
                        counterPage -= 1;
                    else if (lastPage == 0)
                        counterPage += 1;
                    else if (lastPage == 4)
                    {
                        counterPage += 2;
                        if (counterPage > maxItems)
                            counterPage = counterPage - maxItems;
                    }
                    else if (lastPage == 3)
                    {
                        counterPage += 3;
                        if (counterPage > maxItems)
                            counterPage = counterPage - maxItems;
                    }
                }

                else if (pQuestions.SelectedIndex == 2)
                {
                    if (lastPage == 3)
                        counterPage -= 1;
                    else if (lastPage == 1)
                        counterPage += 1;
                    else if (lastPage == 0)
                    {
                        counterPage += 2;
                        if (counterPage > maxItems)
                            counterPage = counterPage - maxItems;
                    }
                    else if (lastPage == 4)
                    {
                        counterPage += 3;
                        if (counterPage > maxItems)
                            counterPage = counterPage - maxItems;
                    }
                }

                else if (pQuestions.SelectedIndex == 3)
                {
                    if (lastPage == 4)
                        counterPage -= 1;
                    else if (lastPage == 2)
                        counterPage += 1;
                    else if (lastPage == 1)
                    {
                        counterPage += 2;
                        if (counterPage > maxItems)
                            counterPage = counterPage - maxItems;
                    }
                    else if (lastPage == 0)
                    {
                        counterPage += 3;
                        if (counterPage > maxItems)
                            counterPage = counterPage - maxItems;
                    }
                }

                else if (pQuestions.SelectedIndex == 4)
                {
                    if (lastPage == 0)
                        counterPage -= 1;
                    else if (lastPage == 3)
                        counterPage += 1;
                    else if (lastPage == 2)
                    {
                        counterPage += 2;
                        if (counterPage > maxItems)
                            counterPage = counterPage - maxItems;
                    }
                    else if (lastPage == 1)
                    {
                        counterPage += 3;
                        if (counterPage > maxItems)
                            counterPage = counterPage - maxItems;
                    }
                }

                else if (pQuestions.SelectedIndex == 0)
                {
                    if (lastPage == 1)
                        counterPage -= 1;
                    else if (lastPage == 4)
                        counterPage += 1;
                    else if (lastPage == 3)
                    {
                        counterPage += 2;
                        if (counterPage > maxItems)
                            counterPage = counterPage - maxItems;
                    }
                    else if (lastPage == 2)
                    {
                        counterPage += 3;
                        if (counterPage > maxItems)
                            counterPage = counterPage - maxItems;
                    }
                }

                if (counterPage > maxItems) counterPage = 1;
                if (counterPage < 1) counterPage = maxItems;

                int helpCounter = counterPage;

                if (pQuestions.SelectedIndex == 1)
                {
                    piPage2.Header = "Frage " + helpCounter + " - " + maxItems;

                    helpCounter += 1;
                    if (helpCounter > maxItems) helpCounter = 1;

                    piPage3.Header = "Frage " + helpCounter + " - " + maxItems;

                    helpCounter += 1;
                    if (helpCounter > maxItems) helpCounter = 1;

                    piPage4.Header = "Frage " + helpCounter + " - " + maxItems;

                    helpCounter += 1;
                    if (helpCounter > maxItems) helpCounter = 1;

                    piPage5.Header = "Frage " + helpCounter + " - " + maxItems;

                }
                else if (pQuestions.SelectedIndex == 2)
                {
                    piPage3.Header = "Frage " + helpCounter + " - " + maxItems;

                    helpCounter += 1;
                    if (helpCounter > maxItems) helpCounter = 1;

                    piPage4.Header = "Frage " + helpCounter + " - " + maxItems;

                    helpCounter += 1;
                    if (helpCounter > maxItems) helpCounter = 1;

                    piPage5.Header = "Frage " + helpCounter + " - " + maxItems;

                    helpCounter += 1;
                    if (helpCounter > maxItems) helpCounter = 1;

                    piPage1.Header = "Frage " + helpCounter + " - " + maxItems;
                }
                else if (pQuestions.SelectedIndex == 3)
                {
                    piPage4.Header = "Frage " + helpCounter + " - " + maxItems;

                    helpCounter += 1;
                    if (helpCounter > maxItems) helpCounter = 1;

                    piPage5.Header = "Frage " + helpCounter + " - " + maxItems;

                    helpCounter += 1;
                    if (helpCounter > maxItems) helpCounter = 1;

                    piPage1.Header = "Frage " + helpCounter + " - " + maxItems;

                    helpCounter += 1;
                    if (helpCounter > maxItems) helpCounter = 1;

                    piPage2.Header = "Frage " + helpCounter + " - " + maxItems;
                }
                else if (pQuestions.SelectedIndex == 4)
                {
                    piPage5.Header = "Frage " + helpCounter + " - " + maxItems;

                    helpCounter += 1;
                    if (helpCounter > maxItems) helpCounter = 1;

                    piPage1.Header = "Frage " + helpCounter + " - " + maxItems;

                    helpCounter += 1;
                    if (helpCounter > maxItems) helpCounter = 1;

                    piPage2.Header = "Frage " + helpCounter + " - " + maxItems;

                    helpCounter += 1;
                    if (helpCounter > maxItems) helpCounter = 1;

                    piPage3.Header = "Frage " + helpCounter + " - " + maxItems;
                }
                else if (pQuestions.SelectedIndex == 0)
                {
                    piPage1.Header = "Frage " + helpCounter + " - " + maxItems;

                    helpCounter += 1;
                    if (helpCounter > maxItems) helpCounter = 1;

                    piPage2.Header = "Frage " + helpCounter + " - " + maxItems;

                    helpCounter += 1;
                    if (helpCounter > maxItems) helpCounter = 1;

                    piPage3.Header = "Frage " + helpCounter + " - " + maxItems;

                    helpCounter += 1;
                    if (helpCounter > maxItems) helpCounter = 1;

                    piPage4.Header = "Frage " + helpCounter + " - " + maxItems;
                }

                // show page
                setPage();

                
            }


           
            lastPage = pQuestions.SelectedIndex;
     
           
           
        }
      

        private void AppBarButton_GotFocus(object sender, RoutedEventArgs e)
        {
            pQuestions.Focus(FocusState.Pointer);
        }

        void Button_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            dsl[counterPage-1].setButtonAnswer((int)btn.Tag);
            
            int lPage = pQuestions.SelectedIndex +1;
            if (lPage > 4)
                lPage = 0;


            pQuestions.SelectedIndex = lPage;



        }

        void AppBarButton_Click(object sender, RoutedEventArgs e)
        {
            AppBarButton abb = (AppBarButton)sender;
            if ((int)abb.Tag == 1)
                counterPage = maxItems;
            else
                counterPage = (int)abb.Tag - 1;
            
            if (pQuestions.SelectedIndex == 0)
            {
                lastPage = 0;
                pQuestions.SelectedIndex = 1;
            }
            else if (pQuestions.SelectedIndex == 1)
            {
                lastPage = 1;
                pQuestions.SelectedIndex = 2;
            }
            else if (pQuestions.SelectedIndex == 2)
            {
                lastPage = 2;
                pQuestions.SelectedIndex = 3;
            }
            else if (pQuestions.SelectedIndex == 3)
            {
                lastPage = 3;
                pQuestions.SelectedIndex = 4;
            }
            else if (pQuestions.SelectedIndex == 4)
            {
                lastPage = 4;
                pQuestions.SelectedIndex = 0;
            }
            
     

        }
      

        private void pQuestions_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            if (!(sender is TextBox))
            {
                pQuestions.Focus(FocusState.Pointer);
            }
        }

        private void abbAccept_Click(object sender, RoutedEventArgs e)
        {
            // bin ich auf einer Textbox-Seite, dann noch schnell den Wert aus der Textbx übernehmen
            // save old datas in textbox
            if (this.dsl[this.counterPage - 1].IsBigAnswer)
            {
                dsl[this.counterPage - 1].BigAnswer = _textBox.Text;
            }
            
            // haben wir schon alle Frage beantwortet?
            int counter = 0;
            string question = "Wollen Sie die Evaluationsdaten abschicken?";
            foreach(DataModel.DataSource list in this.dsl)
            {
                if (list.hasAnswer())
                    counter++;
            }

            if (counter < this.dsl.Count)
                question = "Sie haben " + counter + " von " + this.dsl.Count + " Fragen beantwortet!" + Environment.NewLine + "Wollen Sie die Evaluationsdaten abschicken?";


            mDialog(question, 0);
        }

        public async void mDialog(string text, int type)
        {
            if (!OpenWindow)
            {

                Windows.UI.Popups.MessageDialog msg = new MessageDialog(text);
                if (type == 0)
                {
                    msg.Commands.Add(new UICommand("Ja", new UICommandInvokedHandler(CommandHandlers)));
                    msg.Commands.Add(new UICommand("Nein", new UICommandInvokedHandler(CommandHandlers)));
                }
                else if (type == 1)
                {
                    msg.Commands.Add(new UICommand("OK", new UICommandInvokedHandler(CommandHandlers)));
                }
                else if (type == 2)
                {
                    msg.Commands.Add(new UICommand("Beenden", new UICommandInvokedHandler(CommandHandlers)));
                }

                OpenWindow = true;

                await msg.ShowAsync();
            }
        }

        public void CommandHandlers(IUICommand commandLabel)
        {
            OpenWindow = false;
            
            var Actions = commandLabel.Label;
            switch (Actions)
            {
                //Okay Button.
                case "Ja":
                    if (closeApp)
                        Application.Current.Exit();
                    sendDataToServer();

                    break;
                //Quit Button.
                case "Nein":
                    closeApp = false;
                    
                    break;
                //end.
                case "Beenden":
                    Application.Current.Exit();
                    break;
                case "OK":      
                    break;
            }
        }  

        private async void sendDataToServer()
        {
            // Internet?
            if (System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable())
            {
               
                DTO.AnswersDTO aDTO = Helper.Functions.convertDataSourcetoAnswersDTO(dsl);
                aDTO.voteToken = qrDTO.voteToken;
                aDTO.studyPath = courseName;

                string requestDTOJSON = JsonConvert.SerializeObject(aDTO);

                Helper.Functions.getDataFromServerStruct gDFS = await Helper.Functions.sendDataToServer(qrDTO.host, "/v1/answers", requestDTOJSON);

                // Fehler
                if (gDFS.OK == false)
                {
                    DTO.ResponseDTO reDTO;
                    try
                    {
                        reDTO = JsonConvert.DeserializeObject<DTO.ResponseDTO>(gDFS.json);
                    }
                    catch (Exception e)
                    {
                        reDTO = null;
                    }

                    if (reDTO == null)
                    {
                        mDialog("Daten konnten nicht zum Server gesendet werden!", 1);
                    }
                    else
                    {
                        mDialog(Helper.Functions.serverMessage(reDTO), 1);

                        if ((reDTO.type == DataModel.ErrorTyps.TOKEN_ALLREADY_USED) || 
                            (reDTO.type == DataModel.ErrorTyps.IVALID_TOKEN))
                        {
                            Frame.Navigate(typeof(QR_Code));
                        }
                           
                    }

                }
                else
                    mDialog("Daten wurden an den Server gesendet." + Environment.NewLine + "Danke für Ihre Teilnahme!", 2);
            }
            else
            {
                mDialog("Fehler: Keine Internetverbindung verfügbar!", 1);
            }
        }

        private void HardwareButtons_BackPressed(object sender, Windows.Phone.UI.Input.BackPressedEventArgs e)
        {
            //closeApp = true;
            //e.Handled = true;
            //mDialog("Wollen Sie die Evaluation beenden?", 0);
        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            //HardwareButtons.BackPressed -= HardwareButtons_BackPressed;
        }
        
       
    }
}
