using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CampusApp.Evaluation.DTO
{
    public struct Choices
    {
        public string text;
        public string[] choices;     
    }
    
    [DataContract]
    class QuestionsDTO
    {
        [DataMember]
        internal string voteToken;

        [DataMember]
        internal string evaluationUID;

        [DataMember]
        internal string[] innerSections;

        [DataMember]
        internal string[] textQuestions;

        [DataMember]
        internal Choices[] multipleChoiceQuestions;
        
    }

}
