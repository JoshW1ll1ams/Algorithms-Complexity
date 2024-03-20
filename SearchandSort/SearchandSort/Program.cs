using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Reflection;
using System.Diagnostics;
using System.Collections;

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

            // Initial 3 array search and sort pick
            // Get userinput for the pick of the first 3 arrays, minus 1 to allow for array index starting at 0 
            int ArrayToSort = GetUserInitialArrayPick() - 1;

            // Call our MainSearchAndSortLoop with value true to print 10th values
            MainSearchAndSortLoop(FileDataArray[ArrayToSort].NumberArray, true);

            // Further 3 array search and sort pick
            // Get userinput for the pick of the further 3 arrays, add two to position the user pick correctly within the array index
            int FurtherArrayToSort = GetUserFurtherArrayPick() + 2;
            // Call our MainSearchAndSortLoop with value false to print 50th values
            MainSearchAndSortLoop(FileDataArray[FurtherArrayToSort].NumberArray, false);


            // Merged Net_1_256 and Net_3_256 search and sort
            Console.WriteLine("Now we will check the results of merged array for files Net_1_256 and Net_3_256, press enter to continue...");
            Console.ReadLine();
            // Merge the below arrays then proform the MainSearchAndSortLoop on it printing every 50th value
            int[] Merged_Net_1_256_Net_3_256 = FileDataArray[0].NumberArray.Concat(FileDataArray[1].NumberArray).ToArray();
            MainSearchAndSortLoop(Merged_Net_1_256_Net_3_256, false);

            // Merged Net_1_2048, Net_2_2048 and Net_3_2048 search and sort
            Console.WriteLine("Now we will check the sorted array for files Net_1_2048, Net_2_2048 and Net_3_2048, press enter to continue...");
            Console.ReadLine();

            // Merge the below arrays then proform the MainSearchAndSortLoop on it printing every 50th value
            int[] Merged_2048LengthFiles = FileDataArray[3].NumberArray.Concat(FileDataArray[4].NumberArray).Concat(FileDataArray[5].NumberArray).ToArray();
            MainSearchAndSortLoop(Merged_2048LengthFiles, false);

            Console.ReadLine();
        }

        static void MainSearchAndSortLoop(int[] ArrayToSort, bool Search10th)
        {
            SortResult QuickSortAscending = QuickSort(ArrayToSort, true);
            LogSortResult(QuickSortAscending);
            Console.WriteLine("Press enter to see every 10th value of the QuickSortAscending array");
            Console.ReadLine();

            if (Search10th)
            {
                PrintIntArrayEvery10thValue(QuickSortAscending.SortedArray);
            }
            else
            {
                PrintIntArrayEvery50thValue(QuickSortAscending.SortedArray);
            }

            SortResult BubbleSortAscending = BubbleSort(ArrayToSort, true);
            LogSortResult(BubbleSortAscending);
            Console.WriteLine("Press enter to see every 10th value of the BubbleSortAscending array");
            Console.ReadLine();

            if (Search10th)
            {
                PrintIntArrayEvery10thValue(BubbleSortAscending.SortedArray);
            }
            else
            {
                PrintIntArrayEvery50thValue(BubbleSortAscending.SortedArray);
            }

            SortResult InsertionSortAscending = InsertionSort(ArrayToSort, true);
            LogSortResult(InsertionSortAscending);
            Console.WriteLine("Press enter to see every 10th value of the InsertionSortAscending array");
            Console.ReadLine();

            if (Search10th)
            {
                PrintIntArrayEvery10thValue(InsertionSortAscending.SortedArray);
            }
            else
            {
                PrintIntArrayEvery50thValue(InsertionSortAscending.SortedArray);
            }

            SortResult MergeSortAscending = MergeSort(ArrayToSort, true);
            LogSortResult(MergeSortAscending);
            Console.WriteLine("Press enter to see every 10th value of the MergeSortAscending array");
            Console.ReadLine();

            if (Search10th)
            {
                PrintIntArrayEvery10thValue(MergeSortAscending.SortedArray);
            }
            else
            {
                PrintIntArrayEvery50thValue(MergeSortAscending.SortedArray);
            }

            SortResult BubbleSortDescending = BubbleSort(ArrayToSort, false);
            LogSortResult(BubbleSortDescending);
            Console.WriteLine("Press enter to see every 10th value of the BubbleSortDescending array");
            Console.ReadLine();

            if (Search10th)
            {
                PrintIntArrayEvery10thValue(BubbleSortDescending.SortedArray);
            }
            else
            {
                PrintIntArrayEvery50thValue(BubbleSortDescending.SortedArray);
            }

            SortResult InsertionSortDescending = InsertionSort(ArrayToSort, false);
            LogSortResult(InsertionSortDescending);
            Console.WriteLine("Press enter to see every 10th value of the InsertionSortDescending array");
            Console.ReadLine();

            if (Search10th)
            {
                PrintIntArrayEvery10thValue(InsertionSortDescending.SortedArray);
            }
            else
            {
                PrintIntArrayEvery50thValue(InsertionSortDescending.SortedArray);
            }

            SortResult MergeSortDescending = MergeSort(ArrayToSort, false);
            LogSortResult(MergeSortDescending);
            Console.WriteLine("Press enter to see every 10th value of the MergeSortDescending array");
            Console.ReadLine();

            if (Search10th)
            {
                PrintIntArrayEvery10thValue(MergeSortDescending.SortedArray);
            }
            else
            {
                PrintIntArrayEvery50thValue(MergeSortDescending.SortedArray);
            }

            int Target = GetUserTargetNumberPick();

            SearchResult BinarySearchResult = BinarySearch(BubbleSortAscending.SortedArray, Target);
            SearchResult LinearSearchSearchResult = LinearSearch(BubbleSortAscending.SortedArray, Target);

            Console.WriteLine("To see BinarySearchResult press enter..");
            Console.ReadLine();

            if (!BinarySearchResult.Found)
            {
                Console.WriteLine("Target was not found, searching for nearest value..");
            }
            LogSearchResult(BinarySearchResult);


            Console.WriteLine("To see LinearSearchResult press enter..");
            Console.ReadLine();
            if (!LinearSearchSearchResult.Found)
            {
                Console.WriteLine("Target was not found, searching for nearest value..");
            }
            LogSearchResult(LinearSearchSearchResult);
        }


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
                Console.WriteLine("Pick a number between 0 and 100,000 to search the target array for");
                string input = Console.ReadLine();

                if (int.TryParse(input, out UserTargetNumberPick) && UserTargetNumberPick >= 0 && UserTargetNumberPick <= 100000)
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid input pick a number between 0 and 100,000 to search the target array for");
                }
            }

            return UserTargetNumberPick;
        }

        static SearchResult BinarySearch(int[] ArrayIn, int Target)
        {
            SearchResult BinarySearchResult = new SearchResult();

            BinarySearchResult.SearchType = "Binary Search";
            BinarySearchResult.Found = false;

            int Steps = 0;
            int Low = 0;
            int High = ArrayIn.Length - 1;
            int closestNumber = 0;

            while (Low <= High)
            {
                Steps++;
                int Middle = (Low + High) / 2;

                if (ArrayIn[Middle] == Target)
                {
                    BinarySearchResult.IndexLocation = Middle;
                    BinarySearchResult.Found = true;
                    BinarySearchResult.Steps = Steps;
                    return BinarySearchResult;
                }
                else if (ArrayIn[Middle] < Target)
                {
                    Low = Middle + 1;
                }
                else
                {
                    High = Middle - 1;
                }
                closestNumber = LocalClosestNumber(Low, High, ArrayIn, Target);
            }

            if (!BinarySearchResult.Found)
            {
                BinarySearchResult = BinarySearch(ArrayIn, closestNumber);
                BinarySearchResult.ClosestNumber = closestNumber;
                BinarySearchResult.Found = false;
            }

            BinarySearchResult.Steps = Steps;

            return BinarySearchResult;

            int LocalClosestNumber(int LowIndex, int HighIndex, int[] ArrayIn_, int targetValue)
            {
                int Closest = 0;

                if (LowIndex == 0)
                {
                    Closest = ArrayIn_[LowIndex];
                }
                else if (HighIndex == ArrayIn_.Length - 1)
                {
                    Closest = ArrayIn_[HighIndex];
                }
                else
                {
                    int OldClosest = Math.Abs(targetValue - Closest);
                    int NewClosestLow = Math.Abs(targetValue - ArrayIn_[LowIndex]);
                    int NewClosestHigh = Math.Abs(targetValue - ArrayIn_[HighIndex]);

                    if (NewClosestLow < OldClosest)
                    {
                        Closest = ArrayIn_[LowIndex];
                    }
                    else if (NewClosestHigh < OldClosest)
                    {
                        Closest = ArrayIn_[HighIndex];
                    }
                }
                return Closest;
            }
        }


        static SearchResult LinearSearch(int[] ArrayIn, int Target)
        {
            SearchResult LinearSearchResult = new SearchResult();

            LinearSearchResult.SearchType = "Linear Search";
            LinearSearchResult.Found = false;

            int Steps = 0;

            int ClosestNumber = 0;
            int StartOldClosest = int.MaxValue;

 
            for (int i = 0; i < ArrayIn.Length; i++)
            {
                Steps++;

                if (ArrayIn[i] == Target)
                {
                    LinearSearchResult.IndexLocation = i;
                    LinearSearchResult.Found = true;
                    LinearSearchResult.Steps = Steps;
                    return LinearSearchResult;
                }

                if(i == 0)
                {
                    ClosestNumber = LocalClosestNumber(StartOldClosest, ArrayIn[i]);
                }
                else
                {
                    ClosestNumber = LocalClosestNumber(ClosestNumber, ArrayIn[i]);
                }
        
            }

            if(!LinearSearchResult.Found)
            {
                LinearSearchResult = LinearSearch(ArrayIn, ClosestNumber);
                LinearSearchResult.ClosestNumber = ClosestNumber;
                LinearSearchResult.Found = false;
            }

            LinearSearchResult.Steps = Steps;

            return LinearSearchResult;


            int LocalClosestNumber(int OldClosest, int NewClosest)
            {
                double DifferenceOldClosest = Math.Abs(Target - OldClosest);
                double DifferenceNewClosest = Math.Abs(Target - NewClosest);

                if (DifferenceOldClosest < DifferenceNewClosest)
                {
                    return OldClosest;
                }
                else if (DifferenceNewClosest < DifferenceOldClosest)
                {
                    return NewClosest;
                }

                return OldClosest;
            }
        }

        static SortResult QuickSort(int[] ArrayIn, bool Ascending)
        {

            SortResult QuickSortResult = new SortResult();

            QuickSortResult.SortType = "QuickSort Ascending";


            int[] ArrayToSort = new int[ArrayIn.Length];
            Array.Copy(ArrayIn, ArrayToSort, ArrayIn.Length);


            int Steps = 0;
            int ArrayLength = ArrayToSort.Length;

            LocalQuickSort(0, ArrayLength - 1);

            void LocalQuickSort(int LeftIndex, int RightIndex)
            {
                if (LeftIndex < RightIndex)
                {
                    int pivot = LocalPartition(ArrayToSort, LeftIndex, RightIndex);
                    Steps++;

                    if (pivot > 1)
                    {
                        LocalQuickSort(LeftIndex, pivot - 1);
                        Steps++;
                    }
                    if (pivot + 1 < RightIndex)
                    {
                        LocalQuickSort(pivot + 1, RightIndex);
                        Steps++;
                    }
                }
            }

            int LocalPartition(int[] ArrayToSort_, int LeftIndex_, int RightIndex_)
            {
                int Pivot = ArrayToSort_[LeftIndex_];
                Steps++;

                while (true)
                {
                    while (ArrayToSort_[LeftIndex_] < Pivot)
                    {
                        LeftIndex_++;
                        Steps++;
                    }

                    while (ArrayToSort_[RightIndex_] > Pivot)
                    {
                        RightIndex_--;
                        Steps++;
                    }

                    if (LeftIndex_ < RightIndex_)
                    {
                        if (ArrayToSort_[LeftIndex_] == ArrayToSort_[RightIndex_])
                        {
                            Steps++;
                            return RightIndex_;
                        }

                        int temp = ArrayToSort_[LeftIndex_];
                        ArrayToSort_[LeftIndex_] = ArrayToSort_[RightIndex_];
                        ArrayToSort_[RightIndex_] = temp;

                        Steps = Steps + 3;
                    }
                    else
                    {
                        return RightIndex_;
                    }
                }
            }
            QuickSortResult.SortedArray = ArrayToSort;
            QuickSortResult.Steps = Steps;
        
            return QuickSortResult;
        }

        static SortResult BubbleSort(int[] ArrayIn, bool Ascending)
        {
            SortResult BubbleSortResult = new SortResult();


            int[] ArrayToSort = new int[ArrayIn.Length];
            Array.Copy(ArrayIn, ArrayToSort, ArrayIn.Length);


            int Steps = 0;
            int TempValue;
            int ArrayLength = ArrayToSort.Length;


            if (Ascending)
            {
                BubbleSortResult.SortType = "BubbleSortResult Ascending";

                for (int i = 0; i < ArrayLength; i++)
                {
                    for (int n = 0; n < ArrayLength - 1; n++)
                    {
                        if (ArrayToSort[n] > ArrayToSort[n + 1])
                        {
                            TempValue = ArrayToSort[n + 1];
                            ArrayToSort[n + 1] = ArrayToSort[n];
                            ArrayToSort[n] = TempValue;

                            Steps = Steps + 3;
                        }
                    }
                }
                BubbleSortResult.SortedArray = ArrayToSort;
                BubbleSortResult.Steps = Steps;
            }
            else
            {
                BubbleSortResult.SortType = "BubbleSortResult Decending";

                for (int i = 0; i < ArrayLength; i++)
                {
                    for (int n = 0; n < ArrayLength - 1; n++)
                    {
                        if (ArrayToSort[n] > ArrayToSort[n + 1])
                        {
                            TempValue = ArrayToSort[n + 1];
                            ArrayToSort[n + 1] = ArrayToSort[n];
                            ArrayToSort[n] = TempValue;

                            Steps = Steps + 3;
                        }
                    }
                }
                BubbleSortResult.SortedArray = ArrayToSort;
                BubbleSortResult.Steps = Steps;
            }
  

            return BubbleSortResult;
        }

        static SortResult InsertionSort(int[] ArrayIn, bool Ascending)
        {
            SortResult InsertionSortResult = new SortResult();

            int[] ArrayToSort = new int[ArrayIn.Length];
            Array.Copy(ArrayIn, ArrayToSort, ArrayIn.Length);


            int Steps = 0;
            int ArrayLength = ArrayToSort.Length;

            if (Ascending)
            {
                InsertionSortResult.SortType = "InsertionSort Ascending";

                for (int i = 1; i < ArrayLength; ++i)
                {
                    int Key = ArrayToSort[i];
                    int n = i - 1;
                    Steps = Steps + 2;

                    while (n >= 0 && ArrayToSort[n] > Key)
                    {
                        ArrayToSort[n + 1] = ArrayToSort[n];
                        n = n - 1;
                        Steps = Steps + 2;
                    }
                    ArrayToSort[n + 1] = Key;
                    Steps++;
                }

                InsertionSortResult.SortedArray = ArrayToSort;
                InsertionSortResult.Steps = Steps;
            }
            else
            {
                InsertionSortResult.SortType = "InsertionSort Descending";

                for (int i = 1; i < ArrayLength; ++i)
                {
                    int Key = ArrayToSort[i];
                    int n = i - 1;
                    Steps = Steps + 2;

                    while (n >= 0 && ArrayToSort[n] < Key)
                    {
                        ArrayToSort[n + 1] = ArrayToSort[n];
                        n = n - 1;
                        Steps = Steps + 2;
                    }
                    ArrayToSort[n + 1] = Key;
                    Steps++;
                }


                InsertionSortResult.SortedArray = ArrayToSort;
                InsertionSortResult.Steps = Steps;
            }


       
            return InsertionSortResult;
        }

        static SortResult MergeSort(int[] ArrayIn, bool Ascending)
        {
            SortResult MergeSortResult = new SortResult();

            int[] ArrayToSort = new int[ArrayIn.Length];
            Array.Copy(ArrayIn, ArrayToSort, ArrayIn.Length);

            int Steps = 0;

            if (Ascending)
            {
                MergeSortResult.SortType = "MergeSort Ascending";
            }
            else
            {
                MergeSortResult.SortType = "MergeSort Descending";
            }


            MergeSortLocal(ArrayToSort, 0, ArrayIn.Length - 1);


            MergeSortResult.SortedArray = ArrayToSort;

            void MergeSortLocal(int[] arr, int left, int right)
            {
                if (left < right)
                {
                    int mid = (left + right) / 2;
                    Steps++;

                    MergeSortLocal(arr, left, mid);
                    MergeSortLocal(arr, mid + 1, right);
                    MergeLocal(arr, left, mid, right);
                }
            }

            void MergeLocal(int[] SubArrayIn, int LeftIndex, int MidIndex, int RightIndex)
            {
                int LeftArraySize = MidIndex - LeftIndex + 1;
                int RightArraySize = RightIndex - MidIndex;

                int[] LeftArray = new int[LeftArraySize];
                int[] RightArray = new int[RightArraySize];

                Array.Copy(SubArrayIn, LeftIndex, LeftArray, 0, LeftArraySize);
                Array.Copy(SubArrayIn, MidIndex + 1, RightArray, 0, RightArraySize);

                int i = 0;
                int n = 0;
                int k = LeftIndex;

                Steps = Steps + 9;

                while (i < LeftArraySize && n < RightArraySize)
                {
                    if(Ascending)
                    {
                        if (LeftArray[i] <= RightArray[n])
                        {
                            SubArrayIn[k] = LeftArray[i];
                            i++;
                            Steps = Steps + 2;
                        }
                        else
                        {
                            SubArrayIn[k] = RightArray[n];
                            n++;
                            Steps = Steps + 2;
                        }
                        k++;
                        Steps++;
                    }
                    else
                    {
                        if (LeftArray[i] >= RightArray[n])
                        {
                            SubArrayIn[k] = LeftArray[i];
                            i++;
                            Steps = Steps + 2;
                        }
                        else
                        {
                            SubArrayIn[k] = RightArray[n];
                            n++;
                            Steps = Steps + 2;
                        }
                        k++;
                        Steps++;
                    }
                }

                while (i < LeftArraySize)
                {
                    SubArrayIn[k] = LeftArray[i];
                    i++;
                    k++;
                    Steps = Steps + 3;
                }

                while (n < RightArraySize)
                {
                    SubArrayIn[k] = RightArray[n];
                    n++;
                    k++;
                    Steps = Steps + 3;
                }
            }

            MergeSortResult.Steps = Steps;

            return MergeSortResult;
        }

 
        static GetTextFileData GetNumberArrayForFile(string FileName) 
        {
     
            GetTextFileData Result = new GetTextFileData();

            string CurrentDirectory = Path.Combine(Environment.CurrentDirectory, "..", "..", "Data");
            string FilePath = Path.Combine(CurrentDirectory, FileName);

       
            int[] NumberArray = null;
            bool DataError = false;

            try
            {
                string[] lines = File.ReadAllLines(FilePath);

                NumberArray = new int[lines.Length];

                for (int i = 0; i < lines.Length; i++)
                {
                    if (int.TryParse(lines[i], out int parsedNumber))
                    {
                        NumberArray[i] = parsedNumber;
                    }
                    else
                    {
                        DataError = true;
                    }
                }
            }
            catch (IOException e)
            {
                Console.WriteLine("ERROR - Couldnt read file:");
                Console.WriteLine(e.Message);
            }

            Result.NumberArray = NumberArray;
            Result.DataError = DataError;

            return Result;
        }

        static void LogSortResult(SortResult SortResultToLog)
        {
            Console.WriteLine("-------------------------------------");
            Console.WriteLine("Sort type: " + SortResultToLog.SortType);
            Console.WriteLine("Steps taken: " + SortResultToLog.Steps);
            Console.WriteLine("-------------------------------------");
        }

        static void LogSearchResult(SearchResult SearchResultToLog)
        {

            Console.WriteLine("-------------------------------------");
            Console.WriteLine("Search type: " + SearchResultToLog.SearchType);
            Console.WriteLine("Steps taken: " + SearchResultToLog.Steps);

            if(SearchResultToLog.Found)
            {
                Console.WriteLine("Search value was found at index:");
            }
            else
            {
                Console.WriteLine("Search value was not found, its closest value {0} was found at index:", SearchResultToLog.ClosestNumber);
            }

            Console.Write(SearchResultToLog.IndexLocation);

            Console.WriteLine("\n-------------------------------------");
        }
        static void PrintIntArrayEvery10thValue(int[] IntArray) 
        {
            if(IntArray == null)
            {
                return;
            }
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
            if (IntArray == null)
            {
                return;
            }

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
        public bool Found;
        public int IndexLocation;
        public int ClosestNumber;
    }

    // Struct to hold data for sort result data type
    struct SortResult
    {
        public int[] SortedArray;
        public string SortType;
        public int Steps;
    }

    // Struct to hold data for a text file data type
    struct GetTextFileData
    {
        public int[] NumberArray;
        public bool DataError;
    }
}
