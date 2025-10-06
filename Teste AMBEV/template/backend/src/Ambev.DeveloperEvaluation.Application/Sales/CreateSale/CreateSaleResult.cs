using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale
{
    public class CreateSaleResult
    {
        public Guid Id { get; set; }
        public string SaleNumber { get; set; } = string.Empty;
        public string Customer { get; set; } = string.Empty;
        public string Branch { get; set; } = string.Empty;
        public DateTime Date { get; set; }        

        public List<SaleItemResult> Items { get; set; } = new();
    }

    public class SaleItemResult
    {
        public string Product { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Discount { get; set; }       
    }
}
