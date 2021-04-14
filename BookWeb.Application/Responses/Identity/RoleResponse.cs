using System.ComponentModel.DataAnnotations;

namespace BookWeb.Application.Responses.Identity
{
    public class RoleResponse
    {
        public string Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}