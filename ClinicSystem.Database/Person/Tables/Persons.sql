CREATE TABLE [Person].[Persons] (
    [Id]        INT            IDENTITY (0, 1) NOT NULL,
    [FirstName] NVARCHAR (200) NOT NULL,
    [LastName]  NVARCHAR (200) NOT NULL,
    [BirthDate] DATE           NOT NULL,
    [Gender]    BIT            CONSTRAINT [Default_Persons_0] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_Persons_Id] PRIMARY KEY CLUSTERED ([Id] ASC)
);

