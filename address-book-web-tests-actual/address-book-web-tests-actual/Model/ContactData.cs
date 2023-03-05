using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using LinqToDB.Mapping;


namespace WebaddressbookTests
{
    [Table(Name = "addressbook")]
    public class ContactData : IEquatable<ContactData>, IComparable<ContactData>
    {
        private string text;

        private string allPhones;

        private string allEmails;

        private string allDetails;

        public ContactData()
        {
        }
        public ContactData(string firstname, string lastname)
        {
            Firstname = firstname;
            Lastname = lastname;
        }

        public ContactData(string text)
        {
            this.text = text;
        }

        public bool Equals(ContactData other)
        {
            if (Object.ReferenceEquals(other, null))
            {
                return false;
            }
            if (Object.ReferenceEquals(this, other))
            {
                return true;
            }
            return Lastname == other.Lastname && Firstname == other.Firstname;
        }

        public override int GetHashCode()
        {
            return (Firstname + " " + Lastname).GetHashCode();
        }
        public override string ToString()
        {
            return Firstname + " " + Lastname;

        }
        public int CompareTo(ContactData other)
        {

            if (Object.ReferenceEquals(other, null))
            {
                return 1;
            }

            int result = Lastname.CompareTo(other.Lastname);
            if (result != 0)
            {
                return result;
            }
            else
            {
                return Firstname.CompareTo(other.Firstname);
            }

        }

        [Column(Name = "firstname"), NotNull]
        public string Firstname { get; set; }

        [Column(Name = "lastname"), NotNull]
        public string Lastname { get; set; }

        [Column(Name = "deprecated")]
        public string Deprecated { get; set; }

        [Column(Name = "middlename"), NotNull]
        public string Middlename { get; set; }

        [Column(Name = "nickname"), NotNull]
        public string Nickname { get; set; }

        [Column(Name = "title"), NotNull]
        public string Title { get; set; }

        [Column(Name = "company"), NotNull]
        public string Company { get; set; }

        [Column(Name = "id"), PrimaryKey, Identity]
        public string Id { get; set; }

        [Column(Name = "address"), NotNull]
        public string Address { get; set; }

        [Column(Name = "home"), NotNull]
        public string HomePhone { get; set; }

        [Column(Name = "mobile"), NotNull]
        public string MobilePhone { get; set; }

        [Column(Name = "work"), NotNull]
        public string WorkPhone { get; set; }
        public string AllPhones
        {
            get
            {
                if (allPhones != null)
                {
                    return allPhones;
                }
                else
                {
                    string bufer = "";

                    if (HomePhone != null && HomePhone != "")
                    {
                        bufer = bufer + CleanUpPhone(HomePhone);
                    }
                    if (MobilePhone != null && MobilePhone != "")
                    {
                        if (bufer != "")
                        {
                            bufer = bufer + "\r\n" + CleanUpPhone(MobilePhone);
                        }
                        else
                        {
                            bufer = bufer + CleanUpPhone(MobilePhone);
                        }
                    }
                    if (WorkPhone != null && WorkPhone != "")
                    {
                        if (bufer != "")
                        {
                            bufer = bufer + "\r\n" + CleanUpPhone(WorkPhone);
                        }
                        else
                        {
                            bufer = bufer + CleanUpPhone(WorkPhone);
                        }
                    }
                    return bufer;
                }
            }
            set
            {
                allPhones = value;
            }
        }

        [Column(Name = "fax"), NotNull]
        public string Fax { get; set; }

        [Column(Name = "email"), NotNull]
        public string Email { get; set; }

        [Column(Name = "email2"), NotNull]
        public string Email2 { get; set; }

        [Column(Name = "email3"), NotNull]
        public string Email3 { get; set; }
        public string AllEmails
        {
            get
            {
                if (allEmails != null)
                {
                    return allEmails;
                }
                else
                {
                    string bufer = "";

                    if (Email != null && Email != "")
                    {
                        bufer = bufer + Email;
                    }
                    if (Email2 != null && Email2 != "")
                    {
                        if (bufer != "")
                        {
                            bufer = bufer + "\r\n" + Email2;
                        }
                        else
                        {
                            bufer = bufer + Email2;
                        }
                    }
                    if (Email3 != null && Email3 != "")
                    {
                        if (bufer != "")
                        {
                            bufer = bufer + "\r\n" + Email3;
                        }
                        else
                        {
                            bufer = bufer + Email3;
                        }
                    }
                    return bufer; ;
                }
            }
            set
            {
                allEmails = value;
            }
        }

        [Column(Name = "homepage"), NotNull]
        public string Homepage { get; set; }

        [Column(Name = "notes"), NotNull]
        public string Notes { get; set; }

        public string AllDetails
        {
            get
            {
                if (allDetails != null)
                {
                    return allDetails;
                }
                else
                {
                    string details = (CleanUpAllDetails(GetContacts(Firstname, Middlename, Lastname, Address))
                        + CleanUpAllDetails(GetPhones(HomePhone, MobilePhone, WorkPhone))
                        + CleanUpAllDetails(GetEmails(Email, Email2, Email3))).Trim();

                    return details;
                }
            }
            set
            {
                allDetails = value;
            }
        }

        public string CleanUpPhone(string phone)
        {
            if (phone == null || phone == "")
            {
                return "";
            }
            return Regex.Replace(phone, "[-()]", "");
        }

        private string CleanUpAllDetails(string allDetails)
        {
            if (allDetails == null || allDetails == "")
            {
                return "";
            }
            return allDetails + "\r\n";
        }

        private string GetContacts(string firstname, string middlename, string lastname, string address)
        {
            return CleanUpAllDetails(GetFullName(firstname, middlename, lastname))
                + CleanUpAllDetails(address);
        }

        private string GetFullName(string firstname, string middlename, string lastname)
        {
            string bufer = "";
            if (firstname != null && firstname != "")
            {
                bufer = bufer + Firstname + " ";
            }
            if (middlename != null && middlename != "")
            {
                bufer = bufer + Middlename + " ";
            }
            if (lastname != null && lastname != "")
            {
                bufer = bufer + Lastname + " ";
            }
            return bufer.Trim();
        }

        private string GetPhones(string homePhone, string mobilePhone, string workPhone)
        {
            string bufer = "";
            if (homePhone != null && homePhone != "")
            {
                bufer = bufer + "H: " + HomePhone + "\r\n";
            }
            if (mobilePhone != null && mobilePhone != "")
            {
                bufer = bufer + "M: " + MobilePhone + "\r\n";
            }
            if (workPhone != null && workPhone != "")
            {
                bufer = bufer + "W: " + WorkPhone + "\r\n";
            }
            return bufer;
        }

        private string GetEmails(string email, string email2, string email3)
        {
            string bufer = "";

            if (email != null && email != "")
            {
                bufer = bufer + "\r\n" + email;
            }
            if (email2 != null && email2 != "")
            {
                if (bufer != "")
                {
                    bufer = bufer + "\r\n" + email2;
                }
                else
                {
                    bufer = bufer + email2;
                }
            }
            if (email3 != null && email3 != "")
            {
                if (bufer != "")
                {
                    bufer = bufer + "\r\n" + email3;
                }
                else
                {
                    bufer = bufer + email3;
                }
            }
            return bufer;
        }

        public static List<ContactData> GetAll()
        {
            using (AddressBookDB db = new AddressBookDB())
            {
                return (from c in db.Contacts.Where(x => x.Deprecated == "0000-00-00 00:00:00") select c).ToList();
            }
        }

    }

}

