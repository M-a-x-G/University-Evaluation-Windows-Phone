using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CampusAppEvalWP.DTO
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
        internal string[] studyPaths;

        [DataMember]
        internal string[] textQuestions;

        [DataMember]
        internal Choices[] multipleChoiceQuestions;
        
    }

}
