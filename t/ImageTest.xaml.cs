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
    public partial class ImageTest : ContentPage
    {
        public ImageTest()
        {
            InitializeComponent();
            DBConnect co = new DBConnect();
            if (co.OpenConnection() == true)
            {
                var imageSource = new UriImageSource { Uri = new Uri("https://solaricorp.net/sc/images/Meanwell-SP-150-24.jpg") };
                imageSource.CachingEnabled = false;
                imageSource.CacheValidity = TimeSpan.FromHours(1);
                image.Source = imageSource;
                
            }

        }
        
    }
}