using System;
using System.IO;

namespace SetStream
{
  class Program
  {
    static void Main(string[] args)
    {
      if (args.Length < 2)
      {
        Console.WriteLine("usage: SetStream.exe <xml file path> <stream name>");
        Console.WriteLine("example: SetStream.exe config.xml v8.5.1");
        Console.WriteLine("example: SetStream.exe c:\\temp\\config.xml \"Temp Fix\"");
        
#if DEBUG
        Console.WriteLine("Press any key");
        Console.ReadLine();
#endif
        Environment.Exit(1);
      }

      try
      {
        var file = ReadFile(args[0]);
        file = SetStreamVersion(file, args[1]);
        WriteFile(args[0], file);
      }
      catch (Exception e)
      {
        Console.WriteLine("Error: {0}", e.Message);
        Environment.Exit(100);
      }
#if DEBUG
      Console.WriteLine("Press any key");
      Console.ReadLine();
#endif
    }

    private static void WriteFile(string filePath, string file)
    {
      using (var writer = new StreamWriter(filePath))
      {
        writer.Write(file);
        writer.Flush();
        writer.Close();
      }
    }

    private static string SetStreamVersion(string file, string stream)
    {
      if (!file.Contains("<stream>") || !file.Contains("</stream>"))
      {
        throw new Exception("Config file does not contain a <stream> node.");
      }

      var startPos = file.IndexOf("<stream>",StringComparison.CurrentCulture);
      var endPos = file.IndexOf("</stream>", StringComparison.CurrentCulture);
      var lengthOfCurrentStream = endPos - startPos - 8;
      var curStream = file.Substring(startPos + 8, lengthOfCurrentStream);
      var newFile = file.Replace("<stream>" + curStream + "</stream>", "<stream>" + stream + "</stream>");
      return newFile;
    }

    private static string ReadFile(string filePath)
    {
      if (!File.Exists(filePath))
      {
        throw new FileNotFoundException("File: {0} is not found", filePath);
      }

      using (var reader = new StreamReader(filePath))
      {
        return reader.ReadToEnd();
      }
    }
  }
}
