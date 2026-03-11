using Tools.Domain.Exceptions;

namespace Tools.Domain.Entities
{
    public class Tag
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
        public ICollection<Tool> Tools { get; set; } = new List<Tool>();

        public Tag() { }

        public Tag(string name)
        {

            Id = Guid.NewGuid();
            Name = ValidateTag(name);
        }

        private static string ValidateTag(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new DomainException("tag.name.required", "Nome da tag é obrigatório.");

            name = name.Trim().ToLower();

            if (name.Length > 50)
                throw new DomainException("tag.name.maxLength", "Nome da tag deve conter no máximo 50 caracteres.");

            return name;
        }

    }
}
