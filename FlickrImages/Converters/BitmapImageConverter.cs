namespace FlickrImages.Converters
{
    using System;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Windows.Storage;
    using Windows.Storage.Streams;
    using Windows.UI.Xaml.Data;
    using Windows.UI.Xaml.Media.Imaging;

    using FlickrImages.Settings;

    public class BitmapImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            string filePath = value.ToString(); //get path to picture
            BitmapImage image = new BitmapImage(); //create bitmap image blank
            if (filePath.Contains("Assets")) //if standart picture
            {
                this.LoadImageFromAssetsLibrary(image, filePath);
                return image;
            }
            else if (filePath.Contains("https://") || filePath.Contains("http://"))
            {
                return filePath;
            }
            else
            {
                this.LoadImageFromString(image, filePath);
                return image;
            }
        }
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            // TODO: Implement this method
            throw new NotImplementedException();
        }
        
        private async Task LoadImageFromString(BitmapImage image, string path)
        {
            StorageFolder folder = await KnownFolders.PicturesLibrary.GetFolderAsync(FlickrImagesSettings.ApplicationFolderName);
            string outputPath = path.Substring(path.IndexOf('\\') + 1);
            StorageFile file = await folder.GetFileAsync(outputPath); //find and read file
            
            using (IRandomAccessStream stream = await file.OpenAsync(FileAccessMode.Read))
            {
                await image.SetSourceAsync(stream);
            }
        }

        private async Task LoadImageFromAssetsLibrary(BitmapImage image, string path)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("ms-appx:///");
            builder.Append(path);
            string localPath = builder.ToString();

            Uri requestedFileUri = new Uri(localPath); //set default folder
            StorageFile file = await StorageFile.GetFileFromApplicationUriAsync(requestedFileUri); //read file
            
            using (IRandomAccessStream stream = await file.OpenAsync(FileAccessMode.Read))
            {
                await image.SetSourceAsync(stream);
            }
        }
    }
}
