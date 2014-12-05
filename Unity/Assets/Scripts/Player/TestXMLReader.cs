using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Xml;
using System.Text;
using System.IO;

public class TestXMLReader : MonoBehaviour {

    public string fileName;
    public string textNPC;
    public string textPlayer;

    void XMLReader(string fileName)
    {
        XmlTextReader reader = new XmlTextReader("Assets\\Resources\\Text\\" + fileName);
        while(reader.Read())
        {
            switch(reader.NodeType)
            {
                case XmlNodeType.Element:
                    if(reader.Name.Equals("NPC"))
                    {
                        textNPC = reader.ReadInnerXml();
                    }
                    if(reader.Name.Equals("PLAYER"))
                    {
                        textPlayer = reader.ReadInnerXml();
                    }
                    break;
            }
        }
    }

    void Start()
    {
        XMLReader(fileName);
    }
}
