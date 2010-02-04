namespace Alexandria.Client.Infrastructure
{
    using System.ComponentModel;

    public interface ISupportsViewMode : INotifyPropertyChanged
    {
        ViewMode ViewMode { get; set; }
    }

    public enum ViewMode
    {
        Retrieving,
        Editing,
        ChangesPending,
        Confirmed,
        Error
    }
}