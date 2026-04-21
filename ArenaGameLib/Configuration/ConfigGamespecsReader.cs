using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ArenaGameLib.Configuration
{
	/// <summary>
	/// Config file reading class for reading in game specs from an xml file Config.xml
	/// </summary>
	public class ConfigGamespecsReader
	{
		public int MaxX { get; set; }
		public int MaxY { get; set; }
		public string Difficulty { get; set; }

		/// <summary>
		/// Method for reading in the config file content.
		/// </summary>
		/// <param name="filePath">Type: string - Path to the config xml file</param>
		public void StartReadConfigFile(string filePath)
		{
			FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);
			XmlDocument doc = new XmlDocument();
			doc.Load(fs);

			XmlNode? maxXNode = doc.DocumentElement.SelectSingleNode("MaxX");
			if (maxXNode != null)
			{
				MaxX = int.Parse(maxXNode.InnerText.Trim());
			}

			XmlNode? maxYNode = doc.DocumentElement.SelectSingleNode("MaxY");
			if (maxYNode != null)
			{
				MaxY = int.Parse(maxYNode.InnerText.Trim());
			}

			XmlNode? difficultyNode = doc.DocumentElement.SelectSingleNode("Difficulty");
			if (difficultyNode != null)
			{
				Difficulty = difficultyNode.InnerText.Trim();
			}
		}
	}
}
