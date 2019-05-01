using System;
using System.Text.RegularExpressions;

namespace regex_vs_iteracions
{
    class Program
    {
        public static string ValidateStringWithRegEx(string s)
        {
            var result = default(string);
            var rgx = new Regex("[^A-Za-z0-9_]", RegexOptions.Compiled);
            if (!string.IsNullOrEmpty(s))
            {
                result = rgx.Replace(s, "_");
            }
            return result;
        }

        public static string ValidateStringWithChainedIfs(string stringToClean, char charToReplace = '_')
        {
            var charrArray = stringToClean.ToCharArray();
            int length = charrArray.Length;
            for (int i = 0; i < length; i++)
            {
                // Note the full negation of the if!
                if (!((charrArray[i] >= '0' && charrArray[i] <= '9') ||
                  (charrArray[i] >= 'A' && charrArray[i] <= 'Z') ||
                  (charrArray[i] >= 'a' && charrArray[i] <= 'z') ||
                  (charrArray[i] == '_')))
                {
                    charrArray[i] = charToReplace;
                }
            }
            return new string(charrArray);
        }
        static void Main(string[] args)
        {
            const int LOOPS = 1000;
            string aux = "";
            string strToFilter = "this is the filtered_string/some-characters+are?not\\allowed";

            var timer = System.Diagnostics.Stopwatch.StartNew();

            for (int i = 0; i < LOOPS; i++)
                aux = ValidateStringWithRegEx(strToFilter);

            timer.Stop();
            Console.WriteLine("Done by ValidateStringWithRegEx took: " + timer.ElapsedMilliseconds);

            timer = System.Diagnostics.Stopwatch.StartNew();

            for (int i = 0; i < LOOPS; i++)
                aux = ValidateStringWithChainedIfs(strToFilter);
            
            timer.Stop();
            Console.WriteLine("Done by ValidateStringWithChainedIfs took: " + timer.ElapsedMilliseconds);
            Console.WriteLine(aux);
        }
    }
}
