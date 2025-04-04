CREATE DATABASE Euro2024DB
GO

USE Euro2024DB
GO
/****** Object:  Table [dbo].[Accounts]    Script Date: 6/14/2024 11:30:58 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Accounts](
	[Email] [nvarchar](50) NOT NULL,
	[UserName] [nvarchar](100) NULL,
	[Password] [nvarchar](50) NULL,
	[FullName] [nvarchar](50) NULL,
	[Birthday] [datetime] NULL,
	[Status] [nvarchar](10) NULL,
	[RoleID] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[GroupTeam]    Script Date: 6/14/2024 11:30:58 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GroupTeam](
	[GroupID] [int] IDENTITY(1,1) NOT NULL,
	[GroupName] [nvarchar](50) NULL,
 CONSTRAINT [PK_GroupTeam] PRIMARY KEY CLUSTERED 
(
	[GroupID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Teams]    Script Date: 6/14/2024 11:30:58 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Teams](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[TeamName] [nvarchar](50) NOT NULL,
	[Point] [int] NOT NULL,
	[GroupID] [int] NULL,
	[Position] [int] NOT NULL,
 CONSTRAINT [PK_Teams] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[Accounts] ([Email], [UserName], [Password], [FullName], [Birthday], [Status], [RoleID]) VALUES (N'admin@fb.com', N'admin', N'@5', N'Admin', CAST(N'1990-09-16T00:00:00.000' AS DateTime), N'active', 1)
INSERT [dbo].[Accounts] ([Email], [UserName], [Password], [FullName], [Birthday], [Status], [RoleID]) VALUES (N'manager@fb.com', N'manager', N'@5', N'Manager', CAST(N'1980-12-12T00:00:00.000' AS DateTime), N'active', 3)
INSERT [dbo].[Accounts] ([Email], [UserName], [Password], [FullName], [Birthday], [Status], [RoleID]) VALUES (N'staff@fb.com', N'staff', N'@5', N'Staff', CAST(N'1970-10-10T00:00:00.000' AS DateTime), N'active', 2)
GO
SET IDENTITY_INSERT [dbo].[GroupTeam] ON 

INSERT [dbo].[GroupTeam] ([GroupID], [GroupName]) VALUES (1, N'Group A')
INSERT [dbo].[GroupTeam] ([GroupID], [GroupName]) VALUES (2, N'Group B')
INSERT [dbo].[GroupTeam] ([GroupID], [GroupName]) VALUES (3, N'Group C')
INSERT [dbo].[GroupTeam] ([GroupID], [GroupName]) VALUES (4, N'Group D')
INSERT [dbo].[GroupTeam] ([GroupID], [GroupName]) VALUES (5, N'Group E')
INSERT [dbo].[GroupTeam] ([GroupID], [GroupName]) VALUES (6, N'Group F')
SET IDENTITY_INSERT [dbo].[GroupTeam] OFF
GO
SET IDENTITY_INSERT [dbo].[Teams] ON 

INSERT [dbo].[Teams] ([ID], [TeamName], [Point], [GroupID], [Position]) VALUES (9, N'Germany ', 7, 1, 1)
INSERT [dbo].[Teams] ([ID], [TeamName], [Point], [GroupID], [Position]) VALUES (10, N'Scotland', 5, 1, 2)
INSERT [dbo].[Teams] ([ID], [TeamName], [Point], [GroupID], [Position]) VALUES (11, N'Hungary', 3, 1, 3)
INSERT [dbo].[Teams] ([ID], [TeamName], [Point], [GroupID], [Position]) VALUES (12, N'Switzerland', 1, 1, 4)
INSERT [dbo].[Teams] ([ID], [TeamName], [Point], [GroupID], [Position]) VALUES (13, N'Spain', 6, 2, 1)
INSERT [dbo].[Teams] ([ID], [TeamName], [Point], [GroupID], [Position]) VALUES (14, N'Croatia', 4, 2, 3)
INSERT [dbo].[Teams] ([ID], [TeamName], [Point], [GroupID], [Position]) VALUES (15, N'Italy', 6, 2, 2)
INSERT [dbo].[Teams] ([ID], [TeamName], [Point], [GroupID], [Position]) VALUES (16, N'Albania', 1, 2, 4)
INSERT [dbo].[Teams] ([ID], [TeamName], [Point], [GroupID], [Position]) VALUES (17, N'Slovenia', 6, 3, 2)
INSERT [dbo].[Teams] ([ID], [TeamName], [Point], [GroupID], [Position]) VALUES (18, N'Denmark', 4, 3, 3)
INSERT [dbo].[Teams] ([ID], [TeamName], [Point], [GroupID], [Position]) VALUES (19, N'Serbia', 1, 3, 4)
INSERT [dbo].[Teams] ([ID], [TeamName], [Point], [GroupID], [Position]) VALUES (20, N'England', 6, 3, 1)
INSERT [dbo].[Teams] ([ID], [TeamName], [Point], [GroupID], [Position]) VALUES (21, N'Poland', 1, 4, 4)
INSERT [dbo].[Teams] ([ID], [TeamName], [Point], [GroupID], [Position]) VALUES (22, N'Netherlands', 6, 4, 1)
INSERT [dbo].[Teams] ([ID], [TeamName], [Point], [GroupID], [Position]) VALUES (23, N'Austria', 4, 4, 3)
INSERT [dbo].[Teams] ([ID], [TeamName], [Point], [GroupID], [Position]) VALUES (24, N'France', 6, 4, 1)
INSERT [dbo].[Teams] ([ID], [TeamName], [Point], [GroupID], [Position]) VALUES (25, N'Belgium', 7, 5, 1)
INSERT [dbo].[Teams] ([ID], [TeamName], [Point], [GroupID], [Position]) VALUES (26, N'Slovakia', 0, 5, 4)
INSERT [dbo].[Teams] ([ID], [TeamName], [Point], [GroupID], [Position]) VALUES (27, N'Romania', 4, 5, 3)
INSERT [dbo].[Teams] ([ID], [TeamName], [Point], [GroupID], [Position]) VALUES (28, N'Ukraine', 5, 5, 2)
INSERT [dbo].[Teams] ([ID], [TeamName], [Point], [GroupID], [Position]) VALUES (29, N'Turkey', 3, 6, 4)
INSERT [dbo].[Teams] ([ID], [TeamName], [Point], [GroupID], [Position]) VALUES (30, N'Georgia', 4, 6, 3)
INSERT [dbo].[Teams] ([ID], [TeamName], [Point], [GroupID], [Position]) VALUES (31, N'Portugal', 6, 6, 1)
INSERT [dbo].[Teams] ([ID], [TeamName], [Point], [GroupID], [Position]) VALUES (32, N'Czech Republic', 4, 6, 2)
SET IDENTITY_INSERT [dbo].[Teams] OFF
GO
ALTER TABLE [dbo].[Teams]  WITH CHECK ADD  CONSTRAINT [FK_Teams_GroupTeam] FOREIGN KEY([GroupID])
REFERENCES [dbo].[GroupTeam] ([GroupID])
GO
ALTER TABLE [dbo].[Teams] CHECK CONSTRAINT [FK_Teams_GroupTeam]
GO
