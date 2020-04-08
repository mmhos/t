using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace t
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SearchableDisplay : ContentPage
    {
        public SearchableDisplay()
        {
            InitializeComponent();
            listView.ItemsSource = gItems();
        }

        private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            listView.ItemsSource = gItems(e.NewTextValue);

        }
        IEnumerable<SimpleItem> gItems(string searchText = null){
            var ite = new List<SimpleItem>();
            DBConnect co = new DBConnect();
            if (co.OpenConnection() == true)
            {
                string query = "SELECT * FROM inventory  where id  <" + (Constants.ItemNumberForDisplay+1);
                var cmd = new MySql.Data.MySqlClient.MySqlCommand(query, co.connection);
                 var reader = cmd.ExecuteReader();
                string[] values = {   "fllocation", "flagency", "flcond", "flsolarinumb", "flmodel", "fldesc" };
                while (reader.Read())
                {
                    
                    string des="";
                    for (int i = 0; i <= Constants.AttributeNumberForDisplay-1; i = i + 1)
                    {
                        des= des + values[i] + " " + reader[values[i]] + "  ";
                    }
                    SimpleItem nItem = new SimpleItem(reader["flItem"].ToString(), des);
                    ite.Add(nItem);
                }
                
                
                co.connection.Close();
                
            }

            if (String.IsNullOrWhiteSpace(searchText))
            {
                
                return ite; 
            
            }

            else
            {
                
                return ite.Where(c => c.Name.StartsWith(searchText)).ToList(); }
        }

        private void ItemNumberDisplay_TextChanged(object sender, TextChangedEventArgs e)
        {
            if ((e.NewTextValue) == "") { return; }
            Constants.ItemNumberForDisplay = Int32.Parse(e.NewTextValue);
            listView.ItemsSource = gItems();
        }

        private void AttributeNumberForDisplay_TextChanged(object sender, TextChangedEventArgs e)
        {
            if ((e.NewTextValue) == "") { return; }
            Constants.AttributeNumberForDisplay = Int32.Parse(e.NewTextValue);
            listView.ItemsSource = gItems();

        }

        async private void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            Navigation.InsertPageBefore(new MainPage(), this);
            await Navigation.PopAsync();
        }
    }
}