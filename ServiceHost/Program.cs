using System;
using System.ServiceModel;
using System.ServiceModel.Description;

namespace Host
{
   
    class Program
    {
        static void Main(string[] args)
        {
            RssCommunicator.RssCommunicator news = new RssCommunicator.RssCommunicator();

            var test = news.GetNews("Trump");

            string uri = "http://localhost:8080/NewsService";
            ServiceHost host = new ServiceHost(typeof(NewsService),
                new Uri(uri));

            ////host.AddServiceEndpoint(typeof(INewsService),
            //    new BasicHttpBinding(),
            //    "");

            //ServiceMetadataBehavior behavior = new ServiceMetadataBehavior();
            //behavior.HttpGetEnabled = true;
            //host.Description.Behaviors.Add(behavior);
            //host.AddServiceEndpoint(typeof(IMetadataExchange),
            //    MetadataExchangeBindings.CreateMexHttpBinding(), "mex");
            host.Open();

            Console.WriteLine("Host was started at " + uri);

            // Waiting for interrupt
            Console.ReadKey();

            //host.Close();
        }
    }
}
