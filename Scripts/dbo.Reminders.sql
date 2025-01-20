USE [NWUTaskmaster]
GO

/****** Object: Table [dbo].[Reminders] Script Date: 2024/11/29 07:08:04 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

DROP TABLE [dbo].[Reminders];


GO
CREATE TABLE [dbo].[Reminders] (
    [reminder_id]          INT            IDENTITY (1, 1) NOT NULL,
    [task_id]              INT            NOT NULL,
    [reminder_date]        DATETIME       NULL,
    [is_recurring]         BIT            NOT NULL,
    [recurring_interval]   NVARCHAR (MAX) NULL,
    [relative_to_due_date] BIT            NOT NULL,
    [relative_time]        NVARCHAR (MAX) NULL,
    [created_at]           DATETIME       NOT NULL,
    [is_triggered]         BIT            NULL
);


