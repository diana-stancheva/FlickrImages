namespace FlickrImages.ViewModels
{
    using System;
    using System.Linq;
    using System.Collections.ObjectModel;
    using System.Threading.Tasks;

    using Windows.Web.Http; 
    using Windows.UI.Popups;

    using GalaSoft.MvvmLight;
    using Newtonsoft.Json;

    using FlickrImages.Models;
   
    public class FeedItemsPageViewModel : ViewModelBase
    {
        private readonly string feedUri = "https://api.flickr.com/services/feeds/photos_public.gne?format=json";

        private ObservableCollection<FeedItemViewModel> feedItems;

        public FeedItemsPageViewModel()
        {
            this.LoadFeedItems();
        }

        private async void LoadFeedItems()
        {
            string jsonFeed = string.Empty;

            try
            {
                jsonFeed = await GetFeedAsync(feedUri);
            }
            catch (Exception)
            {
                new MessageDialog("Cannot load feed items. Please try again later!").ShowAsync();
                return;
            }

            jsonFeed = jsonFeed.Replace("jsonFlickrFeed(", "");
            jsonFeed = jsonFeed.Remove(jsonFeed.Length - 1);

            var settings = new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore, DateFormatHandling = DateFormatHandling.IsoDateFormat };
            var feed = JsonConvert.DeserializeObject<FeedModel>(jsonFeed, settings);
            //this.FeedItems = feed.Items.AsQueryable().Select(FeedItemViewModel.FromModel);
            
            var itemsCollection = feed.Items.AsQueryable().Select(FeedItemViewModel.FromModel);
            foreach (var item in itemsCollection)
            {
                this.FeedItems.Add(item);
            }
        }

        private async Task<string> GetFeedAsync(string feedUri)
        {
            var client = new HttpClient();
            var response = await client.GetAsync(new Uri(feedUri));
                
            return await response.Content.ReadAsStringAsync();
        }

        public ObservableCollection<FeedItemViewModel> FeedItems
        {
            get
            {
                if (this.feedItems == null)
                {
                    this.feedItems = new ObservableCollection<FeedItemViewModel>();
                }
                return this.feedItems;
            }
            set
            {
                if (this.feedItems == null)
                {
                    this.feedItems = new ObservableCollection<FeedItemViewModel>();
                }

                this.feedItems.Clear();
                foreach (var item in value)
                {
                    this.feedItems.Add(item);
                }
            }
        }
    }
}