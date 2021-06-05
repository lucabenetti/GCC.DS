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

