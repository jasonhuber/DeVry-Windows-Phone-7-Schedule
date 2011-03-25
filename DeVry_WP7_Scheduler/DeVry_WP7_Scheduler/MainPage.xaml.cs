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
using Microsoft.Phone.Tasks;

namespace DeVry_WP7_Scheduler
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();
        }

        private void txtlnkByProfessor_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            NavigationService.Navigate(new Uri("/ByProfessor.xaml", UriKind.Relative));
        }

        private void txtlnkShowAllClasses_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //http://206.209.106.106/academics/registration/practice%5Fschedule%5Fmobile/getdepartments.asp?term=SPR2011&tod=&day=&session=
            NavigationService.Navigate(new Uri("/ByDepartment.xaml", UriKind.Relative));
        }

        private void txtCallYourCampus_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("Not implemented yet, but I will let you call the Phoenix Campus...");
            PhoneCallTask phoneCallTask = new PhoneCallTask();
            phoneCallTask.PhoneNumber = "602 870 9222";
            phoneCallTask.DisplayName = "Phoenix Campus";
            phoneCallTask.Show();

        }

        private void txtYourCampusHomepage_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            WebBrowserTask webBrowserTask = new WebBrowserTask();
            webBrowserTask.URL = "http://phx.devry.edu";
            webBrowserTask.Show();
        }

        private void txtUnderGradCatalogue_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            WebBrowserTask webBrowserTask = new WebBrowserTask();
            webBrowserTask.URL = "http://www.devry.edu/uscatalog/";
            webBrowserTask.Show();

        }
    }
}