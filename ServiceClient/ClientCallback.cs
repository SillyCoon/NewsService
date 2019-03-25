using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Client.ServiceReference1;

namespace Client
{
    class ClientCallback: INewsServiceCallback
    {
        public void SendNews(Item[] news)
        {
            foreach (var article in news)
            {
                Console.WriteLine(article.Title);
                Console.WriteLine(article.Link);
                Console.WriteLine(article.Date);
                Console.WriteLine("**************************************\n");
            }
        }
    }
}
