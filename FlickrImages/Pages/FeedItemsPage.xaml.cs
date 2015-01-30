// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace FlickrImages.Pages
{
    using System;
    using System.Linq;

    using Windows.UI.Xaml.Controls;
    using Windows.UI.Xaml.Navigation;

    using FlickrImages.Common;
    using FlickrImages.ViewModels;
    using FlickrImages.Helpers;
    using Windows.UI.Popups;
    using System.Collections.ObjectModel;

    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class FeedItemsPage : Page
    {
        private NavigationHelper navigationHelper;
        private ObservableDictionary defaultViewModel = new ObservableDictionary();

        public FeedItemsPage() : this(new FeedItemsPageViewModel())
        {
        }

        public FeedItemsPage(FeedItemsPageViewModel viewModel)
        {
            this.InitializeComponent();

            this.navigationHelper = new NavigationHelper(this);
            this.navigationHelper.LoadState += this.NavigationHelper_LoadState;
            this.navigationHelper.SaveState += this.NavigationHelper_SaveState;

            this.DataContext = viewModel;
        }

        /// <summary>
        /// Gets the <see cref="NavigationHelper"/> associated with this <see cref="Page"/>.
        /// </summary>
        public NavigationHelper NavigationHelper
        {
            get
            {
                return this.navigationHelper;
            }
        }

        /// <summary>
        /// Gets the view model for this <see cref="Page"/>.
        /// This can be changed to a strongly typed view model.
        /// </summary>
        public ObservableDictionary DefaultViewModel
        {
            get
            {
                return this.defaultViewModel;
            }
        }

        /// <summary>
        /// Populates the page with content passed during navigation.  Any saved state is also
        /// provided when recreating a page from a prior session.
        /// </summary>
        /// <param name="sender">
        /// The source of the event; typically <see cref="NavigationHelper"/>
        /// </param>
        /// <param name="e">Event data that provides both the navigation parameter passed to
        /// <see cref="Frame.Navigate(Type, Object)"/> when this page was initially requested and
        /// a dictionary of state preserved by this page during an earlier
        /// session.  The state will be null the first time a page is visited.</param>
        private void NavigationHelper_LoadState(object sender, LoadStateEventArgs e)
        {
        }

        /// <summary>
        /// Preserves state associated with this page in case the application is suspended or the
        /// page is discarded from the navigation cache.  Values must conform to the serialization
        /// requirements of <see cref="SuspensionManager.SessionState"/>.
        /// </summary>
        /// <param name="sender">The source of the event; typically <see cref="NavigationHelper"/></param>
        /// <param name="e">Event data that provides an empty dictionary to be populated with
        /// serializable state.</param>
        private void NavigationHelper_SaveState(object sender, SaveStateEventArgs e)
        {
        }

        #region NavigationHelper registration

        /// <summary>
        /// The methods provided in this section are simply used to allow
        /// NavigationHelper to respond to the page's navigation methods.
        /// <para>
        /// Page specific logic should be placed in event handlers for the  
        /// <see cref="NavigationHelper.LoadState"/>
        /// and <see cref="NavigationHelper.SaveState"/>.
        /// The navigation parameter is available in the LoadState method 
        /// in addition to page state preserved during an earlier session.
        /// </para>
        /// </summary>
        /// <param name="e">Provides data for navigation methods and event
        /// handlers that cannot cancel the navigation request.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var isInternetConnectionActive = NetworkChecker.IsIntenetConnectionAvailable();
            if (!isInternetConnectionActive)
            {
                new MessageDialog("No internet Connection").ShowAsync();
                return;
            }

            this.navigationHelper.OnNavigatedTo(e);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            this.navigationHelper.OnNavigatedFrom(e);
        }

        #endregion

        private void OnFeedItemsSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var itemListView = (sender as ListView);
            var selectedObject = itemListView.SelectedItem;

            ImageDetailsPageDataContextModel context = new ImageDetailsPageDataContextModel();
            context.SelectedItem = selectedObject as FeedItemViewModel;

            FeedItemsPageViewModel dataContextView = this.DataContext as FeedItemsPageViewModel;
            ObservableCollection<FeedItemViewModel> feedItems = dataContextView.FeedItems;
            context.ItemsList = feedItems;

            this.Frame.Navigate(typeof(ImageDetailsPage), context);
        }
    }
}
