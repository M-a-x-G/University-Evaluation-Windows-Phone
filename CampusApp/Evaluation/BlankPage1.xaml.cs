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

namespace CampusApp.Evaluation
{
    /// <summary>
    /// Eine leere Seite, die eigenständig verwendet werden kann oder auf die innerhalb eines Rahmens navigiert werden kann.
    /// </summary>
    public sealed partial class BlankPage1 : Page
    {
       
        private List<AppBarButton> abbl;
        private List<DataModel.DataSource> dsl;

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

        int maxItems;
        int lastPage;
        int counterPage;

        public BlankPage1()
        {
            this.InitializeComponent();

            var appView = Windows.UI.ViewManagement.ApplicationView.GetForCurrentView();
            appView.SetDesiredBoundsMode(ApplicationViewBoundsMode.UseVisible);
       
            dsl = new List<DataModel.DataSource>();

            // hier alle Textboxen speichern
            abbl = new List<AppBarButton>();

            lastPage = -1;
            counterPage = 1;
         

            




                                 
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
            DTO.QuestionsDTO dmq = await GetSampleDataAsync();

            // auf das interne Datenmodel spiegeln
            foreach (DTO.Choices item in dmq.multipleChoiceQuestions)
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

            maxItems = dsl.Count;

            createPages();

            createTemplate();

            setPage();

            //createPages();

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
            bt1.Width = 165;
            bt1.Height = 95;          
            bt1.Click += Button_Click;
            bt1.Foreground = new SolidColorBrush(Colors.Black);
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
            bt2.Width = 175;
            bt2.Height = 95;  
            bt2.Click += Button_Click;
            bt2.Foreground = new SolidColorBrush(Colors.Black);
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
            bt3.Width = 165;
            bt3.Height = 95;
            bt3.Click += Button_Click;
            bt3.Foreground = new SolidColorBrush(Colors.Black);
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
            bt4.Width = 175;
            bt4.Height = 95;
            bt4.Click += Button_Click;
            bt4.Foreground = new SolidColorBrush(Colors.Black);
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
            bt5.Width = 165;
            bt5.Height = 95;
            bt5.Click += Button_Click;
            bt5.Foreground = new SolidColorBrush(Colors.Black);
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
            bt6.Width = 175;
            bt6.Height = 95;
            bt6.Click += Button_Click;
            bt6.Foreground = new SolidColorBrush(Colors.Black);
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
            _textBox.Margin = new Thickness(0, 0, 0, 0);
            _textBox.VerticalAlignment = Windows.UI.Xaml.VerticalAlignment.Stretch;
            _textBox.HorizontalAlignment = Windows.UI.Xaml.HorizontalAlignment.Stretch;
            //_textBox.Loaded += tb_Loaded;
            _textBox.TextWrapping = TextWrapping.Wrap;
            _textBox.AcceptsReturn = true;
            _textBox.Background = new SolidColorBrush(Colors.Gray);
            _textBox.PreventKeyboardDisplayOnProgrammaticFocus = true;

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

        private void setColorAppBarButton()
        {
            foreach(AppBarButton i in abbl)
            {
                if ((int)i.Tag == this.counterPage)
                {
                    
                }

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
                // button1
                tbButton1.DataContext = dsl[this.counterPage - 1];
                bt1.DataContext = dsl[this.counterPage - 1];
                // button2
                tbButton2.DataContext = dsl[this.counterPage - 1];
                bt2.DataContext = dsl[this.counterPage - 1];
                // butoon3
                tbButton3.DataContext = dsl[this.counterPage - 1];
                bt3.DataContext = dsl[this.counterPage - 1];
                // button4
                tbButton4.DataContext = dsl[this.counterPage - 1];
                bt4.DataContext = dsl[this.counterPage - 1];
                // button5
                tbButton5.DataContext = dsl[this.counterPage - 1];
                bt5.DataContext = dsl[this.counterPage - 1];
                // button6
                tbButton6.DataContext = dsl[this.counterPage - 1];
                bt6.DataContext = dsl[this.counterPage - 1];

                if (this.grid.Children.Count > 1)
                    this.grid.Children.RemoveAt(1);
                // set StackPanel to row 1
                Grid.SetRow(sp, 1);
                // add StackPanel to grid
                grid.Children.Add(sp);
            }


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
            
            // test
            piPage1 = new PivotItem();
            piPage1.Loaded += PivotItem_Loaded;          
            piPage1.Header = "Frage 1 - " + maxItems;

            piPage2 = new PivotItem();
            piPage2.Loaded += PivotItem_Loaded;
            piPage2.Header = "Frage 2 - " + maxItems;

            piPage3 = new PivotItem();
            piPage3.Loaded += PivotItem_Loaded;
            piPage3.Header = "Frage 3 - " + maxItems;

            piPage4 = new PivotItem();
            piPage4.Loaded += PivotItem_Loaded;
            piPage4.Header = "Frage 4 - " + maxItems;

            piPage5 = new PivotItem();
            piPage5.Loaded += PivotItem_Loaded;
            piPage5.Header = "Frage 5 - " + maxItems;

            this.pQuestions.Items.Add(piPage1);
            this.pQuestions.Items.Add(piPage2);
            this.pQuestions.Items.Add(piPage3);
            this.pQuestions.Items.Add(piPage4);
            this.pQuestions.Items.Add(piPage5);
            
            
            /*
            int max = dsl.Count;
            for (int i = 0; i < max; i++)
            {

                //Pivot
                PivotItem pi = new PivotItem();
                pi.GotFocus += PivotItem_GotFocus;
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
                sv.Tapped += AppBarButton_GotFocus;

                //TextBlock - Question
                TextBlock tb = new TextBlock();
                tb.TextWrapping = TextWrapping.Wrap;
                tb.FontSize = 30;
                tb.Foreground = new SolidColorBrush(Colors.Black);
                tb.GotFocus += AppBarButton_GotFocus;
                // set datacontext (binding)
                tb.DataContext = dsl[i];
                // binding
                Binding binding = new Binding() { Path = new PropertyPath("Question") };
                tb.SetBinding(TextBlock.TextProperty, binding);

                sv.Content = tb;


                //Grid
                Grid grid = new Grid();
                //grid.Background = new SolidColorBrush(Colors.White);
                RowDefinition rd1 = new RowDefinition();
                RowDefinition rd2 = new RowDefinition();
                rd1.Height = new GridLength(190);
                rd2.Height = new GridLength(1, GridUnitType.Star);
                grid.RowDefinitions.Add(rd1);
                grid.RowDefinitions.Add(rd2);
                grid.Margin = new Thickness(0, 0, 0, 0);

                // set ScrollViewer to row 0
                Grid.SetRow(sv, 0);
                // add ScrollViewer to grid
                grid.Children.Add(sv);

                

                if (!dsl[i].IsBigAnswer)
                {
                    //StackPanel
                    StackPanel sp = new StackPanel();                   
                    sp.VerticalAlignment = Windows.UI.Xaml.VerticalAlignment.Bottom;
                    sp.HorizontalAlignment = Windows.UI.Xaml.HorizontalAlignment.Stretch;
                    
                    string lAnswer = "";
                    string lColor = "";
                    int lButtonID = 0;
                    
                    for (int j = 0; j < 3; j++)
                    {
                        
                        
                        
                        // 2. Grid (Buttons)
                        Grid grid2 = new Grid();
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


                        if (j == 0)
                        {
                            lAnswer = "Answer1";
                            lColor = "Answer1Color";
                            lButtonID = 1;
                        }
                        else if (j == 1)
                        {
                            lAnswer = "Answer3";
                            lColor = "Answer3Color";
                            lButtonID = 3;
                        }
                        else if (j == 2)
                        {
                            lAnswer = "Answer5";
                            lColor = "Answer5Color";
                            lButtonID = 5;
                        }
                      
                        Button bt1 = new Button();
                        bt1.Width = 165;
                        bt1.Height = 95;
                        bt1.Tag = lButtonID;
                        bt1.Click += Button_Click;
                        bt1.Foreground = new SolidColorBrush(Colors.Black);

                        TextBlock tbButton = new TextBlock();
                        tbButton.FontSize = 15;
                        tbButton.TextWrapping = TextWrapping.Wrap;                     
                        // set datacontext (binding)
                        tbButton.DataContext = dsl[i];
                        // binding
                       

                        binding = new Binding() { Path = new PropertyPath(lAnswer) };
                        tbButton.SetBinding(TextBlock.TextProperty, binding);

                        binding = new Binding() { Path = new PropertyPath(lColor) };                        
                        bt1.SetBinding(Button.BackgroundProperty, binding);
                        bt1.SetBinding(Button.BorderBrushProperty, binding);
                        // set datacontext (binding)
                        bt1.DataContext = dsl[i];

                        bt1.Content = tbButton;

                        // set Button to column 0
                        Grid.SetColumn(bt1, 0);
                        // add Button to grid
                        grid2.Children.Add(bt1);

                        if (j == 0)
                        {
                            lAnswer = "Answer2";
                            lColor = "Answer2Color";
                            lButtonID = 2;
                        }
                        else if (j == 1)
                        {
                            lAnswer = "Answer4";
                            lColor = "Answer4Color";
                            lButtonID = 4;
                        }
                        else if (j == 2)
                        {
                            lAnswer = "Answer6";
                            lColor = "Answer6Color";
                            lButtonID = 6;
                        }

                        Button bt2 = new Button();
                        bt2.Width = 175;
                        bt2.Height = 95;
                        bt2.Tag = lButtonID;
                        bt2.Click += Button_Click;
                        bt2.Foreground = new SolidColorBrush(Colors.Black);

                        tbButton = new TextBlock();
                        tbButton.FontSize = 15;
                        tbButton.TextWrapping = TextWrapping.Wrap;
                        // set datacontext (binding)
                        tbButton.DataContext = dsl[i];
                        // binding

                        
                        binding = new Binding() { Path = new PropertyPath(lAnswer) };
                        tbButton.SetBinding(TextBlock.TextProperty, binding);

                        binding = new Binding() { Path = new PropertyPath(lColor) };
                        bt2.SetBinding(Button.BackgroundProperty, binding);
                        bt2.SetBinding(Button.BorderBrushProperty, binding);
                        // set datacontext (binding)
                        bt2.DataContext = dsl[i];

                        bt2.Content = tbButton;

                        // set Button to column 2
                        Grid.SetColumn(bt2, 2);
                        // add Button to grid
                        grid2.Children.Add(bt2);

                        sp.Children.Add(grid2);
                    }

                    // set StackPanel to row 1
                    Grid.SetRow(sp, 1);
                    // add StackPanel to grid
                    grid.Children.Add(sp);

                }
                else
                {
                    Grid grid2 = new Grid();
                    grid2.Margin = new Thickness(0, 0, 0, 0);
                    
                    TextBox _textBox = new TextBox();
                    _textBox.Margin = new Thickness(0, 0, 0, 0);
                    _textBox.VerticalAlignment = Windows.UI.Xaml.VerticalAlignment.Stretch;
                    _textBox.HorizontalAlignment = Windows.UI.Xaml.HorizontalAlignment.Stretch;
                    _textBox.Loaded += tb_Loaded;
                    _textBox.TextWrapping = TextWrapping.Wrap;
                    _textBox.AcceptsReturn = true;
                    _textBox.Background = new SolidColorBrush(Colors.Gray);
                    sTextBox stb = new sTextBox(i, _textBox);
                    tbl.Add(stb);

                    // set TextBox to row 1
                    Grid.SetRow(_textBox, 1);
                    // add TextBox to grid
                    grid.Children.Add(_textBox);
                }







                

                pi.Content = grid;
                

                //CommandBar
                AppBarButton abb = new AppBarButton();
                abb.GotFocus += AppBarButton_GotFocus;
                abb.Click += AppBarButton_Click;
                abb.Tag = i;

                // set datacontext (binding)
                //abb.DataContext = dsl[i];
                // binding
                //binding = new Binding() { Path = new PropertyPath("QuestionWithID") };
                //abb.SetBinding(AppBarButton.LabelProperty, binding);


                abb.Label = (i + 1) + ". " + dsl[i].Question;
                

                this.cbar.SecondaryCommands.Add(abb);
             
            }
             
             */   
        }

        private async Task<DTO.QuestionsDTO> GetSampleDataAsync()
        {

            Uri dataUri = new Uri("ms-appx:///DataModel/Questions.json");           

            StorageFile file = await StorageFile.GetFileFromApplicationUriAsync(dataUri);
            
            using (var fileStream = await file.OpenStreamForReadAsync())
            {
                var memoryStream = new MemoryStream();
                await fileStream.CopyToAsync(memoryStream);

                memoryStream.Seek(0, SeekOrigin.Begin);
                

                DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(DTO.QuestionsDTO));
       
                DTO.QuestionsDTO dmq = (DTO.QuestionsDTO)ser.ReadObject(memoryStream);

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

        private void tb_Loaded(object sender, RoutedEventArgs e)
        {
            
            // Focus für die Textbox setzen, damit die Tastatur gleich geöffnet wird
            
            _textBox.Focus(FocusState.Programmatic);

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

        private void PivotItem_Loaded(object sender, RoutedEventArgs e)
        {
            // damit die Tastatur sofort geöffnet wird
            //_textBox.Focus(FocusState.Programmatic); 
        }

        private void abbAccept_Click(object sender, RoutedEventArgs e)
        {
            mDialog("Wollen Sie die Evaluationsdaten abschicken?");
        }

        public async void mDialog(string text)
        {

            Windows.UI.Popups.MessageDialog msg = new MessageDialog(text);
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
                    

                    break;
                //Quit Button.
                case "Nein":
                    
                    break;
                //end.
            }
        }
        
       
    }
}
