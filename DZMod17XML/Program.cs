using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace DZMod17XML
{
    [Serializable]
    public class docum : chDocum
    {
        public List<chDocum> chItems = new List<chDocum>();

        public docum(string Title, string Link, string Description, DateTime PubDate)
        {
            this.Title = Title;
            this.Link = Link;
            this.Description = Description;
            this.PubDate = PubDate;

        }

        public docum()
        {
        }
    }
    [Serializable]
    public class chDocum
    {
        public string Title;
        public string Link;
        public string Description;
        public DateTime PubDate;

        public chDocum(string Title, string Link, string Description, DateTime PubDate)
        {
            this.Title = Title;
            this.Link = Link;
            this.Description = Description;
            this.PubDate = PubDate;
        }

        public chDocum()
        {
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            List<docum> doc = new List<docum>();

            XmlDocument xDoc = new XmlDocument();
            xDoc.Load("https://habrahabr.ru/rss/interesting/");
            XmlElement xRoot = xDoc.DocumentElement;
            #region
            foreach (XmlElement xnode in xRoot)
            {
                docum d = new docum();
                foreach (XmlNode childnode in xnode.ChildNodes)
                {

                    if (childnode.Name == "title")
                    {
                        d.Title = childnode.InnerText;
                    }
                    if (childnode.Name == "link")
                    {
                        d.Link = childnode.InnerText;
                    }
                    if (childnode.Name == "description")
                    {
                        d.Description = childnode.InnerText;
                    }
                    if (childnode.Name == "pubDate")
                    {
                        d.PubDate = DateTime.Parse(childnode.InnerText);
                    }


                    if (childnode.Name == "item")
                    {
                        chDocum d1 = new chDocum();
                        foreach (XmlElement item in childnode.ChildNodes)
                        {
                            if (item.Name == "title")
                            {
                                d1.Title = item.InnerText;
                            }
                            if (item.Name == "link")
                            {
                                d1.Link = item.InnerText;
                            }
                            if (item.Name == "description")
                            {
                                d1.Description = item.InnerText;
                            }
                            if (item.Name == "pubDate")
                            {
                                d1.PubDate = DateTime.Parse(item.InnerText);
                            }
                        }
                        d.chItems.Add(d1);
                    }

                }
                doc.Add(d);
            }
            #endregion
          

            Stream stream = File.OpenWrite(Environment.CurrentDirectory + "\\doc.xml");
            XmlSerializer xmlSer = new XmlSerializer(typeof(List<docum>));
            xmlSer.Serialize(stream, doc);
            stream.Close();
 
        }
    }
}
