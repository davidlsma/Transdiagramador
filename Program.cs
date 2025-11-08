using System;
using System.IO;
using System.Text;
using Transdiagramdorfinal.Semantica;  // Asegúrate de que este namespace esté disponible

class Program
{
    static void Main(string[] args)
    {
        // Validar argumentos: al menos uno (ruta del archivo .py)
        if (args.Length < 1)
        {
            Console.WriteLine(" Error: Debes proporcionar la ruta del archivo .py como argumento.");
            Console.WriteLine("Uso: MiBackend.exe <ruta_archivo_py> [ruta_archivo_dot]");
            return;
        }

        string archivoOriginal = args[0];  
        string archivoProcesado = Path.Combine(Path.GetDirectoryName(archivoOriginal), Path.GetFileNameWithoutExtension(archivoOriginal) + "_procesado.py");
        string rutaDot = args.Length > 1 ? args[1] : Path.Combine(Path.GetDirectoryName(archivoOriginal), "output.dot");  // Ruta de salida por defecto si no se especifica

        try
        {
            // Verificar si el archivo de entrada existe
            if (!File.Exists(archivoOriginal))
            {
                Console.WriteLine($"Error: El archivo de entrada '{archivoOriginal}' no existe.");
                return;
            }

            string input = File.ReadAllText(archivoOriginal);
            string inputProcesado = PreprocesarIndentacion(input);
            File.WriteAllText(archivoProcesado, inputProcesado);

            Console.WriteLine($"Verificando GRAMATICA");
            Scanner scanner = new Scanner(archivoProcesado);
            Parser parser = new Parser(scanner);
            parser.Parse();
            Console.WriteLine($"Fin de proceso");

            GeneradorDot.GenerarDot(parser.tabla, rutaDot);
            Console.WriteLine($" Archivo DOT generado en {rutaDot}");

            // Imprimir la ruta del .dot para que Python la capture (esto es la "salida" principal)
            Console.WriteLine(rutaDot);  // Esto será capturado por subprocess en Python
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error durante el parsing: {ex.Message}");
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
