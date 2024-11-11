import { Routes } from '@angular/router';
export const routes: Routes = [
  { path: '', redirectTo: '/home', pathMatch: 'full' },
  {
    path: 'admin',
    loadChildren: () =>
      import('./admin/admin.routes').then((m) => m.ADMIN_ROUTES),
  },
  {
    path: 'home',
    loadChildren: () => import('./home/home.routes').then((m) => m.HOME_ROUTES),
  },
  {
    path: 'cart',
    loadChildren: () => import('./cart/cart.routes').then((m) => m.CART_ROUTES),
  },
  {
    path: 'order',
    loadChildren: () =>
      import('./order/order.routes').then((m) => m.ORDER_ROUTES),
  },
];
