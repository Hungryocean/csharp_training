using System;
using NUnit.Framework;

namespace WebaddressbookTests
{
    [TestFixture]
    public class LoginTests : TestBase
    {
        [Test]
        public void LoginWithValidCredentials()
        {
            //prepare
            app.Auth.Logout();
            //actions
            AccountData account = new AccountData("admin", "secret");
            app.Auth.Login(account);
            //verification
            Assert.IsTrue(app.Auth.IsLoggedIn(account));
        }
        [Test]
        public void LoginWithInvalidCredentials()
        {
            //prepare
            app.Auth.Logout();
            //actions
            AccountData account = new AccountData("admin", "123456");
            app.Auth.Login(account);
            //verification
            Assert.IsFalse(app.Auth.IsLoggedIn(account));
        }
    }
}
