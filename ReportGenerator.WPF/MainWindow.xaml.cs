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

namespace ReportGeneratorWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {

            InitializeComponent();

            ViewModel = new ReportGeneratorViewModel();
            DataContext = ViewModel;
        }

        public ReportGeneratorViewModel ViewModel { get; }

        private void Placeholder_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(sender is TextBox Placeholder)
            {
                if (Placeholder.Text.Length != 0)
                {
                    Placeholder.Visibility = System.Windows.Visibility.Hidden;
                }
                else
                {
                    Placeholder.Visibility = System.Windows.Visibility.Visible;
                }
            }
        }
    }
}
