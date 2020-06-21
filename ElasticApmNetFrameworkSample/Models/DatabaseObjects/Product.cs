using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ElasticApmNetFrameworkSample.Models.DatabaseObjects
{
    public class Product : IMappable<DataTransferObjects.Getter.GetProduct>
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? ProductId { get; set; }
        public string ProductBarcode { get; set; }
        [Required]
        public string ProductName { get; set; }
        [Required]
        public decimal ProductUnitPrice { get; set; }

        public DataTransferObjects.Getter.GetProduct Convert()
        {
            DataTransferObjects.Getter.GetProduct damProduct = new DataTransferObjects.Getter.GetProduct
            {
                ProductBarcode = this.ProductBarcode,
                ProductName = this.ProductName,
                ProductUnitPrice = this.ProductUnitPrice
            };
            return damProduct;
        }
    }
}