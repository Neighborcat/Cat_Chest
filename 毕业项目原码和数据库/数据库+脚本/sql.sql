USE [db_StarsResources]
GO
/****** Object:  Database [db_StarsResources]    Script Date: 2020/6/18 13:29:37 ******/
CREATE DATABASE [db_StarsResources]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'db_StarsResources', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\DATA\db_StarsResources.mdf' , SIZE = 4160KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'db_StarsResources_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\DATA\db_StarsResources_log.ldf' , SIZE = 1040KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [db_StarsResources] SET COMPATIBILITY_LEVEL = 110
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [db_StarsResources].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [db_StarsResources] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [db_StarsResources] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [db_StarsResources] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [db_StarsResources] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [db_StarsResources] SET ARITHABORT OFF 
GO
ALTER DATABASE [db_StarsResources] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [db_StarsResources] SET AUTO_CREATE_STATISTICS ON 
GO
ALTER DATABASE [db_StarsResources] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [db_StarsResources] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [db_StarsResources] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [db_StarsResources] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [db_StarsResources] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [db_StarsResources] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [db_StarsResources] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [db_StarsResources] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [db_StarsResources] SET  DISABLE_BROKER 
GO
ALTER DATABASE [db_StarsResources] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [db_StarsResources] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [db_StarsResources] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [db_StarsResources] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [db_StarsResources] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [db_StarsResources] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [db_StarsResources] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [db_StarsResources] SET RECOVERY FULL 
GO
ALTER DATABASE [db_StarsResources] SET  MULTI_USER 
GO
ALTER DATABASE [db_StarsResources] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [db_StarsResources] SET DB_CHAINING OFF 
GO
ALTER DATABASE [db_StarsResources] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [db_StarsResources] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
EXEC sys.sp_db_vardecimal_storage_format N'db_StarsResources', N'ON'
GO
USE [db_StarsResources]
GO
/****** Object:  Table [dbo].[Administrators]    Script Date: 2020/6/18 13:29:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Administrators](
	[AdmID] [int] IDENTITY(1,1) NOT NULL,
	[LoginName] [varchar](30) NOT NULL,
	[LoginPwd] [varchar](30) NOT NULL,
	[Jurisdiction] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[AdmID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Category]    Script Date: 2020/6/18 13:29:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Category](
	[CategoryID] [int] IDENTITY(1,1) NOT NULL,
	[TabelName] [varchar](10) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[CategoryID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Collection]    Script Date: 2020/6/18 13:29:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Collection](
	[CollectionID] [int] NOT NULL,
	[UserID] [int] NULL,
	[ResoucesID] [int] NULL,
 CONSTRAINT [PK_Collection] PRIMARY KEY CLUSTERED 
(
	[CollectionID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Comment]    Script Date: 2020/6/18 13:29:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Comment](
	[ComID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NULL,
	[Content] [varchar](200) NULL,
	[ResoucesID] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[ComID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Lable]    Script Date: 2020/6/18 13:29:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Lable](
	[LableID] [int] IDENTITY(1,1) NOT NULL,
	[CategoryID] [int] NULL,
	[TabelName] [varchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[LableID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Link]    Script Date: 2020/6/18 13:29:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Link](
	[LinkID] [int] IDENTITY(1,1) NOT NULL,
	[Ldescribe] [varchar](200) NOT NULL,
	[Linkline] [varchar](500) NOT NULL,
	[Lremarks] [varchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[LinkID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Picture]    Script Date: 2020/6/18 13:29:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Picture](
	[PictureID] [int] IDENTITY(1,1) NOT NULL,
	[Picture_a] [varchar](200) NULL,
	[Picture_b] [varchar](200) NULL,
	[Picture_c] [varchar](200) NULL,
	[Picture_d] [varchar](200) NULL,
	[Picture_e] [varchar](200) NULL,
PRIMARY KEY CLUSTERED 
(
	[PictureID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Resouces]    Script Date: 2020/6/18 13:29:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Resouces](
	[ResoucesID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NULL,
	[LinkID] [int] NULL,
	[PictureID] [int] NULL,
	[CategoryID] [int] NULL,
	[LableID] [int] NULL,
	[Releasetime] [date] NOT NULL,
	[Rname] [varchar](50) NOT NULL,
	[Rdescribe] [varchar](1000) NOT NULL,
	[Rdemand] [int] NULL,
	[Rstate] [int] NULL,
	[Reading] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ResoucesID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[UserInfo]    Script Date: 2020/6/18 13:29:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[UserInfo](
	[UserID] [int] IDENTITY(1,1) NOT NULL,
	[RegistrationTime] [date] NOT NULL,
	[LoginName] [varchar](30) NOT NULL,
	[LoginPwd] [varchar](30) NOT NULL,
	[E_mail] [varchar](50) NULL,
	[UserName] [varchar](20) NULL,
	[UserSex] [char](2) NULL,
	[Userdescribe] [varchar](500) NULL,
	[integral] [int] NULL,
	[UserState] [int] NULL,
	[UserPicture] [varchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[Administrators] ON 

INSERT [dbo].[Administrators] ([AdmID], [LoginName], [LoginPwd], [Jurisdiction]) VALUES (1, N'Mr.Smith', N'123123', 0)
INSERT [dbo].[Administrators] ([AdmID], [LoginName], [LoginPwd], [Jurisdiction]) VALUES (2, N'Mr.smith', N'123123', 0)
INSERT [dbo].[Administrators] ([AdmID], [LoginName], [LoginPwd], [Jurisdiction]) VALUES (3, N'Mr.Jan', N'123123', 1)
INSERT [dbo].[Administrators] ([AdmID], [LoginName], [LoginPwd], [Jurisdiction]) VALUES (4, N'Mr.Sam', N'123123', 1)
INSERT [dbo].[Administrators] ([AdmID], [LoginName], [LoginPwd], [Jurisdiction]) VALUES (5, N'LaoWang', N'123123', 2)
INSERT [dbo].[Administrators] ([AdmID], [LoginName], [LoginPwd], [Jurisdiction]) VALUES (6, N'LILI', N'666666', 3)
SET IDENTITY_INSERT [dbo].[Administrators] OFF
SET IDENTITY_INSERT [dbo].[Category] ON 

INSERT [dbo].[Category] ([CategoryID], [TabelName]) VALUES (1, N'福利资源')
INSERT [dbo].[Category] ([CategoryID], [TabelName]) VALUES (2, N'手机资源')
INSERT [dbo].[Category] ([CategoryID], [TabelName]) VALUES (3, N'学习资源')
INSERT [dbo].[Category] ([CategoryID], [TabelName]) VALUES (4, N'建站资源')
INSERT [dbo].[Category] ([CategoryID], [TabelName]) VALUES (5, N'PC端资源')
SET IDENTITY_INSERT [dbo].[Category] OFF
SET IDENTITY_INSERT [dbo].[Lable] ON 

INSERT [dbo].[Lable] ([LableID], [CategoryID], [TabelName]) VALUES (37, 1, N'开放下载')
INSERT [dbo].[Lable] ([LableID], [CategoryID], [TabelName]) VALUES (38, 1, N'免费电影')
INSERT [dbo].[Lable] ([LableID], [CategoryID], [TabelName]) VALUES (39, 2, N'安卓')
INSERT [dbo].[Lable] ([LableID], [CategoryID], [TabelName]) VALUES (40, 2, N'苹果')
INSERT [dbo].[Lable] ([LableID], [CategoryID], [TabelName]) VALUES (41, 2, N'破解软件')
INSERT [dbo].[Lable] ([LableID], [CategoryID], [TabelName]) VALUES (42, 3, N'精品课程')
INSERT [dbo].[Lable] ([LableID], [CategoryID], [TabelName]) VALUES (43, 3, N'赚钱教程')
INSERT [dbo].[Lable] ([LableID], [CategoryID], [TabelName]) VALUES (44, 4, N'网站源码')
INSERT [dbo].[Lable] ([LableID], [CategoryID], [TabelName]) VALUES (45, 4, N'Emolg')
INSERT [dbo].[Lable] ([LableID], [CategoryID], [TabelName]) VALUES (46, 4, N'Emolg主题')
INSERT [dbo].[Lable] ([LableID], [CategoryID], [TabelName]) VALUES (47, 4, N'Wordpress')
INSERT [dbo].[Lable] ([LableID], [CategoryID], [TabelName]) VALUES (48, 4, N'Wordpress主题 ')
INSERT [dbo].[Lable] ([LableID], [CategoryID], [TabelName]) VALUES (49, 5, N'Linux')
INSERT [dbo].[Lable] ([LableID], [CategoryID], [TabelName]) VALUES (50, 5, N'Windows')
INSERT [dbo].[Lable] ([LableID], [CategoryID], [TabelName]) VALUES (51, 5, N'破解软件')
INSERT [dbo].[Lable] ([LableID], [CategoryID], [TabelName]) VALUES (52, 5, N'编程源码')
INSERT [dbo].[Lable] ([LableID], [CategoryID], [TabelName]) VALUES (53, 5, N'游戏')
SET IDENTITY_INSERT [dbo].[Lable] OFF
SET IDENTITY_INSERT [dbo].[Link] ON 

INSERT [dbo].[Link] ([LinkID], [Ldescribe], [Linkline], [Lremarks]) VALUES (1, N'1-5部全系列。变形金刚1080P中英双字。合集！', N'https://pan.baidu.com/share/init?surl=rir-0ApnxkY84jm_-se0cw', N'提取码:j9fm')
SET IDENTITY_INSERT [dbo].[Link] OFF
SET IDENTITY_INSERT [dbo].[Picture] ON 

INSERT [dbo].[Picture] ([PictureID], [Picture_a], [Picture_b], [Picture_c], [Picture_d], [Picture_e]) VALUES (1, N'64d99a3d5eee070089a92d6e554e6208.jpg', N'a86db145cdab3ab993b5a595f3164450.jpg', N'51cfd565c04f56db6c2e8d5dd89edf85.jpg', N'feefcba52a144e2e6987d445a4b31da9.jpg', N'7b39e76d30a843e91fc7f949c5862e31.jpg')
SET IDENTITY_INSERT [dbo].[Picture] OFF
SET IDENTITY_INSERT [dbo].[Resouces] ON 

INSERT [dbo].[Resouces] ([ResoucesID], [UserID], [LinkID], [PictureID], [CategoryID], [LableID], [Releasetime], [Rname], [Rdescribe], [Rdemand], [Rstate], [Reading]) VALUES (2, 1, 1, 1, 1, 38, CAST(0x2D410B00 AS Date), N'变形金刚电影下载', N'应广大网友要求，收集变形金刚1-5部电影集合开放下载——中英双字，原声放送', 0, 0, 0)
SET IDENTITY_INSERT [dbo].[Resouces] OFF
SET IDENTITY_INSERT [dbo].[UserInfo] ON 

INSERT [dbo].[UserInfo] ([UserID], [RegistrationTime], [LoginName], [LoginPwd], [E_mail], [UserName], [UserSex], [Userdescribe], [integral], [UserState], [UserPicture]) VALUES (1, CAST(0x2D410B00 AS Date), N'2205579785', N'123123123', N'xcnw666@126.com', N'玄不救非，氪不改命', N'1 ', N'山有木兮木有枝，心悦君兮君不知', 5, 0, N'default.jpg')
INSERT [dbo].[UserInfo] ([UserID], [RegistrationTime], [LoginName], [LoginPwd], [E_mail], [UserName], [UserSex], [Userdescribe], [integral], [UserState], [UserPicture]) VALUES (2, CAST(0x2D410B00 AS Date), N'10086', N'8888', N'China_Mobile@139.com', N'萌新求带', N'0 ', N'懒得很~没有！', 5, 0, N'default.jpg')
INSERT [dbo].[UserInfo] ([UserID], [RegistrationTime], [LoginName], [LoginPwd], [E_mail], [UserName], [UserSex], [Userdescribe], [integral], [UserState], [UserPicture]) VALUES (3, CAST(0x2D410B00 AS Date), N'knowledge', N'123123', N'knowledge@163.com', N'萌新求带', N'0 ', N'懒得很~没有！', 5, 0, N'default.jpg')
INSERT [dbo].[UserInfo] ([UserID], [RegistrationTime], [LoginName], [LoginPwd], [E_mail], [UserName], [UserSex], [Userdescribe], [integral], [UserState], [UserPicture]) VALUES (4, CAST(0x2D410B00 AS Date), N'hentai123', N'123123', N'hentai1@163.com', N'萌新求带', N'0 ', N'懒得很~没有！', 5, 0, N'default.jpg')
INSERT [dbo].[UserInfo] ([UserID], [RegistrationTime], [LoginName], [LoginPwd], [E_mail], [UserName], [UserSex], [Userdescribe], [integral], [UserState], [UserPicture]) VALUES (5, CAST(0x2D410B00 AS Date), N'no123456789', N'123123', N'123456789@qq.com', N'萌新求带', N'0 ', N'懒得很~没有！', 5, 0, N'default.jpg')
INSERT [dbo].[UserInfo] ([UserID], [RegistrationTime], [LoginName], [LoginPwd], [E_mail], [UserName], [UserSex], [Userdescribe], [integral], [UserState], [UserPicture]) VALUES (6, CAST(0x2D410B00 AS Date), N'Tom1991', N'123123', N'MrTom@139.com', N'萌新求带', N'0 ', N'懒得很~没有！', 5, 0, N'default.jpg')
INSERT [dbo].[UserInfo] ([UserID], [RegistrationTime], [LoginName], [LoginPwd], [E_mail], [UserName], [UserSex], [Userdescribe], [integral], [UserState], [UserPicture]) VALUES (7, CAST(0x2D410B00 AS Date), N'BoyJim', N'123123', N'BoyJim@163.com', N'萌新求带', N'0 ', N'懒得很~没有！', 10, 0, N'default.jpg')
INSERT [dbo].[UserInfo] ([UserID], [RegistrationTime], [LoginName], [LoginPwd], [E_mail], [UserName], [UserSex], [Userdescribe], [integral], [UserState], [UserPicture]) VALUES (8, CAST(0x2D410B00 AS Date), N'LadyAilin', N'123123', N'LadyAilin@163.com', N'萌新求带', N'0 ', N'懒得很~没有！', 15, 0, N'default.jpg')
INSERT [dbo].[UserInfo] ([UserID], [RegistrationTime], [LoginName], [LoginPwd], [E_mail], [UserName], [UserSex], [Userdescribe], [integral], [UserState], [UserPicture]) VALUES (9, CAST(0x2D410B00 AS Date), N'OrdKala', N'123123', N'123123123@qq.com', N'萌新求带', N'0 ', N'懒得很~没有！', 20, 0, N'default.jpg')
INSERT [dbo].[UserInfo] ([UserID], [RegistrationTime], [LoginName], [LoginPwd], [E_mail], [UserName], [UserSex], [Userdescribe], [integral], [UserState], [UserPicture]) VALUES (10, CAST(0x2D410B00 AS Date), N'Yangon', N'123123', N'Yangon@139.com', N'萌新求带', N'0 ', N'懒得很~没有！', 30, 0, N'default.jpg')
SET IDENTITY_INSERT [dbo].[UserInfo] OFF
ALTER TABLE [dbo].[Administrators] ADD  DEFAULT ((0)) FOR [Jurisdiction]
GO
ALTER TABLE [dbo].[Resouces] ADD  DEFAULT ((0)) FOR [Rstate]
GO
ALTER TABLE [dbo].[Resouces] ADD  CONSTRAINT [DF_Resouces_Reading]  DEFAULT ((0)) FOR [Reading]
GO
ALTER TABLE [dbo].[UserInfo] ADD  DEFAULT (getdate()) FOR [RegistrationTime]
GO
ALTER TABLE [dbo].[UserInfo] ADD  DEFAULT ('萌新求带') FOR [UserName]
GO
ALTER TABLE [dbo].[UserInfo] ADD  DEFAULT ((0)) FOR [UserSex]
GO
ALTER TABLE [dbo].[UserInfo] ADD  DEFAULT ('懒得很~没有！') FOR [Userdescribe]
GO
ALTER TABLE [dbo].[UserInfo] ADD  DEFAULT ((5)) FOR [integral]
GO
ALTER TABLE [dbo].[UserInfo] ADD  DEFAULT ((0)) FOR [UserState]
GO
ALTER TABLE [dbo].[Collection]  WITH CHECK ADD  CONSTRAINT [FK_Collection_Resouces] FOREIGN KEY([ResoucesID])
REFERENCES [dbo].[Resouces] ([ResoucesID])
GO
ALTER TABLE [dbo].[Collection] CHECK CONSTRAINT [FK_Collection_Resouces]
GO
ALTER TABLE [dbo].[Collection]  WITH CHECK ADD  CONSTRAINT [FK_Collection_UserInfo] FOREIGN KEY([UserID])
REFERENCES [dbo].[UserInfo] ([UserID])
GO
ALTER TABLE [dbo].[Collection] CHECK CONSTRAINT [FK_Collection_UserInfo]
GO
ALTER TABLE [dbo].[Comment]  WITH CHECK ADD FOREIGN KEY([UserID])
REFERENCES [dbo].[UserInfo] ([UserID])
GO
ALTER TABLE [dbo].[Comment]  WITH CHECK ADD  CONSTRAINT [FK_Comment_Resouces] FOREIGN KEY([ResoucesID])
REFERENCES [dbo].[Resouces] ([ResoucesID])
GO
ALTER TABLE [dbo].[Comment] CHECK CONSTRAINT [FK_Comment_Resouces]
GO
ALTER TABLE [dbo].[Lable]  WITH CHECK ADD FOREIGN KEY([CategoryID])
REFERENCES [dbo].[Category] ([CategoryID])
GO
ALTER TABLE [dbo].[Resouces]  WITH CHECK ADD FOREIGN KEY([CategoryID])
REFERENCES [dbo].[Category] ([CategoryID])
GO
ALTER TABLE [dbo].[Resouces]  WITH CHECK ADD FOREIGN KEY([LableID])
REFERENCES [dbo].[Lable] ([LableID])
GO
ALTER TABLE [dbo].[Resouces]  WITH CHECK ADD FOREIGN KEY([LinkID])
REFERENCES [dbo].[Link] ([LinkID])
GO
ALTER TABLE [dbo].[Resouces]  WITH CHECK ADD FOREIGN KEY([PictureID])
REFERENCES [dbo].[Picture] ([PictureID])
GO
ALTER TABLE [dbo].[Resouces]  WITH CHECK ADD FOREIGN KEY([UserID])
REFERENCES [dbo].[UserInfo] ([UserID])
GO
ALTER TABLE [dbo].[Administrators]  WITH CHECK ADD CHECK  (([Jurisdiction]=(0) OR [Jurisdiction]=(1) OR [Jurisdiction]=(2) OR [Jurisdiction]=(3)))
GO
ALTER TABLE [dbo].[Resouces]  WITH CHECK ADD CHECK  (([Rstate]=(0) OR [Rstate]=(1) OR [Rstate]=(2)))
GO
ALTER TABLE [dbo].[UserInfo]  WITH CHECK ADD CHECK  (([UserSex]=(0) OR [UserSex]=(1) OR [UserSex]=(2)))
GO
ALTER TABLE [dbo].[UserInfo]  WITH CHECK ADD CHECK  (([UserState]=(0) OR [UserState]=(1)))
GO
USE [master]
GO
ALTER DATABASE [db_StarsResources] SET  READ_WRITE 
GO
