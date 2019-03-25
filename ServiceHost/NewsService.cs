using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using RssCommunicator;

namespace Host
{
    [ServiceContract(SessionMode = SessionMode.Required,
        CallbackContract = typeof(IClientCallback))]
    public interface INewsService
    {
        [OperationContract(IsOneWay = true)]
        void Register(string name, string tag);
    }

    public interface IClientCallback
    {
        [OperationContract(IsOneWay = true)]
        void SendNews(Item[] news);
    }

    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession)]
    public class NewsService : INewsService
    {
        private double result;
        private IClientCallback callback = null;

        NewsService()
        {
            result = 0.0D;
            callback = OperationContext.Current.GetCallbackChannel<IClientCallback>();
        }

        public void Register(string name, string tag)
        {
            var startTimeSpan = TimeSpan.Zero;
            var periodTimeSpan = TimeSpan.FromSeconds(10);

            RssCommunicator.RssCommunicator news = new RssCommunicator.RssCommunicator();
            Console.WriteLine(name + " subsribes with tag " + tag);

            var timer = new System.Threading.Timer((e) =>
            {
                var updates = news.GetNews(tag);
                callback.SendNews(updates.ToArray());

            }, null, startTimeSpan, periodTimeSpan);
            
        }
    }
}
