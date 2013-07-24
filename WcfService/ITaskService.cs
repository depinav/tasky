using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using tasky.Models;

namespace WcfService {

    [ServiceContract]
    public interface ITaskService {

        [OperationContract]
        ICollection<Task> getTasks();

        [OperationContract]
        ICollection<Task> getTask(int id);

        [OperationContract]
        ICollection<Task> createTask(string title, string description, int hours, int teamMemberID, int storyID);

        [OperationContract]
        ICollection<Task> deleteTask(int id);

        [OperationContract]
        ICollection<Task> updateTask(int id, string title, string decsription, int estimateHours, int remainingHours, int teamMemberID, int storyID);
    }
}