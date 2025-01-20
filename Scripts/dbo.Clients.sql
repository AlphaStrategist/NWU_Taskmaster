USE [NWUTaskmaster]
GO

/****** Object: Table [dbo].[Clients] Script Date: 2024/11/29 07:07:53 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

DROP TABLE [dbo].[Clients];


GO
CREATE TABLE [dbo].[Clients] (
    [client_id]      INT            IDENTITY (1, 1) NOT NULL,
    [username]       NVARCHAR (256) NOT NULL,
    [usersurname]    NVARCHAR (256) NOT NULL,
    [email]          NVARCHAR (256) NULL,
    [password_hash]  NVARCHAR (MAX) NULL,
    [created_at]     DATETIME       NOT NULL,
    [aspnet_user_id] NVARCHAR (128) NOT NULL
);


