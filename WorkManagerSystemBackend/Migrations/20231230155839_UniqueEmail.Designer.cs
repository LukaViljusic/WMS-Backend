﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WorkManagerSystemBackend.Core.Context;

#nullable disable

namespace WorkManagerSystemBackend.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20231230155839_UniqueEmail")]
    partial class UniqueEmail
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("WorkManagerSystemBackend.Core.Entities.Space", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<long>("UsersId")
                        .HasColumnType("bigint");

                    b.Property<bool>("isActive")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("UsersId");

                    b.ToTable("Spaces");
                });

            modelBuilder.Entity("WorkManagerSystemBackend.Core.Entities.Users", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<bool>("isActive")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("WorkManagerSystemBackend.Core.Entities.WorkItem", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("Deadline")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("SpaceId")
                        .HasColumnType("bigint");

                    b.Property<int>("TimeEstimate")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<long>("UsersId")
                        .HasColumnType("bigint");

                    b.Property<long>("WorkItemStateId")
                        .HasColumnType("bigint");

                    b.Property<long>("WorkItemTypeId")
                        .HasColumnType("bigint");

                    b.Property<bool>("isActive")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("SpaceId");

                    b.HasIndex("UsersId");

                    b.HasIndex("WorkItemStateId");

                    b.HasIndex("WorkItemTypeId");

                    b.ToTable("WorkItems");
                });

            modelBuilder.Entity("WorkManagerSystemBackend.Core.Entities.WorkItemState", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("Color")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("FinalStatus")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("InitialStatus")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<long>("WorkItemTypeId")
                        .HasColumnType("bigint");

                    b.Property<bool>("isActive")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("WorkItemTypeId");

                    b.ToTable("WorkItemStates");
                });

            modelBuilder.Entity("WorkManagerSystemBackend.Core.Entities.WorkItemType", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("Color")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("SpaceId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<bool>("isActive")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("SpaceId");

                    b.ToTable("WorkItemTypes");
                });

            modelBuilder.Entity("WorkManagerSystemBackend.Core.Entities.Space", b =>
                {
                    b.HasOne("WorkManagerSystemBackend.Core.Entities.Users", "Users")
                        .WithMany("Spaces")
                        .HasForeignKey("UsersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Users");
                });

            modelBuilder.Entity("WorkManagerSystemBackend.Core.Entities.WorkItem", b =>
                {
                    b.HasOne("WorkManagerSystemBackend.Core.Entities.Space", "Space")
                        .WithMany("WorkItems")
                        .HasForeignKey("SpaceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WorkManagerSystemBackend.Core.Entities.Users", "Users")
                        .WithMany("WorkItems")
                        .HasForeignKey("UsersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WorkManagerSystemBackend.Core.Entities.WorkItemState", "WorkItemState")
                        .WithMany()
                        .HasForeignKey("WorkItemStateId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WorkManagerSystemBackend.Core.Entities.WorkItemType", "WorkItemType")
                        .WithMany("WorkItems")
                        .HasForeignKey("WorkItemTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Space");

                    b.Navigation("Users");

                    b.Navigation("WorkItemState");

                    b.Navigation("WorkItemType");
                });

            modelBuilder.Entity("WorkManagerSystemBackend.Core.Entities.WorkItemState", b =>
                {
                    b.HasOne("WorkManagerSystemBackend.Core.Entities.WorkItemType", "WorkItemType")
                        .WithMany("WorkItemState")
                        .HasForeignKey("WorkItemTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("WorkItemType");
                });

            modelBuilder.Entity("WorkManagerSystemBackend.Core.Entities.WorkItemType", b =>
                {
                    b.HasOne("WorkManagerSystemBackend.Core.Entities.Space", "Space")
                        .WithMany("WorkItemTypes")
                        .HasForeignKey("SpaceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Space");
                });

            modelBuilder.Entity("WorkManagerSystemBackend.Core.Entities.Space", b =>
                {
                    b.Navigation("WorkItemTypes");

                    b.Navigation("WorkItems");
                });

            modelBuilder.Entity("WorkManagerSystemBackend.Core.Entities.Users", b =>
                {
                    b.Navigation("Spaces");

                    b.Navigation("WorkItems");
                });

            modelBuilder.Entity("WorkManagerSystemBackend.Core.Entities.WorkItemType", b =>
                {
                    b.Navigation("WorkItemState");

                    b.Navigation("WorkItems");
                });
#pragma warning restore 612, 618
        }
    }
}
