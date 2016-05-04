using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmTask2
{
    class Program
    {
        static Dictionary<int, List<int>> data = new Dictionary<int, List<int>>();
        static int u = 0;
        static int m = 0;
        static void ReadDataFromFile(string filename)
        {
            StreamReader sr = new StreamReader(filename);
            int counter = 0;
           
            while (!sr.EndOfStream)
            {
                string line = sr.ReadLine();
                if (counter == 0)
                {
                    string[] lineChars = line.Split(' ');
                    u = Convert.ToInt32(lineChars[0]);
                   m = Convert.ToInt32(lineChars[1]);

                }
                else
                {
                    string[] lineChars = line.Split(' ');
                    List<int> myrates = new List<int>();
                    for(int i = 1; i < m + 1; i++) {
                        myrates.Add(Convert.ToInt32(lineChars[i]));
                    }
                    data.Add(Convert.ToInt32(lineChars[0]), myrates);
                }
                counter++;
            }
        }
        static void Main(string[] args)
        {
            ReadDataFromFile("Input.txt");
           Console.WriteLine( CountInversions(951, 178));
        }
        static int Inversions(int[] arr, int start, int end)
        {
            if (start < end)
            {
                int countInv = 0;
                countInv += Inversions(arr, start,(start+ end )/ 2); // Left Half of an array
                countInv += Inversions(arr, (start + end) / 2 + 1, end); //RightHAlf Of an array
                KeyValuePair<int, int[]> res = SortAndSplitInv(arr, start, (start + end) / 2, end); // Count inversions between two halfs
                countInv += res.Key;
                arr = res.Value;
                return countInv;
            }
            else
                return 0;
        } 
        static KeyValuePair<int,int[]> SortAndSplitInv(int[] arr, int s,int avarage, int e)
        {
            int countInv = 0;
            int[] L = new int[avarage + 1 - s + 1]; //Generate left-half array
            int[] R = new int[e - avarage + 1]; // Generate right-half array
            for (int i = 0; i < avarage - s + 1 ; i++)
            {
                L[i] = arr[s + i];
            }
            L[avarage + 1 - s] = Int32.MaxValue;
            for (int i = avarage + 1, k = 0; i < e  + 1; i++, k++)
            {
                R[k] = arr[i];
            }
            R[e - avarage] = Int32.MaxValue;
            int ii = 0;
            int j = 0;
            for(int k = s; k <= e; k++)
            {

                if (L[ii] < R[j]) //sort the array
                {
                    arr[k] = L[ii];
                    ii++;
                }
                else
                {
                    arr[k] = R[j];
                    j++;
                    countInv += L.Length - ii - 1; //When adding an element from right half there are inversions between that equals this expression
                }
               
            }
            KeyValuePair<int, int[]> res = new KeyValuePair<int, int[]>(countInv,arr);
            
            //res.Value = arr;
            return res;
        }
        static int CountInversions(int index1, int index2)
        {
            int[] inversionsArr = new int[m];
            List<int> firstPerson = data[index1];
            List<int> secondPerson = data[index2];
            for(int i =0; i < m ; i++)
            {
                inversionsArr[i] = secondPerson[firstPerson.FindIndex(x => x == i + 1)];
            }
            


            return Inversions(inversionsArr,0, inversionsArr.Length - 1);
        }
    }
}
