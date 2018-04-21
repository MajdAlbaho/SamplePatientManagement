using ClinicSystem.Linq.LinqDb;
using ClinicSystem.Model.RepositoryInterface;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClinicSystem.DataType;
using System;

namespace ClinicSystem.Linq.Repositories
{
    public class PatientRepository : BaseRepository<Model.Patient, Patient, int>, IPatientRepository
    {
        public override Task<IEnumerable<Model.Patient>> GetItems()
        {
            return ExecuteOnDb(db => Mapper.MapCollection<Model.Patient>(db.GetPatient(null).ToList()).AsEnumerable());
        }

        public override async Task<Model.Patient> InsertItem(Model.Patient item)
        {
            var person = new Person()
            {
                FirstName = item.FirstName,
                LastName = item.LastName,
                BirthDate = item.BirthDate,
                Gender = item.Gender
            };
            await ExecuteOnDb(db =>
            {
                db.Persons.InsertOnSubmit(person);
                db.SubmitChanges();
            });

            var patient = new Patient()
            {
                Address = item.Address,
                MedicalPointId = item.MedicalPointId,
                PhoneNumber = item.PhoneNumber,
                RegistrationDate = item.RegistrationDate,
                PersonId = person.Id
            };
            await ExecuteOnDb(db =>
            {
                db.Patients.InsertOnSubmit(patient);
                db.SubmitChanges();
            });

            var model = Mapper.Map<Model.Patient>(patient);
            Mapper.Map(person, model);
            return model;
        }

        public override Task<Model.Patient> UpdateItem(Model.Patient source)
        {
            if (source == null)
                throw new Exception("Parameter can't be null.");
            
            return ExecuteOnDb((db) =>
            {
                Mapper.Map(source, db.Patients.First(e => e.PersonId == source.PersonId));
                Mapper.Map(source, db.Persons.First(e => e.Id == source.PersonId));
                return source;
            });
        }
    }
}
