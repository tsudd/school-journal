namespace DataAccessLayer.Models
{
    public class Mark
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int TimeTableId { get; set; }
        public int SelectedIndex { get; set; }
        public string Date { get; set; }


        public virtual User User { get; set; }
    }
}
