using lab6;
public class Program
{
    static void Main()
    {
        Console.Write("Папка с таблицами: ");
        string folder = Console.ReadLine();
        Console.Write("Имя таблицы: ");
        string name = Console.ReadLine();
        
        char delimiter;
        string filePath = FileFinder.FindFile(folder, name, out delimiter); //поиск файла
        
        if (filePath == null) return;

        var reader = new ReaderFile(filePath, delimiter);
        string[] header = reader.FirstLineReader(); //считываем заголовки   
        Console.WriteLine("\nДоступные столбцы: " + string.Join(", ", header)); //выводим какие есть

        Console.Write("Столбцы (* или через запятую): ");
        string coumnInput = Console.ReadLine()?.Trim() ?? "";
        //если * то выводим все столбцы, иначе выводим только те, что выбрали
        string[] columnName = coumnInput == "*" ? header : coumnInput.Split(',').Select(c => c.Trim()).ToArray();

        var index = new List<int>();
        foreach (string column in columnName)
        {   //ищем столбцы по названию
            int idx = Array.FindIndex(header, h => h.Equals(column, StringComparison.OrdinalIgnoreCase));
            if (idx >= 0) index.Add(idx);
            else Console.WriteLine($"Столбец '{column}' не найден");
        }

        Console.Write("Введите диапазон строк (Пр: 5-10): ");
        string range = Console.ReadLine()?.Trim() ?? "1-10"; //если пусто, то по умолчанию выводим 1-10
        string[] parts = range.Split('-');
        if (!int.TryParse(parts[0], out int start) || !int.TryParse(parts.Length > 1 ? parts[1] : parts[0], out int end))
        {
            Console.WriteLine("Такого диапазона не существует");
            return;
        }
        if (start < 1) start = 1; //начало диапазона не может быть меньше 1 и не больше конца   
        if (end < start) end = start;

        List<string[]> rawData = reader.ReaderRows(start - 1, end - start + 1).ToList(); 
        if (rawData.Count == 0) { Console.WriteLine("\nНичего не нашлось("); return; }

        var filteredRows = rawData.Select(row => 
            index.Select(i => i < row.Length ? row[i] : "").ToArray()
        ).ToList();

        Console.WriteLine($"\nСтроки {start}-{start + rawData.Count - 1}:");
        PrintOut.PrintTable(columnName, filteredRows);
        Console.WriteLine($"Выведено {rawData.Count} строк");
    }
}
