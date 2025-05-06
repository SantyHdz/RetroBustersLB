CREATE
DATABASE RetroBusters
GO

USE RetroBusters
GO

CREATE TABLE Almacenes
(
    Id        INT PRIMARY KEY IDENTITY(1,1),
    Ubicacion NVARCHAR(100),
    Capacidad DECIMAL(18, 2)
);

CREATE TABLE Miembros
(
    Id             INT PRIMARY KEY IDENTITY(1,1),
    Nombre         NVARCHAR(50),
    Fecha_registro SMALLDATETIME,
    Nivel          NVARCHAR(50),
    Puntos         INT NOT NULL
);

CREATE TABLE Empleados
(
    Id                 INT PRIMARY KEY IDENTITY(1,1),
    Nombre             NVARCHAR(100),
    Cargo              NVARCHAR(50),
    Sueldo             DECIMAL(18, 2),
    Fecha_contratacion SMALLDATETIME
);

CREATE TABLE Peliculas
(
    Id              INT PRIMARY KEY IDENTITY(1,1),
    Nombre          NVARCHAR(100),
    Genero          NVARCHAR(50),
    Fecha_Estreno   SMALLDATETIME,
    Estado          BIT            NOT NULL,
    Cantidad        INT            DEFAULT 0, --Atributo Agregado
    Precio_unitario DECIMAL(10, 2) NOT NULL,  --Atributo Agregado
    Total           DECIMAL(10, 2) DEFAULT 0  --Atributo Agregado
);

CREATE TABLE Snacks
(
    Id     INT PRIMARY KEY IDENTITY(1,1),
    Nombre NVARCHAR(100),
    Precio DECIMAL(10, 2) NOT NULL,
    Stock  INT
);

CREATE TABLE Consolas
(
    Id              INT PRIMARY KEY IDENTITY(1,1),
    Tipo            NVARCHAR(100),
    Marca           NVARCHAR(50),
    Estado          INT            NOT NULL,
    Estado_string   NVARCHAR(50),
    Cantidad        INT            DEFAULT 0,--Atributo Agregado
    Precio_unitario DECIMAL(10, 2) NOT NULL, --Atributo Agregado
    Total           DECIMAL(10, 2) DEFAULT 0, -- Atributo Agregado
    almacen         INT,
    FOREIGN KEY (almacen) REFERENCES Almacenes (Id)
);

CREATE TABLE Envios
(
    Id             INT PRIMARY KEY IDENTITY(1,1),
    Estado         NVARCHAR(50),
    Direccion      NVARCHAR(200),
    Transportadora NVARCHAR(50) NULL,
    empleado       INT NOT NULL,
    FOREIGN KEY (empleado) REFERENCES Empleados (Id)
);

CREATE TABLE Reservas
(
    Id            INT PRIMARY KEY IDENTITY(1,1),
    Fecha_Reserva SMALLDATETIME,
    Estado        NVARCHAR(100) NULL,
    Duracion      INT            DEFAULT 1, --Nuevo Atributo
    Total         DECIMAL(10, 2) DEFAULT 0, --Nuevo Atributo
    Miembro       INT,
    Pelicula      INT,
    Consola       INT,
    Empleado      INT,
    FOREIGN KEY (Miembro) REFERENCES Miembros (Id),
    FOREIGN KEY (Pelicula) REFERENCES Peliculas (Id),
    FOREIGN KEY (Consola) REFERENCES Consolas (Id),
    FOREIGN KEY (Empleado) REFERENCES Empleados (Id)
);

CREATE TABLE Reservas_Snacks
(
    Id       INT PRIMARY KEY IDENTITY(1,1),
    Cantidad INT            DEFAULT 0, --Atributo Agregado
    Total    DECIMAL(10, 2) DEFAULT 0,--Atributo Agregado
    Snack    INT NOT NULL,
    Reserva  INT NOT NULL,
    FOREIGN KEY (Snack) REFERENCES Snacks (Id),
    FOREIGN KEY (Reserva) REFERENCES Reservas (Id)
);

CREATE TABLE Auditorias
(
    Id            INT IDENTITY(1,1) PRIMARY KEY,       
    Tabla         NVARCHAR(255) NOT NULL,
    Accion        NVARCHAR(50) NOT NULL,        
    LlavePrimaria NVARCHAR(255) NOT NULL,              
    Cambios       NVARCHAR(MAX) NOT NULL,              
    Fecha         DATETIME NOT NULL DEFAULT GETDATE(), 
    Usuario       NVARCHAR(255) NULL                   
);

-- Insertar datos en la tabla Almacenes
INSERT INTO Almacenes (Ubicacion, Capacidad)
VALUES ('El Poblado', 5000.00),
       ('Laureles', 3000.00),
       ('Envigado', 4000.00),
       ('Bello', 3500.00),
       ('Sabaneta', 4500.00),
       ('Itagüí', 6000.00),
       ('Robledo', 7000.00),
       ('Manrique', 2500.00);

-- Insertar datos en la tabla Miembros
INSERT INTO Miembros (Nombre, Fecha_registro, Nivel, Puntos)
VALUES ('Juan Pérez', '2023-01-15', 'Gold', 1500),
       ('María Gómez', '2023-02-20', 'Silver', 800),
       ('Carlos Rodríguez', '2023-03-10', 'Bronze', 300),
       ('Ana Torres', '2023-04-05', 'Gold', 1200),
       ('Luis Martínez', '2023-05-15', 'Silver', 600),
       ('Sofía López', '2023-06-25', 'Bronze', 200),
       ('Diego Fernández', '2023-07-30', 'Gold', 1800),
       ('Valentina Ruiz', '2023-08-10', 'Silver', 900);

-- Insertar datos en la tabla Empleados
INSERT INTO Empleados (Nombre, Cargo, Sueldo, Fecha_contratacion)
VALUES ('Andrés Martínez', 'Gerente', 3000.00, '2023-01-01'),
       ('Laura Sánchez', 'Vendedor', 1500.00, '2023-01-10'),
       ('Javier López', 'Repartidor', 1200.00, '2023-01-15'),
       ('Claudia Jiménez', 'Asistente', 1000.00, '2023-01-20'),
       ('Fernando Castro', 'Supervisor', 2500.00, '2023-01-25'),
       ('Isabel Salazar', 'Cajera', 1100.00, '2023-02-01'),
       ('Ricardo Gómez', 'Almacenero', 1300.00, '2023-02-05'),
       ('Patricia Ríos', 'Atención al Cliente', 1400.00, '2023-02-10');

-- Insertar datos en la tabla Peliculas
INSERT INTO Peliculas (Nombre, Genero, Fecha_Estreno, Estado, Cantidad, Precio_unitario)
VALUES ('La Venganza de los Nerds', 'Comedia', '2023-01-15', 1, 10, 15.00),
       ('Rápidos y Furiosos 10', 'Acción', '2023-05-20', 1, 5, 20.00),
       ('El Viaje de Chihiro', 'Animación', '2023-03-10', 1, 8, 10.00),
       ('Avatar: El Camino del Agua', 'Ciencia Ficción', '2023-12-15', 1, 12, 25.00),
       ('El Conjuro 4', 'Terror', '2023-07-20', 1, 7, 18.00),
       ('La La Land', 'Musical', '2023-02-10', 1, 6, 12.00),
       ('Spider-Man: No Way Home', 'Acción', '2023-04-05', 1, 9, 22.00),
       ('Coco', 'Animación', '2023-08-30', 1, 11, 14.00);

-- Insertar datos en la tabla Snacks
INSERT INTO Snacks (Nombre, Precio, Stock)
VALUES ('Palomitas', 5.00, 100),
       ('Gaseosa', 3.00, 50),
       ('Chocoramo', 2.00, 75),
       ('Nachos', 4.00, 60),
       ('Perro Caliente', 6.00, 30),
       ('Papas Fritas', 2.50, 80),
       ('Agua Mineral', 1.50, 100),
       ('Galletas', 3.50, 40);

-- Insertar datos en la tabla Consolas
INSERT INTO Consolas (Tipo, Marca, Estado, Estado_string, Cantidad, Precio_unitario, almacen)
VALUES ('PlayStation 5', 'Sony', 1, 'Disponible ', 10, 2000.00, 1),
       ('Xbox Series X', 'Microsoft', 1, 'Disponible', 8, 1800.00, 1),
       ('Nintendo Switch', 'Nintendo', 1, 'Disponible', 15, 300.00, 2),
       ('PlayStation 4', 'Sony', 1, 'Disponible', 12, 1500.00, 1),
       ('Xbox One', 'Microsoft', 1, 'Disponible', 10, 1200.00, 1),
       ('Sega Genesis', 'Sega', 1, 'Disponible', 5, 800.00, 2),
       ('Atari 2600', 'Atari', 1, 'Disponible', 3, 600.00, 2),
       ('Nintendo 64', 'Nintendo', 1, 'Disponible', 7, 900.00, 2);

-- Insertar datos en la tabla Envios
INSERT INTO Envios (Estado, Direccion, Transportadora, empleado)
VALUES ('En camino', 'Calle 123 #45-67', 'Servientrega', 1),
       ('Entregado', 'Carrera 89 #12-34', 'Coordinadora', 2),
       ('Pendiente', 'Avenida 7 #89-10', NULL, 3),
       ('En camino', 'Calle 45 #67-89', 'TCC', 4),
       ('Entregado', 'Carrera 10 #11-12', 'Interrapidísimo', 5),
       ('Pendiente', 'Avenida 50 #22-33', NULL, 6),
       ('En camino', 'Calle 30 #44-55', 'Servientrega', 7),
       ('Entregado', 'Carrera 20 #66-77', 'Coordinadora', 8);

-- Insertar datos en la tabla Reservas
INSERT INTO Reservas (Fecha_Reserva, Estado, Duracion, Total, Miembro, Pelicula, Consola, Empleado)
VALUES ('2023-09-01', 'Confirmada', 2, 0, 1, 1, 1, 1),
       ('2023-09-02', 'Pendiente', 1, 0, 2, 2, 2, 2),
       ('2023-09-03', 'Cancelada', 3, 0, 3, 3, 3, 3),
       ('2023-09-04', 'Confirmada', 2, 0, 4, 4, 4, 4),
       ('2023-09-05', 'Pendiente', 1, 0, 5, 5, 5, 5),
       ('2023-09-06', 'Cancelada', 3, 0, 6, 6, 6, 6),
       ('2023-09-07', 'Confirmada', 2, 0, 7, 7, 7, 7),
       ('2023-09-08', 'Pendiente', 1, 0, 8, 8, 8, 8);

-- Insertar datos en la tabla Reservas_Snacks
INSERT INTO Reservas_Snacks (Cantidad, Snack, Reserva)
VALUES (2, 1, 1),
       (1, 2, 2),
       (3, 3, 3),
       (2, 4, 4),
       (1, 5, 5),
       (3, 6, 6),
       (2, 7, 7),
       (1, 8, 8);