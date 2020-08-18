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
            string st = await DisplayPromptAsync("", "Find By Name ");
            DBConnect co = new DBConnect();
            if (co.OpenConnection() == true)
            {
               
                {
                    string query = "SELECT * FROM inventory  where flItem ='%" + st + "%'";
                    var cmd = new MySql.Data.MySqlClient.MySqlCommand(query, co.connection);
                    var reader = cmd.ExecuteReader();
                    string[] values = { "fllocation", "shelfNumber", "shelfColumn", "shelfRow", "shelfDepth", "flquantity" };
                    Grid grid = new Grid
                    {
                        RowDefinitions =
            {
                new RowDefinition { Height = new GridLength(2, GridUnitType.Star) },
                new RowDefinition(),
                new RowDefinition { Height = new GridLength(100) }
            },
                        ColumnDefinitions =
            {
                new ColumnDefinition(),
                new ColumnDefinition()
            }
                    };
                    var counter = 0;
                    Constants.AttributeNumberForDisplay = values.Length-1;
                    while (reader.Read())
                    {

                        counter = counter + 1;
                        for (int i = 0; i <= Constants.AttributeNumberForDisplay - 1; i = i + 1)
                        {
                            grid.Children.Add(new Label
                            {
                                Text = "Row "+counter+", Column "+i+ reader[values[i]],
                                HorizontalOptions = LayoutOptions.Center,
                                VerticalOptions = LayoutOptions.Center
                            }, counter, i);
                        }
                       
                        
                    }


                }
            }
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