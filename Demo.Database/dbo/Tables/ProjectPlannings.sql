CREATE TABLE [dbo].[ProjectPlannings] (
    [Id]          INT            IDENTITY (1, 1) NOT NULL PRIMARY KEY,
    [ProjectId]   INT            NOT NULL,
    [From]        DATE           NOT NULL,
    [Until]       DATE           NOT NULL,
    [Budget]      DECIMAL(11,2)  NOT NULL,

    CONSTRAINT [FK_ProjectPlanning_ProjectId_Projects_Id] FOREIGN KEY ([ProjectId]) REFERENCES [Projects] ([Id]),

);

