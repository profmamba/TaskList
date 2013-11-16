CREATE TABLE [dbo].[Tasks] (
    [TaskId]      INT           IDENTITY (1, 1) NOT NULL,
    [TaskTypeId]  INT           NOT NULL,
    [Description] VARCHAR (MAX) NOT NULL,
    [CreateDate]  DATETIME2 (7) CONSTRAINT [DF_Tasks_CreateDate] DEFAULT (getdate()) NOT NULL,
    [Deleted]     BIT           CONSTRAINT [DF_Tasks_Deleted] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_Tasks] PRIMARY KEY CLUSTERED ([TaskId] ASC),
    CONSTRAINT [FK_Tasks_TaskTypes] FOREIGN KEY ([TaskTypeId]) REFERENCES [dbo].[TaskTypes] ([TaskTypeId])
);

