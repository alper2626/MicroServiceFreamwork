﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SSTTEK.Contact.DataAccess.Context;

#nullable disable

namespace SSTTEK.Contact.DataAccess.Migrations
{
    [DbContext(typeof(ContactModuleContext))]
    [Migration("20221007045037_first-migration")]
    partial class firstmigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("SSTTEK.Contacts.Entities.Db.ContactEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Firm")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdateTime")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Contacts");
                });

            modelBuilder.Entity("SSTTEK.Contacts.Entities.Db.ContactInformationEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ContactEntityId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("ContactInformationType")
                        .HasColumnType("int");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ContentIndex")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("CreateTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("UpdateTime")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("ContactEntityId");

                    b.HasIndex("ContentIndex");

                    b.ToTable("ContactInformations");
                });

            modelBuilder.Entity("SSTTEK.Contacts.Entities.Db.ContactInformationEntity", b =>
                {
                    b.HasOne("SSTTEK.Contacts.Entities.Db.ContactEntity", "ContactEntity")
                        .WithMany("ContactInformationEntities")
                        .HasForeignKey("ContactEntityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ContactEntity");
                });

            modelBuilder.Entity("SSTTEK.Contacts.Entities.Db.ContactEntity", b =>
                {
                    b.Navigation("ContactInformationEntities");
                });
#pragma warning restore 612, 618
        }
    }
}
