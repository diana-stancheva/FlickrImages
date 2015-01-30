namespace FlickrImages.ViewModels
{
    using System;
    using System.Linq;
    using System.Windows.Input;

    using Windows.UI.Xaml.Controls;
    using Windows.UI.Xaml;

    using GalaSoft.MvvmLight;

    using FlickrImages.Common;
    using FlickrImages.Pages;

    public class MainPageViewModel : ViewModelBase
    {
        
        public MainPageViewModel()
        {
            this.BrowseFlickrImages = new RelayCommand(this.PerformBrowseFlickrImages);
            this.BrowseLocalImages = new RelayCommand(this.PerformBrowseLocalImages);
        }

        private void PerformBrowseFlickrImages()
        {
            Frame rootFrame = Window.Current.Content as Frame;
            rootFrame.Navigate(typeof(FeedItemsPage));
        }

        private void PerformBrowseLocalImages()
        {
            Frame rootFrame = Window.Current.Content as Frame;
            rootFrame.Navigate(typeof(LocalImagesPage));
        }

        public ICommand BrowseFlickrImages { get; set; }

        public ICommand BrowseLocalImages { get; set; } 
    }
}
