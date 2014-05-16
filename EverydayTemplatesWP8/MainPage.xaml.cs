using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using EverydayTemplatesWP8.Resources;
using BugSense;
using System.Diagnostics;
using BugSense.Core.Model;
using EverydayTemplatesWP8.ViewModels;

namespace EverydayTemplatesWP8
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();

            BugSenseHandler.Instance.UnhandledExceptionHandled += (sender, response) =>
                Debug.WriteLine("Exception of type {0} handled by BugSense\r\nClient Request: {1}",
                response.ExceptionObject.GetType(),
                response.ClientJsonRequest);

            BugSenseHandler.Instance.LoggedRequestHandled += (sender, args) =>
            {
                if (args.BugSenseLoggedResponseResult.RequestType == BugSenseRequestType.Event)
                {
                    Debug.WriteLine("Logged Request {0}\r\nServer Response: {1}",
                        args.BugSenseLoggedResponseResult.ClientRequest,
                        args.BugSenseLoggedResponseResult.ServerResponse);
                }
                else
                {
                    Debug.WriteLine("Logged Request {0}\r\nServer Response: {1}\r\nErrorId: {2}\r\nResolved: {3}",
                        args.BugSenseLoggedResponseResult.ClientRequest,
                        args.BugSenseLoggedResponseResult.ServerResponse,
                        args.BugSenseLoggedResponseResult.ErrorId,
                        args.BugSenseLoggedResponseResult.IsResolved ? "Yes" : "No");
                }
            };

            DataContext = App.ViewModel;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            BugSenseHandler.Instance.RegisterAsyncHandlerContext();

            if (!App.ViewModel.IsDataLoaded)
            {
                App.ViewModel.LoadData();
            }
        }

        private void TemplateSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (TemplateSelector.SelectedItem == null)
                return;

            var menuItem = TemplateSelector.SelectedItem as TemplateMenuItem;
            NavigationService.Navigate(new Uri(menuItem.Url, UriKind.Relative));

            TemplateSelector.SelectedItem = null;
        }
    }
}