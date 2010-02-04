namespace Alexandria.Client.ViewModels
{
    using Caliburn.Core;

    public class EditSubscriptionDetails : PropertyChangedBase
    {
        private string city;
        private string country;
        private string creditCard;
        private string houseNumber;
        private string name;
        private string street;
        private string zipCode;

        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                NotifyOfPropertyChange("Name");
            }
        }

        public string Street
        {
            get { return street; }
            set
            {
                street = value;
                NotifyOfPropertyChange("Street");
            }
        }

        public string HouseNumber
        {
            get { return houseNumber; }
            set
            {
                houseNumber = value;
                NotifyOfPropertyChange("HouseNumber");
            }
        }

        public string City
        {
            get { return city; }
            set
            {
                city = value;
                NotifyOfPropertyChange("City");
            }
        }

        public string ZipCode
        {
            get { return zipCode; }
            set
            {
                zipCode = value;
                NotifyOfPropertyChange("ZipCode");
            }
        }

        public string Country
        {
            get { return country; }
            set
            {
                country = value;
                NotifyOfPropertyChange("Country");
            }
        }

        public string CreditCard
        {
            get { return creditCard; }
            set
            {
                creditCard = value;
                NotifyOfPropertyChange("CreditCard");
            }
        }
    }
}