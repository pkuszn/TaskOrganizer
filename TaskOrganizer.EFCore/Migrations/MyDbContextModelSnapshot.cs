﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TaskOrganizer.EFCore;

namespace TaskOrganizer.EFCore.Migrations
{
    [DbContext(typeof(MyDbContext))]
    partial class MyDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.7");

            modelBuilder.Entity("TaskOrganizer.EFCore.TaskContext", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("DoneTaskDate")
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsDoneTask")
                        .HasColumnType("INTEGER");

                    b.Property<string>("TaskDesc")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("tasks");
                });
#pragma warning restore 612, 618
        }
    }
}
