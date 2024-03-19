namespace AutomapperDemo.Models
{
    // DTO
    public class CategoryDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<ProductDTO> Products { get; set; }
    }
}