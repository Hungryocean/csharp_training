using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebaddressbookTests
{
    [TestFixture]
    public class AddContactToGroupTests : AuthTestBase
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
        public void TestAddingContactToGroup()
        {
            GroupData group = GroupData.GetAll()[0];
            app.Contacts.CheckContactExist(group);
            List<ContactData> oldList = group.GetContacts();
            ContactData contact = ContactData.GetAll().Except(oldList).First();

            app.Contacts.AddContactToGroup(contact, group);

            List<ContactData> newList = group.GetContacts();
            oldList.Add(contact);
            newList.Sort();
            oldList.Sort();

            Assert.AreEqual(oldList, newList);
        }
    }
}
