using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace t
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }
         async void OnLogoutButtonClicked(object sender, EventArgs e)
        {
            App.IsUserLoggedIn = false;
            
            await Navigation.PopAsync();
        }
        async void Button_Clicked(object sender, EventArgs e)
        {

            Navigation.InsertPageBefore(new SearchableDisplay(), this);
            await Navigation.PopAsync();

        }
        async void Add_Clicked(object sender, EventArgs e)
        {

            Navigation.InsertPageBefore(new adit(), this);
            await Navigation.PopAsync();

        }
        async void Add2_Clicked(object sender, EventArgs e)
        {
            Constants.branch = "Add";
            Navigation.InsertPageBefore(new adit(), this);
            await Navigation.PopAsync();

        }
        async void Delete_Clicked(object sender, EventArgs e)
        {

            Navigation.InsertPageBefore(new deit(), this);
            await Navigation.PopAsync();

        }

        async void Button_Clicked_1(object sender, EventArgs e)
        {
            Navigation.InsertPageBefore(new fi(), this);
            await Navigation.PopAsync();
        }

       async  private void Button_Clicked_2(object sender, EventArgs e)
        {
            Navigation.InsertPageBefore(new lqd(), this);
            await Navigation.PopAsync();
        }
    }
}
