using EverydayTemplatesWP8.Resources;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EverydayTemplatesWP8.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<TemplateMenuItem> templateMenuItems;
        public ObservableCollection<TemplateMenuItem> TemplateMenuItems
        {
            get
            {
                return templateMenuItems;
            }
            private set
            {
                if (templateMenuItems != value)
                {
                    templateMenuItems = value;
                    NotifyPropertyChanged("TemplateMenuItems");
                }
            }
        }

        public bool IsDataLoaded { get; private set; }

        public MainViewModel()
        {
            //InitializeData();
        }

        public void LoadData()
        {
            TemplateMenuItem[] templateMenuItems = new TemplateMenuItem[] {
                new TemplateMenuItem("check", AppResources.TemplateCheck,String.Format("/CheckTemplatePage.xaml"))
            };
            TemplateMenuItems = new ObservableCollection<TemplateMenuItem>(templateMenuItems);

            this.IsDataLoaded = true;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (null != handler)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }

    public class TemplateMenuItem
    {
        public string UniqueId { get; set; }
        public string Url { get; set; }
        public string Header { get; set; }

        public TemplateMenuItem()
        {

        }

        public TemplateMenuItem(string uniqueId, string header, string url)
        {
            UniqueId = uniqueId;
            Header = header;
            Url = url;
        }
    }
}
