using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace ForaManagerTestClient
{
    class MyWebClient
    {
        internal static string DoRequest(String _URL, String clientID, String encryptionKey, String text, List<String> filterWordList)
        {
            String filterwords = "";
            GetFilterWordsInJSONFormat(ref filterwords, filterWordList);

            String encryptionIV = GenerateIV();

            String jsondata = "{\"inputtext\":\"" + text + "\",\"filterwords\":"+filterwords+",\"time\":" + (Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds + ",\"randomvalue\":" + (new Random()).Next(Int32.MaxValue) + "}";
            String data = AesHelper.Encrypt_AES(jsondata, encryptionKey, encryptionIV);

            using (WebClient client = new WebClient())
            {
                try
                {
                    byte[] response = client.UploadValues(_URL, new NameValueCollection()
                    {
                        { "cid", clientID },
                        { "eiv", encryptionIV },
                        { "data", data }
                    });

                    string result = System.Text.Encoding.UTF8.GetString(response);
                    return AesHelper.Decrypt_AES(result, encryptionKey, encryptionIV);
                }
                catch (Exception e) 
                {
                    Console.WriteLine("Error in webclient: " + e.Message + " " + e.Source + " " + e.StackTrace);
                }
            }
            return null;
        }

        private static String GenerateIV()
        {
            String t = "";
            Random r = new Random();
            while (t.Length < 32)
                t += r.Next(9);
            return t;
        }

        private static void GetFilterWordsInJSONFormat(ref string filterwords, List<string> filterWordList)
        {
            filterwords += "[";
            for (int i = 0; i < filterWordList.Count; i++)
            {
                filterwords += "\"" + filterWordList[i] + "\"";
                if (i < filterWordList.Count - 1)
                    filterwords += ",";
            }
            filterwords += "]";
        }
    }
}
