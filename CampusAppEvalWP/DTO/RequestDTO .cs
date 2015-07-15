using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CampusAppEvalWP.DTO
{
    [DataContract]
    class RequestDTO
    {
        [DataMember]
        internal string voteToken;
    }
}
