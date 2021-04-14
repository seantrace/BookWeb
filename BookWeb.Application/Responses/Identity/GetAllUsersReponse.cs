using System.Collections.Generic;

namespace BookWeb.Application.Responses.Identity
{
    public class GetAllUsersReponse
    {
        public IEnumerable<UserResponse> Users { get; set; }
    }
}