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

        public static async Task<string> getDataFromServer(string aUrl)
        {
            var uri = new Uri(aUrl);
            var httpClient = new HttpClient();
            string result = "";


            // Always catch network exceptions for async methods
            try
            {
                var response = await httpClient.GetAsync(uri);

                var statusCode = response.StatusCode;

                if ((statusCode == HttpStatusCode.Ok) || (statusCode == HttpStatusCode.Accepted))
                    result = await response.Content.ReadAsStringAsync();


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

        public static async Task<string> sendDataToServer(string aUrl, string aEndPoint, string aJson)
        {
            string result = "";
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

                if (statusCode == HttpStatusCode.Ok)
                    result = await response.Content.ReadAsStringAsync();


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

        /// <summary>
        /// Parse a query string into a System.Collections.Generic.Dictionary
        /// </summary>
        /// <param name="uri">Uri to parse query string from</param>
        /*public static Dictionary<string, string> ParseQueryString(Uri uri)
        {
            WwwFormUrlDecoder decoder = new WwwFormUrlDecoder(uri.Query);
            return decoder.ToDictionary(x => x.Name, x => x.Value);
        }*/

        public static List<DataModel.DataSource> convertQuestionDTOToDataSource(DTO.QuestionsDTO aQDTO, String aCourse)
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


                DataModel.DataSource ds = new DataModel.DataSource(item.text, lAnswer1, lAnswer2, lAnswer3, lAnswer4, lAnswer5, lAnswer6, aCourse);
                dsl.Add(ds);
            }

            foreach (string value in aQDTO.textQuestions)
            {
                DataModel.DataSource ds = new DataModel.DataSource(value, aCourse);
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
