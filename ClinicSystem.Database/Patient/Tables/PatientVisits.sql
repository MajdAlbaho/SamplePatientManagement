CREATE TABLE [Patient].[PatientVisits] (
    [Id]       INT  IDENTITY (0, 1) NOT NULL,
    [PersonId] INT  NOT NULL,
    [Date]     DATE NOT NULL,
    CONSTRAINT [PK_MedicalPoint_Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_PatientVisits_Patients_PersonId] FOREIGN KEY ([PersonId]) REFERENCES [Patient].[Patients] ([PersonId]) ON DELETE CASCADE
);

