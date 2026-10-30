CREATE DATABASE [PruebaTecnicaStbcDb]
GO
USE [PruebaTecnicaStbcDb]
GO
/****** Object:  UserDefinedTableType [dbo].[TipoDetalleVenta]    Script Date: 30/10/2026 01:29:00 ******/
CREATE TYPE [dbo].[TipoDetalleVenta] AS TABLE(
	[IdPro] [int] NULL,
	[Cantidad] [int] NULL,
	[Precio] [decimal](18, 2) NULL,
	[Iva] [decimal](18, 2) NULL,
	[Total] [decimal](18, 2) NULL
)
GO
/****** Object:  Table [dbo].[DetalleVentas]    Script Date: 30/10/2026 01:29:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DetalleVentas](
	[IdDe] [int] IDENTITY(1,1) NOT NULL,
	[Fecha] [datetime] NOT NULL,
	[IdVenta] [int] NOT NULL,
	[IdPro] [int] NOT NULL,
	[Cantidad] [int] NOT NULL,
	[Precio] [decimal](18, 2) NOT NULL,
	[Iva] [decimal](18, 2) NOT NULL,
	[Total] [decimal](18, 2) NOT NULL,
 CONSTRAINT [PK_DetalleVentas] PRIMARY KEY CLUSTERED 
(
	[IdDe] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Productos]    Script Date: 30/10/2026 01:29:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Productos](
	[IdPro] [int] IDENTITY(1,1) NOT NULL,
	[Codigo] [varchar](50) NOT NULL,
	[Producto] [varchar](150) NOT NULL,
	[Precio] [decimal](18, 2) NOT NULL,
	[Stock] [int] NOT NULL,
	[IdUsuarioCreacion] [int] NOT NULL,
	[FechaCreacion] [datetime] NOT NULL,
 CONSTRAINT [PK_Productos] PRIMARY KEY CLUSTERED 
(
	[IdPro] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Usuarios]    Script Date: 30/10/2026 01:29:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Usuarios](
	[IdUsuario] [int] IDENTITY(1,1) NOT NULL,
	[Username] [varchar](50) NOT NULL,
	[PasswordHash] [varchar](255) NOT NULL,
	[Rol] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Usuarios] PRIMARY KEY CLUSTERED 
(
	[IdUsuario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Ventas]    Script Date: 30/10/2026 01:29:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Ventas](
	[IdVenta] [int] IDENTITY(1,1) NOT NULL,
	[Fecha] [datetime] NOT NULL,
	[Vendedor] [varchar](100) NOT NULL,
	[Total] [decimal](18, 2) NOT NULL,
	[IdUsuarioCreacion] [int] NOT NULL,
 CONSTRAINT [PK_Ventas] PRIMARY KEY CLUSTERED 
(
	[IdVenta] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[DetalleVentas] ON 
GO
INSERT [dbo].[DetalleVentas] ([IdDe], [Fecha], [IdVenta], [IdPro], [Cantidad], [Precio], [Iva], [Total]) VALUES (1, CAST(N'2026-10-30T00:35:51.650' AS DateTime), 1, 3, 1, CAST(4.10 AS Decimal(18, 2)), CAST(0.53 AS Decimal(18, 2)), CAST(4.10 AS Decimal(18, 2)))
GO
INSERT [dbo].[DetalleVentas] ([IdDe], [Fecha], [IdVenta], [IdPro], [Cantidad], [Precio], [Iva], [Total]) VALUES (2, CAST(N'2026-10-30T00:35:51.650' AS DateTime), 1, 1, 2, CAST(0.75 AS Decimal(18, 2)), CAST(0.20 AS Decimal(18, 2)), CAST(1.50 AS Decimal(18, 2)))
GO
INSERT [dbo].[DetalleVentas] ([IdDe], [Fecha], [IdVenta], [IdPro], [Cantidad], [Precio], [Iva], [Total]) VALUES (3, CAST(N'2026-10-30T00:36:41.173' AS DateTime), 2, 1, 3, CAST(0.75 AS Decimal(18, 2)), CAST(0.29 AS Decimal(18, 2)), CAST(2.25 AS Decimal(18, 2)))
GO
INSERT [dbo].[DetalleVentas] ([IdDe], [Fecha], [IdVenta], [IdPro], [Cantidad], [Precio], [Iva], [Total]) VALUES (4, CAST(N'2026-10-30T00:44:10.590' AS DateTime), 3, 1, 1, CAST(0.75 AS Decimal(18, 2)), CAST(0.10 AS Decimal(18, 2)), CAST(0.75 AS Decimal(18, 2)))
GO
SET IDENTITY_INSERT [dbo].[DetalleVentas] OFF
GO
SET IDENTITY_INSERT [dbo].[Productos] ON 
GO
INSERT [dbo].[Productos] ([IdPro], [Codigo], [Producto], [Precio], [Stock], [IdUsuarioCreacion], [FechaCreacion]) VALUES (1, N'001', N'Lapicero BIC color azul', CAST(0.75 AS Decimal(18, 2)), 494, 1, CAST(N'2026-10-29T14:12:02.677' AS DateTime))
GO
INSERT [dbo].[Productos] ([IdPro], [Codigo], [Producto], [Precio], [Stock], [IdUsuarioCreacion], [FechaCreacion]) VALUES (2, N'063', N'Cuaderno cuadriculado', CAST(3.50 AS Decimal(18, 2)), 150, 1, CAST(N'2026-10-29T14:12:02.677' AS DateTime))
GO
INSERT [dbo].[Productos] ([IdPro], [Codigo], [Producto], [Precio], [Stock], [IdUsuarioCreacion], [FechaCreacion]) VALUES (3, N'010', N'Caja de colores facela', CAST(4.10 AS Decimal(18, 2)), 79, 1, CAST(N'2026-10-29T14:12:02.677' AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[Productos] OFF
GO
SET IDENTITY_INSERT [dbo].[Usuarios] ON 
GO
INSERT [dbo].[Usuarios] ([IdUsuario], [Username], [PasswordHash], [Rol]) VALUES (1, N'admin', N'$2a$12$coyQ2kjNnzU.szFLuSS3DuE7G/AZjcD0wA0eHuzu5WczH8zL82mY6', N'Administrador')
GO
INSERT [dbo].[Usuarios] ([IdUsuario], [Username], [PasswordHash], [Rol]) VALUES (2, N'operador', N'$2a$12$nE.dMLMMAlsLYdesuo0Pf.slItW0B7C2VYwmWnAG/yMX5jHwcu.6a', N'Operador')
GO
SET IDENTITY_INSERT [dbo].[Usuarios] OFF
GO
SET IDENTITY_INSERT [dbo].[Ventas] ON 
GO
INSERT [dbo].[Ventas] ([IdVenta], [Fecha], [Vendedor], [Total], [IdUsuarioCreacion]) VALUES (1, CAST(N'2026-10-30T00:35:51.640' AS DateTime), N'admin', CAST(5.60 AS Decimal(18, 2)), 1)
GO
INSERT [dbo].[Ventas] ([IdVenta], [Fecha], [Vendedor], [Total], [IdUsuarioCreacion]) VALUES (2, CAST(N'2026-10-30T00:36:41.170' AS DateTime), N'operador', CAST(2.25 AS Decimal(18, 2)), 2)
GO
INSERT [dbo].[Ventas] ([IdVenta], [Fecha], [Vendedor], [Total], [IdUsuarioCreacion]) VALUES (3, CAST(N'2026-10-30T00:44:10.590' AS DateTime), N'admin', CAST(0.75 AS Decimal(18, 2)), 1)
GO
SET IDENTITY_INSERT [dbo].[Ventas] OFF
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ_Productos_Codigo]    Script Date: 30/10/2026 01:29:01 ******/
ALTER TABLE [dbo].[Productos] ADD  CONSTRAINT [UQ_Productos_Codigo] UNIQUE NONCLUSTERED 
(
	[Codigo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ_Usuarios_Username]    Script Date: 30/10/2026 01:29:01 ******/
ALTER TABLE [dbo].[Usuarios] ADD  CONSTRAINT [UQ_Usuarios_Username] UNIQUE NONCLUSTERED 
(
	[Username] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[DetalleVentas] ADD  CONSTRAINT [DF_DetalleVentas_Fecha]  DEFAULT (getdate()) FOR [Fecha]
GO
ALTER TABLE [dbo].[Productos] ADD  CONSTRAINT [DF_Productos_FechaCreacion]  DEFAULT (getdate()) FOR [FechaCreacion]
GO
ALTER TABLE [dbo].[Ventas] ADD  CONSTRAINT [DF_Ventas_Fecha]  DEFAULT (getdate()) FOR [Fecha]
GO
ALTER TABLE [dbo].[DetalleVentas]  WITH CHECK ADD  CONSTRAINT [FK_DetalleVentas_Productos] FOREIGN KEY([IdPro])
REFERENCES [dbo].[Productos] ([IdPro])
GO
ALTER TABLE [dbo].[DetalleVentas] CHECK CONSTRAINT [FK_DetalleVentas_Productos]
GO
ALTER TABLE [dbo].[DetalleVentas]  WITH CHECK ADD  CONSTRAINT [FK_DetalleVentas_Ventas] FOREIGN KEY([IdVenta])
REFERENCES [dbo].[Ventas] ([IdVenta])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[DetalleVentas] CHECK CONSTRAINT [FK_DetalleVentas_Ventas]
GO
ALTER TABLE [dbo].[Productos]  WITH CHECK ADD  CONSTRAINT [FK_Productos_Usuarios] FOREIGN KEY([IdUsuarioCreacion])
REFERENCES [dbo].[Usuarios] ([IdUsuario])
GO
ALTER TABLE [dbo].[Productos] CHECK CONSTRAINT [FK_Productos_Usuarios]
GO
ALTER TABLE [dbo].[Ventas]  WITH CHECK ADD  CONSTRAINT [FK_Ventas_Usuarios] FOREIGN KEY([IdUsuarioCreacion])
REFERENCES [dbo].[Usuarios] ([IdUsuario])
GO
ALTER TABLE [dbo].[Ventas] CHECK CONSTRAINT [FK_Ventas_Usuarios]
GO
/****** Object:  StoredProcedure [dbo].[UspActualizarProducto]    Script Date: 30/10/2026 01:29:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- ==========================================================================================
-- Autor:        Miguel Rodríguez
-- Fecha Creación: 29/10/2026
-- Descripción:   Actualiza la información de un producto existente según su ID.
-- ==========================================================================================
-- HISTORIAL DE MODIFICACIONES:
-- Fecha       | Autor             | Descripción del Cambio
-- ------------+-------------------+---------------------------------------------------------
-- 29/10/2026  | Miguel Rodríguez  | Creación inicial del procedimiento almacenado.
-- ==========================================================================================
-- PARÁMETROS DE ENTRADA / SALIDA:
-- @idpro:    INT           - Identificador único del producto (Requerido).
-- @codigo:   VARCHAR(50)   - Nuevo código del producto (Requerido).
-- @producto: VARCHAR(150)  - Nuevo nombre del producto (Requerido).
-- @precio:   DECIMAL(18,2) - Nuevo precio unitario (Requerido).
-- @stock:    INT           - Nuevo stock disponible (Requerido).
-- ==========================================================================================
-- EJEMPLO DE EJECUCIÓN:
-- EXEC dbo.sp_ActualizarProducto @idpro = 1, @codigo = '001', @producto = 'Lapicero Azul', @precio = 0.80, @stock = 450
-- ==========================================================================================
CREATE PROCEDURE [dbo].[UspActualizarProducto]
    @idpro INT,
    @codigo VARCHAR(50),
    @producto VARCHAR(150),
    @precio DECIMAL(18,2),
    @stock INT
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE Productos 
    SET Codigo = @codigo, 
        Producto = @producto, 
        Precio = @precio, 
        Stock = @stock
    WHERE IdPro = @idpro;
END
GO
/****** Object:  StoredProcedure [dbo].[UspEliminarProducto]    Script Date: 30/10/2026 01:29:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- ==========================================================================================
-- Autor:        Miguel Rodríguez
-- Fecha Creación: 29/10/2026
-- Descripción:   Elimina un producto del inventario basado en su ID.
-- ==========================================================================================
-- HISTORIAL DE MODIFICACIONES:
-- Fecha       | Autor             | Descripción del Cambio
-- ------------+-------------------+---------------------------------------------------------
-- 29/10/2026  | Miguel Rodríguez  | Creación inicial del procedimiento almacenado.
-- ==========================================================================================
-- PARÁMETROS DE ENTRADA / SALIDA:
-- @idpro: INT - Identificador único del producto a eliminar (Requerido).
-- ==========================================================================================
-- EJEMPLO DE EJECUCIÓN:
-- EXEC dbo.sp_EliminarProducto @idpro = 1
-- ==========================================================================================
CREATE PROCEDURE [dbo].[UspEliminarProducto]
    @idpro INT
AS
BEGIN
    SET NOCOUNT ON;

    DELETE FROM Productos WHERE IdPro = @idpro;
END
GO
/****** Object:  StoredProcedure [dbo].[UspInsertarProducto]    Script Date: 30/10/2026 01:29:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- ==========================================================================================
-- Autor:        Miguel Rodríguez
-- Fecha Creación: 29/10/2026
-- Descripción:   Inserta un nuevo registro de producto en la base de datos registrando el ID del usuario creador.
-- ==========================================================================================
-- HISTORIAL DE MODIFICACIONES:
-- Fecha       | Autor             | Descripción del Cambio
-- ------------+-------------------+---------------------------------------------------------
-- 29/10/2026  | Miguel Rodríguez  | Creación inicial del procedimiento almacenado.
-- ==========================================================================================
-- PARÁMETROS DE ENTRADA / SALIDA:
-- @codigo:            VARCHAR(50)   - Código único del producto (Requerido).
-- @producto:          VARCHAR(150)  - Nombre o descripción del producto (Requerido).
-- @precio:            DECIMAL(18,2) - Precio unitario del producto (Requerido).
-- @stock:             INT           - Cantidad inicial en inventario (Requerido).
-- @idUsuarioCreacion: INT           - Identificador del usuario que realiza el registro (Requerido).
-- ==========================================================================================
-- EJEMPLO DE EJECUCIÓN:
-- EXEC dbo.sp_InsertarProducto @codigo = '002', @producto = 'Borrador', @precio = 0.50, @stock = 100, @idUsuarioCreacion = 1
-- ==========================================================================================
CREATE PROCEDURE [dbo].[UspInsertarProducto]
    @codigo VARCHAR(50),
    @producto VARCHAR(150),
    @precio DECIMAL(18,2),
    @stock INT,
    @idUsuarioCreacion INT
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO Productos (Codigo, Producto, Precio, Stock, IdUsuarioCreacion, FechaCreacion)
    VALUES (@codigo, @producto, @precio, @stock, @idUsuarioCreacion, GETDATE());
END
GO
/****** Object:  StoredProcedure [dbo].[UspObtenerProductos]    Script Date: 30/10/2026 01:29:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- ==========================================================================================
-- Autor:        Miguel Rodríguez
-- Fecha Creación: 29/10/2026
-- Descripción:   Permite listar todos los productos o buscar uno en específico por código o nombre.
-- ==========================================================================================
-- HISTORIAL DE MODIFICACIONES:
-- Fecha       | Autor             | Descripción del Cambio
-- ------------+-------------------+---------------------------------------------------------
-- 29/10/2026  | Miguel Rodríguez  | Creación inicial del procedimiento almacenado.
-- ==========================================================================================
-- PARÁMETROS DE ENTRADA / SALIDA:
-- @codigo: VARCHAR(50) - Código o texto de búsqueda opcional para filtrar productos.
-- ==========================================================================================
-- EJEMPLO DE EJECUCIÓN:
-- EXEC dbo.sp_ObtenerProductos @codigo = '001'
-- ==========================================================================================
CREATE PROCEDURE [dbo].[UspObtenerProductos]
    @codigo VARCHAR(50) = NULL
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        p.IdPro, 
        p.Codigo, 
        p.Producto, 
        p.Precio, 
        p.Stock, 
        p.IdUsuarioCreacion, 
        u.Username AS UsuarioCreacionNombre,
        p.FechaCreacion
    FROM Productos p
    INNER JOIN Usuarios u ON p.IdUsuarioCreacion = u.IdUsuario
    WHERE @codigo IS NULL OR p.Codigo = @codigo OR p.Producto LIKE '%' + @codigo + '%';
END
GO
/****** Object:  StoredProcedure [dbo].[UspRegistrarVenta]    Script Date: 30/10/2026 01:29:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- ==========================================================================================
-- Autor:        Miguel Rodríguez
-- Fecha Creación: 29/10/2026
-- Descripción:   Registra una transacción de venta completa (cabecera con IdUsuarioCreacion y detalle) 
--               de forma atómica, actualizando además el stock disponible de los productos vendidos.
-- ==========================================================================================
-- HISTORIAL DE MODIFICACIONES:
-- Fecha       | Autor             | Descripción del Cambio
-- ------------+-------------------+---------------------------------------------------------
-- 29/10/2026  | Miguel Rodríguez  | Creación inicial del procedimiento almacenado.
-- ==========================================================================================
-- PARÁMETROS DE ENTRADA / SALIDA:
-- @vendedor:          VARCHAR(100)     - Nombre del vendedor que realiza la operación (Requerido).
-- @total:             DECIMAL(18,2)    - Monto total de la venta incluyendo impuestos (Requerido).
-- @idUsuarioCreacion: INT              - ID del usuario operador que registra la venta (Requerido).
-- @detalle:           TipoDetalleVenta - TVP con la lista de productos y cantidades de la venta (Requerido).
-- @idventa:           INT OUTPUT       - Devuelve el ID generado para la nueva venta.
-- ==========================================================================================
-- EJEMPLO DE EJECUCIÓN:
-- DECLARE @id INT; EXEC dbo.sp_RegistrarVenta @vendedor = 'Juan Perez', @total = 18.19, @idUsuarioCreacion = 2, @detalle = @MiTablaDetalle, @idventa = @id OUTPUT;
-- ==========================================================================================
CREATE PROCEDURE [dbo].[UspRegistrarVenta]
    @vendedor VARCHAR(100),
    @total DECIMAL(18,2),
    @idUsuarioCreacion INT,
    @detalle TipoDetalleVenta READONLY,
    @idventa INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRANSACTION;
    BEGIN TRY
        -- Insertar Encabezado con auditoría de ID de usuario
        INSERT INTO Ventas (Fecha, Vendedor, Total, IdUsuarioCreacion)
        VALUES (GETDATE(), @vendedor, @total, @idUsuarioCreacion);
        
        SET @idventa = SCOPE_IDENTITY();
        
        -- Insertar Detalle
        INSERT INTO DetalleVentas (Fecha, IdVenta, IdPro, Cantidad, Precio, Iva, Total)
        SELECT GETDATE(), @idventa, IdPro, Cantidad, Precio, Iva, Total
        FROM @detalle;

        -- Actualizar Stock de productos
        UPDATE p
        SET p.Stock = p.Stock - d.Cantidad
        FROM Productos p
        INNER JOIN @detalle d ON p.IdPro = d.IdPro;

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[UspReporteVentas]    Script Date: 30/10/2026 01:29:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- ==========================================================================================
-- Autor:        Miguel Rodríguez
-- Fecha Creación: 29/10/2026
-- Descripción:   Genera un reporte detallado de las ventas realizadas (uniendo con el username del creador) 
--               con opción de filtrado por rango de fechas para su exportación a PDF y Excel.
-- ==========================================================================================
-- HISTORIAL DE MODIFICACIONES:
-- Fecha       | Autor             | Descripción del Cambio
-- ------------+-------------------+---------------------------------------------------------
-- 29/10/2026  | Miguel Rodríguez  | Creación inicial del procedimiento almacenado.
-- ==========================================================================================
-- PARÁMETROS DE ENTRADA / SALIDA:
-- @fechaInicio: DATETIME - Fecha de inicio opcional para filtrar el reporte.
-- @fechaFin:    DATETIME - Fecha de fin opcional para filtrar el reporte.
-- ==========================================================================================
-- EJEMPLO DE EJECUCIÓN:
-- EXEC dbo.sp_ReporteVentas @fechaInicio = '2026-10-01', @fechaFin = '2026-10-31'
-- ==========================================================================================
CREATE PROCEDURE [dbo].[UspReporteVentas]
    @fechaInicio DATETIME = NULL,
    @fechaFin DATETIME = NULL
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        v.IdVenta,
        v.Fecha,
        v.Vendedor,
        v.Total AS TotalVenta,
        UPPER(u.Username) AS UsuarioRegistra,
        p.Codigo AS CodigoProducto,
        p.Producto AS NombreProducto,
        d.Cantidad,
        d.Precio AS PrecioUnitario,
        d.Iva,
        d.Total AS TotalDetalle
    FROM Ventas v
    INNER JOIN Usuarios u ON v.IdUsuarioCreacion = u.IdUsuario
    INNER JOIN DetalleVentas d ON v.IdVenta = d.IdVenta
    INNER JOIN Productos p ON d.IdPro = p.IdPro
    WHERE (@fechaInicio IS NULL OR v.Fecha >= @fechaInicio)
      AND (@fechaFin IS NULL OR v.Fecha <= @fechaFin)
    ORDER BY v.Fecha DESC;
END
GO
