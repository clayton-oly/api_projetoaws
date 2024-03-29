﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using TrioConnect.Data;

#nullable disable

namespace TrioConnect.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240226163321_MygrationAWS")]
    partial class MygrationAWS
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseSerialColumns(modelBuilder);

            modelBuilder.Entity("TrioConnect.model.Tema", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseSerialColumn(b.Property<int>("ID"));

                    b.Property<string>("Descricao")
                        .HasColumnType("text");

                    b.HasKey("ID");

                    b.ToTable("tb_temas");
                });

            modelBuilder.Entity("TrioConnect.model.Usuario", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseSerialColumn(b.Property<int>("ID"));

                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.Property<string>("Foto")
                        .HasColumnType("text");

                    b.Property<string>("Nome")
                        .HasColumnType("text");

                    b.HasKey("ID");

                    b.ToTable("tb_usuarios");
                });

            modelBuilder.Entity("TrioConnect.Postagem", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseSerialColumn(b.Property<int>("ID"));

                    b.Property<DateTime>("Data")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("TemaID")
                        .HasColumnType("integer");

                    b.Property<string>("Texto")
                        .HasColumnType("text");

                    b.Property<string>("Titulo")
                        .HasColumnType("text");

                    b.Property<int>("UsuarioID")
                        .HasColumnType("integer");

                    b.HasKey("ID");

                    b.HasIndex("TemaID");

                    b.HasIndex("UsuarioID");

                    b.ToTable("tb_postagens");
                });

            modelBuilder.Entity("TrioConnect.Postagem", b =>
                {
                    b.HasOne("TrioConnect.model.Tema", "Tema")
                        .WithMany("Postagens")
                        .HasForeignKey("TemaID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TrioConnect.model.Usuario", "Usuario")
                        .WithMany("Postagens")
                        .HasForeignKey("UsuarioID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Tema");

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("TrioConnect.model.Tema", b =>
                {
                    b.Navigation("Postagens");
                });

            modelBuilder.Entity("TrioConnect.model.Usuario", b =>
                {
                    b.Navigation("Postagens");
                });
#pragma warning restore 612, 618
        }
    }
}
