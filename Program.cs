using Lab_7_1;

internal class Program
{
    private static void Main(string[] args)
    {
        // Пример для задания 1
        FileTasks.FillFileWithRandomIntsOnePerLine("task1.txt", 10);
        int diff = FileTasks.FindDifferenceMaxMin("task1.txt");
        Console.WriteLine($"Разность max-min: {diff}");

        // Пример для задания 6
        List<int> list1 = new List<int> { 1, 3, 5 };
        List<int> list2 = new List<int> { 2, 4, 6 };
        CollectionTasks.MergeSortedLists(list1, list2);
        Console.WriteLine("Объединенный список: " + string.Join(", ", list1));
    }
}