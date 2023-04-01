using System.Diagnostics;
using System.Net.Mime;

namespace ImageTranslator;

public static class Program
{
    public static void Main(string[] args)
    {
        var path = args.Length > 0 ? args[0] : throw new ArgumentNullException();
        var file = File.ReadAllBytes(path);

        if (file.Length > 32768) 
            throw new FileLoadException("Слишком большой файл! Размер файла не должен превышать 32КБ");
        
        var imageTranslator = new ImageTranslator(file);
        
        var compilerPath = new DirectoryInfo(Directory.GetCurrentDirectory()).Parent.Parent.Parent.ToString(); //путь до /CreativeTask
        
        var destPath = (args.Length > 1 ? args[1] : compilerPath + "\\src") + @"\Bytes.jack";
        
        imageTranslator.WriteFile(destPath);
    }
}