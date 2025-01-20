USE [NWUTaskmaster]
GO

/****** Object: Table [dbo].[AspNetUserRoles] Script Date: 2024/11/29 07:07:41 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

DROP TABLE [dbo].[AspNetUserRoles];


GO
CREATE TABLE [dbo].[AspNetUserRoles] (
    [UserId] NVARCHAR (128) NOT NULL,
    [RoleId] NVARCHAR (128) NOT NULL
);


