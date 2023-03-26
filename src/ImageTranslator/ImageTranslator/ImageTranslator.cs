using System.Text;

namespace ImageTranslator;

public class ImageTranslator
{
    /// <summary>
    /// Принимает битовое представление файла jpeg
    /// </summary>
    public byte[] File;
    public ImageTranslator(byte[] file)
    {
        File = file;
    }

    /// <summary>
    /// Записывает биты файла в файл класса на языке jack в директорию destinationPath, название класса - JPEGBytes
    /// </summary>
    public async void WriteFile(string destinationPath)
    {
        var bytesDict = new Dictionary<int, List<int>>(); //словарь: бит -> все индексы, по которым он лежит
        for (var i = 0; i < File.Length; i++)
        {
            var b = File[i];
            if (!bytesDict.ContainsKey(b)) bytesDict.Add(b, new List<int>(i));
            else bytesDict[b].Add(i);
        }

        var text = BytesToText(bytesDict);
        
        using (FileStream fstream = new FileStream(destinationPath, FileMode.Create)) //здесь в пути должно содержаться название файла с расширением
        {
            // преобразуем строку в байты
            byte[] buffer = Encoding.Default.GetBytes(text);
            // запись массива байтов в файл
            await fstream.WriteAsync(buffer, 0, buffer.Length);
            Console.WriteLine("Текст записан в файл");
        }
    }

    private static string BytesToText(Dictionary<int, List<int>> dict)
    {
        var resList = new List<string>();
        resList.Add("class Bytes {\n");
        resList.Add("\tfunction int GetByte (int index) {\n");
        
        foreach (var e in dict)
        {
            resList.Add("\t\tif (");

            resList.AddRange(e.Value.Select(index => $"index = {e} | ")); //все значения индекса на уникальный бит

            resList.Add("\\\\) {\n");
            resList.Add($"\t\t\treturn{e.Key};\n");
            resList.Add("\t\t}\n");
        }
        
        resList.Add("\t}\n}");

        return resList.Aggregate("", (current, s) => current + s); //ого складываем все строки в одну LINQ'ом
    }
}