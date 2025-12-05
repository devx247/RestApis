namespace CqrsMediatR.ViewModels
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Typename => nameof(ProductViewModel);
    }
}
