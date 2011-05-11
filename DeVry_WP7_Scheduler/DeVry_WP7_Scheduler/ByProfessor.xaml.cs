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
    public partial class ByProfessor : PhoneApplicationPage
    {
        public ByProfessor()
        {
            InitializeComponent();
        }

        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {
            //go get the JSON for professor
            //http://206.209.106.106/academics/registration/practice_schedule_mobile/getprofs.asp?dept=&term=" + Globals.currSession + "&tod=&course=undefined&day=&session=
            WebClient wc = new WebClient();
            wc.DownloadStringCompleted +=new DownloadStringCompletedEventHandler(wc_DownloadStringCompleted);
                      //wc.DownloadStringAsync(new Uri("http://hub3r.com/profs.txt"));
            try
            {

                wc.DownloadStringAsync(new Uri("http://206.209.106.106/academics/registration/practice_schedule_mobile/getprofs2.asp"));
            }
            catch (Exception)
            {
                MessageBox.Show(@"It seems like you are either not connected to the Internet or the server is down. Can you manually browse to http://phx.devry.edu?");
            }

            }

        void  wc_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e) {

            //I am just doing this since I already have an app that is using the "poorly" formatted JSON
      //      string result = e.Result.Replace(@"prof"":[",@"ProfName"":").Replace("]","");
        //    result = "[" + result + "]";
          //  result = result.Replace(",\n", @"},{""ProfName"": ");
            //List< test > myDeserializedObjList = (List< test >)Newtonsoft.Json.JsonConvert.DeserializeObject(Request["jsonString"], typeof(List< test >));
            List<Professor> deserializedJSON = (List<Professor>)Newtonsoft.Json.JsonConvert.DeserializeObject(e.Result, typeof(List<Professor>));
            listBox1.ItemsSource = deserializedJSON;

//            listBox1.ItemsSource = o;

            //Professor profs = ReadToObject(e.Result);
            
         }
      
        public class Professor
        {
            public string ProfName { get; set; }
        }

        private void listBox1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
            if (listBox1.SelectedIndex != -1)
            {
                Professor selectedProf = (Professor)listBox1.Items[listBox1.SelectedIndex];

                //TextBlock tb = (TextBlock)sender;

                NavigationService.Navigate(new Uri("/ByCourse.xaml?professor=" + selectedProf.ProfName, UriKind.Relative));
                
            }
        }

        private void TextBlock_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show(sender.ToString());
        }
    }
}