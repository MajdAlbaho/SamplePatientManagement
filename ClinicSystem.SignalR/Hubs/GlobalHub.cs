using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicSystem.SignalR.Hubs
{
    [HubName(nameof(GlobalHub))]
    public class GlobalHub : Hub
    {
        public void AddPatient(string serializedPatient)
        {
            Clients.Others.AddPatient(serializedPatient);
        }

        public void EditPatient(string serializedPatient)
        {
            Clients.Others.EditPatient(serializedPatient);
        }

        public void DeletePatient(int paitentId)
        {
            Clients.Others.DeletePatient(paitentId);
        }
    }
}
