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
    public partial class fm : ContentPage
    {
         public fm()
        {
            InitializeComponent();
           
        }
        async void OnLogoutButtonClicked(object sender, EventArgs e)
        {
            App.IsUserLoggedIn = false;
            Navigation.InsertPageBefore(new LoginPage(), this);
            await Navigation.PopAsync();
        }

        async private void Button_Clicked(object sender, EventArgs e)
        {
            string st = await DisplayPromptAsync("", "Find By Namwe ");
        }

        async private void Button_Clicked_1(object sender, EventArgs e)
        {
            string st = await DisplayPromptAsync("", "Find By Number");
        }

        async private void Button_Clicked_2(object sender, EventArgs e)
        {
            string st = await DisplayPromptAsync("", "Find By Agency");
        }

        async private void Button_Clicked_3(object sender, EventArgs e)
        {
            string st = await DisplayPromptAsync("", "Find By Customer");
        }
    }
}