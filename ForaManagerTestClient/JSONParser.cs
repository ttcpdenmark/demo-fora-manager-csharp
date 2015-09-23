using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ForaManagerTestClient
{
    class JSONParser
    {
        internal static List<String> Parse(string response)
        {
            List<String> result = new List<String>();
            dynamic res = System.Web.Helpers.Json.Decode(response);
            if (res != null)
            {
                var wordset = ((IEnumerable<dynamic>)res.words);
                foreach (var word in wordset)
                {
                    result.Add(word.foundWord);
                }
            }
            return result;
        }
    }
}
