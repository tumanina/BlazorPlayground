namespace BlazorPlayground.Web.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<Department> Departments { get; set; }
    }
}
