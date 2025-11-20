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
                {
                    string tipoOvalor;
                    if (!string.IsNullOrEmpty(atr.ValorInicial))
                    {
                        string valorEscapado = atr.ValorInicial.Replace("\"", "");
                        tipoOvalor = $"= \\\"{valorEscapado}\\\"";
                    }
                    else
                    {
                        tipoOvalor = $": {atr.Tipo}";
                    }
                    sb.AppendLine($"{atr.Visibilidad} {atr.Nombre} {tipoOvalor}\\l");
                }
                sb.AppendLine("|");
                // Métodos de la clase
                foreach (var met in clase.Metodos)
                    sb.AppendLine($"+ {met.Nombre}({string.Join(", " ,met.Parametros)})\\l");
                sb.AppendLine("}\"];");
                // Relaciones de herencia por ahora
                foreach (var baseClass in clase.Herencias)
                    sb.AppendLine($"\"{clase.Nombre}\" -> \"{baseClass}\" [arrowhead=\"empty\"];");
            }
            foreach (var rel in tabla.Relaciones)
            {
                string arrow;
                if (rel.Tipo == "composicion")
                    arrow = "diamond";
                else if (rel.Tipo == "agregacion")
                    arrow = "odiamond";
                else
                    arrow = "none";
                sb.AppendLine($"\"{rel.Origen}\" -> \"{rel.Destino}\"[arrowhead=\"none\", arrowtail=\"{arrow}\", dir=\"both\", taillabel=\"{rel.CardinalidadOrigen}\", labeldistance=\"1.0\", headlabel=\"{rel.CardinalidadDestino}\"];");
            }
            sb.AppendLine("}");
            File.WriteAllText(rutaSalida, sb.ToString());
        }
    }
}