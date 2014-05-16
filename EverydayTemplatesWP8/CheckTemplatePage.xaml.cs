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
            model.Amount = 10000.00;
            DataContext = model;
            
        }
    }
}