namespace Insurance.Api.Models
{
    public class ProductDetails
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ProductTypeId { get; set; }
        public string ProductTypeName { get; set; }
        public float SalesPrice { get; set; }
        public float InsuranceValue { get; set; }
        public bool ProductTypeHasInsurance { get; set; }
    }
}
