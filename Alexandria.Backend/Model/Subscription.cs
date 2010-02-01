using System;

namespace Alexandria.Backend.Model
{
	public class Subscription
	{
		public virtual long Id { get; set; }
		public virtual int NumberOfPossibleBooksOut { get; set; }
		public virtual decimal MonthlyCost { get; set; }
		public virtual string CreditCard { get; set; }
		public virtual User User { get; set; }
		public virtual DateTime Start{ get; set; }
		public virtual DateTime End { get; set; }
	}
}