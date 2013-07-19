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
    }
}