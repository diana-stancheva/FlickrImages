namespace FlickrImages.ViewModels
{
    using System;
    using System.Linq;
    using System.Windows.Input;
    using System.Net.Http;

    using Windows.Storage;
    using Windows.Storage.Streams;
    using Windows.Storage.FileProperties;
    using Windows.UI.Xaml.Media.Imaging;
    using Windows.UI.Popups;

    using GalaSoft.MvvmLight;

    using FlickrImages.Common;
    using FlickrImages.Settings;

    public class ImageDetailsPageViewModel : ViewModelBase
    {
        private ImageDetailsPageDataContextModel imageDetailsPageContext;

        public ImageDetailsPageViewModel()
        {
            this.LoadPrevious = new RelayCommand(this.PerformLoadPrevious);
            this.LoadNext = new RelayCommand(this.PerformLoadNext);
            this.SaveImage = new RelayCommand(this.PerformSaveImage);
        }

        private void PerformLoadPrevious()
        {
            int index = this.ImageDetailsPageContext.ItemsList.IndexOf(this.ImageDetailsPageContext.SelectedItem);

            if (index == 0)
            {
                return;
            }

            var previousItem = this.ImageDetailsPageContext.ItemsList.ElementAt(index - 1);
            
            this.ImageDetailsPageContext.SelectedItem = previousItem;
            this.RaisePropertyChanged(() => this.ImageDetailsPageContext);
        }

        private void PerformLoadNext()
        {
            int index = this.ImageDetailsPageContext.ItemsList.IndexOf(this.ImageDetailsPageContext.SelectedItem);

            if (index == this.ImageDetailsPageContext.ItemsList.Count - 1)
            {
                return;
            }

            var nextItem = this.ImageDetailsPageContext.ItemsList.ElementAt(index + 1);

            this.ImageDetailsPageContext.SelectedItem = nextItem;
            this.RaisePropertyChanged(() => this.ImageDetailsPageContext);
        }

        private async void PerformSaveImage()
        {
            try
            {
                Uri uri = new Uri(ImageDetailsPageContext.SelectedItem.Media);
                string fileName = "FlickrImage.jpg";                 

                var bitmapImage = new BitmapImage();
                var httpClient = new HttpClient();
                var httpResponse = await httpClient.GetAsync(uri);
                byte[] b = await httpResponse.Content.ReadAsByteArrayAsync();

                using (var stream = new InMemoryRandomAccessStream())
                {
                    using (DataWriter dw = new DataWriter(stream))
                    {
                        dw.WriteBytes(b);
                        await dw.StoreAsync();

                        stream.Seek(0);
                        bitmapImage.SetSource(stream);

                        StorageFolder storageFolder = await KnownFolders.PicturesLibrary.CreateFolderAsync(FlickrImagesSettings.ApplicationFolderName, CreationCollisionOption.OpenIfExists);
                        StorageFile storageFile = await storageFolder.CreateFileAsync(fileName, CreationCollisionOption.GenerateUniqueName);

                        ImageProperties pictureProperties = await storageFile.Properties.GetImagePropertiesAsync();
                        pictureProperties.Title = ImageDetailsPageContext.SelectedItem.Title;
                        pictureProperties.SavePropertiesAsync();

                        using (var storageStream = await storageFile.OpenAsync(FileAccessMode.ReadWrite))
                        {
                            await RandomAccessStream.CopyAndCloseAsync(stream.GetInputStreamAt(0), storageStream.GetOutputStreamAt(0));
                        }
                    }
                }

                new MessageDialog("Image saved").ShowAsync();
            }
            catch (Exception ex)
            {
                new MessageDialog(String.Format("Error Saving Picture: {0}", ex.Message)).ShowAsync();
            }
        }

        public ICommand LoadPrevious { get; set; }

        public ICommand LoadNext { get; set; }

        public ICommand SaveImage { get; set; }

        public ImageDetailsPageDataContextModel ImageDetailsPageContext
        {
            get
            {
                return this.imageDetailsPageContext;
            }
            set
            {
                this.imageDetailsPageContext = value;
                this.RaisePropertyChanged(() => this.ImageDetailsPageContext);
                this.RaisePropertyChanged(() => this.imageDetailsPageContext.SelectedItem);
            }
        }
    }
}