import { Routes } from '@angular/router';
import { LoginComponent } from './Components/login/login.component';

export const routes: Routes = [
  { path: '', component: LoginComponent },
  { path: 'login', component: LoginComponent },
  {
    path: 'pages',
    loadChildren: () =>
      import('./Components/layout/layout.module').then((m) => m.LayoutModule),
  },
  { path: '**', redirectTo: 'login' },
];
