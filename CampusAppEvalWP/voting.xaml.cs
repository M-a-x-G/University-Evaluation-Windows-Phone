using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
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
    public sealed partial class voting : Page
    {

        private TextBlock NoArticlesTextBlock;
        //Loaded="NoArticlesTextBlock_Loaded"  xaml

        private void NoArticlesTextBlock_Loaded(object sender, RoutedEventArgs e)
        {
            NoArticlesTextBlock = (TextBlock)sender;

            NoArticlesTextBlock.Text = "<-    Frage 1 von 22    ->";
        }

        //Test
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
        }

        public voting()
        {
            this.InitializeComponent();
            
            
            
            //this.lb_counter // text = "<-   Frage 1 von 22   ->"

            for (int i = 0; i < 10; i++)
            {
                // Liste mit einer Struktur erstellen, Struktur = id, HubSection
                HubSection hubSection = new HubSection();
                TextBlock headerTextBlock = new TextBlock();
                headerTextBlock.Text = "New Hub Section " + i;
                hubSection.Header = headerTextBlock;
                hubSection.Padding = new Thickness(40, 30, 150, 44);

                this.MainHub.Sections.Add(hubSection);
            }

            //Test
            //TextBlock NoArticlesTextBlock = (TextBlock)FindChildByName(this, "lbCounter");
            //NoArticlesTextBlock.Text = "HALLO";

            

        }

        /// <summary>
        /// Wird aufgerufen, wenn diese Seite in einem Frame angezeigt werden soll.
        /// </summary>
        /// <param name="e">Ereignisdaten, die beschreiben, wie diese Seite erreicht wurde.
        /// Dieser Parameter wird normalerweise zum Konfigurieren der Seite verwendet.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        private void testbutton1_Click(object sender, RoutedEventArgs e)
        {
            MainHub.ScrollToSection(HS1);
        }

        private void testbutton2_Click(object sender, RoutedEventArgs e)
        {
            MainHub.ScrollToSection(HS2);

        }

        private void bt2_Click(object sender, RoutedEventArgs e)
        {
            MainHub.ScrollToSection(HS2);
        }

        private void MainHub_SectionsInViewChanged(object sender, SectionsInViewChangedEventArgs e)
        {
            /*var section = MainHub.SectionsInView[0];
            var tag = section.Tag.ToString();
            if (tag.Equals("1"))
            {


                //myControl.Focus(Windows.UI.Xaml.FocusState.Keyboard);
            }*/
        }

             
    }
}
