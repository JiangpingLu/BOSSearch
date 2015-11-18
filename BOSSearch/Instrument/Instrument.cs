﻿//-------------------------------------------------------------------------------
// <Copyright file="Instrument.cs" company="PwC">
// © 2014 PwC. All rights reserved.
// </Copyright>
// "PwC" refers to PricewaterhouseCoopers LLP, a Delaware limited liability 
// partnership, which is a member firm of PricewaterhouseCoopers International 
// Limited, each member firm of which is a separate legal entity.
// ---------------------------------------------------------------------------------
//	File Description	: It's the public functions for all API
// ---------------------------------------------------------------------------------
//	Date Created		: Nov 05, 2015
//	Author			    : <Haley Qu>, SDC Shanghai
// ---------------------------------------------------------------------------------
// 	Change History
//          Add description
//	Date Modified		: Nov 17, 2015
//	Changed By		    : AJ
//	Change Description  : Add header description
//  Issue number        : 1.0
/////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using System.Xml;
using System.IO;
using System.Xml.Linq; 

namespace BOSSearch.Function
{
    public class Instrument
    {
        public string GetElementValue(XElement element)
        {
            if (null != element)
            {
                return element.Value;
            }

            return string.Empty;
        }
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
        /// XML转Json
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        public string Xml2Json(string xml)
        {
            if (xml == null || xml == "")
            {
                return "no value";
            }
            else
            {
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(xml);
                string json = Newtonsoft.Json.JsonConvert.SerializeXmlNode(doc);
                return json;
            }
        }

        /// <summary>
        /// 根据XML文件内容获取XmlDocument对象
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
        /// 根据XML文件路径获取XmlDocument对象
        /// </summary>
        /// <param name="xmlFilePath"></param>
        /// <returns></returns>
        public static XmlDocument GetXmlDocByFilePath(string xmlFilePath)
        {
            if (string.IsNullOrEmpty(xmlFilePath) || !File.Exists(xmlFilePath))
            {
                return null;
            }

            var xDoc = new XmlDocument();
            try
            {
                xDoc.Load(xmlFilePath);
            }
            catch
            {
                throw new Exception(string.Format("请确认该XML文件格式，路径为：{0}", xmlFilePath));
            }

            return xDoc;
        }

        /// <summary>
        /// 获取指定节点值
        /// </summary>
        /// <param name="doc"></param>
        /// <param name="nodeName">节点名</param>
        /// <param name="nodeAttribute">属性名</param>
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