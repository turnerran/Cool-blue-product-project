using System.ComponentModel.DataAnnotations;

namespace Insurance.Api.Models.Requests
{
    public class OrderCreateRequest
    {
        [Required]
        [Range(0, long.MaxValue, ErrorMessage = "The field {0} must be greater than {1}.")]
        public long AccountId { get; set; }
    }
}
