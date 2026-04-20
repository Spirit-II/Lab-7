using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Lab_7_1
{
    public static class FileTasks
    {
        // Метод для проверки ввода целого числа
        private static int ReadIntWithValidation(string prompt)
        {
            int value;
            while (true)
            {
                Console.Write(prompt);
                if (int.TryParse(Console.ReadLine(), out value))
                    return value;
                Console.WriteLine("Ошибка: введите корректное целое число.");
            }
        }

        // Задание 1: Текстовые файлы (одно число в строке)
        public static void FillFileWithRandomIntsOnePerLine(string filePath, int count)
        {
            Random random = new Random();
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                for (int i = 0; i < count; i++)
                    writer.WriteLine(random.Next(-100, 101));
            }
        }

        public static int FindDifferenceMaxMin(string filePath)
        {
            int min = int.MaxValue, max = int.MinValue;
            using (StreamReader reader = new StreamReader(filePath))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    if (int.TryParse(line, out int num))
                    {
                        if (num < min) min = num;
                        if (num > max) max = num;
                    }
                }
            }
            return max - min;
        }

        // Задание 2: Текстовые файлы (несколько чисел в строке)
        public static void FillFileWithRandomIntsMultiplePerLine(string filePath, int lines, int numsPerLine)
        {
            Random random = new Random();
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                for (int i = 0; i < lines; i++)
                {
                    for (int j = 0; j < numsPerLine; j++)
                        writer.Write($"{random.Next(-100, 101)} ");
                    writer.WriteLine();
                }
            }
        }

        public static int FindMinElement(string filePath)
        {
            int min = int.MaxValue;
            using (StreamReader reader = new StreamReader(filePath))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] nums = line.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (string numStr in nums)
                    {
                        if (int.TryParse(numStr, out int num) && num < min)
                            min = num;
                    }
                }
            }
            return min;
        }

        // Задание 3: Текстовые файлы
        public static void FillTextFileWithRandomText(string filePath, int lines)
        {
            Random random = new Random();
            string[] words = { "apple", "banana", "cherry", "date", "elderberry" };
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                for (int i = 0; i < lines; i++)
                    writer.WriteLine(words[random.Next(words.Length)]);
            }
        }

        public static void RewriteFileStartingWithChar(string inputPath, string outputPath, char startChar)
        {
            using (StreamReader reader = new StreamReader(inputPath))
            using (StreamWriter writer = new StreamWriter(outputPath))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                    if (line.StartsWith(startChar.ToString()))
                        writer.WriteLine(line);
            }
        }

        // Задание 4: Бинарные файлы
        public static void FillBinaryFileWithRandomInts(string filePath, int count)
        {
            Random random = new Random();
            using (BinaryWriter writer = new BinaryWriter(File.Open(filePath, FileMode.Create)))
            {
                for (int i = 0; i < count; i++)
                    writer.Write(random.Next(-100, 101));
            }
        }

        public static void WriteBinaryIntsDivisibleByM(string inputPath, string outputPath, int m, int n)
        {
            using (BinaryReader reader = new BinaryReader(File.Open(inputPath, FileMode.Open)))
            using (BinaryWriter writer = new BinaryWriter(File.Open(outputPath, FileMode.Create)))
            {
                while (reader.BaseStream.Position < reader.BaseStream.Length)
                {
                    int num = reader.ReadInt32();
                    if (num % m == 0 && num % n != 0)
                        writer.Write(num);
                }
            }
        }

        // Задание 5: Бинарные файлы и структуры
        [Serializable]
        public struct LuggageItem
        {
            public string Name;
            public double Weight;
        }

        public static void FillLuggageFileWithRandomData(string filePath, int count)
        {
            Random random = new Random();
            LuggageItem[] items = new LuggageItem[count];
            for (int i = 0; i < count; i++)
            {
                items[i] = new LuggageItem
                {
                    Name = $"Item{i}",
                    Weight = random.NextDouble() * 20
                };
            }
            XmlSerializer serializer = new XmlSerializer(typeof(LuggageItem[]));
            using (FileStream stream = new FileStream(filePath, FileMode.Create))
            {
                serializer.Serialize(stream, items);
            }
        }

        public static bool CheckSingleLightLuggage(string filePath, double m)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(LuggageItem[]));
            using (FileStream stream = new FileStream(filePath, FileMode.Open))
            {
                LuggageItem[] items = (LuggageItem[])serializer.Deserialize(stream);
                foreach (var item in items)
                    if (item.Weight < m)
                        return true;
            }
            return false;
        }
    }

    public static class CollectionTasks
    {
        // Задание 6: List
        public static void MergeSortedLists(List<int> list1, List<int> list2)
        {
            int i = 0, j = 0;
            List<int> result = new List<int>();
            while (i < list1.Count && j < list2.Count)
            {
                if (list1[i] < list2[j])
                    result.Add(list1[i++]);
                else
                    result.Add(list2[j++]);
            }
            while (i < list1.Count) result.Add(list1[i++]);
            while (j < list2.Count) result.Add(list2[j++]);
            list1.Clear();
            list1.AddRange(result);
        }

        // Задание 7: LinkedList
        public static int CountEqualNeighbors(LinkedList<int> list)
        {
            int count = 0;
            if (list.Count < 2) return 0;
            var node = list.First.Next;
            while (node != null && node.Next != null)
            {
                if (node.Value == node.Previous.Value && node.Value == node.Next.Value)
                    count++;
                node = node.Next;
            }
            return count;
        }

        // Задание 8: HashSet
        public static void AnalyzeDishOrders(HashSet<string>[] orders)
        {
            HashSet<string> allDishes = new HashSet<string>();
            HashSet<string> someDishes = new HashSet<string>();
            HashSet<string> noneDishes = new HashSet<string>();

            foreach (var order in orders)
            {
                if (order.Count > 0)
                    someDishes.UnionWith(order);
                else
                    noneDishes.UnionWith(order);
            }
            allDishes = someDishes;
            foreach (var order in orders)
                allDishes.IntersectWith(order);

            Console.WriteLine("Заказывали все: " + string.Join(", ", allDishes));
            Console.WriteLine("Заказывали некоторые: " + string.Join(", ", someDishes.Except(allDishes)));
            Console.WriteLine("Не заказывал никто: " + string.Join(", ", noneDishes));
        }

        // Задание 9: HashSet
        public static void PrintAlphabeticalConsonants(string filePath)
        {
            HashSet<char> consonants = new HashSet<char> { 'б', 'в', 'г', 'д', 'ж', 'з', 'й', 'к', 'л', 'м', 'н', 'п', 'р', 'с', 'т', 'ф', 'х', 'ц', 'ч', 'ш', 'щ' };
            Dictionary<char, int> consonantCounts = new Dictionary<char, int>();
            foreach (var c in consonants) consonantCounts[c] = 0;

            using (StreamReader reader = new StreamReader(filePath))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    foreach (char c in line.ToLower())
                    {
                        if (consonants.Contains(c))
                            consonantCounts[c]++;
                    }
                }
            }
            foreach (var pair in consonantCounts.Where(p => p.Value == 1).OrderBy(p => p.Key))
                Console.Write(pair.Key + " ");
        }

        // Задание 10: Dictionary/SortedList
        public static void FindTopParticipants(string filePath, int topN)
        {
            Dictionary<string, int> participants = new Dictionary<string, int>();
            using (StreamReader reader = new StreamReader(filePath))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] parts = line.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    string name = parts[0] + " " + parts[1];
                    int total = 0;
                    for (int i = 2; i < parts.Length; i++)
                        total += int.Parse(parts[i]);
                    participants[name] = total;
                }
            }
            var topParticipants = participants.OrderByDescending(p => p.Value).Take(topN);
            foreach (var p in topParticipants)
                Console.WriteLine($"{p.Key}: {p.Value}");
        }
    }
}
