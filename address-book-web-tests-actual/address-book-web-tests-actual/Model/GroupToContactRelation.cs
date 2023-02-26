using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinqToDB.Mapping;
using Microsoft.Office.Interop.Excel;

namespace WebaddressbookTests
{
    [Table(Name = "address_in_groups")]
    public class GroupToContactRelation
    {
        [Column(Name = "group_id")]
        public string GroupId { get; }

        [Column(Name = "id")]
        public string ContactId { get; }
    }
}
