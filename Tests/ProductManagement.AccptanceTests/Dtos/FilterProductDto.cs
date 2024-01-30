
namespace ProductManagement.AccptanceTests.Dtos
{
    public class FilterProductDto
    {
        public string Name { get; set; }
        public DateTime ProduceDate { get; set; }
        public string ManufacturePhone { get; set; }
        public string ManufactureEmail { get; set; }
        public bool IsAvailable { get; set; }
        public string CreatorName { get; set; }
    }
}
