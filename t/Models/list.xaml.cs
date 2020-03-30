using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace t.Models
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class list : ContentPage
    {
        public list()
        {
            InitializeComponent();
            var items = new List<Item>
            {
                new Item { Name = "Display" ,Id =1},
                new Item { Name = "Controller" ,Id =1},
                new Item { Name = "Router" ,Id =1},




            };
            itemList.ItemsSource = items;
        }
    }
}