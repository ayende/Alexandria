namespace Alexandria.Client.Infrastructure
{
    using System;
    using System.ComponentModel;

    public class Fact : INotifyPropertyChanged
    {
        private readonly Func<bool> predicate;

        public Fact(INotifyPropertyChanged observable, Func<bool> predicate)
        {
            this.predicate = predicate;
            observable.PropertyChanged += (sender, args) =>
                                          PropertyChanged(this, new PropertyChangedEventArgs("Value"));
        }

        public bool Value
        {
            get { return predicate(); }
        }

        public event PropertyChangedEventHandler PropertyChanged = delegate { };
    }
}