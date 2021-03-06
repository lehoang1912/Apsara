USE [master]
GO
/****** Object:  Database [DbApsara]    Script Date: 3/23/2022 6:24:39 PM ******/
CREATE DATABASE [DbApsara]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'DbApsara', FILENAME = N'D:\databases\DbApsara.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'DbApsara_log', FILENAME = N'D:\databases\DbApsara_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [DbApsara] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [DbApsara].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [DbApsara] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [DbApsara] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [DbApsara] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [DbApsara] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [DbApsara] SET ARITHABORT OFF 
GO
ALTER DATABASE [DbApsara] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [DbApsara] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [DbApsara] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [DbApsara] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [DbApsara] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [DbApsara] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [DbApsara] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [DbApsara] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [DbApsara] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [DbApsara] SET  DISABLE_BROKER 
GO
ALTER DATABASE [DbApsara] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [DbApsara] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [DbApsara] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [DbApsara] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [DbApsara] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [DbApsara] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [DbApsara] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [DbApsara] SET RECOVERY FULL 
GO
ALTER DATABASE [DbApsara] SET  MULTI_USER 
GO
ALTER DATABASE [DbApsara] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [DbApsara] SET DB_CHAINING OFF 
GO
ALTER DATABASE [DbApsara] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [DbApsara] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [DbApsara] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'DbApsara', N'ON'
GO
ALTER DATABASE [DbApsara] SET QUERY_STORE = OFF
GO
USE [DbApsara]
GO
/****** Object:  User [user_apsara]    Script Date: 3/23/2022 6:24:39 PM ******/
CREATE USER [user_apsara] FOR LOGIN [user_apsara] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  User [sabackup]    Script Date: 3/23/2022 6:24:39 PM ******/
CREATE USER [sabackup] FOR LOGIN [sabackup] WITH DEFAULT_SCHEMA=[dbo]
GO
ALTER ROLE [db_owner] ADD MEMBER [user_apsara]
GO
/****** Object:  Table [dbo].[Account]    Script Date: 3/23/2022 6:24:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Account](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [nvarchar](250) NULL,
	[Password] [nvarchar](max) NULL,
	[DateRegistered] [datetime] NULL,
	[DateUpdated] [datetime] NULL,
	[Type] [int] NULL,
	[IsPrevent] [int] NULL,
	[IsDeleted] [int] NULL,
 CONSTRAINT [PK_Account] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ActionLog]    Script Date: 3/23/2022 6:24:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ActionLog](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IdAccount] [int] NULL,
	[DateLogin] [datetime] NULL,
 CONSTRAINT [PK_ActionLog] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DataLink]    Script Date: 3/23/2022 6:24:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DataLink](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IdAccount] [int] NULL,
	[Title] [nvarchar](max) NULL,
	[Link] [nvarchar](max) NULL,
	[DateUpdated] [datetime] NULL,
	[IsDeleted] [int] NULL,
 CONSTRAINT [PK_DataLink] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Account] ON 

INSERT [dbo].[Account] ([Id], [UserName], [Password], [DateRegistered], [DateUpdated], [Type], [IsPrevent], [IsDeleted]) VALUES (1, N'admin', N'obama', NULL, NULL, 0, NULL, 0)
INSERT [dbo].[Account] ([Id], [UserName], [Password], [DateRegistered], [DateUpdated], [Type], [IsPrevent], [IsDeleted]) VALUES (2, N'user113', N'12345', CAST(N'2022-03-21T00:00:00.000' AS DateTime), NULL, 1, NULL, 0)
INSERT [dbo].[Account] ([Id], [UserName], [Password], [DateRegistered], [DateUpdated], [Type], [IsPrevent], [IsDeleted]) VALUES (5, N'ch1201063@gm.uit.edu.vn', N'apsara123', CAST(N'2022-03-23T13:20:11.143' AS DateTime), NULL, 1, 0, 0)
INSERT [dbo].[Account] ([Id], [UserName], [Password], [DateRegistered], [DateUpdated], [Type], [IsPrevent], [IsDeleted]) VALUES (6, N'a@gmail.com', N'apsara123', CAST(N'2022-03-23T13:21:28.133' AS DateTime), NULL, 1, 0, 0)
SET IDENTITY_INSERT [dbo].[Account] OFF
SET IDENTITY_INSERT [dbo].[ActionLog] ON 

INSERT [dbo].[ActionLog] ([Id], [IdAccount], [DateLogin]) VALUES (1, 2, CAST(N'2022-03-23T13:34:28.683' AS DateTime))
INSERT [dbo].[ActionLog] ([Id], [IdAccount], [DateLogin]) VALUES (2, 2, CAST(N'2022-03-23T13:39:27.887' AS DateTime))
INSERT [dbo].[ActionLog] ([Id], [IdAccount], [DateLogin]) VALUES (3, 2, CAST(N'2022-03-23T13:40:30.663' AS DateTime))
INSERT [dbo].[ActionLog] ([Id], [IdAccount], [DateLogin]) VALUES (4, 2, CAST(N'2022-03-23T13:42:04.930' AS DateTime))
INSERT [dbo].[ActionLog] ([Id], [IdAccount], [DateLogin]) VALUES (5, 2, CAST(N'2022-03-23T13:45:26.807' AS DateTime))
INSERT [dbo].[ActionLog] ([Id], [IdAccount], [DateLogin]) VALUES (6, 2, CAST(N'2022-03-23T13:46:52.150' AS DateTime))
INSERT [dbo].[ActionLog] ([Id], [IdAccount], [DateLogin]) VALUES (7, 2, CAST(N'2022-03-23T13:52:10.143' AS DateTime))
INSERT [dbo].[ActionLog] ([Id], [IdAccount], [DateLogin]) VALUES (8, 2, CAST(N'2022-03-23T13:54:32.890' AS DateTime))
INSERT [dbo].[ActionLog] ([Id], [IdAccount], [DateLogin]) VALUES (9, 2, CAST(N'2022-03-23T14:02:17.240' AS DateTime))
INSERT [dbo].[ActionLog] ([Id], [IdAccount], [DateLogin]) VALUES (10, 2, CAST(N'2022-03-23T14:06:14.190' AS DateTime))
INSERT [dbo].[ActionLog] ([Id], [IdAccount], [DateLogin]) VALUES (11, 2, CAST(N'2022-03-23T14:26:36.970' AS DateTime))
INSERT [dbo].[ActionLog] ([Id], [IdAccount], [DateLogin]) VALUES (12, 2, CAST(N'2022-03-23T14:57:55.793' AS DateTime))
INSERT [dbo].[ActionLog] ([Id], [IdAccount], [DateLogin]) VALUES (13, 2, CAST(N'2022-03-23T15:01:27.377' AS DateTime))
INSERT [dbo].[ActionLog] ([Id], [IdAccount], [DateLogin]) VALUES (14, 2, CAST(N'2022-03-23T15:08:42.553' AS DateTime))
INSERT [dbo].[ActionLog] ([Id], [IdAccount], [DateLogin]) VALUES (15, 2, CAST(N'2022-03-23T15:10:31.377' AS DateTime))
INSERT [dbo].[ActionLog] ([Id], [IdAccount], [DateLogin]) VALUES (16, 2, CAST(N'2022-03-23T15:15:34.947' AS DateTime))
INSERT [dbo].[ActionLog] ([Id], [IdAccount], [DateLogin]) VALUES (17, 2, CAST(N'2022-03-23T15:30:46.487' AS DateTime))
INSERT [dbo].[ActionLog] ([Id], [IdAccount], [DateLogin]) VALUES (18, 2, CAST(N'2022-03-23T15:33:11.793' AS DateTime))
INSERT [dbo].[ActionLog] ([Id], [IdAccount], [DateLogin]) VALUES (19, 2, CAST(N'2022-03-23T15:36:46.980' AS DateTime))
INSERT [dbo].[ActionLog] ([Id], [IdAccount], [DateLogin]) VALUES (20, 2, CAST(N'2022-03-23T16:31:25.373' AS DateTime))
INSERT [dbo].[ActionLog] ([Id], [IdAccount], [DateLogin]) VALUES (21, 2, CAST(N'2022-03-23T16:37:30.267' AS DateTime))
INSERT [dbo].[ActionLog] ([Id], [IdAccount], [DateLogin]) VALUES (22, 2, CAST(N'2022-03-23T16:50:30.830' AS DateTime))
INSERT [dbo].[ActionLog] ([Id], [IdAccount], [DateLogin]) VALUES (23, 2, CAST(N'2022-03-23T17:01:10.723' AS DateTime))
INSERT [dbo].[ActionLog] ([Id], [IdAccount], [DateLogin]) VALUES (24, 2, CAST(N'2022-03-23T17:03:27.670' AS DateTime))
INSERT [dbo].[ActionLog] ([Id], [IdAccount], [DateLogin]) VALUES (25, 2, CAST(N'2022-03-23T17:58:42.330' AS DateTime))
INSERT [dbo].[ActionLog] ([Id], [IdAccount], [DateLogin]) VALUES (26, 2, CAST(N'2022-03-23T18:15:15.157' AS DateTime))
INSERT [dbo].[ActionLog] ([Id], [IdAccount], [DateLogin]) VALUES (27, 2, CAST(N'2022-03-23T18:18:54.983' AS DateTime))
SET IDENTITY_INSERT [dbo].[ActionLog] OFF
SET IDENTITY_INSERT [dbo].[DataLink] ON 

INSERT [dbo].[DataLink] ([Id], [IdAccount], [Title], [Link], [DateUpdated], [IsDeleted]) VALUES (1, 2, N'21 Vòng lặp while trong Kotlin', N'https://www.youtube.com/watch?v=e6pZ9Zi_XJA&list=PLmEUE4MG8_b4FTcScxHuXNRTBOxaiqcVX&index=22', CAST(N'2022-03-23T16:26:39.127' AS DateTime), 1)
INSERT [dbo].[DataLink] ([Id], [IdAccount], [Title], [Link], [DateUpdated], [IsDeleted]) VALUES (2, 2, N'22 Vòng lặp do while trong Kotlin', N'https://www.youtube.com/watch?v=Xyq_6ZNCSx8&list=PLmEUE4MG8_b4FTcScxHuXNRTBOxaiqcVX&index=23', CAST(N'2022-03-23T16:26:38.637' AS DateTime), 1)
INSERT [dbo].[DataLink] ([Id], [IdAccount], [Title], [Link], [DateUpdated], [IsDeleted]) VALUES (3, 2, N'Chu Du và Nỗi Oan Ngàn Năm " TRỜI SINH DU SAO CÒN SINH LƯỢNG"', N'https://www.youtube.com/watch?v=E-6mkea729o', CAST(N'2022-03-23T17:01:28.110' AS DateTime), 1)
INSERT [dbo].[DataLink] ([Id], [IdAccount], [Title], [Link], [DateUpdated], [IsDeleted]) VALUES (4, 2, N'Tròn Đạo Làm Người | Thanh Sĩ | 1954', N'https://www.youtube.com/watch?v=WmZBetKKOCI', CAST(N'2022-03-23T17:01:29.570' AS DateTime), 1)
INSERT [dbo].[DataLink] ([Id], [IdAccount], [Title], [Link], [DateUpdated], [IsDeleted]) VALUES (5, 2, N'Nhân Luân Hiếu Nghĩa | Đạo Làm Người | Quyển 03 | Văn Hóa Cội Nguồn', N'https://www.youtube.com/watch?v=LfojwqOhP_s', CAST(N'2022-03-23T17:01:29.997' AS DateTime), 1)
INSERT [dbo].[DataLink] ([Id], [IdAccount], [Title], [Link], [DateUpdated], [IsDeleted]) VALUES (6, 2, N'6 đức tính của đàn ông khí phách', N'https://www.youtube.com/watch?v=IGRG5NWwjmQ', CAST(N'2022-03-23T17:01:31.193' AS DateTime), 1)
INSERT [dbo].[DataLink] ([Id], [IdAccount], [Title], [Link], [DateUpdated], [IsDeleted]) VALUES (7, 2, N'Giáo Dục Văn Hóa Cội Nguồn', N'https://www.youtube.com/watch?v=3P0m1V9wB9A', CAST(N'2022-03-23T17:01:31.603' AS DateTime), 1)
INSERT [dbo].[DataLink] ([Id], [IdAccount], [Title], [Link], [DateUpdated], [IsDeleted]) VALUES (8, 2, N'8 Cạm Bẫy Lớn Của Nhân Sinh Ngộ Ra Sớm ', N'https://www.youtube.com/watch?v=CYYea5fWBwk', CAST(N'2022-03-23T17:01:32.040' AS DateTime), 1)
INSERT [dbo].[DataLink] ([Id], [IdAccount], [Title], [Link], [DateUpdated], [IsDeleted]) VALUES (9, 2, N'21 Vòng lặp while trong Kotlin', N'https://www.youtube.com/watch?v=e6pZ9Zi_XJA&list=PLmEUE4MG8_b4FTcScxHuXNRTBOxaiqcVX&index=22&t=46s', CAST(N'2022-03-23T19:03:36.877' AS DateTime), 1)
INSERT [dbo].[DataLink] ([Id], [IdAccount], [Title], [Link], [DateUpdated], [IsDeleted]) VALUES (10, 2, N'22 Vòng lặp do while trong Kotlin', N'https://www.youtube.com/watch?v=Xyq_6ZNCSx8&list=PLmEUE4MG8_b4FTcScxHuXNRTBOxaiqcVX&index=23', CAST(N'2022-03-23T18:38:59.027' AS DateTime), 1)
INSERT [dbo].[DataLink] ([Id], [IdAccount], [Title], [Link], [DateUpdated], [IsDeleted]) VALUES (11, 2, N'23 Ý nghĩa và cách sử dụng break và continue trong các vòng lặp', N'https://www.youtube.com/watch?v=E-HbL_8Xy4w&list=PLmEUE4MG8_b4FTcScxHuXNRTBOxaiqcVX&index=24', CAST(N'2022-03-23T18:38:59.427' AS DateTime), 1)
INSERT [dbo].[DataLink] ([Id], [IdAccount], [Title], [Link], [DateUpdated], [IsDeleted]) VALUES (12, 2, N'19 Vòng lặp for trong Kotlin Loại Downto', N'https://www.youtube.com/watch?v=oKWIvXEDBDk&list=PLmEUE4MG8_b4FTcScxHuXNRTBOxaiqcVX&index=20', CAST(N'2022-03-23T18:38:59.853' AS DateTime), 1)
INSERT [dbo].[DataLink] ([Id], [IdAccount], [Title], [Link], [DateUpdated], [IsDeleted]) VALUES (13, 2, N'20 Vòng lặp for trong Kotlin Loại Iterator', N'https://www.youtube.com/watch?v=EA7AT1IhUtk&list=PLmEUE4MG8_b4FTcScxHuXNRTBOxaiqcVX&index=21', CAST(N'2022-03-23T19:03:37.360' AS DateTime), 1)
INSERT [dbo].[DataLink] ([Id], [IdAccount], [Title], [Link], [DateUpdated], [IsDeleted]) VALUES (14, 2, N'15 Cấu trúc when trong Kotlin Phần 2', N'https://www.youtube.com/watch?v=3hrnU08ewoo&list=PLmEUE4MG8_b4FTcScxHuXNRTBOxaiqcVX&index=16', CAST(N'2022-03-23T19:03:38.830' AS DateTime), 1)
INSERT [dbo].[DataLink] ([Id], [IdAccount], [Title], [Link], [DateUpdated], [IsDeleted]) VALUES (15, 2, N'18 Vòng lặp for trong Kotlin Loại Step', N'https://www.youtube.com/watch?v=mUsJFwrJ3jE&list=PLmEUE4MG8_b4FTcScxHuXNRTBOxaiqcVX&index=19', CAST(N'2022-03-23T18:51:44.773' AS DateTime), 1)
INSERT [dbo].[DataLink] ([Id], [IdAccount], [Title], [Link], [DateUpdated], [IsDeleted]) VALUES (16, 2, N'17 Vòng lặp for trong Kotlin Loại Half open range', N'https://www.youtube.com/watch?v=SV5e6p2YzOw&list=PLmEUE4MG8_b4FTcScxHuXNRTBOxaiqcVX&index=18', CAST(N'2022-03-23T19:03:34.437' AS DateTime), 1)
INSERT [dbo].[DataLink] ([Id], [IdAccount], [Title], [Link], [DateUpdated], [IsDeleted]) VALUES (17, 2, N'Apsara Cày view Youtube không cần đăng nhập', N'https://www.youtube.com/watch?v=3I4lJIDTAMg', CAST(N'2022-03-23T19:03:47.040' AS DateTime), 0)
INSERT [dbo].[DataLink] ([Id], [IdAccount], [Title], [Link], [DateUpdated], [IsDeleted]) VALUES (18, 2, N'Sinh viên và những ngộ nhận về IT khi học và mới ra trường', N'https://www.youtube.com/watch?v=6hfRDVrpbGI&list=PLmEUE4MG8_b4xfxxuDK0ycZmEcd5QThwZ&index=1&t=683s', CAST(N'2022-03-23T19:04:09.583' AS DateTime), 0)
INSERT [dbo].[DataLink] ([Id], [IdAccount], [Title], [Link], [DateUpdated], [IsDeleted]) VALUES (19, 2, N'23 Ý nghĩa và cách sử dụng break và continue trong các vòng lặp', N'https://www.youtube.com/watch?v=E-HbL_8Xy4w&list=PLmEUE4MG8_b4FTcScxHuXNRTBOxaiqcVX&index=24&t=5s', CAST(N'2022-03-23T19:04:31.663' AS DateTime), 0)
SET IDENTITY_INSERT [dbo].[DataLink] OFF
ALTER TABLE [dbo].[ActionLog]  WITH CHECK ADD  CONSTRAINT [FK_ActionLog_Account] FOREIGN KEY([IdAccount])
REFERENCES [dbo].[Account] ([Id])
GO
ALTER TABLE [dbo].[ActionLog] CHECK CONSTRAINT [FK_ActionLog_Account]
GO
ALTER TABLE [dbo].[DataLink]  WITH CHECK ADD  CONSTRAINT [FK_DataLink_Account] FOREIGN KEY([IdAccount])
REFERENCES [dbo].[Account] ([Id])
GO
ALTER TABLE [dbo].[DataLink] CHECK CONSTRAINT [FK_DataLink_Account]
GO
/****** Object:  StoredProcedure [dbo].[GetCurrentTimeOnServer]    Script Date: 3/23/2022 6:24:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create proc [dbo].[GetCurrentTimeOnServer]
as
select GETDATE ( ) as CurrentTime
GO
USE [master]
GO
ALTER DATABASE [DbApsara] SET  READ_WRITE 
GO
