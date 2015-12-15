using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace BOSMDMProdConsoleApp.Instruments
{
    public class Instrument
    {
        /// <summary>
        /// Get Element value by XElement
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public string GetElementValue(XElement element)
        {
            if (null != element)
            {
                return element.Value;
            }

            return string.Empty;
        }

        /// <summary>
        /// convert special characters to ESC
        /// </summary>
        /// <param name="rawstr"></param>
        /// <returns></returns>
        public string HandleSpecialCharacters(string rawstr)
        {
            if (string.IsNullOrEmpty(rawstr))
            {
                return string.Empty;
            }

            string output = rawstr.Replace("&amp;", "&").Replace("&lt;", "<").Replace("&gt;", ">").Replace("&quot;", "\"").Replace("&apos;", "'");
            output = output.Replace("&", "&amp;").Replace("<", "&lt;").Replace(">", "&gt;").Replace("\"", "&quot;").Replace("'", "&apos;");

            return output;
        }

        /// <summary>
        /// Get XmlDocument object from xml file
        /// </summary>
        /// <param name="xmlFileContent"></param>
        /// <returns></returns>
        public XmlDocument GetXmlDocByXmlContent(string xmlFileContent)
        {
            if (string.IsNullOrEmpty(xmlFileContent))
            {
                return null;
            }

            var xDoc = new XmlDocument();
            try
            {
                xDoc.LoadXml(xmlFileContent);
            }
            catch
            {
                xDoc = null;
            }

            return xDoc;
        }

        /// <summary>
        /// Get node value by nodename
        /// </summary>
        /// <param name="doc"></param>
        /// <param name="nodeName"></param>
        /// <param name="nodeAttribute"></param>
        /// <returns></returns>
        public string GetNodeValue(XmlDocument doc, string nodeName, params string[] nodeAttribute)
        {
            using (XmlNodeReader nodeReader = new XmlNodeReader(doc))
            {
                nodeReader.MoveToContent();
                XDocument xmlDoc = XDocument.Load(nodeReader);
                var result = from c in xmlDoc.Descendants(nodeName) select c;
                string res = "";
                if (nodeAttribute.Length == 0)
                {
                    foreach (var item in result)
                    {
                        res = item.Value.ToString();
                    }
                }
                else
                {
                    foreach (var item in result)
                    {
                        res = item.Attribute(nodeAttribute[0]).Value.ToString();
                    }
                }
                return res;
            }

            //XDocument xmlDoc = XDocument.Load(path);


            //XmlNode node = doc.SelectSingleNode("/getPartyDetailResponse/getPartyDetailResult/serviceStatus/overallStatus");
            //return node.InnerText;
        }
    }
}
