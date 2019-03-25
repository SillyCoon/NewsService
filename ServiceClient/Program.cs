using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ServiceModel;
using System.ServiceModel.Discovery;


namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            InstanceContext instanceContext = new InstanceContext(new ClientCallback());
            var client = new ServiceReference1.NewsServiceClient(instanceContext);
            client.Register("SillyCoon", "Trump");
            Console.ReadKey();
        }
    }
}
