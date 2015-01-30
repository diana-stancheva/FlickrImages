namespace FlickrImages.ViewModels
{
    using System;
    using System.Linq.Expressions;

    using GalaSoft.MvvmLight;

    using FlickrImages.Models;

    public class FeedItemViewModel : ViewModelBase
    {
        public static Expression<Func<FeedItemModel, FeedItemViewModel>> FromModel
        {
            get
            {
                return model => new FeedItemViewModel()
                {
                    Title = model.Title,
                    Media = model.Media.M,
                    Author = model.Author
                };
            }
        }

        public string Title { get; set; }

        public string Media { get; set; }

        public string Author { get; set; }
    }
}
