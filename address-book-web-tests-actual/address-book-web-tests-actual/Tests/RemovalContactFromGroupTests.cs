using Microsoft.Office.Interop.Excel;
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static LinqToDB.Reflection.Methods.LinqToDB.Insert;

namespace WebaddressbookTests
{
    [TestFixture]
    public class RemovalContactFromGroupTests : AuthTestBase
    {
        [SetUp]
        public void Setup()
        {
            ContactData contact = new ContactData("aaa", "bbb");
            contact.Email = "kit@ya";

            if (!app.Contacts.IsContactCreated())

            {
                app.Contacts.Create(contact);
            }

            GroupData newGroup = new GroupData("ccc");
            newGroup.Header = "ddd";
            newGroup.Footer = "eee";

            if (!app.Groups.IsGroupCreated())

            {
                app.Groups.Create(newGroup);
            }
        }

        [Test]
        public void RemovalContactFromGroupTest()

        {
            GroupData group = GroupData.GetAll()[0];
            app.Contacts.CheckContactNotExist(group);
            List<ContactData> oldList = group.GetContacts();
            ContactData contact = oldList.First();

            app.Contacts.RemoveContactFromGroup(contact, group);

            List<ContactData> newList = group.GetContacts();
            oldList.Remove(contact);
            oldList.Sort();
            newList.Sort();

            Assert.AreEqual(oldList, newList);
        }
    }
}
