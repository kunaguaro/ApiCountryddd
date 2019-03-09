
namespace WebApi.Domain.Entities
{
    public class Country : BaseEntity
    {
        public string Name { get; set; }
        public int Population { get; set; }
        public decimal Area { get; set; }
        public string ISO3166 { get; set; }
        public string DrivingSide { get; set; }
        public string Capital { get; set; }

    }
}
