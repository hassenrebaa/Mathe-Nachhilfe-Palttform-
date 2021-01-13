using Mathe_Nachhilfe_Plattform.Controllers;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using plattform;
using System.Linq;

namespace Nachhilfe
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public string TestMethod1Loginn()
        {
            LoginController controller = new LoginController();

            user_tbl usermodel = new user_tbl();
            usermodel.email = " xxxxxxxxxxx@gmail.com";
            usermodel.password = "xxxxxxxxx";
            bool actual = true;
            bool expected = false;
            using (HassenDataBaseEntities7 db = new HassenDataBaseEntities7())
            {

                if (db.user_tbl.Where(x => x.email == usermodel.email && x.password == usermodel.password).FirstOrDefault() == null)
                {

                    return ("Login Succesfully");

                }
                else
                {
                    return ("es Gibt Ein Fehler");
                     actual = false;
                }

            }

            

            Assert.AreEqual(expected, actual);
        }
    }
}
