INSERT INTO Roles(Nombre) VALUES
('Administrador'),
('Empleado'),
('Supervisor');

INSERT INTO Usuarios(NombreCompleto, Correo, IdRol, Clave) VALUES
('Jaider Cortes', 'jaider@mail.com', 1, '123');

INSERT INTO Categorias(Nombre) VALUES
('Laptops'),
('Monitores'),
('Teclados'),
('Auriculares'),
('Memorias'),
('Accesorios')

INSERT INTO Productos(Nombre,IdCategoria,Stock,Precio,Activo) VALUES
('laptop samsung book pro',1,20,2500,1),
('laptop lenovo idea pad',1,30,2200,1),
('laptop asus zenbook duo',1,30,2100,1),
('monitor teros gaming te-2',2,25,1050,1),
('monitor samsung curvo',2,15,1400,1),
('monitor huawei gamer',2,10,1350,1),
('teclado seisen gamer',3,10,800,1),
('teclado antryx gamer',3,10,1000,1),
('teclado logitech',3,10,1000,1),
('auricular logitech gamer',4,15,800,1),
('auricular hyperx gamer',4,20,680,1),
('auricular redragon rgb',4,25,950,1),
('memoria kingston rgb',5,10,200,1),
('ventilador cooler master',6,20,200,1),
('mini ventilador lenono',6,15,200,1)


INSERT INTO Menus(Nombre,Icono,Url) VALUES
('DashBoard','dashboard','/pages/dashboard'),
('Usuarios','group','/pages/usuarios'),
('Productos','collections_bookmark','/pages/productos'),
('Venta','currency_exchange','/pages/venta'),
('Historial Ventas','edit_note','/pages/historial_venta'),
('Reportes','receipt','/pages/reportes')

INSERT INTO MenuRol(IdMenu, IdRol) VALUES
(1,1),
(2,1),
(3,1),
(4,1),
(5,1),
(6,1)

INSERT INTO MenuRol(IdMenu, IdRol) VALUES
(4,2),
(5,2)

INSERT INTO MenuRol(IdMenu, IdRol) VALUES
(3,3),
(4,3),
(5,3),
(6,3)

INSERT INTO NumerosDocumento(UltimoNumero, FechaRegistro) VALUES
(0,getdate())