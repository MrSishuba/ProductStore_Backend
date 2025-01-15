using Assignment3_Backend.Models;
using Assignment3_Backend.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Assignment3_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly IRepository _repository;
        public ReportController(IRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        [Route("fetchReportInfo")]
        public async Task<IActionResult> fetchReportData()
        {
            try
            {
                var brands = await _repository.GetBrandsAsync();
                var products = await _repository.GetProductsAsync();
                var productTypes = await _repository.GetProductTypesAsync();
                DataViewModel reportData = new DataViewModel();

                // Product by Count
                reportData.ProductCountByBrand = brands.Select(x => new ReportViewModel
                {
                    chartLabels = x.Name,
                    chartData = products.Where(y => y.BrandId == x.BrandId).Count()
                }).ToList();


                // Product count by product Type 
                reportData.ProductCountByProductType = productTypes.GroupBy(x => x.ProductTypeId).Select(y => new ReportViewModel
                {
                    chartLabels = y.Where(z => z.ProductTypeId == y.Key)?.FirstOrDefault()?.Name,
                    chartData = y.Count()
                }).ToList();


                // Active Products Report
                reportData.ActiveProductReport = products.Where(x => x.IsActive == true).Select(y => new ReportBrandByProductViewModel
                {
                    storeBrands = y.Name,
                    storeProducts = products.Where(x => x.BrandId == y.BrandId).ToList()
                }).ToList();

                return Ok(reportData);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }


        }



    }

    //[HttpGet]
    //[Route("fetchReportInfo")]
    //public async Task<IActionResult> fetchReportData()
    //{
    //    try
    //    {
    //        var brands = await _repository.GetBrandsAsync();
    //        var products = await _repository.GetProductsAsync();
    //        var productTypes = await _repository.GetProductTypesAsync();
    //        DataViewModel reportData = new DataViewModel();

    //        // Product by Count
    //        reportData.ProductCountByBrand = brands.Select(x => new ReportViewModel
    //        {
    //            chartLabels = x.Name,
    //            chartData = products.Where(y => y.BrandId == x.BrandId).Count()
    //        }).ToList();

    //        // Product count by product Type 
    //        reportData.ProductCountByProductType = productTypes.GroupBy(x => x.ProductTypeId).Select(y => new ReportViewModel
    //        {
    //            chartLabels = y.Where(z => z.ProductTypeId == y.Key)?.FirstOrDefault()?.Name,
    //            chartData = y.Count()
    //        }).ToList();

    //        // Active Products Report
    //        foreach (var item in products)
    //        {
    //            // Set Product Type
    //            var productTypeInDb = productTypes.Where(x => x.ProductTypeId == item.ProductTypeId).FirstOrDefault();
    //            ProductType productType = new ProductType();
    //            productType.ProductTypeId = productTypeInDb.ProductTypeId;
    //            productType.Name = productTypeInDb.Name;
    //            productType.Description = productTypeInDb.Description;
    //            productType.DateCreated = productTypeInDb.DateCreated;
    //            productType.DateModified = productTypeInDb.DateModified;
    //            productType.IsActive = productTypeInDb.IsActive;
    //            productType.IsDeleted = productTypeInDb.IsDeleted;
    //            item.ProductType = productType;

    //            // Set Brand
    //            var brandInDb = brands.Where(x => x.BrandId == item.BrandId).FirstOrDefault();
    //            Brand brand = new Brand();
    //            brand.BrandId = brandInDb.BrandId;
    //            brand.Name = brandInDb.Name;
    //            brand.Description = brandInDb.Description;
    //            brand.DateCreated = brandInDb.DateCreated;
    //            brand.DateModified = brandInDb.DateModified;
    //            brand.IsActive = brandInDb.IsActive;
    //            brand.IsDeleted = brandInDb.IsDeleted;
    //            item.Brand = brand;
    //        }

    //        reportData.ActiveProductReport = products.Where(x => x.IsActive == true).Select(y => new ReportBrandByProductViewModel
    //        {
    //            storeBrands = y.Name,
    //            storeProducts = products.Where(x => x.BrandId == y.BrandId).ToList()
    //        }).ToList();

    //        return Ok(reportData);
    //    }
    //    catch (Exception ex)
    //    {
    //        return BadRequest(ex.Message);
    //    }
    //}


    //    [HttpGet]
    //    [Route("fetchReportInfo")]
    //    public async Task<IActionResult> fetchReportData()
    //    {
    //        try
    //        {
    //            var brands = await _repository.GetBrandsAsync();
    //            var products = await _repository.GetProductsAsync();
    //            var productTypes = await _repository.GetProductTypesAsync();
    //            DataViewModel reportData = new DataViewModel();

    //            // Product by Count
    //            reportData.ProductCountByBrand = brands.Select(x => new ReportViewModel
    //            {
    //                chartLabels = x.Name,
    //                chartData = products.Where(y => y.BrandId == x.BrandId).Count()
    //            }).ToList();

    //            // Product count by product Type 
    //            reportData.ProductCountByProductType = productTypes.GroupBy(x => x.ProductTypeId).Select(y => new ReportViewModel
    //            {
    //                chartLabels = y.Where(z => z.ProductTypeId == y.Key)?.FirstOrDefault()?.Name,
    //                chartData = y.Count()
    //            }).ToList();

    //            // Active Products Report
    //            foreach (var item in products)
    //            {
    //                // Set Product Type
    //                var productTypeInDb = productTypes.FirstOrDefault(x => x.ProductTypeId == item.ProductTypeId);
    //                if (productTypeInDb != null)
    //                {
    //                    ProductType productType = new ProductType
    //                    {
    //                        ProductTypeId = productTypeInDb.ProductTypeId,
    //                        Name = productTypeInDb.Name,
    //                        Description = productTypeInDb.Description,
    //                        DateCreated = productTypeInDb.DateCreated,
    //                        DateModified = productTypeInDb.DateModified,
    //                        IsActive = productTypeInDb.IsActive,
    //                        IsDeleted = productTypeInDb.IsDeleted
    //                    };
    //                    item.ProductType = productType;
    //                }
    //                else
    //                {
    //                    // Log the issue for debugging
    //                    Console.WriteLine($"ProductType with ID {item.ProductTypeId} not found.");
    //                    continue; // Skip this item if the ProductType is not found
    //                }

    //                // Set Brand
    //                var brandInDb = brands.FirstOrDefault(x => x.BrandId == item.BrandId);
    //                if (brandInDb != null)
    //                {
    //                    Brand brand = new Brand
    //                    {
    //                        BrandId = brandInDb.BrandId,
    //                        Name = brandInDb.Name,
    //                        Description = brandInDb.Description,
    //                        DateCreated = brandInDb.DateCreated,
    //                        DateModified = brandInDb.DateModified,
    //                        IsActive = brandInDb.IsActive,
    //                        IsDeleted = brandInDb.IsDeleted
    //                    };
    //                    item.Brand = brand;
    //                }
    //                else
    //                {
    //                    // Log the issue for debugging
    //                    Console.WriteLine($"Brand with ID {item.BrandId} not found.");
    //                    continue; // Skip this item if the Brand is not found
    //                }
    //            }

    //            reportData.ActiveProductReport = products.Where(x => x.IsActive).Select(y => new ReportBrandByProductViewModel
    //            {
    //                storeBrands = y.Name,
    //                storeProducts = products.Where(x => x.BrandId == y.BrandId).ToList()
    //            }).ToList();

    //            return Ok(reportData);
    //        }
    //        catch (Exception ex)
    //        {
    //            return BadRequest(ex.Message);
    //        }
    //    }

    //}
}
