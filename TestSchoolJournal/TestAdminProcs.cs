using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataAccessLayer.DataAccessModel;
using DataAccessLayer.Models;

namespace TestSchoolJournal
{
    [TestClass]
    public class TestAdminProcs
    {
        public DataAccessAdmin AdminsAccess { get; set; }
        [TestInitialize]
        public void Setup()
        {
            //given
            AdminsAccess = new DataAccessAdmin();
        }
        [TestMethod]
        public void TestGetAll()
        {
            // when
            var admins = AdminsAccess.GetAll();

            // then
            int i = 1;
            foreach(var admin in admins)
            {
                Assert.AreEqual(admin.Id, i);
                ++i;
            }
        }

        [TestMethod]
        public void TestGetById()
        {
            //given
            var num = 1;

            //when
            var admin = AdminsAccess.Get(num);

            //then
            Assert.AreEqual(admin.Id, num);
        }

        [TestMethod]
        public void TestGetNonExistById()
        {
            //given
            var num = 228;

            //when
            var admin = AdminsAccess.Get(num);

            //then
            Assert.IsNull(admin);
        }

        [TestMethod]
        public void TestWrongAdminCreation()
        {
            //given
            var admin = new Admin() { UserId = 24 };

            //when
            try
            {
                AdminsAccess.Create(admin);
                Assert.Fail();
            }
            catch
            {
                //then
            }
        }

        [TestMethod]
        public void TestAdminCreation()
        {
            //given
            var admin = new Admin() { UserId = 1 };

            //when
            try
            {
                //then
                AdminsAccess.Create(admin);
            }
            catch
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void TestUpdateAdmin()
        {
            //given
            var admin = new Admin() {Id = 1, UserId = 2023 };
    
            //when
            try
            {
                //then
                AdminsAccess.Update(admin);
            }
            catch
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void TestDeleteAdmin()
        {
            //given
            var num = 2;

            //when
            try
            {
                AdminsAccess.Delete(num);
            }
            catch
            {
                Assert.Fail();
            }
        }
    }
}
