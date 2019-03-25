using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;

namespace RssCommunicator
{
    public class Item
    {
        public string Title { get; set; }
        public string Link { get; set; }
        public string Date { get; set; }
    }

    public class RssCommunicator
    {
        public HashSet<Item> Items { get; private set; }
        private readonly string _uri = "https://news.google.com/rss?hl=en-US&gl=US&ceid=US:en";
        private WebRequest _request;

        public RssCommunicator()
        {
            _request = WebRequest.Create(_uri);
            Items = new HashSet<Item>();
        }

        public IEnumerable<Item> GetNews(string tag)
        {
            HashSet<Item> items = new HashSet<Item>();

            var data = _request.GetResponse().GetResponseStream();
            using (XmlReader reader = XmlReader.Create(data ?? throw new InvalidOperationException()))
            {
                while (reader.Read())
                {
                    if (reader.NodeType == XmlNodeType.Element && reader.Name == "item")
                    {
                        var item = new Item();
                        reader.Read();
                        item.Title = reader.ReadElementContentAsString();
                        item.Link = reader.ReadElementContentAsString();
                        reader.ReadToNextSibling("pubDate");
                        item.Date = reader.ReadElementContentAsString();
                        string description = reader.ReadElementContentAsString();
                        if (description.Contains(tag)) items.Add(item);
                    }
                }
                data.Dispose();
                return items.Except(Items).ToList();
            }
        }
    }
}
