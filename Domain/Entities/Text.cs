using Domain.Common;

namespace Domain.Entities
{
    public sealed class Text : Entity
    {
        public Text(int id, string content) : base(id)
        {
            Content = content;
        }

        public string Content { get; set; }
    }
}
