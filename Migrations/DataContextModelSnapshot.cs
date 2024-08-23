﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
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
            modelBuilder.HasAnnotation("ProductVersion", "8.0.8");

            modelBuilder.Entity("ShortalkB2.Models.LobbyRoomModel", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Host")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("LobbyName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("NumberOfRounds")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("ReadyStatusA1")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("ReadyStatusA2")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("ReadyStatusA3")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("ReadyStatusA4")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("ReadyStatusA5")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("ReadyStatusB1")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("ReadyStatusB2")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("ReadyStatusB3")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("ReadyStatusB4")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("ReadyStatusB5")
                        .HasColumnType("INTEGER");

                    b.Property<string>("TeamMemberA1")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("TeamMemberA2")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("TeamMemberA3")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("TeamMemberA4")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("TeamMemberA5")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("TeamMemberB1")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("TeamMemberB2")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("TeamMemberB3")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("TeamMemberB4")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("TeamMemberB5")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("TimeLimit")
                        .HasColumnType("INTEGER");

                    b.HasKey("ID");

                    b.ToTable("LobbyInfo");
                });
#pragma warning restore 612, 618
        }
    }
}