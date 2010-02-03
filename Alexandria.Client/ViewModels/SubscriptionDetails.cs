using System.ComponentModel;
using Alexandria.Messages;

namespace Alexandria.Client.ViewModels
{
    public class SubscriptionDetails : INotifyPropertyChanged
    {
        private string city;
        private string country;
        private string creditCard;
        private string houseNumber;
        private decimal monthlyCost;
        private string name;
        private int numberOfPossibleBooksOut;

        private string street;
        private string zipCode;

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

        public void UpdateFrom(SubscriptionDetailsDTO subscriptionDetails)
        {
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