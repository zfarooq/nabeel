USE [portal]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 4/6/2020 8:01:39 PM ******/
DROP TABLE [dbo].[Users]
GO
/****** Object:  Table [dbo].[UserRoles]    Script Date: 4/6/2020 8:01:39 PM ******/
DROP TABLE [dbo].[UserRoles]
GO
/****** Object:  Table [dbo].[Roles]    Script Date: 4/6/2020 8:01:39 PM ******/
DROP TABLE [dbo].[Roles]
GO
/****** Object:  Table [dbo].[Clients]    Script Date: 4/6/2020 8:01:39 PM ******/
DROP TABLE [dbo].[Clients]
GO
/****** Object:  Table [dbo].[Clients]    Script Date: 4/6/2020 8:01:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Clients](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[Address] [nvarchar](50) NULL,
	[City] [nvarchar](50) NULL,
	[State] [nvarchar](50) NULL,
	[ContactPhone] [nvarchar](50) NULL,
	[ContactEmail] [nvarchar](50) NULL,
	[Logo] [nvarchar](50) NULL,
	[CreatedOn] [datetime] NULL,
	[CreatedBy] [int] NULL,
	[ModifiedOn] [datetime] NULL,
	[ModifiedBy] [int] NULL,
 CONSTRAINT [PK_Clients] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Roles]    Script Date: 4/6/2020 8:01:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Roles](
	[Id] [int] NULL,
	[Name] [nvarchar](50) NULL,
	[Description] [nvarchar](50) NULL,
	[CreatedOn] [datetime] NULL,
	[CreatedBy] [int] NULL,
	[ModifiedOn] [datetime] NULL,
	[ModifiedBy] [int] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserRoles]    Script Date: 4/6/2020 8:01:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserRoles](
	[Id] [int] NULL,
	[UserId] [int] NULL,
	[RoleId] [int] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 4/6/2020 8:01:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](50) NULL,
	[LastName] [nvarchar](50) NULL,
	[UserName] [nvarchar](50) NULL,
	[Password] [nvarchar](50) NULL,
	[RoleId] [int] NULL,
	[CreatedOn] [datetime] NULL,
	[CreatedBy] [int] NULL,
	[ModifiedOn] [datetime] NULL,
	[ModifiedBy] [int] NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[Roles] ([Id], [Name], [Description], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (NULL, N'Admin', N'Admin', CAST(N'2020-04-05T12:43:51.930' AS DateTime), 1, CAST(N'2020-04-05T12:43:51.930' AS DateTime), 1)
INSERT [dbo].[Roles] ([Id], [Name], [Description], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (NULL, N'Clietn', N'Client', CAST(N'2020-04-05T12:44:09.763' AS DateTime), 1, CAST(N'2020-04-05T12:44:09.763' AS DateTime), 1)
SET IDENTITY_INSERT [dbo].[Users] ON 

INSERT [dbo].[Users] ([Id], [FirstName], [LastName], [UserName], [Password], [RoleId], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (1, N'Nabeel', N'Ashraf', N'nashraf', N'P@ssw0rd', 1, NULL, NULL, NULL, NULL)
INSERT [dbo].[Users] ([Id], [FirstName], [LastName], [UserName], [Password], [RoleId], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (2, N'Ali', N'Ahmad', N'aahmad', N'P@ssw0rd', 2, NULL, NULL, NULL, NULL)
INSERT [dbo].[Users] ([Id], [FirstName], [LastName], [UserName], [Password], [RoleId], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (1003, N'Khan', N'Ali', N'kali', N'P@ssw0rd', 2, CAST(N'2020-04-05T03:43:27.040' AS DateTime), 1, CAST(N'2020-04-05T03:43:27.040' AS DateTime), 1)
SET IDENTITY_INSERT [dbo].[Users] OFF
