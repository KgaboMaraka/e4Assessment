using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Xml.Linq;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

namespace CaptureForm.Models
{
    public class XMLHelper
    {
        public List<Users> ReturnListOfUsers(int Id = 0)
        {
            try
            {
                string xmlData = HttpContext.Current.Server.MapPath("~/App_Data/UsersData.xml");
                DataSet ds = new DataSet();
                ds.ReadXml(xmlData);
                var users = new List<Users>();
                users = (from rows in ds.Tables[0].AsEnumerable()
                            select new Users
                            {
                                Id = Convert.ToInt32(rows[0].ToString()), 
                                Name = rows[1].ToString(),
                                Surname = rows[2].ToString(),
                                CellNo = rows[3].ToString(),
                            }).ToList();
                return users;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public Users GetUserById(int Id)
        {
            try
            {
                XDocument xmlDoc = XDocument.Load(HttpContext.Current.Server.MapPath("~/App_Data/UsersData.xml"));
                var items = (from item in xmlDoc.Descendants("Users").Descendants("User") select item).ToList();
                XElement selected = items.Where(p => p.Element("Id").Value == Id.ToString()).FirstOrDefault();

                XmlSerializer serializer = new XmlSerializer(typeof(Users), new XmlRootAttribute("User"));
                StringReader stringReader = new StringReader(selected.ToString());
                Users user = (Users)serializer.Deserialize(stringReader);

                return user;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void DeleteUser(int Id)
        {
            try
            {
                if (Id > 0)
                {
                    XDocument xmlDoc = XDocument.Load(HttpContext.Current.Server.MapPath("~/App_Data/UsersData.xml"));
                    var items = (from item in xmlDoc.Descendants("Users").Descendants("User") select item).ToList();
                    XElement selected = items.Where(p => p.Element("Id").Value == Id.ToString()).FirstOrDefault();
                    selected.Remove();
                    xmlDoc.Save(HttpContext.Current.Server.MapPath("~/App_Data/UsersData.xml"));
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void AddEditUser(Users user)
        {
            try
            {
                if (user.Id > 0)
                {
                    XDocument xmlDoc = XDocument.Load(HttpContext.Current.Server.MapPath("~/App_Data/UsersData.xml"));
                    var items = (from item in xmlDoc.Descendants("Users").Descendants("User") select item).ToList();
                    XElement selected = items.Where(p => p.Element("Id").Value == user.Id.ToString()).FirstOrDefault();

                    selected.Remove();
                    xmlDoc.Save(HttpContext.Current.Server.MapPath("~/App_Data/UsersData.xml"));
                    xmlDoc.Element("Users").Add(new XElement("User",
                        new XElement("Id", user.Id),
                        new XElement("Name", user.Name),
                        new XElement("Surname", user.Surname),
                        new XElement("CellNo", user.CellNo)
                    ));
                    xmlDoc.Save(HttpContext.Current.Server.MapPath("~/App_Data/UsersData.xml"));

                }
                else
                {
                    XmlDocument oXmlDocument = new XmlDocument();
                    oXmlDocument.Load(HttpContext.Current.Server.MapPath("~/App_Data/UsersData.xml"));
                    XmlNodeList nodelist = oXmlDocument.GetElementsByTagName("User");
                    var x = oXmlDocument.GetElementsByTagName("Id");
                    int Max = 0;
                    foreach (XmlElement item in x)
                    {
                        int EId = Convert.ToInt32(item.InnerText.ToString());
                        if (EId > Max)
                        {
                            Max = EId;
                        }
                    }
                    Max = Max + 1;
                    XDocument xmlDoc = XDocument.Load(HttpContext.Current.Server.MapPath("~/App_Data/UsersData.xml"));
                    xmlDoc.Element("Users").Add(new XElement("User",
                        new XElement("Id", Max),
                        new XElement("Name", user.Name),
                        new XElement("Surname", user.Surname),
                        new XElement("CellNo", user.CellNo)
                    ));
                    xmlDoc.Save(HttpContext.Current.Server.MapPath("~/App_Data/UsersData.xml"));
                }

            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}