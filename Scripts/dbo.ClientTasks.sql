USE [NWUTaskmaster]
GO

/****** Object: Table [dbo].[ClientTasks] Script Date: 2024/11/29 07:07:58 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

DROP TABLE [dbo].[ClientTasks];


GO
CREATE TABLE [dbo].[ClientTasks] (
    [task_id]        INT            IDENTITY (1, 1) NOT NULL,
    [client_id]      INT            NOT NULL,
    [title]          NVARCHAR (MAX) NULL,
    [description]    NVARCHAR (MAX) NULL,
    [due_date]       DATETIME       NOT NULL,
    [created_at]     DATETIME       NOT NULL,
    [is_completed]   BIT            NOT NULL,
    [parent_task_id] INT            NULL,
    [category_id]    INT            NULL,
    [priority]       INT            NOT NULL
);


