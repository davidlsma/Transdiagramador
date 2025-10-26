using System;
using System.IO;
using System.Text;
using Transdiagramdorfinal.Semantica;
class Program
{
    static void Main()
    {
        string archivoOriginal = "C:\\Users\\USUARIO\\source\\repos\\Transdiagramdorfinal\\bin\\Debug\\ejemplo1.py";
        string archivoProcesado = "C:\\Users\\USUARIO\\source\\repos\\Transdiagramdorfinal\\bin\\Debug\\ejemplo1_procesado.py";
        try
        {
            string input = File.ReadAllText(archivoOriginal);
            string inputProcesado = PreprocesarIndentacion(input);
            File.WriteAllText(archivoProcesado, inputProcesado);
            Console.WriteLine($"Verificando GRAMATICA");
            Scanner scanner = new Scanner("C:\\Users\\USUARIO\\source\\repos\\Transdiagramdorfinal\\bin\\Debug\\ejemplo1_procesado.py");
            Parser parser = new Parser(scanner);
            parser.Parse();
            Console.WriteLine($"Fin de proceso");
            string rutaDot = "C:\\Users\\USUARIO\\source\\repos\\Transdiagramdorfinal\\bin\\Debug\\diagrama.dot";
            GeneradorDot.GenerarDot(parser.tabla, rutaDot);
            Console.WriteLine($"✅ Archivo DOT generado en {rutaDot}");

        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ Error durante el parsing: {ex.Message}");
        }
    }
    static string PreprocesarIndentacion(string input)
    {
        var lines = input.Split(new[] { "\r\n", "\n" }, StringSplitOptions.None);
        var result = new StringBuilder();
        int prevIndent = 0;
        for (int i = 0; i < lines.Length; i++)
        {
            var line = lines[i];
            int currentIndent = 0;
            foreach (char c in line)
            {
                if (c == ' ') currentIndent++;
                else if (c == '\t') currentIndent += 4;
                else break;
            }

            string trimmed = line.TrimStart(' ', '\t');
            if (string.IsNullOrWhiteSpace(trimmed)) continue;
            if (currentIndent > prevIndent)
            {
                result.AppendLine("{");
            }
            else if (currentIndent < prevIndent)
            {
                for (int j = 0; j < (prevIndent - currentIndent) / 4; j++)
                {
                    result.AppendLine("}");
                }
            }

            result.AppendLine(trimmed);
            prevIndent = currentIndent;
        }
        for (int i = 0; i < prevIndent / 4; i++)
        {
            result.AppendLine("}");
        }
        return result.ToString();
    }
}