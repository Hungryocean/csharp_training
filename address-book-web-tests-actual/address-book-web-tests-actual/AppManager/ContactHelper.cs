using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace WebaddressbookTests
{
    public class ContactHelper : HelperBase
    {   public ContactHelper(ApplicationManager manager)
            : base(manager)
        {
        }
        public ContactHelper Create(ContactData contact)
        {
           manager.Navigator.OpenHomePage();
           InitContactCreation();
           FillContactForm(contact);
           SubmitContactCreation();
           ReturnToHomePage();
           return this;
        }
        public ContactHelper Modify(int p, ContactData newData)
        {
            manager.Navigator.OpenHomePage();
            ContactData oldContact = new ContactData("bbb");
            CreateContactIfNotAny(oldContact);
            SelectContact(p);
            InitContactModification();
            FillContactForm(newData);
            SubmitContactModification();
            manager.Navigator.OpenHomePage();
            return this;
        }
        public ContactHelper Remove(int p)
        {
            manager.Navigator.OpenHomePage();
            ContactData oldContact = new ContactData("bbb");
            CreateContactIfNotAny(oldContact);
            SelectContact(p);
            RemoveContact();
            ApproveContactRemoval();
            manager.Navigator.OpenHomePage();
            return this;
        }

        public ContactHelper ApproveContactRemoval()
        {
            driver.SwitchTo().Alert().Accept();
            return this;
        }
        public ContactHelper RemoveContact()
        {
            driver.FindElement(By.XPath("//input[@value='Delete']")).Click();
            contactCache = null;
            return this;
        }
        public ContactHelper SelectContact(int index)
        {
            driver.FindElement(By.XPath("(//input[@name='selected[]'])[" + (index + 1) + "]")).Click();
            return this;
        }
        public ContactHelper InitContactCreation()
        {
            driver.FindElement(By.LinkText("add new")).Click();
            return this;
        }
        public ContactHelper FillContactForm(ContactData contact)
        {
            Type(By.Name("firstname"), contact.Firstname);
            Type(By.Name("lastname"), contact.Lastname);
            return this;
        }
        public ContactHelper SubmitContactCreation()
        {
            driver.FindElement(By.XPath("//div[@id='content']/form/input[21]")).Click();
            contactCache = null;
            return this;
        }
        public ContactHelper ReturnToHomePage()
        {
            driver.FindElement(By.LinkText("home page")).Click();
            return this;
        }
        public ContactHelper SubmitContactModification()
        {
            driver.FindElement(By.XPath("//div[@id='content']/form/input[22]")).Click();
            contactCache = null;
            return this;
        }
        public ContactHelper InitContactModification()
        {
            driver.FindElement(By.XPath("//img[@alt='Edit']")).Click();
            return this;
        }
        public void CreateContactIfNotAny(ContactData oldContact)
        {
            if (IsContactCreated())
            {
            }
            else
            {
                Create(oldContact);
            }
        }

        public bool IsContactCreated()
        {
            return IsElementPresent(By.XPath("(//input[@name='selected[]'])"));
        }

        private List<ContactData> contactCache = null;

        public string Id { get; private set; }
        public List<ContactData> GetContactList()
        {
            if (contactCache == null)
            {
                contactCache = new List<ContactData>();
                manager.Navigator.OpenHomePage();
                ICollection<IWebElement> elements = driver.FindElements(By.Name("entry"));
                foreach (IWebElement element in elements)
                {
                    contactCache.Add(new ContactData(element.Text)
                    {
                        Id = element.FindElement(By.TagName("input")).GetAttribute("value")
                    });
                }
            }
            return new List<ContactData>(contactCache); 
        }
        internal int GetContactCount()
        {
            return driver.FindElements(By.Name("entry")).Count;
        }

    }
}
