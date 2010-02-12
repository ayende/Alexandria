namespace Alexandria.Client.ViewModels
{
    using Caliburn.Core;
    using Infrastructure;
    using Messages;
    using Rhino.ServiceBus;

    public class SubscriptionDetails : PropertyChangedBase
    {
        private readonly IServiceBus bus;
        private readonly int userId;
        private ContactInfo details;
        private ContactInfo editable;
		private string errorMessage;
        private decimal monthlyCost;
        private int numberOfPossibleBooksOut;
        private ViewMode viewMode;

        public SubscriptionDetails(IServiceBus bus, int userId)
        {
            this.bus = bus;
            this.userId = userId;

            ViewMode = ViewMode.Retrieving;
            Details = new ContactInfo();
            Editable = new ContactInfo();
        }

        public ContactInfo Details
        {
            get { return details; }
            set
            {
                details = value;
                NotifyOfPropertyChange(() => Details);
            }
        }

        public ContactInfo Editable
        {
            get { return editable; }
            set
            {
                editable = value;
                NotifyOfPropertyChange(() => Editable);
            }
        }

    	public string ErrorMessage
    	{
    		get { return errorMessage; }
    		set
    		{
    			errorMessage = value;
				NotifyOfPropertyChange(() => ErrorMessage);
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
                NotifyOfPropertyChange(() => NumberOfPossibleBooksOut);
            }
        }

        public decimal MonthlyCost
        {
            get { return monthlyCost; }
            set
            {
                monthlyCost = value;
                NotifyOfPropertyChange(() => MonthlyCost);
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
            // This field is explicitly ommitted
            // Editable.CreditCard = Details.CreditCard;
			ErrorMessage = null;
		}

        public void CancelEdit()
        {
            ViewMode = ViewMode.Confirmed;
            Editable = new ContactInfo();
        	ErrorMessage = null;
        }

        public void Save()
        {
            ViewMode = ViewMode.ChangesPending;
            //TODO: add logic to handle credit card changes
            bus.Send(new UpdateAddress
            {
                UserId = userId,
                Details = new AddressDTO
                {
                    Street = Editable.Street,
                    HouseNumber = Editable.HouseNumber,
                    City = Editable.City,
                    ZipCode = Editable.ZipCode,
                    Country = Editable.Country,
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

        //from SubscriptionDetails
    	public void CompleteEdit()
    	{
			Details = Editable;
			Editable = new ContactInfo();
			ErrorMessage = null;
			ViewMode = ViewMode.Confirmed;
    	}

    	public void ErrorEdit(string theErrorMessage)
    	{
    		ViewMode = ViewMode.Error;
			ErrorMessage = theErrorMessage;
    	}
    }
}