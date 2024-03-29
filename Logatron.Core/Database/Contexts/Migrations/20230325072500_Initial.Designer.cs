﻿// <auto-generated />
using Logatron.Core.Database.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Logatron.Core.Database.Migrations;

[DbContext(typeof(LogbookContext))]
[Migration("20230325072500_Initial")]
partial class Initial
{
    /// <inheritdoc />
    protected override void BuildTargetModel(ModelBuilder modelBuilder)
    {
#pragma warning disable 612, 618
        modelBuilder.HasAnnotation("ProductVersion", "7.0.4");

        modelBuilder.Entity("Logatron.Models.Logbook.Entry", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("INTEGER");

                b.Property<string>("Callsign")
                    .IsRequired()
                    .HasColumnType("TEXT");

                b.Property<string>("Comments")
                    .IsRequired()
                    .HasColumnType("TEXT");

                b.Property<DateTime>("EndTime")
                    .HasColumnType("TEXT");

                b.Property<string>("Name")
                    .IsRequired()
                    .HasColumnType("TEXT");

                b.Property<DateTime>("StartTime")
                    .HasColumnType("TEXT");

                b.HasKey("Id");

                b.ToTable("Entries");
            });
#pragma warning restore 612, 618
    }
}
