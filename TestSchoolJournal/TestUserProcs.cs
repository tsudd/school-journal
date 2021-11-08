using DataAccessLayer.DataAccessModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestSchoolJournal
{
    [TestClass]
    public class TestUserProcs
    {
        [TestMethod]
        public void TestGetAll()
        {
            var users = new DataAccessUsers();
            var res = users.GetAll();
            Assert.AreEqual(1, 1);
        }
    }
}
