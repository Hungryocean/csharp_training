﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;
using WebAddressbookTests;

namespace WebaddressbookTests
{
    [TestFixture]
    public class GroupModificationTests : GroupTestBase
    {
        [Test]
        public void GroupModificationTest()
        {
            GroupData newData = new GroupData("zzz");
            newData.Header = null;
            newData.Footer = null;

            GroupData oldGroup = new GroupData("aaa");
            app.Groups.CreateGroupIfNotAny(oldGroup);

            List<GroupData> oldGroups = GroupData.GetAll();
            GroupData oldData = oldGroups[0];

            app.Groups.Modify(oldData, newData);

            Assert.AreEqual(oldGroups.Count, app.Groups.GetGroupCount());

            List<GroupData> newGroups = GroupData.GetAll();
            oldGroups[0].Name = newData.Name;
            oldGroups.Sort();
            newGroups.Sort();
            Assert.AreEqual(oldGroups, newGroups);

            foreach (GroupData group in newGroups)
            {
                if (group.Id == oldData.Id)
                {
                    Assert.AreEqual(newData.Name, group.Name);
                }
            }
        }

    }
}
