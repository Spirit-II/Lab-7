using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Lab_7_1
{
    [Serializable]
    public struct LuggageItem
    {
        public string Name;
        public double Weight;
    }

    /// <summary>
    /// Информация о пассажире.
    /// </summary>
    [Serializable]
    public class Passenger
    {
        public string FullName;

        public LuggageItem[] Luggage;
    }
    internal class FileTasks
    {
        /// Задание 1: Текстовые файлы (одно число в строке)
        public static void CreateNum(string filePath, int count)
        {
            Random random = new Random();
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                for (int i = 0; i < count; i++)
                    writer.WriteLine(random.Next(-100, 101));
            }
        }

        public static int FindMaxMin(string filePath)
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

        /// Задание 2: Текстовые файлы (несколько чисел в строке)
        public static void CreateNums(string filePath, int lines, int numsPerLine)
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

        public static int FindMin(string filePath)
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

        /// Задание 3: Текстовые файлы
        public static void CreateText(string filePath, int lines)
        {
            Random random = new Random();
            string[] words = { "apple", "banana", "Gru", "date", "kodstyle" };
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                for (int i = 0; i < lines; i++)
                    writer.WriteLine(words[random.Next(words.Length)]);
            }
        }

        public static void WriteChar(string inputPath, string outputPath, char startChar)
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

        /// Задание 4: Бинарные файлы
        public static void CreateBinary(string filePath, int count)
        {
            Random random = new Random();
            using (BinaryWriter writer = new BinaryWriter(File.Open(filePath, FileMode.Create)))
            {
                for (int i = 0; i < count; i++)
                    writer.Write(random.Next(-100, 101));
            }
        }

        public static void WriteBinary(string inputPath, string outputPath, int m, int n)
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

        /// Задание 5: Бинарные файлы и структуры

        /// <summary>
        /// Создает XML-файл с информацией о пассажирах.
        /// </summary>
        public static void CreateBag(string filePath, int count)
        {
            Random random = new Random();

            Passenger[] passengers = new Passenger[count];

            for (int i = 0; i < count; i++)
            {
                int luggageCount = random.Next(1, 4);

                passengers[i] = new Passenger();

                passengers[i].FullName = "Passenger" + i;

                passengers[i].Luggage =
                    new LuggageItem[luggageCount];

                for (int j = 0; j < luggageCount; j++)
                {
                    passengers[i].Luggage[j].Name =
                        "Item" + j;

                    passengers[i].Luggage[j].Weight =
                        random.Next(1, 30);
                }
            }

            XmlSerializer serializer =
                new XmlSerializer(typeof(Passenger[]));

            using (FileStream stream =
                   new FileStream(filePath, FileMode.Create))
            {
                serializer.Serialize(stream, passengers);
            }
        }

        /// <summary>
        /// Проверяет наличие пассажира с одной единицей багажа
        /// массой меньше заданной.
        /// </summary>
        public static bool CheckSingleLightLuggage(
            string filePath,
            double m)
        {
            XmlSerializer serializer =
                new XmlSerializer(typeof(Passenger[]));

            using (FileStream stream =
                   new FileStream(filePath, FileMode.Open))
            {
                Passenger[] passengers =
                    (Passenger[])serializer.Deserialize(stream);

                for (int i = 0; i < passengers.Length; i++)
                {
                    if (passengers[i].Luggage.Length == 1 &&
                        passengers[i].Luggage[0].Weight < m)
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }

    public static class CollectionTasks
    {
        /// Задание 6: List
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

        /// Задание 7: LinkedList
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

        /// Задание 8: HashSet
        /// <summary>
        /// Анализирует заказы посетителей кафе.
        /// </summary>
        public static void AnalyzeDishOrders(
            HashSet<string> menu,
            HashSet<string>[] orders)
        {
            HashSet<string> allDishes =
                new HashSet<string>(menu);

            HashSet<string> someDishes =
                new HashSet<string>();

            for (int i = 0; i < orders.Length; i++)
            {
                someDishes.UnionWith(orders[i]);
                allDishes.IntersectWith(orders[i]);
            }

            HashSet<string> nobody =
                new HashSet<string>(menu);

            nobody.ExceptWith(someDishes);

            Console.WriteLine("Заказывали все:");

            foreach (string item in allDishes)
            {
                Console.WriteLine(item);
            }

            Console.WriteLine();

            Console.WriteLine("Заказывали некоторые:");

            foreach (string item in someDishes)
            {
                if (!allDishes.Contains(item))
                {
                    Console.WriteLine(item);
                }
            }

            Console.WriteLine();

            Console.WriteLine("Не заказывал никто:");

            foreach (string item in nobody)
            {
                Console.WriteLine(item);
            }
        }

        /// Задание 9: HashSet
        /// <summary>
        /// Выводит согласные буквы,
        /// входящие ровно в одно слово.
        /// </summary>
        public static void PrintAlphabeticalConsonants(
            string filePath)
        {
            string consonants =
                "бвгджзйклмнпрстфхцчшщ";

            Dictionary<char, int> counts =
                new Dictionary<char, int>();

            for (int i = 0; i < consonants.Length; i++)
            {
                counts.Add(consonants[i], 0);
            }

            string text;

            using (StreamReader reader =
                   new StreamReader(filePath))
            {
                text = reader.ReadToEnd().ToLower();
            }

            char[] separators =
            {
        ' ',
        '\r',
        '\n',
        '.',
        ',',
        ';',
        ':',
        '!',
        '?',
        '-'
    };

            string[] words =
                text.Split(
                    separators,
                    StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < words.Length; i++)
            {
                HashSet<char> letters =
                    new HashSet<char>();

                foreach (char c in words[i])
                {
                    if (counts.ContainsKey(c))
                    {
                        letters.Add(c);
                    }
                }

                foreach (char c in letters)
                {
                    counts[c]++;
                }
            }

            List<char> result =
                new List<char>();

            foreach (KeyValuePair<char, int> pair in counts)
            {
                if (pair.Value == 1)
                {
                    result.Add(pair.Key);
                }
            }

            result.Sort();

            foreach (char c in result)
            {
                Console.Write(c + " ");
            }

            Console.WriteLine();
        }

        /// Задание 10: Dictionary/SortedList
        /// <summary>
        /// Выводит призеров соревнований.
        /// </summary>
        public static void FindTopParticipants(
            string filePath)
        {
            List<string> names =
                new List<string>();

            List<int> scores =
                new List<int>();

            using (StreamReader reader =
                   new StreamReader(filePath))
            {
                string line;

                while ((line = reader.ReadLine()) != null)
                {
                    string[] parts =
                        line.Split(
                            ' ',
                            StringSplitOptions.RemoveEmptyEntries);

                    string fullName =
                        parts[0] + " " + parts[1];

                    int score = 0;

                    for (int i = 2; i < 6; i++)
                    {
                        score += int.Parse(parts[i]);
                    }

                    names.Add(fullName);
                    scores.Add(score);
                }
            }

            List<int> sortedScores =
                new List<int>(scores);

            sortedScores.Sort();
            sortedScores.Reverse();

            int thirdPlaceScore;

            if (sortedScores.Count < 3)
            {
                thirdPlaceScore =
                    sortedScores[sortedScores.Count - 1];
            }
            else
            {
                thirdPlaceScore = sortedScores[2];
            }

            for (int i = 0; i < names.Count; i++)
            {
                if (scores[i] >= thirdPlaceScore)
                {
                    Console.WriteLine(names[i]);
                }
            }
        }
    }
}
