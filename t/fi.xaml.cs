using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace t
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class fi : ContentPage
    {
        public fi()
        {
            InitializeComponent();
        }
        async private void fm(object sender, EventArgs e)
        {
            Navigation.InsertPageBefore(new fm(), this);
            await Navigation.PopAsync();
        }
        async private void fq(object sender, EventArgs e)
        {
            Navigation.InsertPageBefore(new adqr(), this);
            await Navigation.PopAsync();
        }
        async void OnLogoutButtonClicked(object sender, EventArgs e)
        {
            App.IsUserLoggedIn = false;
            Navigation.InsertPageBefore(new LoginPage(), this);
            await Navigation.PopAsync();
        }
    }
}