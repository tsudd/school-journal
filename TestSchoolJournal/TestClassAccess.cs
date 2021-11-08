using DataAccessLayer.DataAccessModel;
using DataAccessLayer.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestSchoolJournal
{
    [TestClass]
    public class TestClassAccess
    {
        public DataAccessClasses ClassesAccess;
        [TestInitialize]
        public void Setup()
        {
            //given
            ClassesAccess = new DataAccessClasses();
        }

        [TestMethod]
        public void TestGetAll()
        {
            // when
            var classes = ClassesAccess.GetAll();

            // then
            int i = 1;
            foreach (var one in classes)
            {
                Assert.AreEqual(one.Id, i);
                ++i;
            }
        }

        [TestMethod]
        public void TestGetById()
        {
            //given
            var num = 1;

            //when
            var admin = ClassesAccess.Get(num);

            //then
            Assert.AreEqual(admin.Id, num);
        }

        [TestMethod]
        public void TestGetNonExistById()
        {
            //given
            var num = 228;

            //when
            var one = ClassesAccess.Get(num);

            //then
            Assert.IsNull(one);
        }

        [TestMethod]
        public void TestWrongClassCreation()
        {
            //given
            var Class = new TheClasses() { TheClass = 11, ClassLetter = "A" };

            //when
            try
            {
                ClassesAccess.Create(Class);
                Assert.Fail();
            }
            catch
            {
                //then
            }
        }

        [TestMethod]
        public void TestClassCreation()
        {
            //////given
            ////var admin = new Admin() { UserId = 1 };

            //////when
            ////try
            ////{
            ////    //then
            ////    AdminsAccess.Create(admin);
            ////}
            ////catch
            ////{
            ////    Assert.Fail();
            ////}
        }

        [TestMethod]
        public void TestUpdateClass()
        {
            //given
            var admin = new TheClasses() { Id = 2, TheClass = 9, ClassLetter = "A" };

            //when
            try
            {
                //then
                ClassesAccess.Update(admin);
            }
            catch
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void TestWrongDeleteClass()
        {
            //given
            var num = 228;

            //when
            try
            {
                ClassesAccess.Delete(num);
                Assert.Fail();
            }
            catch
            {
                
            }
        }

    }
}
