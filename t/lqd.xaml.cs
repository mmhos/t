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
    public partial class lqd : ContentPage
    {
        public lqd()
        {
            InitializeComponent();
            lowd();
        }
        async public void lowd()
        {
            DBConnect co = new DBConnect();
            if (co.OpenConnection() == true)
            {

                {
                    // string query = "SELECT * FROM inventory  where LIKE flItem ='%" + st + "%'";

                    string query = "SELECT * FROM inventory where flquantity < lowQuantityLimit ";


                    var cmd = new MySql.Data.MySqlClient.MySqlCommand(query, co.connection);
                    var reader = cmd.ExecuteReader();
                    await DisplayAlert("Scanned Infos", cmd.CommandText, "OK");
                    string[] values = { "fllocation", "flItem", "flquantity" };
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
                    for (int i = 0; i < 3; i++)
                    {
                        grid.Children.Add(new Label
                        {
                            //Text = "Row " + counter + ", Column " + i + reader[values[i]],
                            //Text = "["+counter+","+i+"]"+ reader[values[i]],
                            Text = " " + values[i].Substring(2),
                            HorizontalOptions = LayoutOptions.Center,
                            VerticalOptions = LayoutOptions.Center
                        }, i, 0);
                    }
                    while (reader.Read())
                    {
                        Constants.AttributeNumberForDisplay = 3;
                        {
                            for (int i = 0; i <= Constants.AttributeNumberForDisplay -1; i = i + 1)
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
                    Title = "All items  with low quantity" ;


                }
            }

        }
    }
}