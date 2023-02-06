using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;


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
            SelectContact(p);
            InitContactModification(0);
            FillContactForm(newData);
            SubmitContactModification();
            manager.Navigator.OpenHomePage();
            return this;
        }
        public ContactHelper Remove(int p)
        {
            manager.Navigator.OpenHomePage();
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
        public void InitContactModification(int index)
        {
            driver.FindElements(By.Name("entry"))[index]
                .FindElements(By.TagName("td"))[7]
                .FindElement(By.TagName("a")).Click();
;
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

        public List<ContactData> GetContactList()
        {
            if (contactCache == null)
            {
                contactCache = new List<ContactData>();
                manager.Navigator.OpenHomePage();
                IList<IWebElement> elements = driver.FindElements(By.CssSelector("tr[name=\"entry\"]"));
                string firstname;
                string lastname;
                foreach (IWebElement element in elements)
                {
                    IList<IWebElement> cells = element.FindElements(By.CssSelector("td"));
                    lastname = cells[2].Text;
                    firstname = cells[1].Text;
                    contactCache.Add(new ContactData(lastname, firstname));
                }
            }
            //string allContactNames = driver.FindElement(By.CssSelector("div#content form")).Text;
            //    string[] parts = allContactNames.Split('\n');
            //    int shift = contactCache.Count - parts.Length;
            //    for (int i = 0; i < contactCache.Count; i++)
            //    {
            //        if (i < shift)
            //        {
            //            contactCache[i].Lastname = "";
            //        }
            //        else
            //        {
            //            contactCache[i].Lastname = parts[i - shift].Trim();
            //        }
            //    }
            return new List<ContactData>(contactCache);
        }

        public int GetContactCount()
        {
            return driver.FindElements(By.Name("entry")).Count;
        }

        public ContactData GetContactInformationFromTable(int index)
        {
            manager.Navigator.OpenHomePage();
            IList<IWebElement> cells = driver.FindElements(By.Name("entry"))[index].FindElements(By.TagName("td"));
            string lastname = cells[1].Text;
            string firstname = cells[2].Text;
            string address = cells[3].Text;
            string allPhones = cells[5].Text;

            return new ContactData(firstname, lastname)
            {
                Address = address,
                AllPhones = allPhones
            };

        }

        public ContactData GetContactInformationFromEditForm(int index)
        {
            manager.Navigator.OpenHomePage();
            InitContactModification(0);
            string firstname = driver.FindElement(By.Name("firstname")).GetAttribute("value");
            string lastname = driver.FindElement(By.Name("lastname")).GetAttribute("value");
            string address = driver.FindElement(By.Name("address")).GetAttribute("value");

            string homePhone = driver.FindElement(By.Name("home")).GetAttribute("value");
            string mobilePhone = driver.FindElement(By.Name("mobile")).GetAttribute("value");
            string workPhone = driver.FindElement(By.Name("work")).GetAttribute("value");

            return new ContactData(firstname, lastname)
            {
                Address = address,
                HomePhone = homePhone,
                MobilePhone = mobilePhone,
                WorkPhone = workPhone
            };

        }
        public int GetNumberOfSearchResults()
        {
            manager.Navigator.OpenHomePage();
            string text = driver.FindElement(By.TagName("label")).Text;
            Match m = new Regex(@"\d+").Match(text);
            return Int32.Parse(m.Value);
            
        }
    }
}
