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
    public partial class deit : ContentPage
    {
        public deit()
        {
            InitializeComponent();
        }
        async private void dema(object sender, EventArgs e)
        {
            Navigation.InsertPageBefore(new dema(), this);
            await Navigation.PopAsync();
        }
        async private void deqr(object sender, EventArgs e)
        {
            Navigation.InsertPageBefore(new deqr(), this);
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