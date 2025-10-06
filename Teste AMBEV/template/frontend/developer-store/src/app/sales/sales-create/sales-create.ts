import { Component } from '@angular/core';
import { FormArray, FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import { MatSnackBar } from '@angular/material/snack-bar';
import { CommonModule } from '@angular/common';
import { MatExpansionModule } from '@angular/material/expansion';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatFormField, MatLabel } from '@angular/material/form-field';
import { MatDatepicker, MatDatepickerModule } from '@angular/material/datepicker';
import { MatInputModule } from '@angular/material/input';

@Component({
  selector: 'app-sales-create',
  standalone: true,
  imports: [
     CommonModule,
     MatExpansionModule,
     MatCardModule,
     MatButtonModule, 
     MatLabel,
     MatFormField,
     MatInputModule,
     MatDatepickerModule,
     MatDatepicker,     
     ReactiveFormsModule,
     MatIconModule],
  templateUrl: './sales-create.html',
  styleUrls: ['./sales-create.scss'],
})
export class SalesCreate {
  saleForm: FormGroup;
  totalAmount = 0;
  loading = false;

  constructor(private fb: FormBuilder, private http: HttpClient, private snack: MatSnackBar) {
    this.saleForm = this.fb.group({
      customer: ['', Validators.required],
      branch: ['', Validators.required],
      saleDate: [new Date(), Validators.required],
      items: this.fb.array([]),
    });
  }

  get items(): FormArray {
    return this.saleForm.get('items') as FormArray;
  }

  addItem() {
    const item = this.fb.group({
      product: ['', Validators.required],
      quantity: [1, [Validators.required, Validators.min(1)]],
      unitPrice: [0, [Validators.required, Validators.min(0.01)]],
    });
    this.items.push(item);
    this.updateTotal();
  }

  removeItem(index: number) {
    this.items.removeAt(index);
    this.updateTotal();
  }

  updateTotal() {
    this.totalAmount = this.items.value.reduce((sum: number, item: any) => {
      const quantity = item.quantity || 0;
      const price = item.unitPrice || 0;
      return sum + quantity * price;
    }, 0);
  }

  submit() {
    if (this.saleForm.invalid || this.items.length === 0) {
      this.snack.open('Preencha todos os campos e adicione pelo menos um item.', 'Fechar', { duration: 3000 });
      return;
    }

    this.loading = true;
    const payload = this.saleForm.value;

    this.http.post('http://localhost:8080/api/sales', payload).subscribe({
      next: () => {
        this.snack.open('Venda criada com sucesso!', 'Fechar', { duration: 3000 });
        this.saleForm.reset();
        this.items.clear();
        this.totalAmount = 0;
        this.loading = false;
      },
      error: (err) => {
        console.error(err);
        this.snack.open('Erro ao criar venda!', 'Fechar', { duration: 3000 });
        this.loading = false;
      },
    });
  }
}
