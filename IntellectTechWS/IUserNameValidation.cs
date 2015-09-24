using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace IntellectTechWS
{
    [ServiceContract]
    public interface IUserNameValidation
    {
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "checkavailability?username={userName}")]
        string CheckAvailability(string userName);
    }
}
