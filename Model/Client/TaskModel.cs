namespace ES_HomeCare_API.Model.Client
{
    public class TaskModel:BaseModel
    {
        public int TaskId { get; set; }
        public string TaskCode { get; set; }
        public string TaskName { get; set; }
        public int TaskDetail { get; set; }
     
    }





    public class ServiceTaskModel : BaseModel
    {
        public int TaskSrvId { get; set; }
        public int TaskId { get; set; }
        public short Frequency { get; set; }
        public string ServiceNote { get; set; }
   

    }



    public class ServiceTaskView
    {
        public int TaskSrvId { get; set; }
        public int UserId { get; set; }
        public string TaskCode { get; set; }
        public string TaskName { get; set; }
        public short Frequency { get; set; }
        public string ServiceNote { get; set; }

    }

}
