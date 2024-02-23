namespace Ecaq.Models
{
    public class CityRoot
    {
        public List<Cities> cities { get; set; }
    }
    public class Cities
    {
        public string cityName { get; set; }
        public string country { get; set; }
        public string emoji { get; set; }
        public DateTime date { get; set; }
        public string notes { get; set; }
        public Position position { get; set; }
        public int id { get; set; }
    }

}
