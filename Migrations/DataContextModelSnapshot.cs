﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ShortalkB2.Service.Context;

#nullable disable

namespace ShortalkB2.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ShortalkB2.Models.GameModel", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<string>("BuzzWords")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("GamePhase")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Host")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("NumberOfRounds")
                        .HasColumnType("int");

                    b.Property<string>("OnePointWord")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("OnePointWordHasBeenSaid")
                        .HasColumnType("bit");

                    b.Property<string>("OnePointWords")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PlayerA1")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PlayerA2")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PlayerA3")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PlayerA4")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PlayerA5")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PlayerB1")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PlayerB2")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PlayerB3")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PlayerB4")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PlayerB5")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("ReadyStatusA1")
                        .HasColumnType("bit");

                    b.Property<bool>("ReadyStatusA2")
                        .HasColumnType("bit");

                    b.Property<bool>("ReadyStatusA3")
                        .HasColumnType("bit");

                    b.Property<bool>("ReadyStatusA4")
                        .HasColumnType("bit");

                    b.Property<bool>("ReadyStatusA5")
                        .HasColumnType("bit");

                    b.Property<bool>("ReadyStatusB1")
                        .HasColumnType("bit");

                    b.Property<bool>("ReadyStatusB2")
                        .HasColumnType("bit");

                    b.Property<bool>("ReadyStatusB3")
                        .HasColumnType("bit");

                    b.Property<bool>("ReadyStatusB4")
                        .HasColumnType("bit");

                    b.Property<bool>("ReadyStatusB5")
                        .HasColumnType("bit");

                    b.Property<string>("RoomName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Round")
                        .HasColumnType("int");

                    b.Property<string>("SkippedWords")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Speaker")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TeamAScore")
                        .HasColumnType("int");

                    b.Property<int>("TeamBScore")
                        .HasColumnType("int");

                    b.Property<string>("ThreePointWord")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("ThreePointWordHasBeenSaid")
                        .HasColumnType("bit");

                    b.Property<string>("ThreePointWords")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Time")
                        .HasColumnType("int");

                    b.Property<int>("TimeLimit")
                        .HasColumnType("int");

                    b.Property<int>("TurnNumber")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.ToTable("GameInfo");
                });
#pragma warning restore 612, 618
        }
    }
}
