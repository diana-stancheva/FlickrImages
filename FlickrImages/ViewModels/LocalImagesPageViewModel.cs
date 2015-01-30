namespace FlickrImages.ViewModels
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    using Windows.Storage;

    using GalaSoft.MvvmLight;

    using FlickrImages.Settings;

    public class LocalImagesPageViewModel : ViewModelBase
    {
        private ObservableCollection<FeedItemViewModel> localImages;

        public LocalImagesPageViewModel()
        {
            this.LoadLocalmages();
        }

        private async void LoadLocalmages()
        {
            StorageFolder folder = await KnownFolders.PicturesLibrary.CreateFolderAsync(FlickrImagesSettings.ApplicationFolderName, CreationCollisionOption.OpenIfExists);
            IReadOnlyList<StorageFile> fileList = await folder.GetFilesAsync();

            if (fileList != null)
            {
                foreach (var file in fileList)
                {
                    this.LocalImages.Add(new FeedItemViewModel
                    {
                        Title = file.Name,
                        Media = file.FolderRelativeId,
                        Author = string.Empty
                    });
                }
            }
        }

        public ObservableCollection<FeedItemViewModel> LocalImages
        {
            get
            {
                if (this.localImages == null)
                {
                    this.localImages = new ObservableCollection<FeedItemViewModel>();
                }
                return this.localImages;
            }
            set
            {
                if (this.localImages == null)
                {
                    this.localImages = new ObservableCollection<FeedItemViewModel>();
                }
                this.localImages.Clear();
                foreach (var item in value)
                {
                    this.localImages.Add(item);
                }
            }
        }
    }
}