//-------------------------------------------------------------------------------
// <Copyright file="CommonAPI.cs" company="PwC">
// © 2014 PwC. All rights reserved.
// </Copyright>
// "PwC" refers to PricewaterhouseCoopers LLP, a Delaware limited liability 
// partnership, which is a member firm of PricewaterhouseCoopers International 
// Limited, each member firm of which is a separate legal entity.
// ---------------------------------------------------------------------------------
//	File Description	: It's the business controller code for the function of GetPartyDetails 
// ---------------------------------------------------------------------------------
//	Date Created		: ‎Nov ‎11, ‎2015
//	Author			    : <Jiangping Lu>, SDC Shanghai
// ---------------------------------------------------------------------------------
// 	Change History
//          Add porduct line URL and APIKey
//	Date Modified		: Nov 16, 2015
//	Changed By	        : Jiangping Lu(AJ)
//	Change Description  : Add Product line URL and change APIKey and APIKeySecret for product line test
//  Issue number        : 1.0
/////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Xml;

namespace PWC.US.USTO.BOSSearch.Common
{
    public class CommonAPI
    {
        /// <summary>
        /// change xml document to json
        /// </summary>
        /// <param name="xmlDoc"></param>
        /// <returns></returns>
        public static string XmlToJson(XmlDocument xmlDoc)
        {
            StringBuilder sbJson = new StringBuilder();
            sbJson.Append("{");
            XmlToJSONnode(sbJson, xmlDoc.DocumentElement, true);
            sbJson.Append("}");
            return sbJson.ToString();
        }

        /// <summary>
        /// change xml to json by node
        /// </summary>
        /// <param name="sbJson"></param>
        /// <param name="node"></param>
        /// <param name="showNodeName"></param>
        private static void XmlToJSONnode(StringBuilder sbJson, XmlElement node, bool showNodeName)
        {
            if (showNodeName)
            {
                sbJson.Append("\\" + SafeJSON(node.Name) + "\\:");
            }
            sbJson.Append("{");

            SortedList childNodeNames = new SortedList();

            //  Add in all node attributes
            if (node.Attributes != null)
                foreach (XmlAttribute attr in node.Attributes)
                    StoreChildNode(childNodeNames, attr.Name, attr.InnerText);

            //  Add in all nodes
            foreach (XmlNode cnode in node.ChildNodes)
            {
                if (cnode is XmlText)
                    StoreChildNode(childNodeNames, "value", cnode.InnerText);
                else if (cnode is XmlElement)
                    StoreChildNode(childNodeNames, cnode.Name, cnode);
            }

            // Now output all stored info
            foreach (string childname in childNodeNames.Keys)
            {
                ArrayList alChild = (ArrayList)childNodeNames[childname];
                if (alChild.Count == 1)
                    OutputNode(childname, alChild[0], sbJson, true);
                else
                {
                    sbJson.Append(" \"" + SafeJSON(childname) + "\": [ ");
                    foreach (object Child in alChild)
                        OutputNode(childname, Child, sbJson, false);
                    sbJson.Remove(sbJson.Length - 2, 2);
                    sbJson.Append(" ], ");
                }
            }
            sbJson.Remove(sbJson.Length - 2, 2);
            sbJson.Append(" }");
        }

        /// <summary>
        /// store child node into xml document
        /// </summary>
        /// <param name="childNodeNames"></param>
        /// <param name="nodeName"></param>
        /// <param name="nodeValue"></param>
        private static void StoreChildNode(SortedList childNodeNames, string nodeName, object nodeValue)
        {
            // Pre-process contraction of XmlElement-s
            if (nodeValue is XmlElement)
            {
                // Convert  <aa></aa> into "aa":null
                //          <aa>xx</aa> into "aa":"xx"
                XmlNode cnode = (XmlNode)nodeValue;
                if (cnode.Attributes.Count == 0)
                {
                    XmlNodeList children = cnode.ChildNodes;
                    if (children.Count == 0)
                        nodeValue = null;
                    else if (children.Count == 1 && (children[0] is XmlText))
                        nodeValue = ((XmlText)(children[0])).InnerText;
                }
            }
            // Add nodeValue to ArrayList associated with each nodeName
            // If nodeName doesn't exist then add it
            object oValuesAL = childNodeNames[nodeName];
            ArrayList ValuesAL;
            if (oValuesAL == null)
            {
                ValuesAL = new ArrayList();
                childNodeNames[nodeName] = ValuesAL;
            }
            else
                ValuesAL = (ArrayList)oValuesAL;
            ValuesAL.Add(nodeValue);
        }

        /// <summary>
        /// output node from xml document
        /// </summary>
        /// <param name="childname"></param>
        /// <param name="alChild"></param>
        /// <param name="sbJSON"></param>
        /// <param name="showNodeName"></param>
        private static void OutputNode(string childname, object alChild, StringBuilder sbJSON, bool showNodeName)
        {
            if (alChild == null)
            {
                if (showNodeName)
                    sbJSON.Append("\"" + SafeJSON(childname) + "\": ");
                sbJSON.Append("null");
            }
            else if (alChild is string)
            {
                if (showNodeName)
                    sbJSON.Append("\"" + SafeJSON(childname) + "\": ");
                string sChild = (string)alChild;
                sChild = sChild.Trim();
                sbJSON.Append("\"" + SafeJSON(sChild) + "\"");
            }
            else
                XmlToJSONnode(sbJSON, (XmlElement)alChild, showNodeName);
            sbJSON.Append(", ");
        }

        /// <summary>
        /// change string to safe json
        /// </summary>
        /// <param name="sIn"></param>
        /// <returns></returns>
        private static string SafeJSON(string sIn)
        {
            StringBuilder sbOut = new StringBuilder(sIn.Length);
            foreach (char ch in sIn)
            {
                if(char.IsControl(ch) || ch=='\'')
                {
                    int ich = (int)ch;
                    sbOut.Append(@"\u" + ich.ToString("x4"));
                    continue;
                }
                else if(ch== '\"' || ch== '\\' || ch== '/')
                {
                    sbOut.Append('\\');
                }
                sbOut.Append(ch);
            }
            return sbOut.ToString();
        }
    }
}