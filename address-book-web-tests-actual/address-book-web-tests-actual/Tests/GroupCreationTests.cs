using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using System.Xml;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace WebaddressbookTests
{
    [TestFixture]
    public class GroupCreationTests : AuthTestBase
    {
        [Test, TestCaseSource("RandomGroupDataProvider")]
        public static IEnumerable<GroupData> RandomGroupDataProvider()

        {
            List<GroupData> groups = new List<GroupData>();
            for (int i = 0; i < 5; i++)
            {
                groups.Add(new GroupData(GenerateRandomString(30))
                {
                    Header = GenerateRandomString(100),
                    Footer = GenerateRandomString(100)
                });
            }
            return groups;
        }

        public static IEnumerable<GroupData> GroupDataFromCsvFile()
        {
            List<GroupData> groups = new List<GroupData>();
            string[] lines = File.ReadAllLines(@"groups.csv");
            foreach (string l in lines)
            {
                string[] parts = l.Split(',');
                groups.Add(new GroupData(parts[0])
                {
                    Header = parts[1],
                    Footer = parts[2]
                });
            }
            return groups;
        }

        public static IEnumerable<GroupData> GroupDataFromXmlFile()
        {
            List<GroupData> groups = new List<GroupData>();
            //privedenie tipa
            return (List<GroupData>) 
                //chtenie iz faila
                new XmlSerializer(typeof(List<GroupData>))
                .Deserialize(new StreamReader(@"groups.xml"));

        }

        public static IEnumerable<GroupData> GroupDataFromJsonFile()
        {
            return JsonConvert.DeserializeObject<List<GroupData>>(
                File.ReadAllText(@"groups.json"));
        }



        [Test, TestCaseSource("GroupDataFromJsonFile")]
        public void GroupCreationTest(GroupData group)
        {

            List<GroupData> oldGroups = app.Groups.GetGroupList();

            app.Groups.Create(group);

            Assert.AreEqual(oldGroups.Count +1, app.Groups.GetGroupCount());

            List<GroupData> newGroups = app.Groups.GetGroupList();
            oldGroups.Add(group);
            oldGroups.Sort();
            newGroups.Sort();
            Assert.AreEqual(oldGroups, newGroups);
        }

    }
}