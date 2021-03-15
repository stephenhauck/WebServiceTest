using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Windows.Input;
using WebServiceTest.Models;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace WebServiceTest.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        public AboutViewModel()
        {
            Title = "About";
            OpenWebCommand = new Command(async () =>
            {
                try
                {

                    var client = new HttpClient();
                    //Authorization
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", "SOMEBASE64encodeduserandpassword");
                    var requestAccept = client.DefaultRequestHeaders.Accept;
                    //Standard header
                    client.DefaultRequestHeaders.Add("Accept", "application/json");
                    //Assemble the URL to call 
                    string urlPath = $"http://10.0.2.2:5000/api/Item";

                    //Encode the POST payload 
                    var keystylePostContent = new StringContent(JsonConvert.SerializeObject(new Item()), Encoding.UTF8, "application/json");

                   
                    var response = await client.PostAsync(urlPath, keystylePostContent);

                    if (response.IsSuccessStatusCode)
                    {
                        Debug.WriteLine("Success");
                    }
                    else
                    {
                        Debug.WriteLine("Failure");
                    }


                }
                catch (Exception ex)
                {
                    if (Debugger.IsAttached)
                    {
                        Debugger.Break();
                    }
                    Debug.WriteLine(ex.Message);
                }
            });
        }

        public ICommand OpenWebCommand { get; }
    }
}