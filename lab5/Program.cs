using System;

namespace lab5
{
    class Program
    {
        static void Main(string[] args)
        {
            string command;
            do
            {
                Console.Write("Натисніть 1, щоб виконати контрольний приклад, або 2 - для вводу слів з клавіатури: ");
                command = Console.ReadLine();
                // контрольний приклад
                if (command == "1")
                {
                    string[] array = new string[]{"she", "sells", "seashells", "by", "the", "sea", "shore", "the",
                        "shells", "she", "sells", "are", "surely", "seashells"};
                    Console.WriteLine();
                    string[] unsortedArray = new string[array.Length];
                    Array.Copy(array, unsortedArray, array.Length);

                    MSDSort(array);
                    string[] sortedArray = GetSortedPartlyReversedArray(array);

                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("UNSORTED:");
                    Console.ResetColor();
                    PrintUnsortedArray(unsortedArray, sortedArray);
                    Console.WriteLine();

                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.WriteLine("SORTED:");
                    Console.ResetColor();
                    PrintSortedArray(unsortedArray, sortedArray);
                    Console.WriteLine();
                    Console.WriteLine("Кольором виділено слова, що сортуються.");
                }
                // ввід слів з клавіатури
                else if (command == "2")
                {
                    int n;
                    bool input;
                    do
                    {
                        Console.Write("Введіть кількість слів, які хочете відсортувати: ");
                        input = int.TryParse(Console.ReadLine(), out n);
                        if (!input)
                        {
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                            Console.WriteLine("Кількість слів має бути цілим числом! Спробуйте ще раз!");
                            Console.ResetColor();
                        }
                    }
                    while (!input);
                    string[] arrayOfStrigs = new string[n];
                    for (int i = 0; i < arrayOfStrigs.Length; i++)
                    {
                        string word;
                        do
                        {
                            Console.Write($"Введіть слово #{i+1}: ");
                            word = Console.ReadLine();
                            if (!IsCorrectInput(word))
                            {
                                Console.ForegroundColor = ConsoleColor.DarkRed;
                                Console.WriteLine("Слово не має містити цифри, знаки пунктуації та пробіли! Спробуйте ще раз!");
                                Console.ResetColor();
                            }
                            else if (IsRepeatingWord(word, arrayOfStrigs))
                            {
                                Console.ForegroundColor = ConsoleColor.DarkRed;
                                Console.WriteLine("Ви вже ввели це слово раніше! Слова не мають повторюватися!");
                                Console.ResetColor();
                            }
                        }
                        while (!IsCorrectInput(word) || IsRepeatingWord(word, arrayOfStrigs));
                        arrayOfStrigs[i] = word;
                    }
                    Console.WriteLine();
                    string[] unsortedArray = new string[arrayOfStrigs.Length];
                    Array.Copy(arrayOfStrigs, unsortedArray, arrayOfStrigs.Length);
                    
                    MSDSort(arrayOfStrigs);
                    string[] sortedArray = GetSortedPartlyReversedArray(arrayOfStrigs);

                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("UNSORTED:");
                    Console.ResetColor();
                    PrintUnsortedArray(unsortedArray, sortedArray);
                    Console.WriteLine();

                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.WriteLine("SORTED:");
                    Console.ResetColor();
                    PrintSortedArray(unsortedArray, sortedArray);
                    Console.WriteLine();
                    Console.WriteLine("Кольором виділено слова, що сортуються.");
                }
                if (command != "1" && command != "2")
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("Помилка! Такої команди не існує! Спробуйте ще раз!");
                    Console.ResetColor();
                }
            }
            while(command != "1" && command != "2");
        }

        static void MSDSort(string[] array)
        {
            int lo = 0;
            int hi = array.Length - 1;
            int radix = 0;
            do
            {
                bool diffBoundaries = true;
                hi = array.Length - 1;
                while (diffBoundaries)
                {
                    for (int i = lo + 1; i <= hi; i++)
                    {
                        string s = array[i];
                        char[] curWord = array[i].ToCharArray();
                        int m = i;
                        if (radix >= curWord.Length)
                        {
                            while (m > lo)
                            {
                                array[m] = array[m - 1];
                                m--;
                            }
                            array[m] = s;
                            lo++;
                            continue;
                        }

                        int j = i;
                        char[] prevWord = array[j - 1].ToCharArray();
                        if (radix >= prevWord.Length)
                        {
                            while (j > lo)
                            {
                                array[j] = array[j - 1];
                                j--;
                            }
                            array[j+1] = s;
                            lo++;
                            continue;
                        }

                        while (j > lo && curWord[radix] < array[j - 1].ToCharArray()[radix])
                        {
                            array[j] = array[j - 1];
                            j--;
                        }
                        array[j] = s;
                    }

                    diffBoundaries = false;
                    int loStart = 0;
                    // встановлюємо межі для сортування підмасиву
                    int k = lo;
                    do
                    {
                        if ((k + 1) > hi)
                        {
                            break;
                        }
                        if (array[k].ToCharArray()[radix] == array[k+1].ToCharArray()[radix])
                        {
                            if (loStart == 0)
                            {
                                lo = k;
                                loStart++;
                            }
                            diffBoundaries = true;
                            for (int j = k; j <= hi; j++)
                            {
                                if (array[k].ToCharArray()[radix] != array[j].ToCharArray()[radix])
                                {
                                    hi = j-1;
                                    break;
                                }
                            }
                        }
                        k++;
                    }
                    while (k < hi);
                    radix++;
                }
                lo = hi + 1;
                radix = 0;
            }
            while (hi != array.Length-1);
        }

        static bool IsCorrectInput(string str)
        {
            if (str == "")
            {
                return false;
            }
            foreach (char item in str)
            {
                if (char.IsNumber(item) || char.IsPunctuation(item) || char.IsSeparator(item))
                {
                    return false;
                }
            }
            return true;
        }

        static bool IsRepeatingWord(string str, string[] arr)
        {
            foreach (string item in arr)
            {
                if (str == item)
                {
                    return true;
                }
            }
            return false;
        }

        static void PrintArray(string[] arr)
        {
            foreach (string element in arr)
            {
                Console.WriteLine(element);
            }
        }

        static string[] GetSortedPartlyReversedArray(string[] arr)
        {
            int middleOfAlphabet = ('z' - 'a') / 2 + 'a';
            int size = 0;
            for (int i = 0; i < arr.Length; i++)
            {
                char[] word = arr[i].ToCharArray();
                if (word[0] > middleOfAlphabet)
                {
                    size++;
                }
            }

            string[] invertedArr = new string[size];
            int j = 0;
            for (int i = 0; i < size; i++)
            {
                char[] word = arr[j].ToCharArray();
                if (word[0] > middleOfAlphabet)
                {
                    invertedArr[i] = arr[j];
                    j++;
                    continue;
                }
                i--;
                j++;
            }
            Array.Reverse(invertedArr);
            string[] sortedArr = MergeArrays(arr, invertedArr);

            return sortedArr;
        }

        static void PrintUnsortedArray(string[] unsortedArray, string[] sortedArray)
        {
            for (int i = 0; i < unsortedArray.Length; i++)
            {
                if (unsortedArray[i] == sortedArray[i])
                {
                    Console.WriteLine(unsortedArray[i]);
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.DarkBlue;
                    Console.WriteLine(unsortedArray[i]);
                    Console.ResetColor();
                }
            }
        }

        static void PrintSortedArray(string[] unsortedArray, string[] sortedArray)
        {
            for (int i = 0; i < sortedArray.Length; i++)
            {
                if (unsortedArray[i] == sortedArray[i])
                {
                    Console.WriteLine(sortedArray[i]);
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.DarkBlue;
                    Console.WriteLine(sortedArray[i]);
                    Console.ResetColor();
                }
            }
        }

        static string[] MergeArrays(string[] arr1, string[] arr2)
        {
            int middleOfAlphabet = ('z' - 'a') / 2 + 'a';
            int j = 0;
            for (int i = 0; i < arr1.Length; i++)
            {
                if (arr1[i].ToCharArray()[0] < middleOfAlphabet)
                {
                    continue;
                }
                arr1[i] = arr2[j];
                j++;
            }
            return arr1;
        }
    }
}
