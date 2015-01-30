// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace FlickrImages.Views
{
    using System;
    using System.Linq;
    
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;

    public sealed partial class HeaderView : UserControl
    {
        public HeaderView()
        {
            this.InitializeComponent();
            this.DataContext = this;
        }

        public string TitleText
        {
            get { return (string)GetValue(TitleTextProperty); }
            set { SetValue(TitleTextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TitleText.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TitleTextProperty =
            DependencyProperty.Register("TitleText", typeof(string), typeof(HeaderView), new PropertyMetadata(null));
    }
}
