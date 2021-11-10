using DataAccessLayer.DataAccessModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataAccessLayer.Models;

namespace TestSchoolJournal
{
    //[TestClass]
    public class TestUserProcs
    {
        //[TestMethod]
        public void TestUserMethods()
        {
            var users = new DataAccessUsers();
            /*User new_user = new User();
            new_user.FirstName = "Slavik";
            new_user.MiddleName = "Gniloy";
            new_user.LastName = "Shuvak";
            new_user.TheClassesId = null;
            new_user.Phone = "+228332608602";
            new_user.Login = "slavik";
            new_user.Password = "slavikslavik";
            new_user.ImagePath = "c:\\some_path";

            users.Create(new_user);*/

            /*var many_users = users.GetAll();
            int i = 0;
            foreach (var item in many_users)
            {
                i++;
            }
            Assert.AreEqual(i, 23);*/

            /*User new_user1 = users.Get(1025);
            Assert.IsNotNull(new_user1);
            Assert.AreEqual(new_user1.FirstName, "Slavik");

            new_user1.Phone = "+222222222";

            users.Update(new_user1);*/

            /*User new_user = new User();
            new_user.Login = "sergey";
            Assert.AreEqual(users.IsLoginExist(new_user), true);
            new_user.Login = "sergey123";
            Assert.AreEqual(users.IsLoginExist(new_user), false);*/

            //users.Delete(1024);

            /*User new_user = users.GetUserByLoginAndPassword("sergey", "cAFqWt6oNp");
            Assert.IsNotNull(new_user);
            new_user = users.GetUserByLoginAndPassword("sergey", "wrong_pass");
            Assert.IsNull(new_user);*/

            /*User new_user = users.GetUserByLogin("sergey");
            Assert.IsNotNull(new_user);
            new_user = users.GetUserByLogin("non_existing-man");
            Assert.IsNull(new_user);*/

            /*var many_users = users.GetAllUsersByClassId(1);
            Assert.AreEqual(many_users.Count, 13);
            many_users = users.GetAllUsersByClassId(111);
            Assert.AreEqual(many_users.Count, 0);
            many_users = users.GetAllUsersByClassId(2);
            Assert.AreEqual(many_users.Count, 1);*/

            /*User new_user = users.GetUserByName("Сергей", "Пискун", "Толя");
            Assert.IsNotNull(new_user);
            new_user = users.GetUserByName("sergey", "123", "2222");
            Assert.IsNull(new_user);*/

        }

    }
}
