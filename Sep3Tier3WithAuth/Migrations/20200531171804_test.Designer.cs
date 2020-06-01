﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Sep3Tier3WithAuth.Helpers;

namespace Sep3Tier3WithAuth.Migrations
{
    [DbContext(typeof(AuthContext))]
    [Migration("20200531171804_test")]
    partial class test
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .HasAnnotation("ProductVersion", "3.1.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("Sep3Tier3WithAuth.Entities.Interactions", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("InteractionName")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Interactions");
                });

            modelBuilder.Entity("Sep3Tier3WithAuth.Entities.LikeReject", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int?>("Fisher1Id")
                        .IsRequired()
                        .HasColumnType("integer");

                    b.Property<int?>("Fisher2Id")
                        .IsRequired()
                        .HasColumnType("integer");

                    b.Property<int>("InteractionsId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("Fisher1Id");

                    b.HasIndex("Fisher2Id");

                    b.HasIndex("InteractionsId");

                    b.ToTable("LikeReject");
                });

            modelBuilder.Entity("Sep3Tier3WithAuth.Entities.PersonSexuality", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("SexualityName")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("PersonSexualities");
                });

            modelBuilder.Entity("Sep3Tier3WithAuth.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<byte[]>("PasswordHash")
                        .HasColumnType("bytea");

                    b.Property<byte[]>("PasswordSalt")
                        .HasColumnType("bytea");

                    b.Property<string>("Username")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasDiscriminator<string>("Discriminator").HasValue("User");
                });

            modelBuilder.Entity("Sep3Tier3WithAuth.Entities.Administrator", b =>
                {
                    b.HasBaseType("Sep3Tier3WithAuth.Entities.User");

                    b.HasDiscriminator().HasValue("Administrator");
                });

            modelBuilder.Entity("Sep3Tier3WithAuth.Entities.Fisher", b =>
                {
                    b.HasBaseType("Sep3Tier3WithAuth.Entities.User");

                    b.Property<int>("Age")
                        .HasColumnType("integer");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.Property<string>("FirstName")
                        .HasColumnType("text");

                    b.Property<string>("Gender")
                        .HasColumnType("text");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<int>("PersonSexualityId")
                        .HasColumnType("integer");

                    b.Property<string>("PicRef")
                        .HasColumnType("text");

                    b.Property<string>("Surname")
                        .HasColumnType("text");

                    b.HasIndex("PersonSexualityId");

                    b.HasDiscriminator().HasValue("Fisher");
                });

            modelBuilder.Entity("Sep3Tier3WithAuth.Entities.LikeReject", b =>
                {
                    b.HasOne("Sep3Tier3WithAuth.Entities.Fisher", "Fisher1")
                        .WithMany("Fishers1")
                        .HasForeignKey("Fisher1Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Sep3Tier3WithAuth.Entities.Fisher", "Fisher2")
                        .WithMany("Fishers2")
                        .HasForeignKey("Fisher2Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Sep3Tier3WithAuth.Entities.Interactions", null)
                        .WithMany("LikeRejects")
                        .HasForeignKey("InteractionsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Sep3Tier3WithAuth.Entities.Fisher", b =>
                {
                    b.HasOne("Sep3Tier3WithAuth.Entities.PersonSexuality", null)
                        .WithMany("Fishers")
                        .HasForeignKey("PersonSexualityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}