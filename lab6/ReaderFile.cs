namespace lab6;

public class ReaderFile
{
    //сохранение пути к файлу и разделитель
    private string _path;
    private char _delimit;
    
    public ReaderFile(string path, char delimit)
    {
        _path = path;
        _delimit = delimit;
    }

    public string[] FirstLineReader()
    {
        using var sr = new StreamReader(_path);
        return sr.ReadLine().Split(_delimit);
    }

    public IEnumerable<string[]> ReaderRows(int skip, int take)
    {
        using var sr = new StreamReader(_path);
        if (sr.ReadLine() == null) //если файл пустой 
        {
           return yield break;
        }

        for (int i = 0; i < skip && sr.ReadLine() != null; i++) {} //пропускаем строки
        
        int count = 0;
        string linees;

        while (count < take && (linees = sr.ReadLine()) != null) //считываем строки пока не взяли тейк или конец файла
        {
            yield return linees.Split(_delimit);
            count++;
        }
    }
    
}
