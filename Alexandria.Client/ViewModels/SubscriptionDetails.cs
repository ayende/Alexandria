namespace Alexandria.Client.ViewModels
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using Caliburn.PresentationFramework;
    using Messages;
    using Rhino.ServiceBus;

    public enum ViewMode
    {
        Retrieving,
        Editing,
        ChangesPending,
        Confirmed,
        Error
    }

    public class SubscriptionDetails : INotifyPropertyChanged
    {
        private readonly IServiceBus bus;
        private ViewMode _viewMode;
        private string city;
        private string country;
        private string creditCard;
        private string houseNumber;
        private decimal monthlyCost;
        private string name;
        private int numberOfPossibleBooksOut;

        private string street;
        private string zipCode;

        public SubscriptionDetails(IServiceBus bus)
        {
            this.bus = bus;
            ViewMode = ViewMode.Retrieving;
        }

        public ViewMode ViewMode
        {
            get { return _viewMode; }
            set
            {
                _viewMode = value;
                PropertyChanged(this, new PropertyChangedEventArgs("ViewMode"));
            }
        }

        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Name"));
            }
        }

        public string Street
        {
            get { return street; }
            set
            {
                street = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Street"));
            }
        }

        public string HouseNumber
        {
            get { return houseNumber; }
            set
            {
                houseNumber = value;
                PropertyChanged(this, new PropertyChangedEventArgs("HouseNumber"));
            }
        }

        public string City
        {
            get { return city; }
            set
            {
                city = value;
                PropertyChanged(this, new PropertyChangedEventArgs("City"));
            }
        }

        public string ZipCode
        {
            get { return zipCode; }
            set
            {
                zipCode = value;
                PropertyChanged(this, new PropertyChangedEventArgs("ZipCode"));
            }
        }

        public string Country
        {
            get { return country; }
            set
            {
                country = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Country"));
            }
        }

        public int NumberOfPossibleBooksOut
        {
            get { return numberOfPossibleBooksOut; }
            set
            {
                numberOfPossibleBooksOut = value;
                PropertyChanged(this, new PropertyChangedEventArgs("NumberOfPossibleBooksOut"));
            }
        }

        public decimal MonthlyCost
        {
            get { return monthlyCost; }
            set
            {
                monthlyCost = value;
                PropertyChanged(this, new PropertyChangedEventArgs("MonthlyCost"));
            }
        }

        public string CreditCard
        {
            get { return creditCard; }
            set
            {
                creditCard = value;
                PropertyChanged(this, new PropertyChangedEventArgs("CreditCard"));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        public void BeginEdit()
        {
            ViewMode = ViewMode.Editing;
        }

        public void Save()
        {
            ViewMode = ViewMode.ChangesPending;

            bus.Send(new UpdateCreditCardRequest
            {
                UserId = 1,
                CreditCard = CreditCard
            });
        }

        public void UpdateFrom(SubscriptionDetailsDTO subscriptionDetails)
        {
            ViewMode = ViewMode.Confirmed;

            Name = subscriptionDetails.Name;
            Street = subscriptionDetails.Street;
            HouseNumber = subscriptionDetails.HouseNumber;
            City = subscriptionDetails.City;
            ZipCode = subscriptionDetails.ZipCode;
            Country = subscriptionDetails.Country;
            NumberOfPossibleBooksOut = subscriptionDetails.NumberOfPossibleBooksOut;
            MonthlyCost = subscriptionDetails.MonthlyCost;
            CreditCard = subscriptionDetails.CreditCard;
        }
    }
}