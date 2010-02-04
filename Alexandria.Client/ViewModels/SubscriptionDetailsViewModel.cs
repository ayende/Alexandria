namespace Alexandria.Client.ViewModels
{
    using Caliburn.Core;
    using Infrastructure;
    using Messages;
    using Rhino.ServiceBus;

    public class SubscriptionDetailsViewModel : PropertyChangedBase
    {
        private readonly IServiceBus bus;
        private PersonalDetailsModel details;
        private PersonalDetailsModel editable;
        private decimal monthlyCost;
        private int numberOfPossibleBooksOut;
        private ViewMode viewMode;

        public SubscriptionDetailsViewModel(IServiceBus bus)
        {
            this.bus = bus;

            ViewMode = ViewMode.Retrieving;
            Details = new PersonalDetailsModel();
            Editable = new PersonalDetailsModel();
        }

        public PersonalDetailsModel Details
        {
            get { return details; }
            set
            {
                details = value;
                NotifyOfPropertyChange( ()=> Details );
            }
        }

        public PersonalDetailsModel Editable
        {
            get { return editable; }
            set
            {
                editable = value;
                NotifyOfPropertyChange( ()=> Editable);
            }
        }

        public ViewMode ViewMode
        {
            get { return viewMode; }
            set
            {
                viewMode = value;
                NotifyOfPropertyChange(() => ViewMode);
            }
        }

        public int NumberOfPossibleBooksOut
        {
            get { return numberOfPossibleBooksOut; }
            set
            {
                numberOfPossibleBooksOut = value;
                NotifyOfPropertyChange(()=>NumberOfPossibleBooksOut);
            }
        }

        public decimal MonthlyCost
        {
            get { return monthlyCost; }
            set
            {
                monthlyCost = value;
                NotifyOfPropertyChange( ()=> MonthlyCost );
            }
        }

        public void BeginEdit()
        {
            ViewMode = ViewMode.Editing;

            Editable.Name = Details.Name;
            Editable.Street = Details.Street;
            Editable.HouseNumber = Details.HouseNumber;
            Editable.City = Details.City;
            Editable.ZipCode = Details.ZipCode;
            Editable.Country = Details.Country;
            //Editable.CreditCard = Details.CreditCard;
        }

        public void CancelEdit()
        {
            ViewMode = ViewMode.Confirmed;
            Editable = new PersonalDetailsModel();
        }

        public void Save()
        {
            ViewMode = ViewMode.ChangesPending;

            bus.Send(new UpdateDetailsRequest
                         {
                             UserId = 1,
                             Details = new SubscriptionDetailsDTO
                                           {
                                               Name = Editable.Name,
                                               Street = Editable.Street,
                                               HouseNumber = Editable.HouseNumber,
                                               City = Editable.City,
                                               ZipCode = Editable.ZipCode,
                                               Country = Editable.Country,
                                               CreditCard = Editable.CreditCard
                                           }
                         });
        }

        public void UpdateFrom(SubscriptionDetailsDTO subscriptionDetails)
        {
            ViewMode = ViewMode.Confirmed;

            Details.Name = subscriptionDetails.Name;
            Details.Street = subscriptionDetails.Street;
            Details.HouseNumber = subscriptionDetails.HouseNumber;
            Details.City = subscriptionDetails.City;
            Details.ZipCode = subscriptionDetails.ZipCode;
            Details.Country = subscriptionDetails.Country;

            NumberOfPossibleBooksOut = subscriptionDetails.NumberOfPossibleBooksOut;
            MonthlyCost = subscriptionDetails.MonthlyCost;

            Details.CreditCard = subscriptionDetails.CreditCard;
        }
    }
}