using Infa.Domain.Models.SellersProduct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infa.Domain.ViewModels.Products
{
    public class FilterProductsVM
    {
        public string ProductTitle { get; set; }

        public string? SellerId { get; set; }

        public FilterProductState FilterProductState { get; set; }

        public List<Product> Products { get; set; }
    }
    public enum FilterProductState
    {
        All,
        UnderProgress,
        Accepted,
        Rejected,
        Active,
        NotActive
    }
}
