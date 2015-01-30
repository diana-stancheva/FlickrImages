namespace FlickrImages.Helpers
{
    using System.Linq;
    using Windows.Networking.Connectivity;

    public class NetworkChecker
    {
        public static bool IsIntenetConnectionAvailable()
        {
            var profiles = NetworkInformation.GetConnectionProfiles();
            var internetProfile = NetworkInformation.GetInternetConnectionProfile();
            var isConnected = profiles.Any(s => s.GetNetworkConnectivityLevel() == NetworkConnectivityLevel.InternetAccess) ||
                              (internetProfile != null && internetProfile.GetNetworkConnectivityLevel() == NetworkConnectivityLevel.InternetAccess);

            return isConnected;
        }
    }
}