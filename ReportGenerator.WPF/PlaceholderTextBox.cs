using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    /// Follow steps 1a or 1b and then 2 to use this custom control in a XAML file.
    ///
    /// Step 1a) Using this custom control in a XAML file that exists in the current project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is 
    /// to be used:
    ///
    ///     xmlns:MyNamespace="clr-namespace:Report_o_matic_WPF"
    ///
    ///
    /// Step 1b) Using this custom control in a XAML file that exists in a different project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is 
    /// to be used:
    ///
    ///     xmlns:MyNamespace="clr-namespace:Report_o_matic_WPF;assembly=Report_o_matic_WPF"
    ///
    /// You will also need to add a project reference from the project where the XAML file lives
    /// to this project and Rebuild to avoid compilation errors:
    ///
    ///     Right click on the target project in the Solution Explorer and
    ///     "Add Reference"->"Projects"->[Browse to and select this project]
    ///
    ///
    /// Step 2)
    /// Go ahead and use your control in the XAML file.
    ///
    ///     <MyNamespace:PlaceholderTextBox/>
    ///
    /// </summary>
    public class PlaceholderTextBox : TextBox
    {
        private TextBlock _placeholder;

        static PlaceholderTextBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PlaceholderTextBox), new FrameworkPropertyMetadata(typeof(PlaceholderTextBox)));
        }

        public static readonly DependencyProperty PlaceholderTextProperty = DependencyProperty.Register("PlaceholderText",
            typeof(string),
            typeof(PlaceholderTextBox),
            new FrameworkPropertyMetadata("Placeholder"));

        public string PlaceholderText
        {
            get { return (string)GetValue(PlaceholderTextProperty); }
            set { SetValue(PlaceholderTextProperty, value); }
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            GotFocus += PlaceholderTextBox_GotFocus;
            LostFocus += PlaceholderTextBox_LostFocus;

            _placeholder = Template.FindName("PART_Placeholder", this) as TextBlock;
        }

        private void PlaceholderTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (_placeholder!= null && Text.Length == 0 )
            {
                _placeholder.Visibility = Visibility.Visible;
            }
        }

        private void PlaceholderTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (_placeholder != null)
            {
                _placeholder.Visibility = Visibility.Hidden;
            }
        }
    }
}
