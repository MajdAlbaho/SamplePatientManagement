CREATE TABLE [Patient].[ConsultationDiagnosis] (
    [VisitConsultationId] INT NOT NULL,
    [DiagnosisId]         INT NOT NULL,
    CONSTRAINT [FK_ConsultationDiagnosis_Diagnosis_DiagnosisId] FOREIGN KEY ([DiagnosisId]) REFERENCES [Medical].[Diagnosis] ([Id]),
    CONSTRAINT [FK_ConsultationDiagnosis_VisitConsultations_VisitConsultationId] FOREIGN KEY ([VisitConsultationId]) REFERENCES [Patient].[VisitConsultations] ([Id]) ON DELETE CASCADE
);

