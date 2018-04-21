CREATE TABLE [MedicalPoint].[MedicalPoints] (
    [Id]   INT            IDENTITY (0, 1) NOT NULL,
    [Name] NVARCHAR (200) NOT NULL,
    CONSTRAINT [PK_MedicalPoints_Id] PRIMARY KEY CLUSTERED ([Id] ASC)
);

