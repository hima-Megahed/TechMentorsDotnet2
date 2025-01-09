﻿// <auto-generated />
using System;
using DoctorAvailability.Data.DbContext;
using DoctorAvailability.Internal.Data.DbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DoctorAvailability.Data.DbContext.Migrations
{
    [DbContext(typeof(DoctorAvailabilityContext))]
    [Migration("20250107173117_Init")]
    partial class Init
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("DoctorAvailability")
                .HasAnnotation("ProductVersion", "9.0.0");

            modelBuilder.Entity("DoctorAvailability.Data.Models.DoctorSlot", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<decimal>("Cost")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("Date")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("DoctorId")
                        .HasColumnType("TEXT");

                    b.Property<string>("DoctorName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsReserved")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("DoctorSlots", "DoctorAvailability");
                });
#pragma warning restore 612, 618
        }
    }
}
