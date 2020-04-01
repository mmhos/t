﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace t.UI
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Item : ContentPage
    {
        public Item()
        {
            InitializeComponent();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            favoriteSwitch.Toggled += OnFavoriteClicked;
        }

        protected override void OnBindingContextChanged()
        {
            //base.OnBindingContextChanged();
            //var personInfo = (PersonViewModel)BindingContext;
            //Title = personInfo.Person.Name;
            //favoriteLabel.Text = personInfo.IsFavorite ? "Added to favorites" : "Not in favorites";
            //DownloadImage();
        }

        private void OnFavoriteClicked(object sender, EventArgs e)
        {
            //var personInfo = (PersonViewModel)BindingContext;
            //personInfo.ToggleFavorite();
            //favoriteLabel.Text = personInfo.IsFavorite ? "Added to favorites" : "Not in favorites";
            //Navigation.PopAsync();
        }

        public void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            //var property = (PersonViewModel.Property)e.SelectedItem;
            //System.Diagnostics.Debug.WriteLine("Property clicked " + property.Type + " " + property.Value);

            //switch (property.Type)
            //{
            //    case PersonViewModel.PropertyType.Email:
            //        App.PhoneFeatureService.Email(property.Value);
            //        break;
            //    case PersonViewModel.PropertyType.Twitter:
            //        App.PhoneFeatureService.Tweet(property.Value);
            //        break;
            //    case PersonViewModel.PropertyType.Url:
            //        App.PhoneFeatureService.Browse(property.Value);
            //        break;
            //    case PersonViewModel.PropertyType.Phone:
            //        App.PhoneFeatureService.Call(property.Value);
            //        break;
            //    case PersonViewModel.PropertyType.Address:
            //        App.PhoneFeatureService.Map(property.Value);
            //        break;
            //}
        }

        private void OnCancelClicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

        private void DownloadImage()
        {
            //var personInfo = (PersonViewModel)BindingContext;
            //var person = personInfo.Person;

            //if (person.HasEmail)
            //{
            //    //var imageUrl = Gravatar.GetImageUrl(person.Email, IMAGE_SIZE);
            //    //PersonImage.Source = new UriImageSource { Uri = imageUrl };
            //}
        }
    
}
}