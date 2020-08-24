using System;
using Xamarin.Forms;
using Xamarin.Essentials;

namespace t
{
	public partial class LoginPage : ContentPage
	{
		public LoginPage ()
		{
			
			InitializeComponent ();
			
			




		}

		async void OnSignUpButtonClicked (object sender, EventArgs e)
		{
			await Navigation.PushAsync (new SignUpPage ());
		}

		async void OnLoginButtonClicked (object sender, EventArgs e)
		{
			var user = new User {
				Username = usernameEntry.Text,
				Password = passwordEntry.Text
			};

			var isValid = AreCredentialsCorrect (user);
			if (isValid) {
				App.IsUserLoggedIn = true;
				Navigation.InsertPageBefore (new MainPage (), this);
				await Navigation.PopAsync ();
			} else {
				messageLabel.Text = "Login failed";
				passwordEntry.Text = string.Empty;
			}
		}

		bool AreCredentialsCorrect (User user)
		{
			DBConnect co = new DBConnect();
			co.connection.Open();
			string query = "SELECT password FROM users where username = '" + user.Username+"'";
			var cmd = new MySql.Data.MySqlClient.MySqlCommand(query, co.connection);
			var reader = cmd.ExecuteReader();
			var p="";
			while (reader.Read())
			{
				p = ""+reader["password"];
			}
			//return user.Username == Constants.Username && user.Password == Constants.Password;
			return p == user.Password;
		}
		public   async void  gloc() {
			try
			{
				var request = new GeolocationRequest(GeolocationAccuracy.Lowest);
				
				var   location =  await Geolocation.GetLocationAsync(request);

				if (location != null)
				{
					gloca.Text = $"Latitude: {location.Latitude}, Longitude: {location.Longitude}, Altitude: {location.Altitude}";
					 //Console.WriteLine($"Latitude: {location.Latitude}");
					Console.WriteLine("jchjjhfkhfkh");
				}
			}
			catch (FeatureNotSupportedException fnsEx)
			{
				// Handle not supported on device exception
			}
			catch (FeatureNotEnabledException fneEx)
			{
				// Handle not enabled on device exception
			}
			catch (PermissionException pEx)
			{
				// Handle permission exception
			}
			catch (Exception ex)
			{
				// Unable to get location
			}
		} 
	}
}
