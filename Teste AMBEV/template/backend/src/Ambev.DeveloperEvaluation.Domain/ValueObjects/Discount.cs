using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Domain.ValueObjects
{
    public record Discount(decimal Percentage)
    {
        public static readonly Discount None = new(0m);
        public static readonly Discount TenPercent = new(0.10m);
        public static readonly Discount TwentyPercent = new(0.20m);

        /// <summary>
        /// Calcula o valor final de um preço com este desconto aplicado.
        /// </summary>
        public decimal Apply(decimal unitPrice, int quantity)
        {
            return quantity * unitPrice * (1 - Percentage);
        }

        public override string ToString() => $"{Percentage:P0}";
    }
}
