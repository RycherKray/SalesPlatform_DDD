using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Domain.Entities
{
    /// <summary>
    /// Aggregate Root que representa uma Venda no domínio.
    /// Responsável por controlar regras de negócio e ciclo de vida dos itens.
    /// </summary>
    public class Sale
    {
        public Guid Id { get; private set; }
        public string SaleNumber { get; private set; } = default!;
        public DateTime SaleDate { get; private set; }
        public string Customer { get; private set; } = default!;
        public string Branch { get; private set; } = default!;
        public decimal TotalAmount { get; private set; }
        public bool Cancelled { get; private set; }

        private readonly List<SaleItem> _items = new();
        public IReadOnlyCollection<SaleItem> Items => _items.AsReadOnly();

        protected Sale() { }
            

        public Sale(DateTime saleDate, string customer, string branch)
        {
            Id = Guid.NewGuid();
            SaleNumber = $"SL-{DateTime.UtcNow:yyyyMMdd}-{Guid.NewGuid().ToString()[..5].ToUpper()}";        
            SaleDate = saleDate;
            Customer = customer;
            Branch = branch;
            Cancelled = false;
        }

        public void AddItem(string product, int quantity, decimal unitPrice)
        {
            if (quantity > 20)
                throw new InvalidOperationException("Não é permitido vender mais que 20 itens iguais.");

            var discount = CalculateDiscount(quantity);
            var item = new SaleItem(product, quantity, unitPrice, discount);

            _items.Add(item);
            RecalculateTotal();
        }

        public void Cancel()
        {
            Cancelled = true;
        }

        private void RecalculateTotal()
        {
            TotalAmount = _items.Sum(i => i.Total);
        }

        private decimal CalculateDiscount(int quantity)
        {
            if (quantity >= 10 && quantity <= 20)
                return 0.20m;
            if (quantity >= 4)
                return 0.10m;
            return 0m;
        }
    }
}
