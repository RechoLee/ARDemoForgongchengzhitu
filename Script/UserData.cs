using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO;
using UnityEngine;
using System.Xml;
using System.Xml.Serialization;
using System.Collections;

using UnityEngine.UI;


public class UserData : MonoBehaviour {

    public InputField Inp_Name;
    public InputField Inp_Password;
    public InputField Inp_Email;
    public InputField Inp_Telphone;

    public Text Name;

    private int i = 0;

    public void CreateXml()
    {
        //xml保存的路径，这里放在Asset路径，注意路径
        string filePath = Application.dataPath + "/UserXML/UserData.xml";
        //继续判断路径下是否有该文件
        if (!File.Exists(filePath))
        {
            //创建XML文档实例
            XmlDocument xmlDoc = new XmlDocument();
            //创建root节点，也就是最上一层节点
            XmlElement root = xmlDoc.CreateElement("UserList");
            //继续创建下一层节点
            XmlElement elmNew = xmlDoc.CreateElement("User");
            //设置节点的两个属性
            elmNew.SetAttribute("id", i.ToString());
            //继续创建下一层节点
            XmlElement User_name = xmlDoc.CreateElement("name");
            User_name.InnerText = Inp_Name.text;
            Name.text = Inp_Name.text;
            XmlElement User_password = xmlDoc.CreateElement("password");
            User_password.InnerText = Inp_Password.text;
            XmlElement User_email = xmlDoc.CreateElement("email");
            User_email.InnerText = Inp_Email.text;
            XmlElement User_telphone = xmlDoc.CreateElement("telphone");
            User_telphone.InnerText = Inp_Telphone.text;

            //把节点一层一层的添加至XMLDoc中，请仔细看他们之间的先后顺序，这将是生成XML文件的顺序
            elmNew.AppendChild(User_name);
            elmNew.AppendChild(User_password);
            elmNew.AppendChild(User_email);
            elmNew.AppendChild(User_telphone);
            root.AppendChild(elmNew);
            xmlDoc.AppendChild(root);
            //把xml文件保存至本地
            xmlDoc.Save(filePath);
            Debug.Log("CreateXML OK!");

        }
    }
   
    public void AddXml()
    {
        string filepath = Application.dataPath + "/UserXML /UserData.xml";
        if (File.Exists(filepath))
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(filepath);
            XmlNode root = xmlDoc.SelectSingleNode("UserList");
            XmlElement elmNew = xmlDoc.CreateElement("User");
            elmNew.SetAttribute("id", (i+1).ToString());

            XmlElement User_name = xmlDoc.CreateElement("name");
            User_name.InnerText = Inp_Name.text;
            Name.text = Inp_Name.text;
            XmlElement User_password = xmlDoc.CreateElement("password");
            User_password.InnerText = Inp_Password.text;
            XmlElement User_email = xmlDoc.CreateElement("email");
            User_email.InnerText = Inp_Email.text;
            XmlElement User_telphone = xmlDoc.CreateElement("telphone");
            User_telphone.InnerText = Inp_Telphone.text;

            elmNew.AppendChild(User_name);
            elmNew.AppendChild(User_password);
            elmNew.AppendChild(User_email);
            elmNew.AppendChild(User_telphone);
            root.AppendChild(elmNew);
            xmlDoc.AppendChild(root);
            xmlDoc.Save(filepath);
            Debug.Log("AddXml OK!");
        }
    }

    public void deleteXml()
    {
        string filepath = Application.dataPath + @"/my.xml";
        if (File.Exists(filepath))
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(filepath);
            XmlNodeList nodeList = xmlDoc.SelectSingleNode("transforms").ChildNodes;
            foreach (XmlElement xe in nodeList)
            {
                if (xe.GetAttribute("id") == "1")
                {
                    xe.RemoveAttribute("id");
                }

                foreach (XmlElement x1 in xe.ChildNodes)
                {
                    if (x1.Name == "z")
                    {
                        x1.RemoveAll();

                    }
                }
            }
            xmlDoc.Save(filepath);
            Debug.Log("deleteXml OK!");
        }

    }

    public void showXml()
    {
        string filepath = Application.dataPath + @"/my.xml";
        if (File.Exists(filepath))
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(filepath);
            XmlNodeList nodeList = xmlDoc.SelectSingleNode("transforms").ChildNodes;
            //遍历每一个节点，拿节点的属性以及节点的内容
            foreach (XmlElement xe in nodeList)
            {
                Debug.Log("Attribute :" + xe.GetAttribute("name"));
                Debug.Log("NAME :" + xe.Name);
                foreach (XmlElement x1 in xe.ChildNodes)
                {
                    if (x1.Name == "y")
                    {
                        Debug.Log("VALUE :" + x1.InnerText);

                    }
                }
            }
            Debug.Log("all = " + xmlDoc.OuterXml);

        }
    }

    public void UpdateXml()
    {
        string filepath = Application.dataPath + @"/my.xml";
        if (File.Exists(filepath))
        {
            XmlDocument xmlDoc = new XmlDocument();
            //根据路径将XML读取出来
            xmlDoc.Load(filepath);
            //得到transforms下的所有子节点
            XmlNodeList nodeList = xmlDoc.SelectSingleNode("transforms").ChildNodes;
            //遍历所有子节点
            foreach (XmlElement xe in nodeList)
            {
                //拿到节点中属性ID =0的节点
                if (xe.GetAttribute("id") == "0")
                {
                    //更新节点属性
                    xe.SetAttribute("id", "1000");
                    //继续遍历
                    foreach (XmlElement x1 in xe.ChildNodes)
                    {
                        if (x1.Name == "z")
                        {
                            //这里是修改节点名称对应的数值，而上面的拿到节点连带的属性。。。
                            x1.InnerText = "update00000";
                        }

                    }
                    break;
                }
            }
            xmlDoc.Save(filepath);
            Debug.Log("UpdateXml OK!");
        }

    }

    
}
