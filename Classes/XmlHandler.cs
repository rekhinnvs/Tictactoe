using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml;
using TicTacToe.Classes;

namespace TicTacToe.Classes
{
    public class XmlHandler
    {

        XmlWriterSettings xmlWriterSettings;
        XmlWriter xmlWriter;
        XmlDocument doc;

        public void CreateXMLFile(XmlAttributes xmlElements)
        {
            xmlWriterSettings = new XmlWriterSettings();
            xmlWriterSettings.Indent = true;
            
            // xmlWriterSettings.NewLineOnAttributes = true;

            using (xmlWriter = XmlWriter.Create(xmlElements.FileName, xmlWriterSettings))
            {
                xmlWriter.WriteStartDocument();
                xmlWriter.WriteStartElement("Game");
                xmlWriter.WriteStartElement("Move");

                xmlWriter.WriteEndDocument();
                xmlWriter.Flush();
                xmlWriter.Close();
            }


        }

        public void AppendToRoot(XmlAttributes xmlElements)
        {
            doc = new XmlDocument();
            doc.Load(xmlElements.FileName);

            XmlElement Move = doc.CreateElement("Move");
            XmlElement StepID = doc.CreateElement("Step");
            StepID.SetAttribute("StepID",xmlElements.StepID.ToString());
            StepID.SetAttribute("Time", xmlElements.StepTime);

            XmlElement Player = doc.CreateElement("Player");
            Player.SetAttribute("ID", xmlElements.PlayerID);
            Player.SetAttribute("Type",xmlElements.PlayerType);

            XmlElement Play = doc.CreateElement("Play");
            Play.SetAttribute("sign",xmlElements.PlaySign);
            Play.InnerXml = xmlElements.PlayLocation;

            Move.AppendChild(StepID);
            Move.AppendChild(Player);
            Move.AppendChild(Play);
            doc.DocumentElement.AppendChild(Move);
            doc.Save(xmlElements.FileName);


        }
    }
}
