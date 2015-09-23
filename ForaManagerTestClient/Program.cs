using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ForaManagerTestClient
{
    class Program
    {
        private static String URL = "";
        private static String ClientID = "";
        private static String EncryptionKey = "";
        private static String EncryptionIV = "";
        private static List<String> FilterWords = new List<String>() { "chokoladekage", "kaffe" };

        static void Main(string[] args)
        {
            List<String> result = null;
            result = AnalyseText("Chok0ladekage smager godt.");
            foreach (String word in result)
                Console.WriteLine("Demo 1 - found word: " + word);

            result = AnalyseText("Chokolade kage smager godt.");
            foreach (String word in result)
                Console.WriteLine("Demo 2 - found word: " + word);

            result = AnalyseText("Chokolade-kage smager godt.");
            foreach (String word in result)
                Console.WriteLine("Demo 3 - found word: " + word);

            result = AnalyseText("Chok0lade-kage smager godt.");
            foreach (String word in result)
                Console.WriteLine("Demo 4 - found word: " + word);

            result = AnalyseText("Chokol adekage smager godt.");
            foreach (String word in result)
                Console.WriteLine("Demo 5 - found word: " + word);

            result = AnalyseText("Chokolade_kage smager godt.");
            foreach (String word in result)
                Console.WriteLine("Demo 6 - found word: " + word);

            result = AnalyseText("C6okoladekage smager godt.");
            foreach (String word in result)
                Console.WriteLine("Demo 7 - found word: " + word);

            result = AnalyseText("C6oko1adekage smager godt.");
            foreach (String word in result)
                Console.WriteLine("Demo 8 - found word: " + word);

            result = AnalyseText("Chokoladekage smager godt.");
            foreach (String word in result)
                Console.WriteLine("Demo 9 - found word: " + word);

            result = AnalyseText("Ristede kaffebønner er mørke.");
            foreach (String word in result)
                Console.WriteLine("Demo 10 - found word: " + word);

            result = AnalyseText("En kop kaffer skal helst være varm.");
            foreach (String word in result)
                Console.WriteLine("Demo 11 - found word: " + word);

            result = AnalyseText("En kop klffe skal helst være varm.");
            foreach (String word in result)
                Console.WriteLine("Demo 12 - found word: " + word);

            result = AnalyseText("En kop klffe og et stykke ckokoladekege.");
            foreach (String word in result)
                Console.WriteLine("Demo 13 - found word: " + word);

            Console.ReadKey();
        }

        private static List<String> AnalyseText(String text)
        {
            String response = MyWebClient.DoRequest(URL, ClientID, EncryptionKey, EncryptionIV, text, FilterWords);
            return JSONParser.Parse(response);
        }
    }

    
}
