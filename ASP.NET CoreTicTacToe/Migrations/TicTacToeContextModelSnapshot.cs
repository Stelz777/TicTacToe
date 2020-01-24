﻿// <auto-generated />
using System;
using ASP.NETCoreTicTacToe.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ASP.NETCoreTicTacToe.Migrations
{
    [DbContext(typeof(TicTacToeContext))]
    partial class TicTacToeContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ASP.NET_CoreTicTacToe.Infrastructure.DTO.BoardDataTransferObject", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("SerializedSquares")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Boards");
                });

            modelBuilder.Entity("ASP.NET_CoreTicTacToe.Infrastructure.DTO.GameDataTransferObject", b =>
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

            modelBuilder.Entity("ASP.NET_CoreTicTacToe.Infrastructure.DTO.HistoryDataTransferObject", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("Histories");
                });

            modelBuilder.Entity("ASP.NET_CoreTicTacToe.Infrastructure.DTO.TurnDataTransferObject", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("CellNumber")
                        .HasColumnType("int");

                    b.Property<Guid?>("HistoryDataTransferObjectId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("HistoryId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("WhichTurn")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("HistoryDataTransferObjectId");

                    b.ToTable("Turns");
                });

            modelBuilder.Entity("ASP.NET_CoreTicTacToe.Infrastructure.DTO.GameDataTransferObject", b =>
                {
                    b.HasOne("ASP.NET_CoreTicTacToe.Infrastructure.DTO.BoardDataTransferObject", "Board")
                        .WithMany()
                        .HasForeignKey("BoardId");

                    b.HasOne("ASP.NET_CoreTicTacToe.Infrastructure.DTO.HistoryDataTransferObject", "History")
                        .WithMany()
                        .HasForeignKey("HistoryId");
                });

            modelBuilder.Entity("ASP.NET_CoreTicTacToe.Infrastructure.DTO.TurnDataTransferObject", b =>
                {
                    b.HasOne("ASP.NET_CoreTicTacToe.Infrastructure.DTO.HistoryDataTransferObject", null)
                        .WithMany("Turns")
                        .HasForeignKey("HistoryDataTransferObjectId");
                });
#pragma warning restore 612, 618
        }
    }
}
