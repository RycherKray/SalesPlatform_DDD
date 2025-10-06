import { ChangeDetectorRef, Component } from '@angular/core';
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

  constructor(private fb: FormBuilder, private http: HttpClient, private snack: MatSnackBar, private cdr: ChangeDetectorRef) {
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
      let discount = 0;
      if (quantity >= 4 && quantity < 10) discount = 0.1;
      else if (quantity >= 10 && quantity <= 20) discount = 0.2;
      else if (quantity > 20) {
        alert('Não é possível vender mais de 20 unidades do mesmo produto.');
        item.quantity = 20;
      }  

      item.discount = discount;

      const itemTotal = quantity * price * (1 - discount);
      return sum + itemTotal;
    }, 0);
  }

  submit() {
    if (this.saleForm.invalid || this.items.length === 0) {
      this.snack.open('Preencha todos os campos e adicione pelo menos um item.', 'Fechar', { duration: 3000 });
      return;
    }

    this.loading = true;
    this.cdr.detectChanges();
    const payload = this.saleForm.value;

    this.http.post('http://localhost:8080/api/sales', payload).subscribe({
      next: (res) => {
        this.loading = false;
        alert('Venda registrada com sucesso!');
        this.saleForm.reset();
        this.items.clear();        
        this.totalAmount = 0;  
        this.loading = false;
        this.cdr.detectChanges();
      },
      error: (err) => {
        console.error('Erro ao salvar venda:', err);
        alert('Erro ao salvar venda. Verifique os dados e tente novamente.');
        
        this.items.clear();
        this.loading = false;
        this.cdr.detectChanges();        
      },
    });
  }
}
