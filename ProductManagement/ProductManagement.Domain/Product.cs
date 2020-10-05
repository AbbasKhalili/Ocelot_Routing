using System;

namespace ProductManagement.Domain
{
    public class Product
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }

        public Product(string name, string description)
        {
            Id = Guid.NewGuid();
            SetProperties(name, description);
        }

        public void Update(string name, string description)
        {
            SetProperties(name, description);
        }

        private void SetProperties(string name, string description)
        {
            Name = name;
            Description = description;
        }
    }
}
