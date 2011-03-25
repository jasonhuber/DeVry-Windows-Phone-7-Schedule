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
    public partial class CourseDetail : PhoneApplicationPage
    {
        public CourseDetail()
        {
            InitializeComponent();
        }

        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {
            String courseid = "";
            
            bool itemExists = NavigationContext.QueryString.TryGetValue("courseid", out courseid);
            if (itemExists)
            {
                PageTitle.Text = courseid;

            //    WebClient wc = new WebClient();
            //    wc.DownloadStringCompleted +=new DownloadStringCompletedEventHandler(wc_DownloadStringCompleted);
            //    wc.DownloadStringAsync(new Uri("http://206.209.106.106/academics/registration/practice_schedule_mobile/getclasses.asp?term=SPR2011&courseid=" + courseid));
            //
                webBrowser1.Source = new Uri("http://206.209.106.106/academics/registration/practice_schedule_mobile/getclass.asp?term=SPR2011&courseid=" + courseid);
                
            }

        }

    }
}