using System.ComponentModel.DataAnnotations;




namespace Infa.Domain.Models.SellersProduct
{
    public class ProductColor : BaseEntity
    {
        #region properties

        public string ProductId { get; set; }

        [Display(Name = "رنگ")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
        public string ColorName { get; set; }

        public int Price { get; set; }

        #endregion

        #region relations

        public Product Product { get; set; }

        #endregion
    }
}
