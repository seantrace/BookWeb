using BookWeb.Domain.Contracts;

namespace BookWeb.Domain.Entities
{
    public class Document : AuditableEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsPublic { get; set; } = false;
        public string URL { get; set; }
    }
}