
CREATE DATABASE [VETEFINAL]
Go
USE DATABase [VETEFINAL]
Go
CREATE TABLE [dbo].[Atenciones](
	[atencion_nro] [int] IDENTITY(1,1) NOT NULL,
	[fecha] [datetime] NULL,
	[descripcion] [int] NULL,
	[importe] [decimal](16, 2) NULL,
	[id_mascota] [int] NOT NULL,
	[detalle] [varchar](150) NULL,
 CONSTRAINT [pk_atencion] PRIMARY KEY CLUSTERED 
(
	[atencion_nro] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Clientes]    Script Date: 10/11/2021 17:18:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Clientes](
	[id_cliente] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [varchar](50) NULL,
	[sexo] [char](1) NULL,
 CONSTRAINT [pk_clientes] PRIMARY KEY CLUSTERED 
(
	[id_cliente] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Mascotas]    Script Date: 10/11/2021 17:18:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Mascotas](
	[id_mascota] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [varchar](50) NULL,
	[fec_nac] [date] NULL,
	[id_tipo] [int] NULL,
	[id_cliente] [int] NULL,
	[fallecio] [date] NULL,
 CONSTRAINT [pk_mascota] PRIMARY KEY CLUSTERED 
(
	[id_mascota] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Servicios]    Script Date: 10/11/2021 17:18:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Servicios](
	[id_servicio] [int] IDENTITY(1,1) NOT NULL,
	[servicio] [varchar](50) NULL,
 CONSTRAINT [pk_servicio] PRIMARY KEY CLUSTERED 
(
	[id_servicio] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[T_USUARIOS]    Script Date: 10/11/2021 17:18:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[T_USUARIOS](
	[ID_USUARIO] [int] IDENTITY(1,1) NOT NULL,
	[USUARIO] [varchar](50) NULL,
	[CONTRASENA] [varchar](50) NULL,
 CONSTRAINT [pk_usuarios] PRIMARY KEY CLUSTERED 
(
	[ID_USUARIO] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tipos]    Script Date: 10/11/2021 17:18:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tipos](
	[id_tipo] [int] IDENTITY(1,1) NOT NULL,
	[tipo] [varchar](50) NULL,
 CONSTRAINT [pk_tipos] PRIMARY KEY CLUSTERED 
(
	[id_tipo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Atenciones]  WITH CHECK ADD  CONSTRAINT [fk_atencion_mascota] FOREIGN KEY([id_mascota])
REFERENCES [dbo].[Mascotas] ([id_mascota])
GO
ALTER TABLE [dbo].[Atenciones] CHECK CONSTRAINT [fk_atencion_mascota]
GO
ALTER TABLE [dbo].[Atenciones]  WITH CHECK ADD  CONSTRAINT [fk_atencion_servicio] FOREIGN KEY([descripcion])
REFERENCES [dbo].[Servicios] ([id_servicio])
GO
ALTER TABLE [dbo].[Atenciones] CHECK CONSTRAINT [fk_atencion_servicio]
GO
ALTER TABLE [dbo].[Mascotas]  WITH CHECK ADD  CONSTRAINT [fk_mascota_cliente] FOREIGN KEY([id_cliente])
REFERENCES [dbo].[Clientes] ([id_cliente])
GO
ALTER TABLE [dbo].[Mascotas] CHECK CONSTRAINT [fk_mascota_cliente]
GO
ALTER TABLE [dbo].[Mascotas]  WITH CHECK ADD  CONSTRAINT [fk_mascota_tipo] FOREIGN KEY([id_tipo])
REFERENCES [dbo].[Tipos] ([id_tipo])
GO
ALTER TABLE [dbo].[Mascotas] CHECK CONSTRAINT [fk_mascota_tipo]
GO
/****** Object:  StoredProcedure [dbo].[pa_consultar_atencion_por_id]    Script Date: 10/11/2021 17:18:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pa_consultar_atencion_por_id]
	@nro int	
AS
BEGIN
	SELECT *
	FROM Mascotas m
	JOIN Atenciones a ON m.id_mascota=a.id_mascota

	WHERE a.atencion_nro = @nro

END
GO
/****** Object:  StoredProcedure [dbo].[pa_consultar_atenciones]    Script Date: 10/11/2021 17:18:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pa_consultar_atenciones]
	@fecha_desde date,
	@fecha_hasta date,
	@mascota int
	
AS
BEGIN
declare @fecha_hasta_inc datetime
SET @fecha_hasta_inc = @fecha_hasta
SELECT @fecha_hasta_inc = dateadd(second, 86400, @fecha_hasta_inc)

	SELECT atencion_nro, fecha, detalle, importe, id_mascota,servicio
	FROM Atenciones a
	LEFT JOIN Servicios s ON a.descripcion=s.id_servicio
	WHERE 
	 id_mascota=@mascota
	 AND fecha BETWEEN @fecha_desde AND @fecha_hasta_inc
END
GO
/****** Object:  StoredProcedure [dbo].[pa_consultar_cliente]    Script Date: 10/11/2021 17:18:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pa_consultar_cliente]
AS
BEGIN
	
	SELECT * FROM Clientes;
END
GO
/****** Object:  StoredProcedure [dbo].[pa_consultar_mascota_por_id]    Script Date: 10/11/2021 17:18:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pa_consultar_mascota_por_id]
	@nro int	
AS
BEGIN
	SELECT *
	FROM Mascotas m
	JOIN Atenciones a ON m.id_mascota=a.id_mascota
	JOIN Servicios s ON s.id_servicio=a.descripcion
	WHERE m.id_mascota = @nro

END
GO
/****** Object:  StoredProcedure [dbo].[pa_consultar_mascotas]    Script Date: 10/11/2021 17:18:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROCEDURE [dbo].[pa_consultar_mascotas]
AS
BEGIN
	
	SELECT * FROM Mascotas;
END
GO
/****** Object:  StoredProcedure [dbo].[pa_consultar_servicios]    Script Date: 10/11/2021 17:18:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pa_consultar_servicios]
AS
BEGIN
	
	SELECT * FROM Servicios;
END
GO
/****** Object:  StoredProcedure [dbo].[pa_consultar_tiposMascotas]    Script Date: 10/11/2021 17:18:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pa_consultar_tiposMascotas]
AS
BEGIN
	
	SELECT * FROM Tipos;
END
GO
/****** Object:  StoredProcedure [dbo].[pa_detalle_atencion_dia]    Script Date: 10/11/2021 17:18:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pa_detalle_atencion_dia]
@fechaDesde datetime,
@fechaHasta Datetime
as

SELECT m.nombre, t.tipo, c.nombre, s.servicio, convert(date,a.fecha), a.importe


--exec pa_detalle_atencion_dia '05/05/2021 00:00:00', '05/12/2021 00:00:00'
FROM MASCOTAS m
INNER JOIN Atenciones A on a.id_mascota = m.id_mascota
INNER JOIN Clientes c on c.id_cliente = m.id_cliente
LEFT JOIN Servicios s on s.id_servicio = a.descripcion
INNER JOIN Tipos t on T.id_tipo = m.id_tipo
where a.fecha between @fechaDesde and @fechaHasta
GO
/****** Object:  StoredProcedure [dbo].[pa_detalle_atencion_mes]    Script Date: 10/11/2021 17:18:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pa_detalle_atencion_mes]
as
SELECT m.nombre, t.tipo, c.nombre, s.servicio, convert(date,a.fecha), a.importe


--exec pa_detalle_atencion_mes
FROM MASCOTAS m
INNER JOIN Atenciones A on a.id_mascota = m.id_mascota
INNER JOIN Clientes c on c.id_cliente = m.id_cliente
LEFT JOIN Servicios s on s.id_servicio = a.descripcion
INNER JOIN Tipos t on T.id_tipo = m.id_tipo
WHERE day(a.fecha) = day(getdate()) AND year(a.fecha) = year(getdate()) AND month(a.fecha) = month(getdate())
GO
/****** Object:  StoredProcedure [dbo].[pa_detalle_atencion_siempre]    Script Date: 10/11/2021 17:18:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pa_detalle_atencion_siempre]
as
SELECT m.nombre, t.tipo, c.nombre, s.servicio, convert(date,a.fecha), a.importe



FROM MASCOTAS m
INNER JOIN Atenciones A on a.id_mascota = m.id_mascota
INNER JOIN Clientes c on c.id_cliente = m.id_cliente
INNER JOIN Servicios s on s.id_servicio = a.descripcion
INNER JOIN Tipos t on T.id_tipo = m.id_tipo
GO
/****** Object:  StoredProcedure [dbo].[pa_eliminar_mascota]    Script Date: 10/11/2021 17:18:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROC [dbo].[pa_eliminar_mascota]
@m_nro int
AS
BEGIN
	UPDATE Mascotas
	SET fallecio = GETDATE()
	WHERE id_mascota = @m_nro
END
GO
/****** Object:  StoredProcedure [dbo].[pa_insertar_atencion]    Script Date: 10/11/2021 17:18:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROCEDURE [dbo].[pa_insertar_atencion] 
	
	 
	@importe decimal (16,2),
	@id_mascota int,
	@detalle varchar(150)
AS
BEGIN
	INSERT INTO Atenciones( fecha, importe, id_mascota, detalle)
    VALUES (GETDATE(), @importe, @id_mascota,@detalle);
  
END
GO
/****** Object:  StoredProcedure [dbo].[pa_insertar_atencion2]    Script Date: 10/11/2021 17:18:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROCEDURE [dbo].[pa_insertar_atencion2] 
	
	 
	@importe decimal (16,2),
	@id_mascota int
AS
BEGIN
	INSERT INTO Atenciones( fecha, importe, id_mascota)
    VALUES (GETDATE(), @importe, @id_mascota);
  
END
GO
/****** Object:  StoredProcedure [dbo].[pa_insertar_mascota]    Script Date: 10/11/2021 17:18:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pa_insertar_mascota]
	@id_mascota int OUTPUT,
	@nombre varchar(50), 
	@fec_nac datetime,
	@id_tipo int,
	@id_cliente int
AS
BEGIN


	INSERT INTO Mascotas(nombre, fec_nac, id_tipo, id_cliente)
    VALUES (@nombre, @fec_nac, @id_tipo, @id_cliente);	
    SET @id_mascota = SCOPE_IDENTITY();

END
GO
/****** Object:  StoredProcedure [dbo].[pa_mascota_filtro]    Script Date: 10/11/2021 17:18:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create procedure [dbo].[pa_mascota_filtro]
	@id_cliente int
	as 
	begin
	SELECT id_mascota, nombre, id_cliente from mascotas
			
		WHERE id_cliente = @id_cliente and fallecio is null
	end







/****** Object:  StoredProcedure [dbo].[pa_insertar_mascota]    Script Date: 8/11/2021 18:18:46 ******/
SET ANSI_NULLS ON
GO
/****** Object:  StoredProcedure [dbo].[pa_proximo_id]    Script Date: 10/11/2021 17:18:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pa_proximo_id]
@next int OUTPUT
AS
BEGIN
	SET @next = (SELECT COUNT(atencion_nro)+1  FROM Atenciones);
END	
GO
/****** Object:  StoredProcedure [dbo].[pa_reporte]    Script Date: 10/11/2021 17:18:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[pa_reporte]
@fec1 date,
@fec2 date
AS
BEGIN
SELECT c.nombre, t.tipo, s.servicio
FROM clientes c
 JOIN mascotas m ON c.id_cliente=m.id_cliente
 JOIN tipos t ON m.id_tipo=t.id_tipo
 JOIN atenciones a ON m.id_mascota=a.id_mascota
 JOIN servicios s ON a.descripcion=s.id_servicio
WHERE a.fecha BETWEEN @fec1 AND @fec2
GROUP BY c.nombre, tipo, servicio
ORDER BY 2
END
GO
/****** Object:  StoredProcedure [dbo].[SP_LOGIN]    Script Date: 10/11/2021 17:18:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_LOGIN]
    @USUARIO VARCHAR(50) = null,
    @CONTRASENA VARCHAR(50) = null,
    @USUARIOS int OUTPUT
AS
BEGIN
    SELECT * FROM T_USUARIOS
    WHERE USUARIO = @USUARIO AND @CONTRASENA = CONTRASENA;

    SELECT @USUARIOS = @@ROWCOUNT;
END
GO
USE [master]
GO
ALTER DATABASE [VETEFINAL] SET  READ_WRITE 
GO
