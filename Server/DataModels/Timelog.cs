namespace Server.DataModels
{
    public class TimeLog
{
    public int TimeLogId { get; set; }
    public int UserId { get; set; }
    public int ProjectId { get; set; }
    public DateTime Date { get; set; }
    public float HoursWorked { get; set; }
}

}