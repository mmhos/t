using System;
using System.Linq;
using Xamarin.Forms;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;

namespace t
{
	public partial class SignUpPage : ContentPage
	{
		public SignUpPage ()
		{
			InitializeComponent ();
		}
		static string ComputeSha256Hash(string rawData)
		{
			// Create a SHA256   
			using (MD5 sha256Hash = MD5.Create())
			{
				// ComputeHash - returns byte array  
				byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

				// Convert byte array to a string   
				StringBuilder builder = new StringBuilder();
				for (int i = 0; i < bytes.Length; i++)
				{
					builder.Append(bytes[i].ToString("x2"));
				}
				return builder.ToString();
			}
		}
		async void OnSignUpButtonClicked (object sender, EventArgs e)
		{
			var user = new User () {
				Username = usernameEntry.Text,
				
				Password = passwordEntry.Text,
				Email = emailEntry.Text
			};

			// Sign up logic goes here

			var signUpSucceeded = AreDetailsValid (user);

			if (signUpSucceeded) {
				try
				{

					MailMessage mail = new MailMessage();
					SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

					mail.From = new MailAddress("abracadabra123098456@gmail.com");
					mail.To.Add(user.Email);
					mail.Subject = "Email Verification";
					//					Thanks for signing up!
					//Your account has been created, you can login with the following credentials after you have activated your account by pressing the url below.


					//------------------------
					//Username: '.$name.'
					//Password: '.$password.'
					//------------------------


					//Please click this link to activate your account:
					//http://www.yourwebsite.com/verify.php?email='.$email.'&hash='.$hash.'
					var hash = ComputeSha256Hash(user.Password);

					mail.Body = "Thanks for signing up. \n" +
						"Your account has been created, you can login with the following credentials after you have activated your account by pressing the url below \n" +
						"Username" + user.Username + "\n" +
						"Password" + user.Password + "\n" +
						"Please click this link to activate your account:" + "\n" +
					"http://www.solaricorp.net/EmailVerification/ev.php?email="+ user.Email+"&name="+user.Username+"&hash="+hash;
					;
					DBConnect co = new DBConnect();
					co.connection.Open();
					string query =  "INSERT INTO users(username, password, email, hash) VALUES('" + user.Username + "', '" + user.Password + "', '" + user.Email + "', '" + hash + "')";
					var cmd = new MySql.Data.MySqlClient.MySqlCommand(query, co.connection);
					await DisplayAlert("Scanned Infos", cmd.CommandText, "OK");
					cmd.ExecuteNonQuery();
					co.connection.Close();
					SmtpServer.Port = 587;
					SmtpServer.Host = "smtp.gmail.com";
					SmtpServer.EnableSsl = true;
					SmtpServer.UseDefaultCredentials = false;
					SmtpServer.Credentials = new System.Net.NetworkCredential("abracadabra123098456@gmail.com", "agdum1Bagdum!");

					SmtpServer.Send(mail);
					
				}
				catch (Exception ex)
				{
					await DisplayAlert("Faild", ex.Message, "OK");
				}
				var rootPage = Navigation.NavigationStack.FirstOrDefault ();
				if (rootPage != null) {
					App.IsUserLoggedIn = false;
					Navigation.InsertPageBefore (new MainPage (), Navigation.NavigationStack.First ());
					await Navigation.PopToRootAsync ();
				}
			} else {
				messageLabel.Text = "Sign up failed";
			}
		}

		bool AreDetailsValid (User user)
		{
			return (!string.IsNullOrWhiteSpace (user.Username) && !string.IsNullOrWhiteSpace (user.Password) && !string.IsNullOrWhiteSpace (user.Email) && user.Email.EndsWith ("@solaricorp.com"));
		}
	}
}
