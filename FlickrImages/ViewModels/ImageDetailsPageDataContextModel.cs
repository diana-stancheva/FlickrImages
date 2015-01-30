namespace FlickrImages.ViewModels
{
    using System;
    using System.Collections.ObjectModel;

    public class ImageDetailsPageDataContextModel
    {
        public ObservableCollection<FeedItemViewModel> ItemsList { get; set; }
        public FeedItemViewModel SelectedItem { get; set; }
    }
}
