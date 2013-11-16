CREATE TABLE [dbo].[TaskTypes] (
    [TaskTypeId]   INT           NOT NULL,
    [TaskTypeName] NVARCHAR (50) NOT NULL,
    CONSTRAINT [PK_TaskTypes] PRIMARY KEY CLUSTERED ([TaskTypeId] ASC)
);

