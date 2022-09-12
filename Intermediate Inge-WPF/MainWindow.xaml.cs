using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Intermediate_Inge_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public bool cheems = false;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void UIElement_OnClick(object sender, RoutedEventArgs routedEventArgs)
        {
            Gaming gamewindow = new Gaming(cheems);
            gamewindow.Show();
        }

        private void ToggleButton_OnChecked(object sender, RoutedEventArgs e)
        {
            cheems = true;
        }

        private void ToggleButton_OnUnchecked(object sender, RoutedEventArgs e)
        {
            cheems = false;
        }
    }
}