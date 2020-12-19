using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
        XmlReader reader;

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
            }


        }

        public void AppendToRoot(XmlAttributes xmlElements)
        {
            doc = new XmlDocument();
            doc.Load(xmlElements.FileName);

            XmlElement Steps = doc.CreateElement("Step");
            XmlElement StepID = doc.CreateElement("Step");
            StepID.SetAttribute("StepID", xmlElements.StepID.ToString());
            StepID.SetAttribute("Time", xmlElements.StepTime);

            XmlElement Player = doc.CreateElement("Player");
            Player.SetAttribute("ID", xmlElements.PlayerID);
            Player.SetAttribute("Type", xmlElements.PlayerType);

            XmlElement Play = doc.CreateElement("Play");
            Play.SetAttribute("sign", xmlElements.PlaySign);
            Play.InnerXml = xmlElements.PlayLocation;

            Steps.AppendChild(StepID);
            Steps.AppendChild(Player);
            Steps.AppendChild(Play);
            doc.DocumentElement.AppendChild(Steps);
            doc.Save(xmlElements.FileName);
        }

        public string LoadFile()
        {
            string fileName = string.Empty;

            // Configure open file dialog box
            OpenFileDialog dlg = new OpenFileDialog();

            //Initialize the directory where the xml file is located.
            string CombinedPath = Path.Combine(Directory.GetCurrentDirectory(), "..\\..\\xmlFiles");
            dlg.InitialDirectory = Path.GetFullPath(CombinedPath);

            dlg.FileName = "Document"; // Default file name
            dlg.DefaultExt = ".xml"; // Default file extension
            dlg.Filter = "XML Files|*.xml"; // Filter files by extension

            // Show open file dialog box
            Nullable<bool> result = dlg.ShowDialog();

            // Process open file dialog box results
            if (result == true)
            {
                // Open document
                fileName = dlg.FileName;
                //MessageBox.Show(filename);
            }
            return fileName;
        }

        public string ReadXml(string fileName, string counter)
        {
            string coordinates = string.Empty;
            bool status = false;
            
            try
            {
                using (reader = XmlReader.Create(fileName))
                {
                    while (reader.Read())
                    {
                        if ((reader.NodeType == XmlNodeType.Element) && (reader.Name == "Step") && (reader.GetAttribute("StepID") == counter))
                        {
                            status = true;
                        }
                        if (status)
                        {
                            if ((reader.NodeType == XmlNodeType.Element) && (reader.Name == "Play"))
                            {
                                coordinates = reader.ReadInnerXml();
                                return coordinates;
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {

                MessageBox.Show($"Unable to read xml file {e.Message}");
                
            }
            return coordinates;
        }

    }
}
     
