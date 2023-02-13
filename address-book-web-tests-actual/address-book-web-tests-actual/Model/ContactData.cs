using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace WebaddressbookTests
{
    public class ContactData : IEquatable<ContactData>, IComparable<ContactData>
    {
        private string text;

        private string allPhones;

        private string allEmails;

        private string allDetails;


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

        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Middlename { get; set; }
        public string Nickname { get; set; }
        public string Title { get; set; }
        public string Company { get; set; }
        public string Id { get; set; }
        public string Address { get; set; }

        public string HomePhone { get; set; }
        public string MobilePhone { get; set; }
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
                    return CleanUpPhone(HomePhone) + CleanUpPhone(MobilePhone) + CleanUpPhone(WorkPhone).Trim();
                }
            }
            set
            {
                allPhones = value;
            }
        }

        public string Fax { get; set; }

        public string Email { get; set; }
        public string Email2 { get; set; }
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
                    return CleanUpEmail(Email) + CleanUpEmail(Email2) + CleanUpEmail(Email3).Trim();
                }
            }
            set
            {
                allEmails = value;
            }
        }

        public string Homepage { get; set; }
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
            return Regex.Replace(phone, "[-()]", "") + "\r\n";
        }
        private string CleanUpEmail(string email)
        {
            if (email == null || email == "")
            {
                return "";
            }
            return Regex.Replace(email, "[-()]", "") + "\r\n";
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
                bufer = bufer + email + "\r\n";
            }
            if (email2 != null && email2 != "")
            {
                bufer = bufer + email2 + "\r\n";
            }
            if (email3 != null && email3 != "")
            {
                bufer = bufer + email3 + "\r\n";
            }
            return bufer;
        }

    }

}

