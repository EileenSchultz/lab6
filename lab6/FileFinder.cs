namespace lab6;

public class FileFinder
{
    public static string FindFile(string folder, string fileNames, out char delimiter)
    {
        delimiter = ' ';
        
        if (string.IsNullOrEmpty(folder) || string.IsNullOrEmpty(fileNames)) //проверка на пустоту файла или имени
        {
            Console.WriteLine("Папка или имя файла не указано");
            return null;
        }

        try
        {
            string[] allFiles = Directory.GetFiles(folder, "*.*"); //полученаем все файлы в папке
            
            foreach (string file in allFiles) //перебор
            {
                string fileWithEx = Path.GetFileNameWithoutExtension(file); 
                string extension = Path.GetExtension(file).ToLower();

                if (fileWithEx.Equals(fileNames, StringComparison.OrdinalIgnoreCase) 
                    && (extension == ".csv" || extension == ".tsv"))
                {
                    delimiter = (extension == ".csv") ? ';' : '\t'; //проверка на расширение файла (csv или tsv)
                    Console.WriteLine($"Файл {file} найден");
                    return file;
                }
                    
            }
            Console.WriteLine("Файл не найден");
        }
        catch (FormException)
        {
            Console.WriteLine("Не тот формат");
        }
        catch (Exception e)
        {
            Console.WriteLine($"Что-то пошло не так... {e.Message}");
        }
    }
}