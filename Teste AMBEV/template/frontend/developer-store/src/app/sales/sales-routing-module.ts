import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { SalesList } from './sales-list/sales-list';
import { SalesCreate } from './sales-create/sales-create';

const routes: Routes = [
  { path: 'list', component: SalesList },
  { path: 'create', component: SalesCreate },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class SalesRoutingModule {}
