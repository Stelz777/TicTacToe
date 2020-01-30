﻿// <auto-generated />
using System;
using ASP.NETCoreTicTacToe.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ASP.NETCoreTicTacToe.Migrations
{
    [DbContext(typeof(TicTacToeContext))]
    [Migration("20200127143155_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ASP.NETCoreTicTacToe.Infrastructure.DTO.BoardDataTransferObject", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("SerializedSquares")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Boards");
                });

            modelBuilder.Entity("ASP.NETCoreTicTacToe.Infrastructure.DTO.GameDataTransferObject", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<Guid?>("BoardId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("HistoryId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("ID");

                    b.HasIndex("BoardId");

                    b.HasIndex("HistoryId");

                    b.ToTable("Games");
                });

            modelBuilder.Entity("ASP.NETCoreTicTacToe.Infrastructure.DTO.HistoryDataTransferObject", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("Histories");
                });

            modelBuilder.Entity("ASP.NETCoreTicTacToe.Infrastructure.DTO.TurnDataTransferObject", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("CellNumber")
                        .HasColumnType("int");

                    b.Property<Guid>("HistoryDataTransferObjectId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("WhichTurn")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("HistoryDataTransferObjectId");

                    b.ToTable("Turns");
                });

            modelBuilder.Entity("ASP.NETCoreTicTacToe.Infrastructure.DTO.GameDataTransferObject", b =>
                {
                    b.HasOne("ASP.NETCoreTicTacToe.Infrastructure.DTO.BoardDataTransferObject", "Board")
                        .WithMany()
                        .HasForeignKey("BoardId");

                    b.HasOne("ASP.NETCoreTicTacToe.Infrastructure.DTO.HistoryDataTransferObject", "History")
                        .WithMany()
                        .HasForeignKey("HistoryId");
                });

            modelBuilder.Entity("ASP.NETCoreTicTacToe.Infrastructure.DTO.TurnDataTransferObject", b =>
                {
                    b.HasOne("ASP.NETCoreTicTacToe.Infrastructure.DTO.HistoryDataTransferObject", null)
                        .WithMany("Turns")
                        .HasForeignKey("HistoryDataTransferObjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
