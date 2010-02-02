namespace Alexandria.Client.Infrastructure
{
    using System;
    using System.Windows;

    public interface IPresenter
    {
        DependencyObject View { get; }
        object Result { get; }
        void Show();
        event Action Disposed;
        void ShowDialog();
    }
}