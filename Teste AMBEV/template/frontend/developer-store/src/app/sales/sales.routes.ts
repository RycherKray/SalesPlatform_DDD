import { Routes } from '@angular/router';
import { SalesList } from './sales-list/sales-list';
import { SalesCreate } from './sales-create/sales-create';
import { Home } from '../home/home'; 

export const SALES_ROUTES: Routes = [
  { path: '', component: Home },
  { path: 'list', component: SalesList },
  { path: 'create', component: SalesCreate },
];