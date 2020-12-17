using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml;

namespace TicTacToe.Classes
{
    public class XmlHandler
    {

        XmlWriterSettings xmlWriterSettings;
        XmlWriter xmlWriter;


        public void CreateXMLFile(string fileName)
        {
            xmlWriterSettings = new XmlWriterSettings();
            xmlWriterSettings.Indent = true;
           // xmlWriterSettings.NewLineOnAttributes = true;

            using (xmlWriter = XmlWriter.Create(fileName, xmlWriterSettings))
            {
                xmlWriter.WriteStartDocument();
                xmlWriter.WriteStartElement("Game");
                xmlWriter.WriteStartElement("Move");


                xmlWriter.WriteStartElement("Step");
                xmlWriter.WriteAttributeString("StepID", "value");
                xmlWriter.WriteAttributeString("Time", "value");
                xmlWriter.WriteEndElement();
                xmlWriter.WriteStartElement("Player");
                xmlWriter.WriteAttributeString("ID", "value");
                xmlWriter.WriteAttributeString("Type", "value");
                xmlWriter.WriteEndElement();
                xmlWriter.WriteStartElement("Play");
                xmlWriter.WriteAttributeString("sign", "value");
                xmlWriter.WriteString("hwllo");
                xmlWriter.WriteEndElement();


                xmlWriter.WriteEndDocument();
                xmlWriter.Flush();
                xmlWriter.Close();
            }

        }

       
    }
}
