namespace Products.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Items> Items{ get; set; }
    }
}
 