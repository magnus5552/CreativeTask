namespace ImageTranslator;

public static class Program
{
    public static void Main(string[] args)
    {
        var path = args[0];
        var file = File.ReadAllBytes(path);
        
        var imageTranslator = new ImageTranslator(file);
        
        var destPath = args.Length > 1 ? args[1] : Directory.GetCurrentDirectory();
        
        imageTranslator.WriteFile(destPath);
        
        // TODO: Далее нужен код, который автоматически запускает JackCompiler и компилирует папку src
    }
}