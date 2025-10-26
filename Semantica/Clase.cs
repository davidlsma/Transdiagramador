using System.Collections.Generic;

namespace Transdiagramdorfinal.Semantica
{
    public class Clase
    {
        public string Nombre { get; set; }
        public List<string> Herencias { get; set; } = new List<string>(); 
        public List<Atributo> Atributos { get; set; } = new List<Atributo>();
        public List<Metodo> Metodos { get; set; } = new List<Metodo>();
    }
    public class Atributo
    {
        public string Nombre { get; set; }
        public string Tipo { get; set; } = "Desconocido";
        public Atributo(string nombre, string tipo = "Desconocido")
        {
            Nombre = nombre;
            Tipo = tipo;
        }
    }
    public class Metodo
    {
        public string Nombre { get; set; }
        public List<string> Parametros { get; set; } = new List<string>();
        public Metodo(string nombre)
        {
            Nombre = nombre;
        }
    }
}
