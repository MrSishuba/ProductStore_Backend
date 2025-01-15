namespace Assignment3_Backend.ViewModels
{
    public class DataViewModel
    {
        public List<ReportViewModel> ProductCountByBrand { get; set; }

        public List<ReportViewModel> ProductCountByProductType { get; set; }

        public List<ReportBrandByProductViewModel> ActiveProductReport { get; set; }


        //This view model will hold the report data to use for the chart but first have to get the count of each reqiriement
        //prodcut by brand count and product by product count 
        //Also will include/ add a list from of the report brnd by product view model and have an active product report to filter
    }
}
