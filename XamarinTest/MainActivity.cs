using System;
using System.Net.Http;
using System.Text;
using System.Xml.Linq;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using SubsonicSharp;
using SubsonicSharp.SubTypes;

namespace XamarinTest
{
    [Activity(Label = "XamarinTest", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        int count = 1;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Get our button from the layout resource,
            // and attach an event to it
            Button button = FindViewById<Button>(Resource.Id.MyButton);

            button.Click += delegate { GetPingResult();};
        }

        private void GetPingResult()
        {
            UserToken user = new UserToken("test", "test", true);
            ServerInfo server = new ServerInfo("192.168.1.140");
            SubsonicClient client = new SubsonicClient(user, server);
            //RestCommand ping = new RestCommand { MethodName = "ping" };
            //string command = client.FormatCommand(ping);
            //HttpClient htp = new HttpClient();
            //string response = htp.GetStringAsync(command).Result;

            //XDocument document = client.GetResponseXDocument(ping);
            StringBuilder sb = new StringBuilder();
            foreach (Genre genre in client.Browsing.GetGenres())
            {
                sb.Append(genre.Name).Append("\r\n");
            }
            TextView results = FindViewById<TextView>(Resource.Id.results);
            results.Text = sb.ToString();
        }
    }
}

