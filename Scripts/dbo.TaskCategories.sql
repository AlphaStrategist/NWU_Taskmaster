USE [NWUTaskmaster]
GO

/****** Object: Table [dbo].[TaskCategories] Script Date: 2024/11/29 07:08:09 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

DROP TABLE [dbo].[TaskCategories];


GO
CREATE TABLE [dbo].[TaskCategories] (
    [category_id]   INT            IDENTITY (1, 1) NOT NULL,
    [category_name] NVARCHAR (MAX) NULL
);


