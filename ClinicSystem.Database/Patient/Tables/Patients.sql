CREATE TABLE [Patient].[Patients] (
    [PersonId]         INT            NOT NULL,
    [RegistrationDate] DATE           NOT NULL,
    [Address]          NVARCHAR (200) NOT NULL,
    [PhoneNumber]      NVARCHAR (10)  NOT NULL,
    [MedicalPointId]   INT            NOT NULL,
    CONSTRAINT [PK_Patients_PersonId] PRIMARY KEY CLUSTERED ([PersonId] ASC),
    CONSTRAINT [FK_Patients_MedicalPoint_MedicalPointId] FOREIGN KEY ([MedicalPointId]) REFERENCES [MedicalPoint].[MedicalPoints] ([Id]),
    CONSTRAINT [FK_Patients_Persons_PersonId] FOREIGN KEY ([PersonId]) REFERENCES [Person].[Persons] ([Id]) ON DELETE CASCADE
);


GO


CREATE TRIGGER [Patient].[Tr_Patients_DeletePerson]
ON [Patient].[Patients]
AFTER DELETE
AS
BEGIN
    SET NOCOUNT ON;

    DELETE FROM Person.Persons
	FROM Deleted
	WHERE Persons.Id = Deleted.PersonId
	AND NOT EXISTS(SELECT PersonId FROM Medical.Doctors)
END;




