using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebaddressbookTests 
{
     public class AuthTestBase : TestBase
    {
        [SetUp]
        public void SetupLogin()
        {
            app.Auth.Login(new AccountData("admin", "secret"));

        }

    }
}
