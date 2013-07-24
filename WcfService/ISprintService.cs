using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WcfService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "ISprintService" in both code and config file together.
    [ServiceContract]
    public interface ISprintService
    {
        [OperationContract]
        ICollection<Sprint> getAllSprints();

        [OperationContract]
        ICollection<Sprint> getSprint(int id);

        [OperationContract]
        void createSprint(string newTitle, DateTime newStartDate, DateTime newEndDate);

        [OperationContract]
        void deleteSprint(int id);

        [OperationContract]
        ICollection<Sprint> updateSprint(int id, string newTitle, DateTime newStartDate, DateTime newEndDate);
    }
}
