using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace SearchandSort
{
    internal class Program
    {
        static void Main(string[] args)
        {

            string fileName = "Net_1_2048.txt";
            string directory = Path.Combine(Environment.CurrentDirectory, "..", "..", "Data"); // Move up two levels to get to the Data folder
            string desiredPath = Path.Combine(directory, fileName);


            Console.WriteLine("Actual path {0}", desiredPath);
            Console.WriteLine("Desired path C:\\Users\\jlw19\\Desktop\\Algorithms-Complexity\\SearchandSort\\SearchandSort\\Data\\Net_1_2048.txt");


            string contents = File.ReadAllText(desiredPath);

            Console.WriteLine("{0}", contents);

            Console.ReadLine();
     
        }
    }
}
