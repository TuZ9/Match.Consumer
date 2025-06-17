namespace Suitability.Consumer.Domain.Dto
{
    public class ProductDto
    {
        public required AppList applist { get; set; }
    }
    public class AppList
    {
        public required IEnumerable<Apps> apps { get; set; }
    }
    public class Apps
    {
        public int appid { get; set; }
        public string? name { get; set; }
    }
}
