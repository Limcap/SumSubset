﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using QuizApi.DAL;

namespace QuizzApi.DAL.Migrations
{
    [DbContext(typeof(MyDbContext))]
    [Migration("20220601035527_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.17");

            modelBuilder.Entity("QuizApi.Models.Quiz", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("Date")
                        .HasColumnType("TEXT");

                    b.Property<string>("Sequence")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Solution")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Target")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Quiz");
                });
#pragma warning restore 612, 618
        }
    }
}