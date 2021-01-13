using Microsoft.VisualStudio.TestTools.UnitTesting;
using plattform;
using plattform.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nachhilfe.Test
{
    [TestClass]
    class UnitTest2
    {
        [TestMethod]
        public string TestMethodsuchen()
        {
            DocumentController controller = new DocumentController();
            Doc_tbl dokumentmodel = new Doc_tbl();
            dokumentmodel.bezeichnung = "aufgabe";
            dokumentmodel.SerieNr = "AL-N1";
            bool actual = true;
            bool expected = false;
            using(HassenDataBaseEntitiesDokument db= new HassenDataBaseEntitiesDokument())
            {
                if (db.Doc_tbl.Where(x => x.bezeichnung == dokumentmodel.bezeichnung && x.SerieNr == dokumentmodel.SerieNr).FirstOrDefault() == null)
                {
                    return (" such Succesfully");

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
 