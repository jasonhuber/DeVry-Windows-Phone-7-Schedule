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
using Microsoft.Phone.Controls;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json; //needed to add a reference to System.ServiceModel.Web for this.
using System.Text;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using Microsoft.Phone.Tasks;

namespace DeVry_WP7_Scheduler
{
    public partial class ByCourse : PhoneApplicationPage
    {
        public ByCourse()
        {
            InitializeComponent();
        }
        String professor = "";

        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {
            
            bool itemExists = NavigationContext.QueryString.TryGetValue("professor", out professor);
            if (itemExists)
            {
                PageTitle.Text = professor;

                WebClient wc = new WebClient();
                wc.DownloadStringCompleted +=new DownloadStringCompletedEventHandler(wc_DownloadStringCompleted);
                try
                {

                    wc.DownloadStringAsync(new Uri("http://206.209.106.106/academics/registration/practice_schedule_mobile/getclasses2.asp?term=" + Globals.currSession + "&prof=" + professor));
                }
                catch (Exception)
                {
                    MessageBox.Show(@"It seems like you are either not connected to the Internet or the server is down. Can you manually browse to http://phx.devry.edu?");
                }
                }

        }

        void  wc_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e) {

            //I am just doing this since I already have an app that is using the "poorly" formatted JSON
        //    string result = e.Result.Replace(@"cls"":[", @"ClassName"":").Replace("]", "");
          //  result = "[" + result + "]";
           // result = result.Replace(",\n", @"},{""ClassName"": ");
            //List< test > myDeserializedObjList = (List< test >)Newtonsoft.Json.JsonConvert.DeserializeObject(Request["jsonString"], typeof(List< test >));
            List<Cls> deserializedJSON = (List<Cls>)Newtonsoft.Json.JsonConvert.DeserializeObject(e.Result, typeof(List<Cls>));
            listBox1.ItemsSource = deserializedJSON;

//            listBox1.ItemsSource = o;

            //Professor profs = ReadToObject(e.Result);
            
         }
      
        public class Cls
        {
            public string ClassName { get; set; }
            public string Title { get; set; }
            public string Day { get; set; }
            public string Time { get; set; }
            public string Room { get; set; }

        }

        
        private void listBox1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listBox1.SelectedIndex != -1)
            {

                Cls cls = (Cls)listBox1.Items[listBox1.SelectedIndex];

                NavigationService.Navigate(new Uri("/CourseDetail.xaml?courseid=" + cls.ClassName, UriKind.Relative));
            }
        }

        private void TextBlock_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            int commaloc = professor.IndexOf(",");
            string lastname = professor.Substring(0, commaloc);
            string firstinitial = professor.Substring(commaloc + 2, 1);
            string email = firstinitial + lastname + "@devry.edu";
            EmailComposeTask emailComposeTask = new EmailComposeTask();
            emailComposeTask.To = email;
            emailComposeTask.Body = "\n\n This message was initialized from the Windows Phone 7 Scheduler App";
//            emailComposeTask.Cc = "user2@example.com";
            emailComposeTask.Subject = "";
            emailComposeTask.Show();
        }

        private void TextBlock_MouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
        {
            PhoneCallTask phoneCallTask = new PhoneCallTask();
            phoneCallTask.PhoneNumber = "602 870 9222";
            phoneCallTask.DisplayName = professor;
            phoneCallTask.Show();
        }
    }
}