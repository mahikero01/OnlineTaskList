﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using OTL_API.Entities;
using System;

namespace OTL_API.Migrations
{
    [DbContext(typeof(OnlineTaskListsContext))]
    partial class OnlineTaskListsContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("OTL_API.Entities.UserTask", b =>
                {
                    b.Property<Guid>("UserTaskID")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DateCreated");

                    b.Property<string>("Description")
                        .HasMaxLength(50);

                    b.Property<bool>("IsDone");

                    b.Property<string>("Title");

                    b.Property<string>("UserID")
                        .HasMaxLength(450);

                    b.HasKey("UserTaskID");

                    b.ToTable("UserTasks");
                });
#pragma warning restore 612, 618
        }
    }
}
