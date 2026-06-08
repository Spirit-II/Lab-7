using Lab_7_1;

internal class Program
{
    private static void Main(string[] args)
    {
        FileTasks.CreateNum("task1.txt", 20);

        Console.WriteLine(
            "Задание 1: " +
            FileTasks.FindMaxMin("task1.txt"));

        FileTasks.CreateNums("task2.txt", 5, 10);

        Console.WriteLine(
            "Задание 2: " +
            FileTasks.FindMin("task2.txt"));

        FileTasks.CreateText("task3.txt", 10);

        FileTasks.WriteChar(
            "task3.txt",
            "task3_result.txt",
            'a');

        Console.WriteLine("Задание 3 выполнено");

        FileTasks.CreateBinary(
            "task4.bin",
            30);

        FileTasks.WriteBinary(
            "task4.bin",
            "task4_result.bin",
            2,
            3);

        Console.WriteLine("Задание 4 выполнено");

        FileTasks.CreateBag(
            "task5.xml",
            10);

        Console.WriteLine(
            "Задание 5: " +
            FileTasks.CheckSingleLightLuggage(
                "task5.xml",
                10));

        List<int> list1 =
            new List<int>
            {
                1,
                3,
                5
            };

        List<int> list2 =
            new List<int>
            {
                2,
                4,
                6
            };

        CollectionTasks.MergeSortedLists(
            list1,
            list2);

        Console.WriteLine("Задание 6:");

        foreach (int item in list1)
        {
            Console.Write(item + " ");
        }

        Console.WriteLine();

        LinkedList<int> linkedList =
            new LinkedList<int>();

        linkedList.AddLast(1);
        linkedList.AddLast(5);
        linkedList.AddLast(5);
        linkedList.AddLast(5);
        linkedList.AddLast(2);

        Console.WriteLine(
            "Задание 7: " +
            CollectionTasks.CountEqualNeighbors(
                linkedList));

        HashSet<string> menu =
            new HashSet<string>
            {
                "Суп",
                "Чай",
                "Кофе",
                "Сок"
            };

        HashSet<string>[] orders =
        {
            new HashSet<string>
            {
                "Суп",
                "Чай"
            },

            new HashSet<string>
            {
                "Суп",
                "Кофе"
            }
        };

        CollectionTasks.AnalyzeDishOrders(
            menu,
            orders);

        CollectionTasks.PrintAlphabeticalConsonants(
            "task9.txt");

        CollectionTasks.FindTopParticipants(
            "task10.txt");
    }
}
