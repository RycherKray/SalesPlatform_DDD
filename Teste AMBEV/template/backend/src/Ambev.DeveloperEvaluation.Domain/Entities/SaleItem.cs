using Ambev.DeveloperEvaluation.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Domain.Entities
{
    /// <summary>
    /// Entidade interna do Aggregate Root Sale.
    /// Não deve existir de forma independente.
    /// </summary>
    public class SaleItem
    {
        public Guid Id { get; private set; }
        public string Product { get; private set; } = default!;
        public int Quantity { get; private set; }
        public decimal UnitPrice { get; private set; }
        public Discount Discount { get; private set; } = default!;
        public decimal Total { get; private set; }

        protected SaleItem() { }

        internal SaleItem(string product, int quantity, decimal unitPrice, Discount discount)
        {
            Id = Guid.NewGuid();
            Product = product;
            Quantity = quantity;
            UnitPrice = unitPrice;
            Discount = discount;

            Total = discount.Apply(unitPrice, quantity);
        }
    }
}
