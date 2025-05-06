USE [ojtPract_Rice]
GO

/****** Object: Table [dbo].[users] Script Date: 5/6/2025 9:41:28 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[users] (
    [ID]       INT           IDENTITY (1, 1) NOT NULL,
    [username] VARCHAR (128) NULL,
    [password] VARCHAR (255) NULL,
    [rank]     INT           NOT NULL
);


