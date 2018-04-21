CREATE TABLE [Management].[Cultures] (
    [Id]        INT            IDENTITY (0, 1) NOT NULL,
    [Code]      NVARCHAR (200) NULL,
    [IsCurrent] BIT            NOT NULL,
    CONSTRAINT [PK_Settings_Id] PRIMARY KEY CLUSTERED ([Id] ASC)
);

