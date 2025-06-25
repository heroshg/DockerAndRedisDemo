namespace DockerAndRedisDemo
{
    public class Product
    {
        public Product(string name, decimal price, string description)
        {
            Id = Guid.NewGuid();
            Name = name;
            Price = price;
            Description = description;
        }

        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public decimal Price { get; private set; }
        public string Description { get; private set; }
    }
}
