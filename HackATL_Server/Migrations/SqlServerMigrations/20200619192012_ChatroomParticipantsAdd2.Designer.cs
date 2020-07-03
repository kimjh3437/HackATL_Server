﻿// <auto-generated />
using System;
using HackATL_Server.Helper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace HackATL_Server.Migrations.SqlServerMigrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20200619192012_ChatroomParticipantsAdd2")]
    partial class ChatroomParticipantsAdd2
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("HackATL_Server.Models.Model.Agenda_Item", b =>
                {
                    b.Property<string>("ID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Cateogory")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Datetime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Day")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EventName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageSource")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Location")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LongDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ShortDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Speaker_one")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Speaker_two")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("AgendaItems");
                });

            modelBuilder.Entity("HackATL_Server.Models.Model.User", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("PasswordHash")
                        .HasColumnType("varbinary(max)");

                    b.Property<byte[]>("PasswordSalt")
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("Role")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("HackATL_Server.Models.Model.UserChat", b =>
                {
                    b.Property<string>("UId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UId");

                    b.ToTable("UserChatLog");
                });

            modelBuilder.Entity("HackATL_Server.Models.Model.UserChatList_Component", b =>
                {
                    b.Property<string>("Uid")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("UserChatList_GroupRId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("UserNames")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Uid");

                    b.HasIndex("UserChatList_GroupRId");

                    b.ToTable("UserChatList_Component");
                });

            modelBuilder.Entity("HackATL_Server.Models.Model.UserChatList_Group", b =>
                {
                    b.Property<string>("RId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("UserChat_LogListId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("RId");

                    b.HasIndex("UserChat_LogListId");

                    b.ToTable("UserChatList_Group");
                });

            modelBuilder.Entity("HackATL_Server.Models.Model.UserChatList_LogHistory", b =>
                {
                    b.Property<string>("RId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("UserChatUId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("RId");

                    b.HasIndex("UserChatUId");

                    b.ToTable("UserChatList_LogHistory");
                });

            modelBuilder.Entity("HackATL_Server.Models.Model.UserChat_LogList", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.ToTable("User_GroupChatList");
                });

            modelBuilder.Entity("HackATL_Server.Models.Model.User_chatlog", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Message")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Time")
                        .HasColumnType("datetime2");

                    b.Property<string>("UserChatList_LogHistoryRId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserChatList_LogHistoryRId");

                    b.ToTable("User_chatlog");
                });

            modelBuilder.Entity("HackATL_Server.Models.Model.UserChatList_Component", b =>
                {
                    b.HasOne("HackATL_Server.Models.Model.UserChatList_Group", null)
                        .WithMany("UsersList")
                        .HasForeignKey("UserChatList_GroupRId");
                });

            modelBuilder.Entity("HackATL_Server.Models.Model.UserChatList_Group", b =>
                {
                    b.HasOne("HackATL_Server.Models.Model.UserChat_LogList", null)
                        .WithMany("ChatList")
                        .HasForeignKey("UserChat_LogListId");
                });

            modelBuilder.Entity("HackATL_Server.Models.Model.UserChatList_LogHistory", b =>
                {
                    b.HasOne("HackATL_Server.Models.Model.UserChat", null)
                        .WithMany("ChatLog")
                        .HasForeignKey("UserChatUId");
                });

            modelBuilder.Entity("HackATL_Server.Models.Model.User_chatlog", b =>
                {
                    b.HasOne("HackATL_Server.Models.Model.UserChatList_LogHistory", null)
                        .WithMany("Log")
                        .HasForeignKey("UserChatList_LogHistoryRId");
                });
#pragma warning restore 612, 618
        }
    }
}
