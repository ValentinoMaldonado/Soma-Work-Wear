USE DBCARRITO;
GO

IF OBJECT_ID('dbo.sp_RegistrarUsuario', 'P') IS NOT NULL
    DROP PROCEDURE dbo.sp_RegistrarUsuario;
GO

CREATE PROCEDURE dbo.sp_RegistrarUsuario
(
    @Nombres varchar(100),
    @Apellidos varchar(100),
    @Correo varchar(100),
    @Clave varchar(150),
    @Reestablecer bit,
    @Activo bit,
    @Mensaje varchar(500) OUTPUT,
    @Resultado int OUTPUT
)
AS
BEGIN
    SET NOCOUNT ON;
    SET @Resultado = 0;
    SET @Mensaje = '';

    IF NOT EXISTS (SELECT 1 FROM dbo.USUARIO WHERE Correo = @Correo)
    BEGIN
        INSERT INTO dbo.USUARIO (Nombres, Apellidos, Correo, Clave, Reestablecer, Activo)
        VALUES (@Nombres, @Apellidos, @Correo, @Clave, @Reestablecer, @Activo);

        SET @Resultado = SCOPE_IDENTITY();
    END
    ELSE
    BEGIN
        SET @Mensaje = 'El correo del usuario ya existe';
    END
END
GO

IF OBJECT_ID('dbo.sp_EditarUsuario', 'P') IS NOT NULL
    DROP PROCEDURE dbo.sp_EditarUsuario;
GO

CREATE PROCEDURE dbo.sp_EditarUsuario
(
    @IdUsuario int,
    @Nombres varchar(100),
    @Apellidos varchar(100),
    @Correo varchar(100),
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
        FROM dbo.USUARIO
        WHERE Correo = @Correo
          AND IdUsuario <> @IdUsuario
    )
    BEGIN
        UPDATE dbo.USUARIO
        SET Nombres = @Nombres,
            Apellidos = @Apellidos,
            Correo = @Correo,
            Activo = @Activo
        WHERE IdUsuario = @IdUsuario;

        SET @Resultado = CASE WHEN @@ROWCOUNT > 0 THEN 1 ELSE 0 END;
    END
    ELSE
    BEGIN
        SET @Mensaje = 'El correo del usuario ya existe';
    END
END
GO
