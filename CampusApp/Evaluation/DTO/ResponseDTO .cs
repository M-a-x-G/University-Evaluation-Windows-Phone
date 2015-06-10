using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CampusApp.Evaluation.DTO
{
     [DataContract]
    class ResponseDTO
    {
         [DataMember]
         internal string message;
    }
}
