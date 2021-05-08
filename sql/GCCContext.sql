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

CREATE TABLE [CRM] (
    [Id] uniqueidentifier NOT NULL,
    [Numero] int NOT NULL,
    [UF] int NOT NULL,
    CONSTRAINT [PK_CRM] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [JornadaDeTrabalho] (
    [Id] uniqueidentifier NOT NULL,
    [Domingo] bit NOT NULL,
    [Segunda] bit NOT NULL,
    [Terca] bit NOT NULL,
    [Quarta] bit NOT NULL,
    [Quinta] bit NOT NULL,
    [Sexta] bit NOT NULL,
    [Sabado] bit NOT NULL,
    [HoraInicio] datetime2 NOT NULL,
    [HoraFim] datetime2 NOT NULL,
    [HoraInicioIntervalo] datetime2 NOT NULL,
    [HoraFimIntervalo] datetime2 NOT NULL,
    CONSTRAINT [PK_JornadaDeTrabalho] PRIMARY KEY ([Id])
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
    [Telefone] varchar(11) NOT NULL,
    [DataNascimento] datetime2 NOT NULL,
    CONSTRAINT [PK_Paciente] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Prontuario] (
    [Id] uniqueidentifier NOT NULL,
    [Descricao] varchar(500) NOT NULL,
    CONSTRAINT [PK_Prontuario] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Secretaria] (
    [Id] uniqueidentifier NOT NULL,
    [UsuarioId] uniqueidentifier NOT NULL,
    [Nome] varchar(200) NOT NULL,
    [CPF] varchar(11) NOT NULL,
    [Sexo] int NOT NULL,
    [Endereco] varchar(200) NOT NULL,
    [Telefone] varchar(11) NOT NULL,
    [DataNascimento] datetime2 NOT NULL,
    CONSTRAINT [PK_Secretaria] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Medico] (
    [Id] uniqueidentifier NOT NULL,
    [CRMId] uniqueidentifier NULL,
    [Especialidade] nvarchar(max) NULL,
    [JornadaDeTrabalhoId] uniqueidentifier NULL,
    [UsuarioId] uniqueidentifier NOT NULL,
    [Nome] varchar(200) NOT NULL,
    [CPF] varchar(11) NOT NULL,
    [Sexo] int NOT NULL,
    [Endereco] varchar(200) NOT NULL,
    [Telefone] varchar(11) NOT NULL,
    [DataNascimento] datetime2 NOT NULL,
    CONSTRAINT [PK_Medico] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Medico_CRM_CRMId] FOREIGN KEY ([CRMId]) REFERENCES [CRM] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Medico_JornadaDeTrabalho_JornadaDeTrabalhoId] FOREIGN KEY ([JornadaDeTrabalhoId]) REFERENCES [JornadaDeTrabalho] ([Id]) ON DELETE NO ACTION
);
GO

CREATE TABLE [Consulta] (
    [Id] uniqueidentifier NOT NULL,
    [Data] datetime2 NOT NULL,
    [Duracao] time NOT NULL,
    [ProntuarioId] uniqueidentifier NULL,
    [PacienteId] uniqueidentifier NULL,
    [MedicoId] uniqueidentifier NULL,
    [Realizada] bit NOT NULL,
    CONSTRAINT [PK_Consulta] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Consulta_Medico_MedicoId] FOREIGN KEY ([MedicoId]) REFERENCES [Medico] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Consulta_Paciente_PacienteId] FOREIGN KEY ([PacienteId]) REFERENCES [Paciente] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Consulta_Prontuario_ProntuarioId] FOREIGN KEY ([ProntuarioId]) REFERENCES [Prontuario] ([Id]) ON DELETE NO ACTION
);
GO

CREATE INDEX [IX_Consulta_MedicoId] ON [Consulta] ([MedicoId]);
GO

CREATE INDEX [IX_Consulta_PacienteId] ON [Consulta] ([PacienteId]);
GO

CREATE INDEX [IX_Consulta_ProntuarioId] ON [Consulta] ([ProntuarioId]);
GO

CREATE INDEX [IX_Medico_CRMId] ON [Medico] ([CRMId]);
GO

CREATE INDEX [IX_Medico_JornadaDeTrabalhoId] ON [Medico] ([JornadaDeTrabalhoId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20210508202428_Initial', N'5.0.5');
GO

COMMIT;
GO

