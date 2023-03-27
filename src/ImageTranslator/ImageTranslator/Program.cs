using System.Diagnostics;

namespace ImageTranslator;

public static class Program
{
    public static void Main(string[] args)
    {
        var path = args[0];
        var file = File.ReadAllBytes(path);

        if (file.Length > 32768) 
            throw new FileLoadException("Слишком большой файл! Размер файла не должен превышать 32КБ");
        
        var imageTranslator = new ImageTranslator(file);
        
        var destPath = (args.Length > 1 ? args[1] : Directory.GetCurrentDirectory()) + @"\Bytes.jack";
        
        imageTranslator.WriteFile(destPath);
        
        //Далее код, который автоматически запускает JackCompiler и компилирует папку src

        var compilerPath = new DirectoryInfo(Directory.GetCurrentDirectory()).Parent.Parent.Parent.ToString(); //путь до /CreativeTask

        Process.Start(compilerPath + @"\tools\JackCompiler.bat",
            compilerPath + @"\src");                                //Запуск JackCompiler

        Process.Start(compilerPath + @"\tools\VMEmulator.bat");
    }
    public static void MoveFilesToVM(){
        var fileNames = new string[]
        {
            "Bytes",
            "Cos",
            "Decoder",
            "Extensions",
            "Float",
            "GigachadInt",
            "HuffmanTable",
            "IDCT",
            "List",
            "Main",
            "Stream"
        };

        foreach(string file in fileNames){
            var filePath = new DirectoryInfo(Directory.GetCurrentDirectory()).Parent.Parent.ToString();
            var destPath = new DirectoryInfo(Directory.GetCurrentDirectory()).Parent.Parent.Parent.ToString();
            filePath += file+".vm";
            destPath+=@"\vm";
            var fileToMove = new FileInfo(filePath);
            fileToMove.MoveTo(destPath);
        }
    }
}