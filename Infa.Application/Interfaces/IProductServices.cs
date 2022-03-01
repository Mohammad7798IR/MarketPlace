using Infa.Domain.Models.Store;
using Infa.Domain.ViewModels.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infa.Application.Interfaces
{
    public interface IProductServices
    {
        Task<FilterProductsVM> filterProducts(FilterProductsVM filterProductsVM);

    
    }
}
