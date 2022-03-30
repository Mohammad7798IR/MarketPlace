using Infa.Domain.Models.SellersProduct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infa.Domain.ViewModels.Products
{
    public class EditProductVM : CreateProductVM
    {
        public string Id { get; set; }

        public List<ProductColor> Colors { get; set; }


        public List<ProductCategory> Categories { get; set; }

        public string Image { get; set; }
    }
}
