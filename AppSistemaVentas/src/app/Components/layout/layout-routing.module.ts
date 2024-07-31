import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LayoutComponent } from './layout.component';
import { DashBoardComponent } from './Pages/dash-board/dash-board.component';
import { UsuariosComponent } from './Pages/usuarios/usuarios.component';
import { ProductosComponent } from './Pages/productos/productos.component';
import { VentasComponent } from './Pages/ventas/ventas.component';
import { HistorialVentasComponent } from './Pages/historial-ventas/historial-ventas.component';
import { ReporteComponent } from './Pages/reporte/reporte.component';

const routes: Routes = [
  {
    path: '',
    component: LayoutComponent,
    children: [
      { path: 'dashboard', component: DashBoardComponent },
      { path: 'usuarios', component: UsuariosComponent },
      { path: 'productos', component: ProductosComponent },
      { path: 'ventas', component: VentasComponent },
      { path: 'historial_ventas', component: HistorialVentasComponent },
      { path: 'reportes', component: ReporteComponent },
    ],
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class LayoutRoutingModule {}
