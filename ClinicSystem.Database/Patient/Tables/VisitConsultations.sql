CREATE TABLE [Patient].[VisitConsultations] (
    [Id]             INT IDENTITY (0, 1) NOT NULL,
    [PatientVisitId] INT NOT NULL,
    [ClinicId]       INT NOT NULL,
    [DoctorId]       INT NOT NULL,
    CONSTRAINT [PK_VisitConsultations_Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_PatientVisitConsultations_PatientVisit_PatientVisitId] FOREIGN KEY ([PatientVisitId]) REFERENCES [Patient].[PatientVisits] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_VisitConsultations_Clinics_ClinicId] FOREIGN KEY ([ClinicId]) REFERENCES [Medical].[Clinics] ([Id]),
    CONSTRAINT [FK_VisitConsultations_Doctors_DoctorId] FOREIGN KEY ([DoctorId]) REFERENCES [Medical].[Doctors] ([Id])
);

