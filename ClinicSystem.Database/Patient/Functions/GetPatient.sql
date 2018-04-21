CREATE FUNCTION Patient.GetPatient
(
    @PersonId INT
)
RETURNS TABLE
AS
RETURN
(
    SELECT PersonId,
           Persons.Id,
           RegistrationDate,
           Address,
           PhoneNumber,
           MedicalPointId,
           FirstName,
           LastName,
           BirthDate,
           Gender
    FROM Patient.Patients
        INNER JOIN Person.Persons
            ON Persons.Id = Patients.PersonId
    WHERE (
              @PersonId IS NULL
              OR PersonId = @PersonId
          )
);
