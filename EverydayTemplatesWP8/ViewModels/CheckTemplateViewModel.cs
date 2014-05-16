using EverydayTemplatesWP8.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EverydayTemplatesWP8.ViewModels
{
    public class CheckTemplateViewModel : INotifyPropertyChanged
    {
        private DateTime date;
        public DateTime Date
        {
            get
            {
                return date;
            }
            set
            {
                if (date != value)
                {
                    date = value;
                    NotifyPropertyChanged("Date");
                }
            }
        }

        private double amount;
        public double Amount
        {
            get
            {
                return amount;
            }
            set
            {
                amount = value;
                WrittenAmount = NumberToWordsConverter.ConvertToMoney(amount);
                NotifyPropertyChanged("Amount");
            }
        }

        private string writtenAmount;
        public string WrittenAmount
        {
            get
            { 
                return writtenAmount;
            }
            set
            {
                if (writtenAmount != value)
                {
                    writtenAmount = value;
                    NotifyPropertyChanged("WrittenAmount");
                }
            }
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
}
