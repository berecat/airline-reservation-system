using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using dbo = ElasticApmNetFrameworkSample.Models.DatabaseObjects;
using dtoG = ElasticApmNetFrameworkSample.Models.DataTransferObjects.Getter;
using dtoS = ElasticApmNetFrameworkSample.Models.DataTransferObjects.Setter;

namespace ElasticApmNetFrameworkSample.Models.DataAccessObjects
{
    public class ProductService
    {
        private string _connectionString;
        private DbConnection _dbConnection;
        public ProductService(string connectionString)
        {
            this._connectionString = connectionString;
        }
        public ProductService(DbConnection dbConnection)
        {
            this._dbConnection = dbConnection;
        }
        public async Task AddProductAsync(dtoS.SetProduct product)
        {
            using (var context = new OrderDbContext(this._connectionString))
            {
                dbo.Product dboProduct = product.Convert();
                _ = context.Products.Add(dboProduct);
                _ = await context.SaveChangesAsync();
            }
        }
        public async Task<IEnumerable<dtoG.GetProduct>> GetProducts()
        {
            using (var context = new OrderDbContext(this._connectionString))
            {
                IEnumerable<dbo.Product> dboProducts = await context.Products.ToListAsync();
                IEnumerable<dtoG.GetProduct> dtoProducts = dboProducts.ConvertAll<dbo.Product, dtoG.GetProduct>();
                return dtoProducts;
            }
        }
        public IEnumerable<dtoG.GetProduct> GetProductsByName(string productName)
        {
            using (var context = new OrderDbContext(this._connectionString))
            {
                IEnumerable<dbo.Product> dboProducts = context.Products.Where(x => x.ProductName == productName);
                IEnumerable<dtoG.GetProduct> dtoProducts = dboProducts.ConvertAll<dbo.Product, dtoG.GetProduct>();
                return dtoProducts;
            }
        }

        public async Task<dtoG.GetProduct> GetProductByBarcodeAsync(string barcode)
        {
            using (var context = new OrderDbContext(this._connectionString))
            {
                dbo.Product dboProduct = await context.Products.FirstOrDefaultAsync(x => x.ProductBarcode == barcode);
                dtoG.GetProduct dtoProduct = dboProduct.Convert();
                return dtoProduct;
            }
        }
    }
}