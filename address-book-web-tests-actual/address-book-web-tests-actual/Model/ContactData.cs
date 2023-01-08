using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebaddressbookTests
{
    public class ContactData : IEquatable<ContactData>, IComparable<ContactData>
    {
        

        public ContactData(string firstname)
        {
            Firstname = firstname;
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
            Firstname.GetHashCode();
            Lastname.GetHashCode();
            return 1;
        }
        public override string ToString()
        {
            return  (Lastname + Firstname);
            
        }
        public int CompareTo(ContactData other)
        {

            if (Object.ReferenceEquals(other, null))
            {
                return 1;
            }

            return (Lastname.CompareTo(other.Lastname) + Firstname.CompareTo(other.Firstname));
        }

        public string Firstname { get; set; }

        public string Lastname { get; set; }

        public string Id { get; set; }
    }
}

