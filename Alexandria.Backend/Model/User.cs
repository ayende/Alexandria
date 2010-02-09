using System;
using System.Collections.Generic;

namespace Alexandria.Backend.Model
{
	public class User
	{
		public virtual long Id { get; set; }

		public virtual string Name { get; set; }
		public virtual Address Address { get; set; }

		public virtual IList<Book> Recommendations { get; set; }
		public virtual IList<Book> Queue { get; set; }
		public virtual ICollection<Book> CurrentlyReading { get; set; }
		public virtual ICollection<Subscription> Subscriptions { get; set; }

		public virtual void AddToQueue(Book book)
		{
			if (Queue.Contains(book) == false)
				Queue.Add(book);
			Recommendations.Remove(book);
			// add any other business logic related to adding a book to the queue
		}

		public virtual void ChangePositionInQueue(Book book, int newPosition)
		{
			Queue.Remove(book);
			Queue.Insert(newPosition, book);
			// add any other business logic related to shifting position in queue
		}

		public virtual void RemoveFromQueue(Book book)
		{
			Queue.Remove(book);
			// if it was on the queue, it probably means that the user
			// might want to read it again, so let us recommend it
			Recommendations.Add(book);
			// add any other business logic related to removing book from queue
		}

        public virtual void ChangeAddress(string street, string houseNumber, string city, string country, string zipCode)
	    {
            Address.Street = street;
            Address.HouseNumber = houseNumber;
            Address.City = city;
	        Address.Country = country;
            Address.ZipCode = zipCode;

            //add business logic to handle change of address logic, such as increased shipping fees, etc
        }
	}
}