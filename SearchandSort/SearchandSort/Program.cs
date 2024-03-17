using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Reflection;

namespace SearchandSort
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Create a string array of all our files
            string[] FileName = {"Net_1_256.txt", "Net_2_256.txt", "Net_3_256.txt","Net_1_2048.txt", "Net_2_2048.txt", "Net_3_2048.txt"};

            // Create a GetTextFileData to hold the data of all our text files
            GetTextFileData[] FileDataArray = new GetTextFileData[6];

            // Iterate our file names and create a new file data for each also checking all data returned is valid 
            for (int i = 0; i < FileName.Length; i++)
            {
                GetTextFileData FileData = GetNumberArrayForFile(FileName[i]);

                if (FileData.DataError)
                {
                    Console.WriteLine($"{FileName[i]} Data Contains invalid data type");
                }
                else
                {
                    Console.WriteLine($"{FileName[i]} All data is valid");
                    FileDataArray[i] = FileData;
                }
            }

            // Get userinput for the pick of the first 3 arrays, minus 1 to allow for array index starting at 0 
            int ArrayToSort = GetUserInitialArrayPick() - 1;

            // Call our MainSearchAndSortLoop with value true to print 10th values
            MainSearchAndSortLoop(FileDataArray[ArrayToSort].NumberArray, true);

            // Get userinput for the pick of the further 3 arrays, add two to position the user pick correctly within the array index
            int FurtherArrayToSort = GetUserFurtherArrayPick() + 2;

            // Call our MainSearchAndSortLoop with value false to print 50th values
            MainSearchAndSortLoop(FileDataArray[FurtherArrayToSort].NumberArray, false);


            Console.WriteLine("Now we will check the sorted array for files Net_1_256 and Net_3_256, press enter to continue...");
            Console.ReadLine();

            // Merge the below arrays then proform the MainSearchAndSortLoop on it printing every 50th value
            int[] Merged_Net_1_256_Net_3_256 = FileDataArray[0].NumberArray.Concat(FileDataArray[1].NumberArray).ToArray();
            MainSearchAndSortLoop(Merged_Net_1_256_Net_3_256, false);


            Console.WriteLine("Now we will check the sorted array for files Net_1_2048, Net_2_2048 and Net_3_2048, press enter to continue...");
            Console.ReadLine();

            // Merge the below arrays then proform the MainSearchAndSortLoop on it printing every 50th value
            int[] Merged_2048LengthFiles = FileDataArray[3].NumberArray.Concat(FileDataArray[4].NumberArray).Concat(FileDataArray[5].NumberArray).ToArray();
            MainSearchAndSortLoop(Merged_2048LengthFiles, false);

            Console.ReadLine();
        }

        static void MainSearchAndSortLoop(int[] ArrayToSort, bool Search10th)
        {
            // Create an array of SortResult and call the SortArrayAscending passing in the relevant array
            SortResult[] UserPickedArraySortResultAscending = SortArrayAscending(ArrayToSort);


            // Create an array of SortResult and call the SortArrayDescending passing in the relevant array
            SortResult[] UserPickedArraySortResultDescending = SortArrayDescending(ArrayToSort);

            // Log all 3 sort results for Ascending
            LogSortResult(UserPickedArraySortResultAscending[0]);
            LogSortResult(UserPickedArraySortResultAscending[1]);
            LogSortResult(UserPickedArraySortResultAscending[2]);
            // Log all 3 sort results for Descending
            LogSortResult(UserPickedArraySortResultDescending[0]);
            LogSortResult(UserPickedArraySortResultDescending[1]);
            LogSortResult(UserPickedArraySortResultDescending[2]);



            int[] BubbleSortAscending = UserPickedArraySortResultAscending[0].SortedArray;
            int[] BubbleSortDescending = UserPickedArraySortResultDescending[0].SortedArray;

            // If search 10th is true print every 10th value else print every 50th
            if(Search10th)
            {
                // Print every 10th value of our  bubblesort ascending sorted array 
                PrintIntArrayEvery10thValue(BubbleSortAscending);

                // Print every 10th value of our  bubblesort ascending descending array 
                PrintIntArrayEvery10thValue(BubbleSortDescending);
            }
            else
            {
                // Print every 50th value of our  bubblesort ascending sorted array 
                PrintIntArrayEvery50thValue(BubbleSortAscending);

                // Print every 50th value of our  bubblesort ascending descending array 
                PrintIntArrayEvery50thValue(BubbleSortDescending);
            }
     

            int Target = GetUserTargetNumberPick();

            // Serach the UserPickedArraySortResultAscending for a user specified value
            SearchResult[] BubbleSortAscendingSearchResults = SearchArray(BubbleSortAscending, Target);


            if (BubbleSortAscendingSearchResults[0].IndexLocations != null)
            {
                // If length of below array is 0 it means the target number is not in the sorted array and we must find the next nearest number
                if (BubbleSortAscendingSearchResults[0].IndexLocations.Length == 0)
                {
                    Console.WriteLine("Target number was not found, searching for the nearest number...");
                    // FIND NEAREST NUMBER FUNCTION return the nearest number and its locations
                }
                else
                {
                    // If the target is found print each index it was found at 
                    foreach (int Index in BubbleSortAscendingSearchResults[0].IndexLocations)
                    {
                        Console.WriteLine(Index);
                    }
                }
            }
        }

        // Function to get initial user pick of the first 3 data types 
        static int GetUserInitialArrayPick()
        {
            int UserArrayChoice;

            while (true)
            {
                Console.WriteLine("Pick a file to analyse 1: Net_1_256.txt   2: Net_2_256.txt   3: Net_3_256.txt");
                string input = Console.ReadLine();

                if (int.TryParse(input, out UserArrayChoice) && UserArrayChoice > 0 && UserArrayChoice <= 3)
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Input invalid pick a value between 1 and 3");
                }
            }

            return UserArrayChoice;
        }

        static int GetUserFurtherArrayPick()
        {
            int UserArrayChoice;

            while (true)
            {
                Console.WriteLine("Pick a file to analyse 1: Net_1_2048.txt   2: Net_2_2048.txt   3: Net_3_2048.txt");
                string input = Console.ReadLine();

                if (int.TryParse(input, out UserArrayChoice) && UserArrayChoice > 0 && UserArrayChoice <= 3)
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Input invalid pick a value between 1 and 3");
                }
            }

            return UserArrayChoice;
        }

        static int GetUserTargetNumberPick()
        {
            int UserTargetNumberPick;

            while (true)
            {
                Console.WriteLine("Pick a number between 0 and 100,000 to search for");
                string input = Console.ReadLine();

                if (int.TryParse(input, out UserTargetNumberPick) && UserTargetNumberPick >= 0 && UserTargetNumberPick <= 100000)
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid input pick a number between 0 and 100,000 to search for");
                }
            }

            return UserTargetNumberPick;
        }

        static SearchResult[] SearchArray(int[] ArrayIn, int Target)
        {
            SearchResult[] SearchResultArray = new SearchResult[2];

            SearchResultArray[0] = BinarySearch(ArrayIn, Target);
            SearchResultArray[1] = LinearSearch(ArrayIn, Target);

            return SearchResultArray;
        }

        static SearchResult BinarySearch(int[] ArrayIn, int target)
        {
            SearchResult BinarySearchResult = new SearchResult();

          
            return BinarySearchResult;
        }
        static SearchResult LinearSearch(int[] ArrayIn, int target)
        {
            SearchResult LinearSearchResult = new SearchResult();


            return LinearSearchResult;
        }


        static SortResult[] SortArrayAscending(int[] ArrayIn) 
        {
            SortResult[] SortArrayAscendingResultArray = new SortResult[3];

            SortArrayAscendingResultArray[0] = BubbleSort(ArrayIn, true);
            SortArrayAscendingResultArray[1] = InsertionSort(ArrayIn, true);
            SortArrayAscendingResultArray[2] = MergeSort(ArrayIn, true);


            return SortArrayAscendingResultArray;
        }

        static SortResult[] SortArrayDescending(int[] ArrayIn)
        {
            SortResult[] SortArrayDescendingResultArray = new SortResult[3];

            SortArrayDescendingResultArray[0] = BubbleSort(ArrayIn, false);
            SortArrayDescendingResultArray[1] = InsertionSort(ArrayIn, false);
            SortArrayDescendingResultArray[2] = MergeSort(ArrayIn, false);

            return SortArrayDescendingResultArray;

        }

        static SortResult BubbleSort(int[] ArrayIn, bool Ascending)
        {
            SortResult BubbleSortResult = new SortResult();

            BubbleSortResult.SortedArray = ArrayIn;

            if (Ascending)
            {
                BubbleSortResult.SortType = "BubbleSortResult Ascending";
            }
            else
            {
                BubbleSortResult.SortType = "BubbleSortResult Descending";
            }

            return BubbleSortResult;
        }

        static SortResult InsertionSort(int[] ArrayIn, bool Ascending)
        {
            SortResult InsertionSortResult = new SortResult();

            if (Ascending)
            {
                InsertionSortResult.SortType = "InsertionSort Ascending";
            }
            else
            {
                InsertionSortResult.SortType = "InsertionSort Descending";
            }

            return InsertionSortResult;
        }

        static SortResult MergeSort(int[] ArrayIn, bool Ascending)
        {
            SortResult MergeSortResult = new SortResult();

            if (Ascending)
            {
                MergeSortResult.SortType = "MergeSort Ascending";
            }
            else
            {
                MergeSortResult.SortType = "MergeSort Descending";
            }

            return MergeSortResult;
        }

        // Function that taks a text file string as input and returns a GetTextFileData data type
        static GetTextFileData GetNumberArrayForFile(string FileName) 
        {
            // Create a new instance of GetNumberArrayForFileData
            GetTextFileData Result = new GetTextFileData();

            // Get the correct file path by getting the environment current directory so the file can work on other systems and work spaces
            string CurrentDirectory = Path.Combine(Environment.CurrentDirectory, "..", "..", "Data");
            string FilePath = Path.Combine(CurrentDirectory, FileName);

            // Initialise our struct return types 
            int[] NumberArray = null;
            bool DataError = false;

            try
            {
                // Read all file lines in file path provided and assign them to a string array
                string[] lines = File.ReadAllLines(FilePath);

                // Set the length of our int array to the amount of lines in the file 
                NumberArray = new int[lines.Length];

                // For each string array item try parse to an int and add to our int array 
                for (int i = 0; i < lines.Length; i++)
                {
                    if (int.TryParse(lines[i], out int parsedNumber))
                    {
                        NumberArray[i] = parsedNumber;
                    }
                    else
                    {
                        // If parse fales set data error to true
                        DataError = true;
                    }
                }
            }
            catch (IOException e)
            {
                Console.WriteLine("ERROR - Couldnt read file:");
                Console.WriteLine(e.Message);
            }

            // Set our struct return types to the result values
            Result.NumberArray = NumberArray;
            Result.DataError = DataError;

            return Result;
        }

        static void LogSortResult(SortResult SortResultToLog)
        {
            Console.WriteLine("Sort type: " + SortResultToLog.SortType);
            Console.WriteLine("Steps taken: " + SortResultToLog.Steps);
            Console.WriteLine("Time taken: " + SortResultToLog.TimeSeconds);
            Console.WriteLine("-------------------------------------");
        }

        static void PrintIntArrayEvery10thValue(int[] IntArray) 
        {
            for (int i = 0; i < IntArray.Length; i++)
            {
                if ((i + 1) % 10 == 0)
                {
                    Console.Write(IntArray[i] + " ");
                }
            }
            Console.WriteLine();
        }

        static void PrintIntArrayEvery50thValue(int[] IntArray)
        {
            for (int i = 0; i < IntArray.Length; i++)
            {
                Console.Write(IntArray[i] + " ");

                if ((i + 1) % 50 == 0)
                {
                    Console.Write(IntArray[i] + " ");
                }
            }
            Console.WriteLine();
        }
    }

    // Struct to hold data for search result data type
    struct SearchResult
    {
        public string SearchType;
        public int Steps;
        public float TimeSeconds;
        public int[] IndexLocations;
    }

    // Struct to hold data for sort result data type
    struct SortResult
    {
        public int[] SortedArray;
        public string SortType;
        public int Steps;
        public float TimeSeconds;
    }

    // Struct to hold data for a text file data type
    struct GetTextFileData
    {
        public int[] NumberArray;
        public bool DataError;
    }
}
