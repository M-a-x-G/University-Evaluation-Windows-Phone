using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampusAppEvalWP.DataModel
{
    class ErrorTyps
    {
        public static int UNKNOWN_ERROR = -1;
        public static int IVALID_TOKEN = 1;
        public static int ANSWERS_SUCCESSFUL_ADDED = 2;
        public static int TOKEN_ALLREADY_USED = 3;
        public static int EVALUATION_CLOSED = 4;
        public static int EVALUATION_CLOSED_WITH_FAILURE = 5;
        public static int ERROR_WHILE_FILE_CREATION = 6;
        public static int ERROR_WHILE_CREATING_EVALUATION = 7;
    }
}
