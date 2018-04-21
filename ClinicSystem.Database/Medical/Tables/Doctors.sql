CREATE TABLE [Medical].[Doctors] (
    [Id]       INT IDENTITY (0, 1) NOT NULL,
    [ClinicId] INT NOT NULL,
    [PersonId] INT NULL,
    CONSTRAINT [PK_Doctors_Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Doctors_Clinics_ClinicId] FOREIGN KEY ([ClinicId]) REFERENCES [Medical].[Clinics] ([Id]),
    CONSTRAINT [FK_Doctors_Persons_PersonID] FOREIGN KEY ([PersonId]) REFERENCES [Person].[Persons] ([Id]),
    CONSTRAINT [UNIQUE_Doctors_ClinicId_Person_Id] UNIQUE NONCLUSTERED ([ClinicId] ASC, [PersonId] ASC)
);


GO

CREATE TRIGGER [Medical].[Tr_Doctors_DeletePerson]
ON [Medical].[Doctors]
AFTER DELETE
AS
BEGIN
    SET NOCOUNT ON;

    DELETE FROM Person.Persons
	FROM Deleted
	WHERE Persons.Id = Deleted.PersonId
	AND Deleted.PersonId NOT IN (SELECT PersonId FROM Patient.Patients)
END;




