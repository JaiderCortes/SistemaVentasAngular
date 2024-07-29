import { DetalleVenta } from './detalle-venta';

export interface Ventas {
  IdVenta?: number;
  NumeroDocumento?: string;
  TipoPago: string;
  TotalTexto: string;
  FechaRegistro?: string;
  DetalleVenta: DetalleVenta[];
}
