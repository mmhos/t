using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZXing.Net.Mobile.Forms;

namespace t
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class deqr : ContentPage
    {
        public deqr()
        {
            InitializeComponent();
            
            lp();
        }
        async void OnLogoutButtonClicked(object sender, EventArgs e)
        {
            App.IsUserLoggedIn = false;
            Navigation.InsertPageBefore(new LoginPage(), this);
            await Navigation.PopAsync();
        }
        

        private async  void ZXingScannerView_OnScanResult(ZXing.Result r)
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                await DisplayAlert("Scanned result", r.Text, "OK");
            });

        }
        private void lp()
        {
#if __ANDROID__
	// Initialize the scanner first so it can track the current context
	MobileBarcodeScanner.Initialize (Application);
#endif
            //Button btn = sender as Button;
            //string s = btn.Text;
          
            var email =  new em();
            try
            {


                var overlay = new ZXingDefaultOverlay
                {
                    TopText = "Please scan QR code",
                    BottomText = "Align the QR code within the frame"
                };

                var QRScanner = new ZXingScannerPage();

                Navigation.PushModalAsync(QRScanner);

                QRScanner.OnScanResult += (result) =>
                {
                    // Stop scanning
                    QRScanner.IsScanning = false;

                    // Pop the page and show the result
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        Navigation.PopModalAsync(true);

                        //await DisplayAlert("Current destination", s, "OK");
                        //Constants.currentLocation = s;
                        await DisplayAlert("Scanned Barcode", result.Text, "OK");



                        string phrase = result.Text;
                        string[] words = phrase.Split(new string[] { " ll " }, StringSplitOptions.None); ;
                        int counter = 0;
                        int counter2 = 0;
                        string[] qrdata = { "abra", "cad", "abra" };
                        foreach (var word in words)
                        {

                            if ((counter % 2) == 1)
                            {
                                // DisplayAlert(counter.ToString(), word, "OK");

                                qrdata[counter2] = word;
                                counter2 = counter2 + 1;
                            }
                            counter = counter + 1;

                        }
                        List<String> possibleLocations = new List<string> { };
                        DBConnect co = new DBConnect();
                        if (co.OpenConnection() == true)
                        {
                            string query = "SELECT * FROM inventory  where  flItem ='" + qrdata[1] + "' AND flsolarinumb = " + qrdata[0] + " AND fldesc = '" + qrdata[2] + "'";
                            var cmd = new MySql.Data.MySqlClient.MySqlCommand(query, co.connection);
                            var reader = cmd.ExecuteReader();

                            while (reader.Read())
                            {

                                possibleLocations.Add("" + reader["fllocation"]);



                            }


                            co.connection.Close();

                        }
                        
                        var s = await DisplayActionSheet("Select the current location of the item ", "Abort the transfer", "x", possibleLocations.ToArray());
                        string[] destination = { "New York", "Jamaica","Warehouse", "Atlantic Terminal", "Newark","Trenton",





            "NJT NY",





            "Secaucus",





            "Grand Central Terminal",





            "The National WWII Museum",







            "Metro North",





            "Metro Park" };
                        
                        string[] agency = { "MTA"," NJ Transit"," MBTA"," Port Authority"," TRIMET"," MoMa"," Luis Vuitton"," Metro North",""," New Orleans "};
                        string [] customer = { "LIRR"," NJ Transit"," NYCTA"," MBTA"," PATH"," TRIMET"," Massachusetts Bay Transportation Authority"," MoMa"," Luis Vuitton North America"," Metro North Railroad"," Solari Corp"," The National WWII Museum"," Port Authority" };
                        string[] location = { " NY Penn"," Jamaica"," Warehouse"," Atlantic Terminal"," Newark"," Trenton"," NJT NY"," Secaucus"," GCT"," The National WWII Museum"," Grand Central"," Metro North" };
                        var age = await DisplayActionSheet("Select the agency the item is being given to ", "Abort the transfer", "x", agency);
                        if (age == "Abort the transfer") { await Navigation.PushAsync(new deit()); }
                        else {
                            var cus = await DisplayActionSheet("Select the customer the item is being given to ", "Abort the transfer", "x", customer);
                            if (cus == "Abort the transfer") { await Navigation.PushAsync(new deit()); }
                            else
                            {
                                var loc = await DisplayActionSheet("Select the destination ", "Abort the transfer", "x", location);
                                if (loc == "Abort the transfer") { await Navigation.PushAsync(new deit()); return; }
                                var rec = await DisplayPromptAsync("Required info", "Name of the recipient?","OK","Cancel");
                                var isnull = string.IsNullOrEmpty(rec);
                                if (isnull) { await Navigation.PushAsync(new deit()); return; }
                                var objective = await DisplayPromptAsync("Required info", "Objective of the transfer?", "OK", "Cancel");
                                isnull = string.IsNullOrEmpty(objective);
                                if (isnull) { await Navigation.PushAsync(new deit()); return; }
                                var emp = await DisplayPromptAsync("Required info", "Type your name?", "OK", "Cancel");
                                isnull = string.IsNullOrEmpty(emp);
                                if (isnull) { await Navigation.PushAsync(new deit()); return; }
                                DateTime today = DateTime.Today;
                                String date = today.ToString("yyyy-MM-dd");
                                await DisplayAlert("Today's Date", today.ToString(), "OK");
                                 co = new DBConnect();
                                if (co.OpenConnection() == true)
                                {
                                    string query = "Update inventory Set flquantity = flquantity-1 where fllocation = '" + s + "' AND flItem ='" + qrdata[1] + "' AND flsolarinumb = " + qrdata[0] + " AND fldesc = '" + qrdata[2] + "'";
                                    var cmd = new MySql.Data.MySqlClient.MySqlCommand(query, co.connection);
                                    //await DisplayAlert("Scanned Infos", cmd.CommandText, "OK");
                                    cmd.ExecuteNonQuery();

                                    //query = "Update inventory Set flquantity = flquantity+1  where fllocation = '" + dest + "' AND flItem ='" + qrdata[1] + "' AND flsolarinumb = " + qrdata[0] + " AND fldesc = '" + qrdata[2] + "'";
                                    query = " INSERT INTO CustomerInventory (rec,ag,loc,it,itdes,itnum,emp,date) VALUES('" + rec + "', '" + age + "', '" + loc + "', '" + qrdata[1] + "', '" + qrdata[2] + "', '" + qrdata[0] + "', '" + emp + "', '" + date + "')";
                                    cmd = new MySql.Data.MySqlClient.MySqlCommand(query, co.connection);
                                    //await DisplayAlert("Scanned Infos", cmd.CommandText, "OK");
                                    cmd.ExecuteNonQuery();
                                    //string[] values = { "fllocation", "flagency", "flcond", "flsolarinumb", "flmodel", "fldesc" };

                                    query = "Select flquantity from inventory  where fllocation = '" + s + "' AND  flItem ='" + qrdata[1] + "' AND flsolarinumb = " + qrdata[0] + " AND fldesc = '" + qrdata[2] + "'";
                                    cmd = new MySql.Data.MySqlClient.MySqlCommand(query, co.connection);
                                    var reader = cmd.ExecuteReader();
                                    var quan = "";

                                    ;
                                    var c = 0;
                                    while (reader.Read())
                                    {
                                        quan = "" + reader["flquantity"];
                                        //int y = Int32.Parse(quan);
                                        //if (y >x)
                                        c++;
                                    }

                                    int x = Int32.Parse(quan);
                                    await DisplayAlert(c + "  Quantity of  " + qrdata[1] + " at " + s, quan, "OK");

                                    var reci = new List<string>();
                                    reci.Add("mhosain@solaricorp.com");
                                    if (x < 5)
                                    {

                                        await email.SendEmail("test", qrdata[1] + " is in low quantity" + " at " + s, reci);
                                    }

                                    co.connection.Close();

                                }
                            }
                        }
                    });

                };

            }
            catch (Exception ex)
            {
                //GlobalScript.SeptemberDebugMessages("ERROR", "BtnScanQR_Clicked", "Opening ZXing Failed: " + ex);
            }
        }

    }
}