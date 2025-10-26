using System.Collections.Generic;
using System.Linq;

namespace Transdiagramdorfinal.Semantica
{
    public class TablaClases
    {
        public List<Clase> Clases { get; private set; } = new List<Clase>();

        public Clase ClaseActual { get; private set; }

        public void NuevaClase(string nombre)
        {
            Clase clase = new Clase { Nombre = nombre };
            Clases.Add(clase);
            ClaseActual = clase;
        }
        public void AgregarHerencia(string nombreBase)
        {
            ClaseActual?.Herencias.Add(nombreBase);
        }
        public void AgregarAtributo(string nombre, string tipo = "Desconocido")
        {
            ClaseActual?.Atributos.Add(new Atributo(nombre, tipo));
        }
        public void AgregarMetodo(string nombre, List<string> parametros)
        {
            Metodo m = new Metodo(nombre) { Parametros = parametros };
            ClaseActual?.Metodos.Add(m);
        }
        public Clase BuscarClase(string nombre)
        {
            return Clases.FirstOrDefault(c => c.Nombre == nombre);
        }
    }
}
