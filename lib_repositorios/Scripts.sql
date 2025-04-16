CREATE
DATABASE RetroBusters
GO

USE RetroBusters
GO

CREATE TABLE Almacenes
(
    Id_bodega        INT PRIMARY KEY IDENTITY(1,1),
    Ubicacion_bodega NVARCHAR(100),
    Capacidad_bodega DECIMAL(18, 2)
);

CREATE TABLE Miembros
(
    Id_miembros    INT PRIMARY KEY IDENTITY(1,1),
    Nombre         NVARCHAR(50),
    Fecha_registro SMALLDATETIME,
    Nivel          NVARCHAR(50),
    Puntos         INT NOT NULL
);

CREATE TABLE Empleados
(
    Id_empleados       INT PRIMARY KEY IDENTITY(1,1),
    Nombre_empleado    NVARCHAR(100),
    Cargo_empleado     NVARCHAR(50),
    Sueldo             DECIMAL(18, 2),
    Fecha_contratacion SMALLDATETIME
);

CREATE TABLE Peliculas
(
    Id_pelicula     INT PRIMARY KEY IDENTITY(1,1),
    Nombre_pelicula NVARCHAR(100),
    Genero_Pelicula NVARCHAR(50),
    Fecha_Estreno   SMALLDATETIME,
    Estado_pelicula BIT NOT NULL
);

CREATE TABLE Snacks
(
    Id_Snack INT PRIMARY KEY IDENTITY(1,1),
    Nombre   NVARCHAR(100),
    Precio   DECIMAL(10, 2),
    Stock    INT
);

CREATE TABLE Consolas
(
    Id_consola     INT PRIMARY KEY IDENTITY(1,1),
    Tipo_consola   NVARCHAR(100),
    Marca_consola  NVARCHAR(50),
    Estado_consola INT NOT NULL,
    Estado_string  NVARCHAR(50),
    almacen        INT,
    FOREIGN KEY (almacen) REFERENCES Almacenes (Id_bodega)
);

CREATE TABLE Envios
(
    Id_envios      INT PRIMARY KEY IDENTITY(1,1),
    Estado         NVARCHAR(50),
    Direccion      NVARCHAR(200),
    Transportadora NVARCHAR(50) NULL,
    empleados      INT NOT NULL,
    FOREIGN KEY (empleados) REFERENCES Empleados (Id_empleados)
);

CREATE TABLE Reservas
(
    Id_Reserva    INT PRIMARY KEY IDENTITY(1,1),
    Fecha_Reserva SMALLDATETIME,
    Estado        NVARCHAR(100) NULL,
    MiembroId     INT,
    PeliculaId    INT,
    ConsolaId     INT,
    EmpleadoId    INT,
    FOREIGN KEY (MiembroId) REFERENCES Miembros (Id_miembros),
    FOREIGN KEY (PeliculaId) REFERENCES Peliculas (Id_pelicula),
    FOREIGN KEY (ConsolaId) REFERENCES Consolas (Id_consola),
    FOREIGN KEY (EmpleadoId) REFERENCES Empleados (Id_empleados)
);

CREATE TABLE Reservas_Snacks
(
    Id_Reservas_Snacks INT PRIMARY KEY IDENTITY(1,1),
    SnackId            INT NOT NULL,
    Reserva            INT NOT NULL,
    FOREIGN KEY (SnackId) REFERENCES Snacks (Id_Snack),
    FOREIGN KEY (Reserva) REFERENCES Reservas (Id_Reserva)
);

-- Almacenes
INSERT INTO Almacenes (Ubicacion_bodega, Capacidad_bodega)
VALUES ('Bodega Central, Calle Principal 123', 1500.50),
       ('Almacén Norte, Avenida Industrial 45', 2000.00),
       ('Centro Logístico Sur, Carrera 78 #65-21', 3000.75),
       ('Depósito Este, Zona Franca 12', 1800.25),
       ('Bodega Occidental, Polígono Industrial 9', 2500.00),
       ('Almacén Portuario, Muelle 4', 4000.50),
       ('Centro Distribución Capital', 3500.00),
       ('Bodega Tecnológica, Parque de Innovación', 2750.30);

-- Miembros
INSERT INTO Miembros (Nombre, Fecha_registro, Nivel, Puntos)
VALUES ('Juan Pérez', '2023-01-15 09:00', 'Oro', 500),
       ('María García', '2023-02-20 14:30', 'Plata', 250),
       ('Pedro López', '2023-03-10 10:15', 'Bronce', 100),
       ('Ana Martínez', '2023-04-05 16:45', 'Oro', 750),
       ('Carlos Rodríguez', '2023-05-12 11:20', 'Plata', 300),
       ('Laura Sánchez', '2023-06-18 08:00', 'Oro', 600),
       ('Diego Fernández', '2023-07-22 13:10', 'Bronce', 150),
       ('Sofía González', '2023-08-30 17:30', 'Plata', 400);

-- Empleados
INSERT INTO Empleados (Nombre_empleado, Cargo_empleado, Sueldo, Fecha_contratacion)
VALUES ('Roberto Jiménez', 'Gerente', 4500.00, '2020-01-10 08:00'),
       ('Mónica Vega', 'Asistente', 2500.50, '2021-05-15 09:30'),
       ('Fernando Cruz', 'Técnico', 3000.00, '2022-03-20 07:45'),
       ('Patricia Ruiz', 'Supervisor', 3800.75, '2019-11-05 10:00'),
       ('Jorge Mendoza', 'Almacenista', 2800.00, '2023-02-28 08:15'),
       ('Lucía Herrera', 'Atención al cliente', 2700.25, '2020-07-12 09:00'),
       ('Ricardo Castro', 'Mantenimiento', 2900.50, '2021-09-01 07:30'),
       ('Valeria Ortega', 'Logística', 3200.00, '2022-12-10 08:45');

-- Peliculas
INSERT INTO Peliculas (Nombre_pelicula, Genero_Pelicula, Fecha_Estreno, Estado_pelicula)
VALUES ('Viaje al Espacio', 'Ciencia Ficción', '2023-01-01 00:00', 1),
       ('Risas Eternas', 'Comedia', '2023-02-14 00:00', 1),
       ('Misterio en París', 'Suspenso', '2022-12-25 00:00', 0),
       ('Aventuras Submarinas', 'Acción', '2023-03-08 00:00', 1),
       ('Amor en Verona', 'Romance', '2023-04-20 00:00', 1),
       ('Zombie Apocalypse', 'Terror', '2023-05-30 00:00', 0),
       ('Héroes Galácticos', 'Animación', '2023-06-15 00:00', 1),
       ('Drama Familiar', 'Drama', '2023-07-04 00:00', 1);

-- Snacks
INSERT INTO Snacks (Nombre, Precio, Stock)
VALUES ('Palomitas Grandes', 5.99, 200),
       ('Refresco 500ml', 3.50, 300),
       ('Nachos con Queso', 6.75, 150),
       ('Chocolatina', 2.25, 500),
       ('Agua Mineral', 2.00, 400),
       ('Hot Dog', 4.99, 250),
       ('Helado', 3.75, 180),
       ('Mix de Dulces', 7.25, 120);

-- Consolas
INSERT INTO Consolas (Tipo_consola, Marca_consola, Estado_consola, almacen)
VALUES ('PlayStation 5', 'Sony', 1, 1),
       ('Xbox Series X', 'Microsoft', 1, 2),
       ('Nintendo Switch', 'Nintendo', 2, 3),
       ('PC Gaming', 'Alienware', 1, 4),
       ('PlayStation 4', 'Sony', 3, 5),
       ('Xbox One', 'Microsoft', 1, 6),
       ('Steam Deck', 'Valve', 2, 7),
       ('Arcade Cabinet', 'Capcom', 4, 8);

-- Envios
INSERT INTO Envios (Estado, Direccion, Transportadora, empleados)
VALUES ('En tránsito', 'Calle 123 #45-67', 'DHL', 1),
       ('Entregado', 'Avenida Principal 789', 'FedEx', 2),
       ('En preparación', 'Carrera 56 #12-34', NULL, 3),
       ('En camino', 'Diagonal 78B #9-10', 'Servientrega', 4),
       ('Recibido en bodega', 'Transversal 23 #45-67', 'Interrapidísimo', 5),
       ('Retrasado', 'Calle 90 #10-20', 'Coordinadora', 6),
       ('Entregado', 'Avenida Norte 123-45', 'DHL', 7),
       ('En tránsito', 'Carrera 11 #22-33', 'FedEx', 8);

-- Reservas
INSERT INTO Reservas (Fecha_Reserva, Estado, MiembroId, PeliculaId, ConsolaId, EmpleadoId)
VALUES ('2023-10-01 14:00', 'Activa', 1, 1, NULL, 1),
       ('2023-10-02 16:30', 'Completada', 2, NULL, 2, 2),
       ('2023-10-03 10:15', 'Pendiente', 3, 3, 3, 3),
       ('2023-10-04 11:00', 'Cancelada', 4, 4, NULL, 4),
       ('2023-10-05 09:45', 'Activa', 5, NULL, 5, 5),
       ('2023-10-06 17:20', 'En proceso', 6, 6, 6, 6),
       ('2023-10-07 15:30', 'Completada', 7, 7, NULL, 7),
       ('2023-10-08 12:00', 'Activa', 8, NULL, 8, 8);

-- Reservas_Snacks
INSERT INTO Reservas_Snacks (SnackId, Reserva)
VALUES (1, 1),
       (2, 2),
       (3, 3),
       (4, 4),
       (5, 5),
       (6, 6),
       (7, 7),
       (8, 8);