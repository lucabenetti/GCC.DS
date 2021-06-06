IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [Medico] (
    [Id] uniqueidentifier NOT NULL,
    [CRMNumero] int NULL,
    [CRMUF] int NULL,
    [Especialidade] nvarchar(max) NULL,
    [Domingo] bit NULL,
    [Segunda] bit NULL,
    [Terca] bit NULL,
    [Quarta] bit NULL,
    [Quinta] bit NULL,
    [Sexta] bit NULL,
    [Sabado] bit NULL,
    [HoraInicio] datetime2 NULL,
    [HoraFim] datetime2 NULL,
    [HoraInicioIntervalo] datetime2 NULL,
    [HoraFimIntervalo] datetime2 NULL,
    [UsuarioId] uniqueidentifier NOT NULL,
    [Nome] varchar(200) NOT NULL,
    [CPF] varchar(11) NOT NULL,
    [Sexo] int NOT NULL,
    [Endereco] varchar(200) NOT NULL,
    [Telefone] varchar(11) NULL,
    [DataNascimento] datetime2 NOT NULL,
    CONSTRAINT [PK_Medico] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Paciente] (
    [Id] uniqueidentifier NOT NULL,
    [NomeDaMae] varchar(200) NOT NULL,
    [UsuarioId] uniqueidentifier NOT NULL,
    [Nome] varchar(200) NOT NULL,
    [CPF] varchar(11) NOT NULL,
    [Sexo] int NOT NULL,
    [Endereco] varchar(200) NOT NULL,
    [Telefone] varchar(11) NULL,
    [DataNascimento] datetime2 NOT NULL,
    CONSTRAINT [PK_Paciente] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Secretaria] (
    [Id] uniqueidentifier NOT NULL,
    [UsuarioId] uniqueidentifier NOT NULL,
    [Nome] varchar(200) NOT NULL,
    [CPF] varchar(11) NOT NULL,
    [Sexo] int NOT NULL,
    [Endereco] varchar(200) NOT NULL,
    [Telefone] varchar(11) NULL,
    [DataNascimento] datetime2 NOT NULL,
    CONSTRAINT [PK_Secretaria] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Consulta] (
    [Id] uniqueidentifier NOT NULL,
    [Data] datetime2 NOT NULL,
    [Duracao] time NOT NULL,
    [Realizada] bit NOT NULL,
    [PacienteId] uniqueidentifier NOT NULL,
    [MedicoId] uniqueidentifier NOT NULL,
    CONSTRAINT [PK_Consulta] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Consulta_Medico_MedicoId] FOREIGN KEY ([MedicoId]) REFERENCES [Medico] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Consulta_Paciente_PacienteId] FOREIGN KEY ([PacienteId]) REFERENCES [Paciente] ([Id]) ON DELETE NO ACTION
);
GO

CREATE INDEX [IX_Consulta_MedicoId] ON [Consulta] ([MedicoId]);
GO

CREATE INDEX [IX_Consulta_PacienteId] ON [Consulta] ([PacienteId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20210605164057_Initial', N'5.0.5');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [Consulta] ADD [ExameId] uniqueidentifier NOT NULL DEFAULT '00000000-0000-0000-0000-000000000000';
GO

ALTER TABLE [Consulta] ADD [Observacao] nvarchar(max) NULL;
GO

ALTER TABLE [Consulta] ADD [Receita] nvarchar(max) NULL;
GO

CREATE TABLE [Exame] (
    [Id] uniqueidentifier NOT NULL,
    [Nome] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Exame] PRIMARY KEY ([Id])
);
GO

CREATE INDEX [IX_Consulta_ExameId] ON [Consulta] ([ExameId]);
GO

ALTER TABLE [Consulta] ADD CONSTRAINT [FK_Consulta_Exame_ExameId] FOREIGN KEY ([ExameId]) REFERENCES [Exame] ([Id]) ON DELETE NO ACTION;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20210605195549_Initial2', N'5.0.5');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [Consulta] DROP CONSTRAINT [FK_Consulta_Exame_ExameId];
GO

DROP INDEX [IX_Consulta_ExameId] ON [Consulta];
GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Consulta]') AND [c].[name] = N'ExameId');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [Consulta] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [Consulta] DROP COLUMN [ExameId];
GO

CREATE TABLE [ConsultaExame] (
    [ConsultaId] uniqueidentifier NOT NULL,
    [ExameId] uniqueidentifier NOT NULL,
    CONSTRAINT [PK_ConsultaExame] PRIMARY KEY ([ConsultaId], [ExameId]),
    CONSTRAINT [FK_ConsultaExame_Consulta_ConsultaId] FOREIGN KEY ([ConsultaId]) REFERENCES [Consulta] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_ConsultaExame_Exame_ExameId] FOREIGN KEY ([ExameId]) REFERENCES [Exame] ([Id]) ON DELETE NO ACTION
);
GO

CREATE INDEX [IX_ConsultaExame_ExameId] ON [ConsultaExame] ([ExameId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20210605200618_Initial3', N'5.0.5');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [Consulta] DROP CONSTRAINT [FK_Consulta_Medico_MedicoId];
GO

ALTER TABLE [Consulta] DROP CONSTRAINT [FK_Consulta_Paciente_PacienteId];
GO

ALTER TABLE [ConsultaExame] DROP CONSTRAINT [FK_ConsultaExame_Consulta_ConsultaId];
GO

ALTER TABLE [ConsultaExame] DROP CONSTRAINT [FK_ConsultaExame_Exame_ExameId];
GO

ALTER TABLE [Consulta] ADD CONSTRAINT [FK_Consulta_Medico_MedicoId] FOREIGN KEY ([MedicoId]) REFERENCES [Medico] ([Id]) ON DELETE CASCADE;
GO

ALTER TABLE [Consulta] ADD CONSTRAINT [FK_Consulta_Paciente_PacienteId] FOREIGN KEY ([PacienteId]) REFERENCES [Paciente] ([Id]) ON DELETE CASCADE;
GO

ALTER TABLE [ConsultaExame] ADD CONSTRAINT [FK_ConsultaExame_Consulta_ConsultaId] FOREIGN KEY ([ConsultaId]) REFERENCES [Consulta] ([Id]) ON DELETE CASCADE;
GO

ALTER TABLE [ConsultaExame] ADD CONSTRAINT [FK_ConsultaExame_Exame_ExameId] FOREIGN KEY ([ExameId]) REFERENCES [Exame] ([Id]) ON DELETE CASCADE;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20210606121909_Initial5', N'5.0.5');
GO

COMMIT;
GO

