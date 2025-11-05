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
            if (tabla.Relaciones != null)
            {
                foreach (var rel in tabla.Relaciones)
                {
                    string estilo = "";
                    string color = "black";

                    switch (rel.Tipo)
                    {
                        case "Asociacion":
                            estilo = "[arrowhead=none, style=\"solid\", color=\"black\"]";
                            break;

                        case "Agregacion":
                            estilo = "[arrowhead=\"odiamond\", style=\"dashed\", color=\"gray50\"]";
                            break;

                        case "Composicion":
                            estilo = "[arrowhead=\"diamond\", style=\"solid\", color=\"black\"]";
                            break;

                        default:
                            estilo = "[style=\"dotted\", color=\"gray50\"]";
                            break;
                    }

                    sb.AppendLine($"\"{rel.Origen}\" -> \"{rel.Destino}\" {estilo};");
                }
            }
            sb.AppendLine("}");
            File.WriteAllText(rutaSalida, sb.ToString());

        }
    }
}
