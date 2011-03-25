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

namespace DeVry_WP7_Scheduler
{
    public partial class ByCourse : PhoneApplicationPage
    {
        public ByCourse()
        {
            InitializeComponent();
        }

        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {
            String professor = "";
            
            bool itemExists = NavigationContext.QueryString.TryGetValue("professor", out professor);
            if (itemExists)
            {
                PageTitle.Text = professor;

                WebClient wc = new WebClient();
                wc.DownloadStringCompleted +=new DownloadStringCompletedEventHandler(wc_DownloadStringCompleted);
                wc.DownloadStringAsync(new Uri("http://206.209.106.106/academics/registration/practice_schedule_mobile/getclasses.asp?term=SPR2011&prof=" + professor));
            }

        }

        void  wc_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e) {

            //I am just doing this since I already have an app that is using the "poorly" formatted JSON
            string result = e.Result.Replace(@"cls"":[", @"ClassName"":").Replace("]", "");
            result = "[" + result + "]";
            result = result.Replace(",\n", @"},{""ClassName"": ");
            //List< test > myDeserializedObjList = (List< test >)Newtonsoft.Json.JsonConvert.DeserializeObject(Request["jsonString"], typeof(List< test >));
            List<Cls> deserializedJSON = (List<Cls>)Newtonsoft.Json.JsonConvert.DeserializeObject(result, typeof(List<Cls>));
            listBox1.ItemsSource = deserializedJSON;

//            listBox1.ItemsSource = o;

            //Professor profs = ReadToObject(e.Result);
            
         }
      
        public class Cls
        {
            public string ClassName { get; set; }
        }

        
        private void listBox1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Cls cls = (Cls)listBox1.Items[listBox1.SelectedIndex];

           // MessageBox.Show();
            //TextBlock tb = (TextBlock)sender;

            NavigationService.Navigate(new Uri("/CourseDetail.xaml?courseid=" + cls.ClassName, UriKind.Relative));
        }
    }
}