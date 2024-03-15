CREATE TABLE [dbo].[Projects] (
    [Id]          INT            IDENTITY (1, 1) NOT NULL PRIMARY KEY,
    [Title]       NVARCHAR (50)  NULL,
    [Description] NVARCHAR (MAX) NULL,
    [Number]      INT            NOT NULL
);

