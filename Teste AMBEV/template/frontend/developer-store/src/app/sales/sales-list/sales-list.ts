import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { CommonModule } from '@angular/common';
import { MatExpansionModule } from '@angular/material/expansion';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';

interface SaleItem {
  product: string;
  quantity: number;
  unitPrice: number;
  total: number;
}

interface Sale {
  id: string;
  saleNumber: string;
  customer: string;
  branch: string;
  totalAmount: number;
  cancelled: boolean;
  items: SaleItem[];
}

@Component({
  selector: 'app-sales-list',
  standalone: true,
  imports: [CommonModule, MatExpansionModule, MatCardModule, MatButtonModule, MatIconModule],
  templateUrl: './sales-list.html',
  styleUrls: ['./sales-list.scss'],
})
export class SalesList implements OnInit {
  sales: Sale[] = [];
  loading = false;

  constructor(private http: HttpClient) {}

  ngOnInit() {
    this.fetchSales();
  }

  fetchSales() {
    this.loading = true;
    this.http.get<any>('http://localhost:8080/api/sales').subscribe({
      next: (res) => {
        console.log('Retorno da API:', res);
        this.sales = res.data?.data || res.data || [];
        this.loading = false;
      },
      error: (err) => {
        console.error('Erro ao buscar vendas:', err);
        this.loading = false;
      },
    });
  }

  cancelSale(id: string) {
    if (confirm('Tem certeza que deseja cancelar esta venda?')) {
      this.http.patch(`http://localhost:8080/api/sales/${id}/cancel`, {}).subscribe({
        next: () => {
          this.sales = this.sales.map(s =>
            s.id === id ? { ...s, cancelled: true } : s
          );
        },
        error: (err) => console.error('Erro ao cancelar venda:', err),
      });
    }
  }
}
