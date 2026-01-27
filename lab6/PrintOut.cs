namespace lab6;

public class PrintOut
{
    public static void PrintTable(string[] headers, List<string[]> rows)
    {
        if (headers.Length == 0 || rows.Count == 0) return; // Проверка на пустоту заголовка или строк
     
        int[] widths = new int[headers.Length];
        for (int i = 0; i < headers.Length; i++) // максимальная длина заголовка
        {
            int maxLength = headers[i].Length;
            foreach (string[] row in rows)
            {
                if (i < row.Length && row[i].Length > maxLength) //если длина больше теущей, то обнавляем
                    maxLength = row[i].Length;
            }
            widths[i] = maxLength;
        }
        
        private static void PrinterSeparat(int[] widths) 
        {
            Console.Write("+");
            foreach (int width in widths) //делаем линию
                Console.Write(new string('-', width + 2) + "+"); //делаем отступы
            Console.WriteLine();
        }
        
        private static void PrinterRow(string[] lineSeparat, int[] widthLine) //делаем что-то типо таблицы
        {
            Console.Write("|");
            for (int i = 0; i < widthLine.Length; i++) 
            {
                string value = (i < lineSeparat.Length) ? lineSeparat[i] : ""; //если есть что выводить, то выводим
            
                //выравниваем хоть как-то по правому краю
                Console.Write($" {value.PadRight(widthLine[i])} |");
            }
            Console.WriteLine();
        }
        
}