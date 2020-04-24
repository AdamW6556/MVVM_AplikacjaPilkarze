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

namespace MVVMpilkarzezad.View
{
    /// <summary>
    /// Logika interakcji dla klasy TextBoxErrorProvider.xaml
    /// </summary>
    public partial class TextBoxErrorProvider : UserControl
    {
        public TextBoxErrorProvider()
        {
            InitializeComponent();
        }

      public static readonly RoutedEvent TextChangedEvent =
      EventManager.RegisterRoutedEvent("TabItemSelected",
                   RoutingStrategy.Bubble, typeof(RoutedEventHandler),
                   typeof(TextBoxErrorProvider));
     
        public event RoutedEventHandler NumberChanged
        {
            add { AddHandler(TextChangedEvent, value); }
            remove { RemoveHandler(TextChangedEvent, value); }
        }
    
        private void RaiseTextChanged()
        {
            
            RoutedEventArgs newEventArgs =
                    new RoutedEventArgs(TextBoxErrorProvider.TextChangedEvent);
          
            RaiseEvent(newEventArgs);
        }
             
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register(
                "Text",
                typeof(string),
                typeof(TextBoxErrorProvider),
                new FrameworkPropertyMetadata(null)
            );
     
        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }
         
        private void textBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!e.Text.All(char.IsLetter))
            {
                e.Handled = true;
            }
        }

        private void textBox_TextChanged(object sender, TextChangedEventArgs e)
        {
      
            RaiseTextChanged();
        }

        private void textBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                e.Handled = true;
            }
        }
    }
}
