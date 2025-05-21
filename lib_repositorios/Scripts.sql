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
    Cantidad        INT            DEFAULT 0,
    Precio_unitario DECIMAL(10, 2) NOT NULL,
    Total           DECIMAL(10, 2) DEFAULT 0
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
    Cantidad        INT            DEFAULT 0,
    Precio_unitario DECIMAL(10, 2) NOT NULL,
    Total           DECIMAL(10, 2) DEFAULT 0,
    almacen         INT,
    FOREIGN KEY (almacen) REFERENCES Almacenes (Id) ON DELETE CASCADE
);

CREATE TABLE Envios
(
    Id             INT PRIMARY KEY IDENTITY(1,1),
    Estado         NVARCHAR(50),
    Direccion      NVARCHAR(200),
    Transportadora NVARCHAR(50) NULL,
    empleado       INT NOT NULL,
    FOREIGN KEY (empleado) REFERENCES Empleados (Id) ON DELETE CASCADE
);

CREATE TABLE Reservas
(
    Id            INT PRIMARY KEY IDENTITY(1,1),
    Fecha_Reserva SMALLDATETIME,
    Estado        NVARCHAR(100) NULL,
    Duracion      INT            DEFAULT 1,
    Total         DECIMAL(10, 2) DEFAULT 0,
    Miembro       INT,
    Pelicula      INT,
    Consola       INT,
    Empleado      INT,
    FOREIGN KEY (Miembro) REFERENCES Miembros (Id) ON DELETE CASCADE,
    FOREIGN KEY (Pelicula) REFERENCES Peliculas (Id) ON DELETE CASCADE,
    FOREIGN KEY (Consola) REFERENCES Consolas (Id) ON DELETE CASCADE,
    FOREIGN KEY (Empleado) REFERENCES Empleados (Id) ON DELETE CASCADE
);

CREATE TABLE Reservas_Snacks
(
    Id       INT PRIMARY KEY IDENTITY(1,1),
    Cantidad INT            DEFAULT 0,
    Total    DECIMAL(10, 2) DEFAULT 0,
    Snack    INT NOT NULL,
    Reserva  INT NOT NULL,
    FOREIGN KEY (Snack) REFERENCES Snacks (Id) ON DELETE CASCADE,
    FOREIGN KEY (Reserva) REFERENCES Reservas (Id) ON DELETE CASCADE
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

-- Tabla de Roles
CREATE TABLE Roles
(
    Id     INT PRIMARY KEY IDENTITY(1,1),
    Nombre NVARCHAR(100) NOT NULL
);

-- Tabla de Permisos
CREATE TABLE Permisos
(
    Id     INT PRIMARY KEY IDENTITY(1,1),
    Nombre NVARCHAR(100) NOT NULL
);

-- Tabla intermedia RolPermiso (muchos a muchos)
CREATE TABLE RolPermiso
(
    RolId     INT NOT NULL,
    PermisoId INT NOT NULL,
    PRIMARY KEY (RolId, PermisoId),
    FOREIGN KEY (RolId) REFERENCES Roles (Id),
    FOREIGN KEY (PermisoId) REFERENCES Permisos (Id)
);

-- Tabla de Usuarios
CREATE TABLE Usuarios
(
    Id             INT PRIMARY KEY IDENTITY(1,1),
    Nombre         NVARCHAR(100) NOT NULL,
    Correo         NVARCHAR(150) NOT NULL UNIQUE,
    ContrasenaHash NVARCHAR(255) NOT NULL,
    Direccion      NVARCHAR(255),
    RolId          INT NOT NULL,
    FOREIGN KEY (RolId) REFERENCES Roles (Id)
);


-- Almacenes
INSERT INTO Almacenes (Ubicacion, Capacidad)
VALUES ('Bogotá - Centro', 500.00),
       ('Medellín - Laureles', 300.00),
       ('Cali - San Fernando', 400.00),
       ('Barranquilla - Norte', 350.00),
       ('Cartagena - Getsemaní', 250.00),
       ('Bucaramanga - Cabecera', 280.00),
       ('Pereira - Cuba', 320.00),
       ('Manizales - Chipre', 270.00);

-- Miembros
INSERT INTO Miembros (Nombre, Fecha_registro, Nivel, Puntos)
VALUES ('Juan Pérez', '2024-01-15', 'Oro', 1500),
       ('María Rodríguez', '2023-11-20', 'Plata', 900),
       ('Andrés Gómez', '2024-03-05', 'Bronce', 300),
       ('Laura Fernández', '2023-07-12', 'Plata', 1100),
       ('David Sánchez', '2024-04-01', 'Oro', 1700),
       ('Carolina Rojas', '2023-10-23', 'Bronce', 450),
       ('Jorge Martínez', '2024-02-14', 'Plata', 800),
       ('Ana López', '2023-12-30', 'Oro', 1900);

-- Empleados
INSERT INTO Empleados (Nombre, Cargo, Sueldo, Fecha_contratacion)
VALUES ('Carlos Martínez', 'Gerente', 3500000.00, '2022-06-01'),
       ('Ana Torres', 'Asistente', 1800000.00, '2023-02-15'),
       ('Luis Ramírez', 'Técnico', 2200000.00, '2023-09-10'),
       ('Sofía Herrera', 'Ventas', 1900000.00, '2023-01-05'),
       ('Miguel Ángel', 'Soporte', 2100000.00, '2022-11-11'),
       ('Camila Ortiz', 'Marketing', 2300000.00, '2023-03-22'),
       ('Diego Vélez', 'Logística', 2000000.00, '2023-05-16'),
       ('Natalia Castro', 'Administración', 2400000.00, '2023-07-25');

-- Peliculas 
INSERT INTO Peliculas (Nombre, Genero, Fecha_Estreno, Estado, Cantidad, Precio_unitario, Total)
VALUES ('The Matrix', 'Ciencia Ficción', '1999-03-31', 1, 10, 12000.00, 120000.00),
       ('El laberinto del fauno', 'Fantasía', '2006-10-11', 1, 5, 15000.00, 75000.00),
       ('La La Land', 'Terror', '2016-12-09', 1, 7, 13000.00, 91000.00),
       ('Avengers: Endgame', 'Acción', '2019-04-26', 1, 12, 20000.00, 240000.00),
       ('Inception', 'Thriller', '2010-07-16', 1, 8, 18000.00, 144000.00),
       ('Interstellar', 'Ciencia Ficción', '2014-11-07', 1, 6, 17000.00, 102000.00),
       ('Coco', 'Animación', '2017-11-22', 1, 9, 14000.00, 126000.00),
       ('Jumanji', 'Aventura', '2017-12-20', 1, 11, 16000.00, 176000.00);

-- Snacks (8)
INSERT INTO Snacks (Nombre, Precio, Stock)
VALUES ('Palomitas Grandes', 8000.00, 50),
       ('Gaseosa 500ml', 5000.00, 100),
       ('Nachos con Queso', 7000.00, 30),
       ('Dulces surtidos', 4000.00, 75),
       ('Chocolate', 3500.00, 80),
       ('Agua Mineral', 3000.00, 120),
       ('Café', 6000.00, 40),
       ('Chicles', 2000.00, 60);

-- Consolas
INSERT INTO Consolas (Tipo, Marca, Estado, Estado_string, Cantidad, Precio_unitario, almacen)
VALUES ('PlayStation 5', 'Sony', 1, 'Disponible ', 10, 20000.00, 1),
       ('Xbox Series X', 'Microsoft', 2, 'Disponible', 8, 18000.00, 1),
       ('Nintendo Switch', 'Nintendo', 3, 'Disponible', 15, 30000.00, 2),
       ('PlayStation 4', 'Sony', 2, 'Disponible', 12, 15000.00, 1),
       ('Xbox One', 'Microsoft', 3, 'Disponible', 10, 12000.00, 1),
       ('Sega Genesis', 'Sega', 1, 'Disponible', 5, 8000.00, 2),
       ('Atari 2600', 'Atari', 3, 'Disponible', 3, 6000.00, 2),
       ('Nintendo 64', 'Nintendo', 2, 'Disponible', 7, 9000.00, 2);


-- Envios (8)
INSERT INTO Envios (Estado, Direccion, Transportadora, empleado)
VALUES ('En tránsito', 'Calle 45 # 12-34, Bogotá', 'Servientrega', 1),
       ('Entregado', 'Av. El Poblado # 23-45, Medellín', 'Deprisa', 2),
       ('Pendiente', 'Calle 5 # 67-89, Cali', 'Fedex', 3),
       ('En tránsito', 'Carrera 7 # 50-60, Barranquilla', 'Envía', 4),
       ('Entregado', 'Calle 10 # 30-40, Cartagena', 'Servientrega', 5),
       ('Pendiente', 'Av. 9 # 70-80, Bucaramanga', 'Deprisa', 6),
       ('En tránsito', 'Carrera 21 # 15-25, Pereira', 'Envía', 7),
       ('Entregado', 'Calle 23 # 45-55, Manizales', 'Global Transportadora', 8);

-- Reservas (8) con IDs variados en las claves foráneas (Miembro, Pelicula, Consola, Empleado)
INSERT INTO Reservas (Fecha_Reserva, Estado, Duracion, Total, Miembro, Pelicula, Consola, Empleado)
VALUES ('2024-05-01', 'Confirmada', 3, 36000.00, 1, 3, null, 4),
       ('2024-05-02', 'Pendiente', 1, 15000.00, 3, null, 4, 1),
       ('2024-05-03', 'Cancelada', 2, 0.00, 2, 4, 1, 3),
       ('2024-05-04', 'Confirmada', 4, 80000.00, 4, 1, 3, 2),
       ('2024-05-05', 'Confirmada', 1, 20000.00, 5, 5, 5, 6),
       ('2024-05-06', 'Pendiente', 2, 30000.00, 6, 6, null, 7),
       ('2024-05-07', 'Confirmada', 3, 45000.00, 2, 7, 7, 8),
       ('2024-05-08', 'Cancelada', 1, 0.00, 8, 8, 8, 5);

-- Reservas_Snacks (8)
INSERT INTO Reservas_Snacks (Cantidad, Total, Snack, Reserva)
VALUES (2, 16000.00, 1, 1),
       (1, 5000.00, 2, 1),
       (3, 12000.00, 4, 2),
       (1, 3500.00, 5, 3),
       (2, 6000.00, 7, 4),
       (4, 8000.00, 1, 5),
       (1, 2000.00, 8, 6),
       (3, 15000.00, 3, 7);

-- Insertar Roles
INSERT INTO Roles (Nombre) VALUES ('Admin');
INSERT INTO Roles (Nombre) VALUES ('Lector');

-- Insertar Usuarios
INSERT INTO Usuarios (Nombre, Correo, ContrasenaHash, Direccion, RolId)
VALUES ('Admin', 'admin@empresa.com', 'ef92b778bafe771e89245b89ecbc08a44a4e166c06659911881f383d4473e94f', 'Calle Falsa 123', 1);
-- Contraseña: lector123
INSERT INTO Usuarios (Nombre, Correo, ContrasenaHash, Direccion, RolId)
VALUES ('Lector', 'lector@empresa.com', 'ecb21a7ddf1d31c2f53b349c2a65e6c8e08db66f490ce7bc1d1efc7165b5806f', 'Av. Siempre Viva 742', 2);

