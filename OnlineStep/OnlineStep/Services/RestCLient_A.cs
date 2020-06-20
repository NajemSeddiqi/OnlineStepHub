using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace OnlineStep.Services
{
    class RestCLient_A
    {

        public enum HttpVerb
        {
            GET,POST,PUT,DELETE
        }

        public string EndPoint { get; set; }

        public HttpVerb HttpMethod { get; set; }

        public RestCLient_A()
        {
        }

        public string DoRequest()
        {
            string resVal = string.Empty;

            HttpWebRequest request = (HttpWebRequest) WebRequest.Create(EndPoint);

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                //OK reponse = 200
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    throw new ApplicationException("DoRequest exception" + response.StatusCode.ToString());
                }
                //Manipulate/process the response, this can be anything (json, xml, html etc..)
                using (Stream responseStream = response.GetResponseStream())
                {
                    if (responseStream != null)
                    {
                        //Expecting a stream of data
                        //Up to the user to do what ever they want with it
                        using (StreamReader reader = new StreamReader(responseStream))
                        {
                            //This is what returns
                            resVal = reader.ReadToEnd();
                        }
                    }
                }
                response.Close();
            }

            return resVal;
        }
    }
}
