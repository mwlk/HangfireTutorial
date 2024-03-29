﻿using System;
using System.IO;
using System.Net;
using System.Net.Http;

namespace HangfireTutorial.Service
{
    public class NotificationService
    {
        private HttpClient client = new HttpClient();
        string _strURL = string.Empty;
        string _strAPI = string.Empty;

        public NotificationService()
        {
            _strURL = System.Configuration.ConfigurationManager.AppSettings.Get("wURL");
            _strAPI = System.Configuration.ConfigurationManager.AppSettings.Get("wAPI");
        }

        public void SendRequest()
        {
            try
            {
                var url = _strURL + _strAPI;

                var request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "GET";
                request.ContentType = "application/json";
                request.Accept = "application/json";

                using (WebResponse _response = request.GetResponse())
                {
                    using (Stream strmReader = _response.GetResponseStream())
                    {
                        if (strmReader == null) return;

                        using (StreamReader objReader = new StreamReader(strmReader))
                        {
                            if (objReader == null)
                            {
                                return;
                            }
                            string requestResponse = objReader.ReadToEnd();
                            Console.WriteLine(requestResponse);
                        }
                    }
                }

            }
            catch (Exception e)
            {
                new ErrorNotifyService().AlertException(e);
                Console.WriteLine(e.Message);
            }
        }
    }
}