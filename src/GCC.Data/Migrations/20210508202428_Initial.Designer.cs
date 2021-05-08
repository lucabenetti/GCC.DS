﻿// <auto-generated />
using System;
using GCC.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace GCC.Data.Migrations
{
    [DbContext(typeof(GCCContext))]
    [Migration("20210508202428_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.5")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("GCC.Business.Modelos.CRM", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Numero")
                        .HasColumnType("int");

                    b.Property<int>("UF")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("CRM");
                });

            modelBuilder.Entity("GCC.Business.Modelos.Consulta", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Data")
                        .HasColumnType("datetime2");

                    b.Property<TimeSpan>("Duracao")
                        .HasColumnType("time");

                    b.Property<Guid?>("MedicoId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("PacienteId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("ProntuarioId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Realizada")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("MedicoId");

                    b.HasIndex("PacienteId");

                    b.HasIndex("ProntuarioId");

                    b.ToTable("Consulta");
                });

            modelBuilder.Entity("GCC.Business.Modelos.JornadaDeTrabalho", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Domingo")
                        .HasColumnType("bit");

                    b.Property<DateTime>("HoraFim")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("HoraFimIntervalo")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("HoraInicio")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("HoraInicioIntervalo")
                        .HasColumnType("datetime2");

                    b.Property<bool>("Quarta")
                        .HasColumnType("bit");

                    b.Property<bool>("Quinta")
                        .HasColumnType("bit");

                    b.Property<bool>("Sabado")
                        .HasColumnType("bit");

                    b.Property<bool>("Segunda")
                        .HasColumnType("bit");

                    b.Property<bool>("Sexta")
                        .HasColumnType("bit");

                    b.Property<bool>("Terca")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("JornadaDeTrabalho");
                });

            modelBuilder.Entity("GCC.Business.Modelos.Medico", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CPF")
                        .IsRequired()
                        .HasColumnType("varchar(11)");

                    b.Property<Guid?>("CRMId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("DataNascimento")
                        .HasColumnType("datetime2");

                    b.Property<string>("Endereco")
                        .IsRequired()
                        .HasColumnType("varchar(200)");

                    b.Property<string>("Especialidade")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("JornadaDeTrabalhoId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("varchar(200)");

                    b.Property<int>("Sexo")
                        .HasColumnType("int");

                    b.Property<string>("Telefone")
                        .IsRequired()
                        .HasColumnType("varchar(11)");

                    b.Property<Guid>("UsuarioId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("CRMId");

                    b.HasIndex("JornadaDeTrabalhoId");

                    b.ToTable("Medico");
                });

            modelBuilder.Entity("GCC.Business.Modelos.Paciente", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CPF")
                        .IsRequired()
                        .HasColumnType("varchar(11)");

                    b.Property<DateTime>("DataNascimento")
                        .HasColumnType("datetime2");

                    b.Property<string>("Endereco")
                        .IsRequired()
                        .HasColumnType("varchar(200)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("varchar(200)");

                    b.Property<string>("NomeDaMae")
                        .IsRequired()
                        .HasColumnType("varchar(200)");

                    b.Property<int>("Sexo")
                        .HasColumnType("int");

                    b.Property<string>("Telefone")
                        .IsRequired()
                        .HasColumnType("varchar(11)");

                    b.Property<Guid>("UsuarioId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("Paciente");
                });

            modelBuilder.Entity("GCC.Business.Modelos.Prontuario", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasColumnType("varchar(500)");

                    b.HasKey("Id");

                    b.ToTable("Prontuario");
                });

            modelBuilder.Entity("GCC.Business.Modelos.Secretaria", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CPF")
                        .IsRequired()
                        .HasColumnType("varchar(11)");

                    b.Property<DateTime>("DataNascimento")
                        .HasColumnType("datetime2");

                    b.Property<string>("Endereco")
                        .IsRequired()
                        .HasColumnType("varchar(200)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("varchar(200)");

                    b.Property<int>("Sexo")
                        .HasColumnType("int");

                    b.Property<string>("Telefone")
                        .IsRequired()
                        .HasColumnType("varchar(11)");

                    b.Property<Guid>("UsuarioId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("Secretaria");
                });

            modelBuilder.Entity("GCC.Business.Modelos.Consulta", b =>
                {
                    b.HasOne("GCC.Business.Modelos.Medico", "Medico")
                        .WithMany()
                        .HasForeignKey("MedicoId");

                    b.HasOne("GCC.Business.Modelos.Paciente", "Paciente")
                        .WithMany("Consultas")
                        .HasForeignKey("PacienteId");

                    b.HasOne("GCC.Business.Modelos.Prontuario", "Prontuario")
                        .WithMany()
                        .HasForeignKey("ProntuarioId");

                    b.Navigation("Medico");

                    b.Navigation("Paciente");

                    b.Navigation("Prontuario");
                });

            modelBuilder.Entity("GCC.Business.Modelos.Medico", b =>
                {
                    b.HasOne("GCC.Business.Modelos.CRM", "CRM")
                        .WithMany()
                        .HasForeignKey("CRMId");

                    b.HasOne("GCC.Business.Modelos.JornadaDeTrabalho", "JornadaDeTrabalho")
                        .WithMany()
                        .HasForeignKey("JornadaDeTrabalhoId");

                    b.Navigation("CRM");

                    b.Navigation("JornadaDeTrabalho");
                });

            modelBuilder.Entity("GCC.Business.Modelos.Paciente", b =>
                {
                    b.Navigation("Consultas");
                });
#pragma warning restore 612, 618
        }
    }
}