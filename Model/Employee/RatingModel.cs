namespace ES_HomeCare_API.Model.Employee
{
    public class RatingModel
    {
        public long RatingId { get; set; }
        public long UserId { get; set; }
        public bool Star1 { get; set; }
        public bool Star2 { get; set; }
        public bool Star3 { get; set; }
        public bool Star4 { get; set; }
        public bool Star5 { get; set; }
    }
}
