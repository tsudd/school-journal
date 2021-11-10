using DataAccessLayer.DataAccessModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataAccessLayer.Models;

namespace TestSchoolJournal
{
    [TestClass]
    public class TestTeachersProcs
    {
        [TestMethod]
        public void TestTeacherMethods()
        {
            var teachers = new DataAccessTeachers();

            /*var many_teacher = teachers.GetAll();
            int i = 0;
            foreach (var item in many_teacher)
            {
                i++;
            }
            Assert.AreEqual(i, 9);*/

            /*var teacher = teachers.Get(3);
            Assert.IsNotNull(teacher);
            teacher = teachers.Get(33);
            Assert.IsNull(teacher);*/

            /*Teacher new_teacher = new Teacher();
            new_teacher.UserId = 1;
            new_teacher.SubjectId = 4;
            teachers.Create(new_teacher);*/

            /*Teacher teacher = teachers.Get(10);
            teacher.SubjectId = 9;
            teachers.Update(teacher);*/

            teachers.Delete(10);
        }
    }
}
