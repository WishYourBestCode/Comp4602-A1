using System.Collections.Generic;

namespace Blog.ViewModels
{
    public class AdminUserViewModel
    {
        public string? Id { get; set; }
        public string? Email { get; set; }
        public bool IsApproved { get; set; } = false;
        public IList<string> Roles { get; set; } = new List<string>();
    }
}
