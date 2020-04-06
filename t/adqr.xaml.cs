using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZXing.Net.Mobile.Forms;

namespace t
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class adqr : ContentPage
    {
        public adqr()
        {
            InitializeComponent();
        }
        async void OnLogoutButtonClicked(object sender, EventArgs e)
        {
            App.IsUserLoggedIn = false;
            Navigation.InsertPageBefore(new LoginPage(), this);
            await Navigation.PopAsync();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
#if __ANDROID__
	// Initialize the scanner first so it can track the current context
	MobileBarcodeScanner.Initialize (Application);
#endif

            try
            {
                

                var overlay = new ZXingDefaultOverlay
                {
                    TopText = "Please scan QR code",
                    BottomText = "Align the QR code within the frame"
                };

                var QRScanner = new ZXingScannerPage();

                await Navigation.PushModalAsync(QRScanner);

                QRScanner.OnScanResult += (result) =>
                {
                    // Stop scanning
                    QRScanner.IsScanning = false;

                    // Pop the page and show the result
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        Navigation.PopModalAsync(true);
                        DisplayAlert("Scanned Barcode", result.Text, "OK");
                        
                        
                        UniqueItemIdentifier uii = new UniqueItemIdentifier();
                        AddItem(uii);
                    });

                };

            }
            catch (Exception ex)
            {
                //GlobalScript.SeptemberDebugMessages("ERROR", "BtnScanQR_Clicked", "Opening ZXing Failed: " + ex);
            }
        }
        public void AddItem(UniqueItemIdentifier ui) {

            DBConnect addconn = new DBConnect();
            if (addconn.OpenConnection() == true) {
                string query = "SELECT EXISTS (SELECT * FROM inventory  where flcustomer =" + ui.customer + " AND flagency =" + ui.agency +  " AND  flsolarinumb = " + ui.solnum + ")";
                //query = "SELECT EXISTS(SELECT * FROM inventory  where  flsolarinumb = "+ui.solnum+")";
                var cmd = new MySql.Data.MySqlClient.MySqlCommand(query,addconn.connection);
                var reader = cmd.ExecuteReader();
                while (reader.Read()) {
                    DisplayAlert("Can read", reader[0].ToString(),"ok");
                }
            }
        }
    }


    

}