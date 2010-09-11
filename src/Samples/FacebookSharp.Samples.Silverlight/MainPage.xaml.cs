using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using FacebookSharp;
using FacebookSharp.Schemas.Graph;

namespace FacebookSharp.Samples.Silverlight
{
    public partial class MainPage : UserControl
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void WriteLine(string str)
        {
            txtInfo.Dispatcher.BeginInvoke(
                () =>
                {
                    txtInfo.Text += str + "\n";
                });
        }

        private void btnGetMyInfo_Click(object sender, RoutedEventArgs e)
        {
            var fb = new Facebook(txtAccessToken.Text);

            // using async method, silverlight supports only async methods
            fb.GetAsync("/me",
                result =>
                {
                    // incase you are using async,
                    // always check if it was successful.
                    if (result.IsSuccessful)
                    {
                        // this prints out the raw json, // make sure u write on the appropriate thread
                        WriteLine(result.RawResponse + "\n");

                        // this mite be preferable - the generic version of the result
                        var user = result.GetResponseAs<User>();
                        WriteLine("Hi " + user.Name);
                    }
                    else
                    {
                        // exception is stored in result.Exception
                        // u can extract the message using result.Exception.Message
                        // or u can get raw facebook json exception using result.Response.
                        WriteLine("Error: " + "\n" + result.Exception.Message + "\n");
                    }
                });

            //// example for posting on the wall:
            //fb.PostAsync("/me/feed",
            //             new Dictionary<string, string>
            //                 {
            //                     { "message", "testing form Silverlight FacebookSharp" }
            //                 },
            //             result =>
            //             {
            //                 if (result.IsSuccessful)
            //                 {
            //                     WriteLine("New post id" + result.RawResponse);
            //                 }
            //                 else
            //                 {
            //                     WriteLine("Error: " + "\n" + result.Exception.Message + "\n");
            //                 }
            //             });

            //// example for deleting
            //fb.DeleteAsync("/id",
            //               result =>
            //               {
            //                   if (result.IsSuccessful)
            //                   {
            //                       WriteLine(result.RawResponse);
            //                   }
            //                   else
            //                   {
            //                       WriteLine("Error: \n" + result.Exception.Message);
            //                   }
            //               });
        }

    }
}
