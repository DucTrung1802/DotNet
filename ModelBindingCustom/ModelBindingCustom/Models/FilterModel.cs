namespace ModelBinding.Models
{
    public class FilterModel
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public List<string> Interests { get; set; } = new List<string>();
    }
}