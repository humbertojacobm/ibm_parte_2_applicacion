
USE [master];
go
CREATE DATABASE Parte02ProblemaPractico;
GO  
-- Verify the database files and sizes  
SELECT name, size, size*1.0/128 AS [Size in MBs]   
FROM sys.master_files  
WHERE name = N'Parte02ProblemaPractico'; 
GO

USE Parte02ProblemaPractico;


GO
PRINT N'Creando [dbo].[Banco]...';


GO
CREATE TABLE [dbo].[Banco] (
    [IDBanco]       INT           IDENTITY (1, 1) NOT NULL,
    [Nombre]        VARCHAR (128) NOT NULL,
    [Direccion]     VARCHAR (250) NOT NULL,
    [FechaRegistro] DATETIME      NOT NULL,
    PRIMARY KEY CLUSTERED ([IDBanco] ASC)
);


GO
PRINT N'Creando [dbo].[EstadoPago]...';


GO
CREATE TABLE [dbo].[EstadoPago] (
    [IDEstado] TINYINT      NOT NULL,
    [Nombre]   VARCHAR (64) NOT NULL,
    PRIMARY KEY CLUSTERED ([IDEstado] ASC)
);


GO
PRINT N'Creando [dbo].[Moneda]...';


GO
CREATE TABLE [dbo].[Moneda] (
    [IDMoneda] TINYINT      NOT NULL,
    [Nombre]   VARCHAR (64) NOT NULL,
    PRIMARY KEY CLUSTERED ([IDMoneda] ASC)
);


GO
PRINT N'Creando [dbo].[OrdenPago]...';


GO
CREATE TABLE [dbo].[OrdenPago] (
    [IDOrdenPago] BIGINT          IDENTITY (1, 1) NOT NULL,
    [Monto]       DECIMAL (16, 2) NOT NULL,
    [IDMoneda]    TINYINT         NOT NULL,
    [IDEstado]    TINYINT         NOT NULL,
    [FechaPago]   DATETIME        NOT NULL,
    PRIMARY KEY CLUSTERED ([IDOrdenPago] ASC)
);


GO
PRINT N'Creando [dbo].[Sucursales]...';


GO
CREATE TABLE [dbo].[Sucursales] (
    [IDSucursales]  INT           IDENTITY (1, 1) NOT NULL,
    [Nombre]        VARCHAR (50)  NOT NULL,
    [Direccion]     VARCHAR (250) NOT NULL,
    [FechaRegistro] DATETIME      NOT NULL,
    [IDBanco]       INT           NOT NULL,
    PRIMARY KEY CLUSTERED ([IDSucursales] ASC)
);


GO
PRINT N'Creando [dbo].[SucursalesOrdenPago]...';


GO
CREATE TABLE [dbo].[SucursalesOrdenPago] (
    [IDSucursales] INT      NOT NULL,
    [IDOrdenPago]  BIGINT   NOT NULL,
    [FechaPago]    DATETIME NOT NULL,
    CONSTRAINT [PK_SucursalesOrdenPago] PRIMARY KEY CLUSTERED ([IDSucursales] ASC, [IDOrdenPago] ASC)
);


GO
PRINT N'Creando restricción sin nombre en [dbo].[Banco]...';


GO
ALTER TABLE [dbo].[Banco]
    ADD DEFAULT (getdate()) FOR [FechaRegistro];


GO
PRINT N'Creando restricción sin nombre en [dbo].[OrdenPago]...';


GO
ALTER TABLE [dbo].[OrdenPago]
    ADD DEFAULT (getdate()) FOR [FechaPago];


GO
PRINT N'Creando restricción sin nombre en [dbo].[Sucursales]...';


GO
ALTER TABLE [dbo].[Sucursales]
    ADD DEFAULT (getdate()) FOR [FechaRegistro];


GO
PRINT N'Creando restricción sin nombre en [dbo].[SucursalesOrdenPago]...';


GO
ALTER TABLE [dbo].[SucursalesOrdenPago]
    ADD DEFAULT (getdate()) FOR [FechaPago];


GO
PRINT N'Creando restricción sin nombre en [dbo].[OrdenPago]...';


GO
ALTER TABLE [dbo].[OrdenPago]
    ADD FOREIGN KEY ([IDMoneda]) REFERENCES [dbo].[Moneda] ([IDMoneda]);


GO
PRINT N'Creando restricción sin nombre en [dbo].[OrdenPago]...';


GO
ALTER TABLE [dbo].[OrdenPago]
    ADD FOREIGN KEY ([IDEstado]) REFERENCES [dbo].[EstadoPago] ([IDEstado]);


GO
PRINT N'Creando restricción sin nombre en [dbo].[Sucursales]...';


GO
ALTER TABLE [dbo].[Sucursales]
    ADD FOREIGN KEY ([IDBanco]) REFERENCES [dbo].[Banco] ([IDBanco]);


GO
PRINT N'Creando restricción sin nombre en [dbo].[SucursalesOrdenPago]...';


GO
ALTER TABLE [dbo].[SucursalesOrdenPago]
    ADD FOREIGN KEY ([IDSucursales]) REFERENCES [dbo].[Sucursales] ([IDSucursales]);


GO
PRINT N'Creando restricción sin nombre en [dbo].[SucursalesOrdenPago]...';


GO
ALTER TABLE [dbo].[SucursalesOrdenPago]
    ADD FOREIGN KEY ([IDOrdenPago]) REFERENCES [dbo].[OrdenPago] ([IDOrdenPago]);


GO
DECLARE @VarDecimalSupported AS BIT;

SELECT @VarDecimalSupported = 0;

IF ((ServerProperty(N'EngineEdition') = 3)
    AND (((@@microsoftversion / power(2, 24) = 9)
          AND (@@microsoftversion & 0xffff >= 3024))
         OR ((@@microsoftversion / power(2, 24) = 10)
             AND (@@microsoftversion & 0xffff >= 1600))))
    SELECT @VarDecimalSupported = 1;

IF (@VarDecimalSupported > 0)
    BEGIN
        EXECUTE sp_db_vardecimal_storage_format N'$(DatabaseName)', 'ON';
    END


GO
PRINT N'Actualización completada.';


GO

INSERT INTO dbo.EstadoPago( IDEstado, Nombre )VALUES  ( 1,'Pagada' )
INSERT INTO dbo.EstadoPago( IDEstado, Nombre )VALUES  ( 2,'Declinada' )
INSERT INTO dbo.EstadoPago( IDEstado, Nombre )VALUES  ( 3,'Fallida' )
INSERT INTO dbo.EstadoPago( IDEstado, Nombre )VALUES  ( 4,'Anulada' )

INSERT INTO dbo.Moneda( IDMoneda, Nombre )VALUES  ( 1, 'Soles')
INSERT INTO dbo.Moneda( IDMoneda, Nombre )VALUES  ( 2, 'Dolares')