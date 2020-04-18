﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SecureChat.DAL.Contexts;

namespace SecureChat.DAL.Migrations.MessagesDB
{
    [DbContext(typeof(MessagesDBContext))]
    [Migration("20200417153905_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.11-servicing-32099")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("SecureChat.DAL.Models.Message", b =>
                {
                    b.Property<int>("MessageID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Body");

                    b.Property<DateTime>("DeletedDate");

                    b.Property<string>("From");

                    b.Property<DateTime>("SendDate");

                    b.Property<bool>("Status");

                    b.Property<string>("To");

                    b.HasKey("MessageID");

                    b.ToTable("Messages");
                });
#pragma warning restore 612, 618
        }
    }
}
