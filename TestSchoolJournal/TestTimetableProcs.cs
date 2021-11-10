using DataAccessLayer.DataAccessModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataAccessLayer.Models;

namespace TestSchoolJournal
{
    //[TestClass]
    public class TestTimetableProcs
    {
        //[TestMethod]
        public void TestTimeTableMethods()
        {
            var tables = new DataAccessTimetables();

            /*var mane_tables = tables.GetAll();
            int i = 0;
            foreach (var item in mane_tables)
            {
                i++;
            }
            Assert.AreEqual(i, 11);*/

            /*var table = tables.Get(3);
            Assert.IsNotNull(table);
            table = tables.Get(33);
            Assert.IsNull(table);*/

            /*Timetable new_user = new Timetable();
            new_user.LessonNumber = 1;
            new_user.SubjectId = 2;
            new_user.ClassId = 1;
            new_user.TeacherId = 1;
            new_user.Monday = false;
            new_user.Tuesday = true;
            new_user.Wednesday = false;
            new_user.Thursday = true;
            new_user.Friday = false;
            tables.Create(new_user);*/

            /*var table = tables.Get(15);
            table.TeacherId = 5;
            tables.Update(table);*/

            //tables.Delete(15);
        }

    }
}
