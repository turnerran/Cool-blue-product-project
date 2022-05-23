using System.ComponentModel.DataAnnotations;

namespace Insurance.Api.Models.Requests
{
    public class InsuranceRequest
    {
        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "The field {0} must be greater than {1}.")]
        public int ProductId { get; set; }
    }
}
