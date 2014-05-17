using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using EverydayTemplatesWP8.ViewModels;

namespace EverydayTemplatesWP8
{
    public partial class CheckTemplatePage : PhoneApplicationPage
    {
        private CheckTemplateViewModel model;
        public CheckTemplatePage()
        {
            InitializeComponent();
            model = new CheckTemplateViewModel();
            model.Date = DateTime.Now;
            model.Amount = 0.00;
            DataContext = model;
            Loaded += CheckTemplatePage_Loaded;
        }

        private void CheckTemplatePage_Loaded(object sender, RoutedEventArgs e)
        {
            Loaded -= CheckTemplatePage_Loaded;
            AmountTextBox.Focus();
        }

        private void AmountArea_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {

        }

        private void AmountTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            textBox.SelectionStart = 0;
            textBox.SelectionLength = textBox.Text.Length;
        }
    }
}