using MetaweatherTests.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Text.Json;

namespace MetaweatherTests
{
    public static class RequestsManager
    {
        public static HttpWebResponse SendRequest(string url)
        {
            HttpWebRequest webRequest = (HttpWebRequest)HttpWebRequest.Create(string.Concat(url));
            HttpWebResponse webResponse = (HttpWebResponse)webRequest.GetResponse();

            return webResponse;
        }

        public static string GetResponceBody(HttpWebResponse response)
        {
            using (Stream stream = response.GetResponseStream())
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    string responceString = reader.ReadToEnd();
                    return responceString;
                }
            }
        }

        public static T DeserializeStringFromJsonTo<T>(string body)
        {
            try
            {
                T desBody = JsonSerializer.Deserialize<T>(body);
                return desBody;
            }
            catch (Exception ex)
            {
                return default;
            }
        }

        public static T RequestToEntitiesT<T>(string url)
        {
            var responce = SendRequest(url);
            string body = GetResponceBody(responce);
            var entity = DeserializeStringFromJsonTo<T>(body);
            return entity;
        }
    }
}
