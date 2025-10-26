using System.IO;
using System.Text;

namespace Transdiagramdorfinal.Semantica
{
    public static class GeneradorDot
    {
        public static void GenerarDot(TablaClases tabla, string rutaSalida)
        {
            var sb = new StringBuilder();
            sb.AppendLine("digraph G {");
            sb.AppendLine("rankdir=BT;");
            sb.AppendLine("node [shape=record, style=filled, fillcolor=lightyellow];");
            foreach (var clase in tabla.Clases)
            {
                sb.AppendLine($"\"{clase.Nombre}\" [label=\"{{{clase.Nombre}|");
                // Atributos de la clase
                foreach (var atr in clase.Atributos)
                    sb.AppendLine($"{atr.Nombre}\\l");
                sb.AppendLine("|");
                // Métodos de la clase
                foreach (var met in clase.Metodos)
                    sb.AppendLine($"{met.Nombre}()\\l");
                sb.AppendLine("}\"];");
                // Relaciones de herencia por ahora
                foreach (var baseClass in clase.Herencias)
                    sb.AppendLine($"\"{clase.Nombre}\" -> \"{baseClass}\" [arrowhead=\"empty\"];");
            }
            sb.AppendLine("}");
            File.WriteAllText(rutaSalida, sb.ToString());
        }
    }
}
