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
            Constants.findTerm = "flItem";
            string st = await DisplayPromptAsync("", "Find By Name ");
            fp(st);
            }

        async private void Button_Clicked_1(object sender, EventArgs e)
        {
            Constants.findTerm = "flsolarinumber";
            string st = await DisplayPromptAsync("", "Find By Number");
            fp(st);
        }

        async private void Button_Clicked_2(object sender, EventArgs e)
        {
            Constants.findTerm = "flagency";
            string st = await DisplayPromptAsync("", "Find By Agency");
            fp(st);
        }

        async private void Button_Clicked_3(object sender, EventArgs e)
        {
            Constants.findTerm = "flcustomer";
            string st = await DisplayPromptAsync("", "Find By Customer");
            fp(st);
        }
        async public void fp(string st) {
            DBConnect co = new DBConnect();
            if (co.OpenConnection() == true)
            {

                {
                    // string query = "SELECT * FROM inventory  where LIKE flItem ='%" + st + "%'";

                    string query = "SELECT * FROM inventory ";


                    var cmd = new MySql.Data.MySqlClient.MySqlCommand(query, co.connection);
                    var reader = cmd.ExecuteReader();
                    await DisplayAlert("Scanned Infos", cmd.CommandText, "OK");
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
                    var counter = 1;
                    Constants.AttributeNumberForDisplay = values.Length;
                    await DisplayAlert("Scanned Infos", "Before while loop", "OK");
                    grid.Children.Add(new Label
                    {
                        //Text = "Row " + counter + ", Column " + i + reader[values[i]],
                        //Text = "["+counter+","+i+"]"+ reader[values[i]],
                        Text = " " + values[0].Substring(2),
                        HorizontalOptions = LayoutOptions.Center,
                        VerticalOptions = LayoutOptions.Center
                    }, 0, 0);
                    for (int i = 1; i <= Constants.AttributeNumberForDisplay - 2; i = i + 1)
                    {
                        string t = " " + values[i];
                        t = t.Substring(6);
                        grid.Children.Add(new Label
                        {
                            //Text = "Row " + counter + ", Column " + i + reader[values[i]],
                            //Text = "["+counter+","+i+"]"+ reader[values[i]],
                            Text = " " + t,
                            HorizontalOptions = LayoutOptions.Center,
                            VerticalOptions = LayoutOptions.Center
                        }, i, 0);


                    }
                    grid.Children.Add(new Label
                    {
                        //Text = "Row " + counter + ", Column " + i + reader[values[i]],
                        //Text = "["+counter+","+i+"]"+ reader[values[i]],
                        Text = " " + values[Constants.AttributeNumberForDisplay - 1],
                        HorizontalOptions = LayoutOptions.Center,
                        VerticalOptions = LayoutOptions.Center
                    }, Constants.AttributeNumberForDisplay - 1, 0);
                    while (reader.Read())
                    {
                        string currentItem = "" + reader[Constants.findTerm];
                        if (currentItem.Contains(st))
                        {
                            for (int i = 0; i <= Constants.AttributeNumberForDisplay - 1; i = i + 1)
                            {
                                string t = " " + reader[values[i]];

                                grid.Children.Add(new Label
                                {
                                    //Text = "Row " + counter + ", Column " + i + reader[values[i]],
                                    //Text = "["+counter+","+i+"]"+ reader[values[i]],
                                    Text = t,
                                    HorizontalOptions = LayoutOptions.Center,
                                    VerticalOptions = LayoutOptions.Center
                                }, i, counter);
                            }
                            counter = counter + 1;
                        }

                    }

                    await DisplayAlert("Scanned Infos", "After while loop " + counter, "OK");
                    ScrollView scrollView = new ScrollView { Content = grid };

                    Content = scrollView;
                    Title = "All items with " + st+ " in "+Constants.findTerm.Substring(2);


                }
            }

        }
    }
}