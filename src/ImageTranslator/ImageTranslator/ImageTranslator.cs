using System.Text;

namespace ImageTranslator;

public class ImageTranslator
{
    /// <summary>
    /// Принимает битовое представление файла jpeg
    /// </summary>
    public byte[] file;
    public ImageTranslator(byte[] file)
    {
        this.file = file;
    }
    
    /// <summary>
    /// Записывает биты файла в файл класса на языке jack в директорию destinationPath, название класса - JPEGBytes
    /// </summary>
    public void WriteFile(string destinationPath)
    {
        var bytesDict = new Dictionary<int, List<int>>(); //словарь: бит -> все индексы, по которым он лежит
        for (var i = 0; i < file.Length; i++)
        {
            var b = file[i];
            if (!bytesDict.ContainsKey(b)) bytesDict.Add(b, new List<int>(){i});
            else bytesDict[b].Add(i);
        }

        var text = BytesToText(bytesDict);
        
        File.WriteAllLines(destinationPath, text);
    }

    private string[] BytesToText(Dictionary<int, List<int>> dict)
    {
        var resList = new List<string>();
        resList.Add("class Bytes {");
        resList.Add($"\tfunction int GetLen() {{ return {file.Length}; }}");
        resList.Add("\tfunction int GetByte (int index) {");
        
        foreach (var e in dict)
        {
            resList.Add($"\t\tif ({String.Join(" | ", e.Value.Select(index => $"(index = {index})"))}) {{");
            //resList.AddRange(e.Value.Select(index => $"index = {e}")); //все значения индекса на уникальный бит
            
            resList.Add($"\t\t\treturn {e.Key};");
            resList.Add("\t\t}");
        }
        
        resList.Add("\t\treturn -1;\n\t}\n}");

        return resList.ToArray();
    }
}