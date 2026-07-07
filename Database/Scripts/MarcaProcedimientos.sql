USE DBCARRITO;
GO

IF OBJECT_ID('dbo.sp_RegistrarMarca', 'P') IS NOT NULL
    DROP PROCEDURE dbo.sp_RegistrarMarca;
GO

CREATE PROCEDURE dbo.sp_RegistrarMarca
(
    @Descripcion varchar(100),
    @Activo bit,
    @Mensaje varchar(500) OUTPUT,
    @Resultado int OUTPUT
)
AS
BEGIN
    SET NOCOUNT ON;
    SET @Resultado = 0;
    SET @Mensaje = '';

    IF NOT EXISTS (SELECT 1 FROM dbo.MARCA WHERE Descripcion = @Descripcion)
    BEGIN
        INSERT INTO dbo.MARCA (Descripcion, Activo)
        VALUES (@Descripcion, @Activo);

        SET @Resultado = SCOPE_IDENTITY();
    END
    ELSE
    BEGIN
        SET @Mensaje = 'La marca ya existe';
    END
END
GO

IF OBJECT_ID('dbo.sp_EditarMarca', 'P') IS NOT NULL
    DROP PROCEDURE dbo.sp_EditarMarca;
GO

CREATE PROCEDURE dbo.sp_EditarMarca
(
    @IdMarca int,
    @Descripcion varchar(100),
    @Activo bit,
    @Mensaje varchar(500) OUTPUT,
    @Resultado bit OUTPUT
)
AS
BEGIN
    SET NOCOUNT ON;
    SET @Resultado = 0;
    SET @Mensaje = '';

    IF NOT EXISTS (
        SELECT 1
        FROM dbo.MARCA
        WHERE Descripcion = @Descripcion
          AND IdMarca <> @IdMarca
    )
    BEGIN
        UPDATE TOP (1) dbo.MARCA
        SET Descripcion = @Descripcion,
            Activo = @Activo
        WHERE IdMarca = @IdMarca;

        SET @Resultado = CASE WHEN @@ROWCOUNT > 0 THEN 1 ELSE 0 END;
    END
    ELSE
    BEGIN
        SET @Mensaje = 'La marca ya existe';
    END
END
GO

IF OBJECT_ID('dbo.sp_EliminarMarca', 'P') IS NOT NULL
    DROP PROCEDURE dbo.sp_EliminarMarca;
GO

CREATE PROCEDURE dbo.sp_EliminarMarca
(
    @IdMarca int,
    @Mensaje varchar(500) OUTPUT,
    @Resultado bit OUTPUT
)
AS
BEGIN
    SET NOCOUNT ON;
    SET @Resultado = 0;
    SET @Mensaje = '';

    IF NOT EXISTS (
        SELECT 1
        FROM dbo.PRODUCTO
        WHERE IdMarca = @IdMarca
    )
    BEGIN
        DELETE TOP (1)
        FROM dbo.MARCA
        WHERE IdMarca = @IdMarca;

        SET @Resultado = CASE WHEN @@ROWCOUNT > 0 THEN 1 ELSE 0 END;
    END
    ELSE
    BEGIN
        SET @Mensaje = 'La marca se encuentra relacionada a un producto';
    END
END
GO
