namespace ElasticApmNetFrameworkSample.Models.DataTransferObjects.Setter
{
    public class SetProduct : IMappable<DatabaseObjects.Product>
    {
        public string ProductBarcode { get; set; }
        public string ProductName { get; set; }
        public decimal ProductUnitPrice { get; set; }

        public DatabaseObjects.Product Convert()
        {
            DatabaseObjects.Product product = new DatabaseObjects.Product
            {
                ProductName = this.ProductName,
                ProductBarcode = this.ProductBarcode,
                ProductUnitPrice = this.ProductUnitPrice
            };
            return product;
        }
    }
}