using System;
using Windows.Data.Xml.Dom;
using Windows.UI.Notifications;

namespace FTEReader.Tile
{
    public class Tile
    {
        public static string GetAttriByName(IXmlNode nodes, string name)
        {
            foreach (var attri in nodes.Attributes)
            {
                if (attri.NodeName.Equals(name))
                {
                    return attri.InnerText;
                }
            }
            return null;
        }

        public static void SetAttriByName(IXmlNode nodes, string name, string text)
        {
            foreach (var attri in nodes.Attributes)
            {
                if (attri.NodeName.Equals(name))
                {
                    attri.NodeValue = text;
                    break;
                }
            }
        }
        public static void AddTile(string bookname, string des)
        {
            // 加载xml文档
            XmlDocument document = new XmlDocument();
            document.LoadXml(System.IO.File.ReadAllText("Tile/Tile.xml"));
            XmlNodeList textElements = document.GetElementsByTagName("text");
            foreach (var text in textElements)
            {
                // 替换里面预设的字符串
                if (text.InnerText.Equals("bookname"))
                {
                    text.InnerText = bookname;
                }
                else if (text.InnerText.Equals("currentPage"))
                {
                    text.InnerText = des;
                }
            }
            // Then create the tile notification
            var notification = new TileNotification(document);
            TileUpdateManager.CreateTileUpdaterForApplication().Update(notification);
            TileUpdateManager.CreateTileUpdaterForApplication().EnableNotificationQueue(true);
        }
    }
}
