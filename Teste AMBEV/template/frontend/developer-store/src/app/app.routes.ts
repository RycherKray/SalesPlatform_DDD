import { Routes } from '@angular/router';
import { provideRouter } from '@angular/router';

export const routes: Routes = [
  {
    path: 'sales',
    loadChildren: () =>
      import('./sales/sales.routes').then((m) => m.SALES_ROUTES)
  },
  {
    path: '',
    redirectTo: 'sales',
    pathMatch: 'full'
  }
];
