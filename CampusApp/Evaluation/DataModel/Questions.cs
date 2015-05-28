using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CampusApp.Evaluation.DataModel
{
    public struct Choices
    {
        public string text;
        public string[] choices;     
    }
    
    [DataContract]
    class Questions
    {
        [DataMember]
        internal string token;

        [DataMember]
        internal string[] textQuestions;

        [DataMember]
        internal Choices[] multipleChoiceQuestions;
        
    }

}
