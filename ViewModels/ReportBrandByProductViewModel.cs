using Assignment3_Backend.Models;

namespace Assignment3_Backend.ViewModels
{
    public class ReportBrandByProductViewModel
    {
        public string storeBrands { get; set; }
        public List<Product> storeProducts { get; set; } = new List<Product>();

        //custom viewmodel to have the store brands while it also contains the list of products
    }
}
