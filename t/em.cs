using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace t
{
    public class em
    {
        public async Task SendEmail(string subject, string body, List<string> recipients)
        {
            try
            {
                var message = new EmailMessage
                {
                    Subject = subject,
                    Body = body,
                    To = recipients,
                    //Cc = ccRecipients,
                    //Bcc = bccRecipients
                };
                await Email.ComposeAsync(message);
            }
            catch (FeatureNotSupportedException fbsEx)
            {
                // Email is not supported on this device
            }
            catch (Exception ex)
            {
                // Some other exception occurred
            }
        }
        public void photoLoad(int n)
        {
            

            DBConnect co = new DBConnect();
            if (co.OpenConnection() == true)
            {
                string query = "SELECT * FROM inventory  where id  <" + (Constants.ItemNumberForDisplay + 1);
                var cmd = new MySql.Data.MySqlClient.MySqlCommand(query, co.connection);
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {

                }
            }

                //try
                //{
                //    string query = "SELECT * FROM inventory  where id  <" + (Constants.ItemNumberForDisplay + 1);
                //    var cmd = new MySql.Data.MySqlClient.MySqlCommand(query, co.connection);
                //    var reader = cmd.ExecuteReader();

                //    while (reader.Read())
                //    {
                //        ImageByte = (Byte[])(row["image"]);
                //    }

                //    if (ImageByte != null)
                //    {
                //        // You need to convert it in bitmap to display the image
                //        pictureBox1.Image = ByteToImage(ImageByte);
                //        pictureBox1.Refresh();
                //    }
                //}
                //catch (Exception ex)
                //{
                //    MessageBox.Show(ex.Message);
                //}
            }
    }
}
