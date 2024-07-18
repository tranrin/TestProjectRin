namespace TestBackEnd.Domain
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }


    }
    public class CategoryRequest
    {
        public string Name { get; set; }
    }
}