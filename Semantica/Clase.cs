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
        public string Visibilidad { get; set; }
        public string Nombre { get; set; }
        public string Tipo { get; set; } = "Desconocido";
        public string ValorInicial { get; set; } = null;

        public Atributo(string visibilidad, string nombre, string tipo = "Desconocido", string valorInicial = null)
        {
            Visibilidad = visibilidad;
            Nombre = nombre;
            Tipo = tipo;
            ValorInicial = valorInicial;
        }
    }
    public class Metodo
    {
        public string Nombre { get; set; }
        public List<string> Parametros { get; set; } = new List<string>();

        public Metodo(string nombre, List<string> parametros)
        {
            Nombre = nombre;
            Parametros = parametros;
        }
    }

    public class Relacion
    {
        public string Origen { get; set; }
        public string Destino { get; set; }
        public string Tipo { get; set; }
        public string CardinalidadOrigen { get; set; } = "1";
        public string CardinalidadDestino { get; set; } = "1";
        public Relacion(string origen, string destino, string tipo, string cardOrigen = "1", string cardDestino = "1")
        {
            Origen = origen;
            Destino = destino;
            Tipo = tipo;
            CardinalidadOrigen = cardOrigen;
            CardinalidadDestino = cardDestino;
        }
    }
}