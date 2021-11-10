using DataAccessLayer.DataAccessModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestSchoolJournal
{
    [TestClass]
    public class TestMarkProcs
    {
        DataAccessMarks MarksAccess;
        [TestInitialize]
        public void Setup()
        {
            MarksAccess = new DataAccessMarks();    
        }

        [TestMethod]
        public void TestGetAll()
        {
            //when
            try
            {
                var marks = MarksAccess.GetAll();
            }
            catch
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void TestGetById()
        {
            try
            {
                //when
                var mark = MarksAccess.Get(1);
            }
            catch
            {
                Assert.Fail();
            }
        }
    }
}
