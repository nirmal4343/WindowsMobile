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
using System.Xml.Linq;
using System.Collections.ObjectModel;
using Newtonsoft.Json;
using System.IO;
using System.Text;
using Newtonsoft.Json.Linq;



namespace WindowsPhoneApplication1
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();
            SupportedOrientations = SupportedPageOrientation.Portrait | SupportedPageOrientation.Landscape;
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            WebClient twitter = new WebClient();
            
            twitter.DownloadStringCompleted +=new DownloadStringCompletedEventHandler(twitter_DownloadStringCompleted);
            twitter.DownloadStringAsync(new Uri("http://learnresfull-restcall.rhcloud.com/restaurent/"));
        }

        void twitter_DownloadStringCompleted(Object sender, DownloadStringCompletedEventArgs e)
        {
            string jsonArrayAsString = e.Result; //"[ { Name : \"Marcos\", Country : \"Brazil\" }, { Name : \"John\", Country : \"England\" }]";
            JArray jsonArray = JArray.Parse(jsonArrayAsString);
            JToken jsonArray_Item = jsonArray.First;

            List<Employee> employeeList = new List<Employee>();


            while (jsonArray_Item != null)
            {
                string name = jsonArray_Item.Value<string>("firstName");
                string country = jsonArray_Item.Value<string>("title");

                // Be careful, you take the next from the current item, not from the JArray object.

                Console.WriteLine(" ****** {0}   *******  {1}" , name,country);

                employeeList.Add(new Employee
                {
                    EmployeeName = jsonArray_Item.Value<string>("firstName"),
                    Title = jsonArray_Item.Value<string>("title"),
                    Email = jsonArray_Item.Value<string>("email"),
                    WorkPhone = jsonArray_Item.Value<string>("officePhone"),
                    EmployeeImage = jsonArray_Item.Value<string>("imageDownloadPath")
                });
                jsonArray_Item = jsonArray_Item.Next;
            }

            listBox1.ItemsSource = employeeList;

        }
    }
}