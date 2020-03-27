using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace t
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class T : ContentPage
    {
        public T()
        {
            InitializeComponent();
            //Content = new Label
            //{
            //    HorizontalOptions = LayoutOptions.Center,
            //    VerticalOptions = LayoutOptions.Center,
            //    Text = "Hello World"

            //};
        }

        //private void Button_Clicked(object sender, EventArgs e)
        //{
        //    DisplayAlert("Title", "Hello World", "Ok");
        //}
    }
}