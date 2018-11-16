using System.IO;
using System.Xml;

namespace ZNC.DataAccess.DA
{
    public class OperXml
    {
        private string strFile;
        private XmlDocument doc;

        public OperXml(string xmlFile)
        {
            strFile = xmlFile;
        }
        //加载Xml
        private void LoadXml()
        {
            doc = new XmlDocument();
            doc.Load(strFile);
        }
        public string ReadNoteValue(string key)
        {
            LoadXml();
            XmlNodeList nodeList = doc.GetElementsByTagName(key);
            if (nodeList.Count == 0)
            {
                return "notFound";
            }
            else
            {
                XmlNode mNode = nodeList[0];
                return mNode.InnerText;
            }

        }
        public void AddNoteValue(string nkey, string mValue)
        {
            if (ReadNoteValue(nkey) == "notFound")
            {
                LoadXml();
                XmlNodeList nodeList = doc.GetElementsByTagName("RegRoot");
                XmlNode mNode = nodeList[0];
                XmlElement nElement = doc.CreateElement(nkey);
                nElement.InnerText = mValue;
                mNode.AppendChild(nElement);
                WriteXml();
            }

        }
        public void UpdateNoteValue(string nkey, string mValue)
        {
            if (ReadNoteValue(nkey) != "notFound")
            {
                LoadXml();
                XmlNodeList nodeList = doc.GetElementsByTagName(nkey);
                XmlNode mNode = nodeList[0];
                mNode.InnerText = mValue;
                WriteXml();
            }

        }
        public void DeleteNoteValue(string nKey)
        {
            if (ReadNoteValue(nKey) != "notFound")
            {
                LoadXml();
                XmlNodeList mParentnodeList = doc.GetElementsByTagName("RegRoot");
                XmlNode mParentNode = mParentnodeList[0];
                XmlNodeList nodeList = doc.GetElementsByTagName(nKey);
                XmlNode node = nodeList[0];
                mParentNode.RemoveChild(node);
                WriteXml();
            }

        }
        //写内容到Xml
        private void WriteXml()
        {
            XmlTextWriter xw = new XmlTextWriter(new StreamWriter(strFile));
            xw.Formatting = Formatting.Indented;
            doc.WriteTo(xw);
            xw.Close();
        }
    }
}
