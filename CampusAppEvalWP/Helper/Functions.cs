using CampusAppEvalWP.DataModel;
using CampusAppEvalWP.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Security.Cryptography;
using Windows.Security.Cryptography.Core;
using Windows.Storage.Streams;
using Windows.System.Profile;
using Windows.Web.Http;
using Windows.Web.Http.Headers;

namespace CampusAppEvalWP.Helper
{
    class Functions
    {
        public struct sendDataTOCourse
        {
            public DTO.QuestionsDTO qDTO;
            public DTO.QRCodeDTO qrDTO;                    
        }

        public struct sendDataTOEvaluation
        {
            public List<DataModel.DataSource> dsl;
            public DTO.QRCodeDTO qrDTO;
            public string course;
        }

        public struct getDataFromServerStruct
        {
            public bool OK;
            public string json;
        }
        
        /*public static string GetDeviceID()
        {
            HardwareToken token = HardwareIdentification.GetPackageSpecificToken(null);
            IBuffer hardwareId = token.Id;

            HashAlgorithmProvider hasher = HashAlgorithmProvider.OpenAlgorithm("MD5");
            IBuffer hashed = hasher.HashData(hardwareId);

            string hashedString = CryptographicBuffer.EncodeToHexString(hashed);
            return hashedString;
        }*/

        /*public static async Task<getDataFromServerStruct> getDataFromServer(string aUrl)
        {
            var uri = new Uri(aUrl);
            var httpClient = new HttpClient();

            getDataFromServerStruct result = new getDataFromServerStruct();


            // Always catch network exceptions for async methods
            try
            {
                var response = await httpClient.GetAsync(uri);

                var statusCode = response.StatusCode;

                result.json = await response.Content.ReadAsStringAsync();

                if ((statusCode == HttpStatusCode.Ok) || (statusCode == HttpStatusCode.Accepted))
                {
                    result.OK = true;
                }
                else
                {
                    result.OK = false;
                }
            }
            catch
            {
                // Details in ex.Message and ex.HResult.       
            }

            // Once your app is done using the HttpClient object call dispose to 
            // free up system resources (the underlying socket and memory used for the object)
            httpClient.Dispose();

            return result;
        }*/

        public static async Task<getDataFromServerStruct> sendDataToServer(string aUrl, string aEndPoint, string aJson)
        {
            getDataFromServerStruct result = new getDataFromServerStruct();
           
            var httpClient = new HttpClient();
            
            // Always catch network exceptions for async methods
            try
            {

                httpClient = new Windows.Web.Http.HttpClient();
                Windows.Web.Http.HttpRequestMessage msg = new Windows.Web.Http.HttpRequestMessage(new Windows.Web.Http.HttpMethod("POST"), new Uri(aUrl + aEndPoint));             
                msg.Content = new HttpStringContent((aJson));
                msg.Content.Headers.ContentType = new HttpMediaTypeHeaderValue("application/json");

                var response = await httpClient.SendRequestAsync(msg).AsTask();
              
                var statusCode = response.StatusCode;

                result.json = await response.Content.ReadAsStringAsync();

                if (statusCode == HttpStatusCode.Ok)
                {                 
                    result.OK = true;                  
                }
                else
                {
                    result.OK = false;
                }


            }
            catch
            {
                // Details in ex.Message and ex.HResult.       
            }

            // Once your app is done using the HttpClient object call dispose to 
            // free up system resources (the underlying socket and memory used for the object)
            httpClient.Dispose();

            return result;
        }

        public static string serverMessage(ResponseDTO rdto)
        {
            string message = "";

            if (rdto.type == ErrorTyps.ANSWERS_SUCCESSFUL_ADDED)
                message = "Server: Daten erfolgreich erhalten";
            if (rdto.type == ErrorTyps.ERROR_WHILE_CREATING_EVALUATION)
                message = "Fehler: Server erstellt eine Evaluation";
            if (rdto.type == ErrorTyps.ERROR_WHILE_FILE_CREATION)
                message = "Fehler: Server erstellt eine Datei";
            if (rdto.type == ErrorTyps.EVALUATION_CLOSED)
                message = "Server: Evaluation wurde schon beendet";
            if (rdto.type == ErrorTyps.EVALUATION_CLOSED_WITH_FAILURE)
                message = "Server: Evaluation wurde mit einem Fehler beendet";
            if (rdto.type == ErrorTyps.IVALID_TOKEN)
                message = "Server: QR-Code is nicht mehr gültig";
            if (rdto.type == ErrorTyps.TOKEN_ALLREADY_USED)
                message = "Server: QR-Code wurde schon verwendet (Token)";
            if (rdto.type == ErrorTyps.UNKNOWN_ERROR)
                message = "Server: Unbekannter Fehler";

            return message;
        }

        /// <summary>
        /// Parse a query string into a System.Collections.Generic.Dictionary
        /// </summary>
        /// <param name="uri">Uri to parse query string from</param>
        /*public static Dictionary<string, string> ParseQueryString(Uri uri)
        {
            WwwFormUrlDecoder decoder = new WwwFormUrlDecoder(uri.Query);
            return decoder.ToDictionary(x => x.Name, x => x.Value);
        }*/

        public static List<DataModel.DataSource> convertQuestionDTOToDataSource(DTO.QuestionsDTO aQDTO)
        {
            List<DataModel.DataSource> dsl = new List<DataModel.DataSource>();
            
            string lAnswer1 = "";
            string lAnswer2 = "";
            string lAnswer3 = "";
            string lAnswer4 = "";
            string lAnswer5 = "";
            string lAnswer6 = "";

            // auf das interne Datenmodel spiegeln
            foreach (DTO.Choices item in aQDTO.multipleChoiceQuestions)
            {
                lAnswer1 = "";
                lAnswer2 = "";
                lAnswer3 = "";
                lAnswer4 = "";
                lAnswer5 = "";
                lAnswer6 = "";

                for (int j = 0; j < item.choices.Length; j++)
                {
                    if (j == 0) lAnswer1 = item.choices[j];
                    else if (j == 1) lAnswer2 = item.choices[j];
                    else if (j == 2) lAnswer3 = item.choices[j];
                    else if (j == 3) lAnswer4 = item.choices[j];
                    else if (j == 4) lAnswer5 = item.choices[j];
                    else if (j == 5) lAnswer6 = item.choices[j];
                }


                DataModel.DataSource ds = new DataModel.DataSource(item.text, lAnswer1, lAnswer2, lAnswer3, lAnswer4, lAnswer5, lAnswer6);
                dsl.Add(ds);
            }

            foreach (string value in aQDTO.textQuestions)
            {
                DataModel.DataSource ds = new DataModel.DataSource(value);
                dsl.Add(ds);
            }

            return dsl;

           

        }

        public static DTO.AnswersDTO convertDataSourcetoAnswersDTO(List<DataModel.DataSource> aDSL)
        {
            DTO.AnswersDTO lDTO = new DTO.AnswersDTO();
            Dictionary<string, string> answers = new Dictionary<string, string>();

            if (aDSL.Count > 0)
            {
 
                foreach(DataModel.DataSource ds in aDSL)
                {
                    string lquestion = "";
                    string lanswer = "";

                   // Frage
                    lquestion = ds.Question;
                    
                    // Textantworten
                    if (ds.IsBigAnswer)
                    {                       
                        lanswer = ds.BigAnswer;
                    }
                    else 
                    {
                        if (ds.Answer1State)
                            lanswer = ds.Answer1;
                        else if (ds.Answer2State)
                            lanswer = ds.Answer2;
                        else if (ds.Answer3State)
                            lanswer = ds.Answer3;
                        else if (ds.Answer4State)
                            lanswer = ds.Answer4;
                        else if (ds.Answer5State)
                            lanswer = ds.Answer5;
                        else if (ds.Answer6State)
                            lanswer = ds.Answer6;
                        else
                            lanswer = "";
                    }


                    answers.Add(lquestion, lanswer);
                  
                }

                lDTO.answers = answers;

                

            }


            return lDTO;
        }

        /*public static string cutURLBeforEndpoint(String aURL, String aEndpoint)
        {
            string lURL = aURL;

            if (aURL.Contains(aEndpoint))
            {
                int pos = aURL.IndexOf(aEndpoint);

                if (pos != 0)
                {
                    lURL = aURL.Remove(pos);
                }

            }
           
            return lURL;

        }*/


    }
}
