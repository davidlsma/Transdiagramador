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
        public void AgregarAtributo(string visibilidad, string nombre, string tipo = "Desconocido", string valorInicial = null)
        {
            ClaseActual?.Atributos.Add(new Atributo(visibilidad, nombre, tipo, valorInicial));
        }
        public void AgregarMetodo(string nombre, List<string> parametros)
        {
            Metodo m = new Metodo(nombre, parametros);
            ClaseActual?.Metodos.Add(m);
        }
        public Clase BuscarClase(string nombre)
        {
            return Clases.FirstOrDefault(c => c.Nombre == nombre);
        }
        public List<Relacion> Relaciones { get; private set; } = new List<Relacion>();
        public void AgregarRelacion(string origen, string destino, string tipo, string cardOrigen = "1", string cardDestino = "1")
        {
            if (origen == destino) return;

            var existente = Relaciones.FirstOrDefault(r =>
                (r.Origen == origen && r.Destino == destino) ||
                (r.Origen == destino && r.Destino == origen));
            if (existente == null)
            {
                Relaciones.Add(new Relacion(origen, destino, tipo, cardOrigen, cardDestino));
            }
            else
            {
                if (tipo == "composicion" && existente.Tipo != "composicion")
                {
                    existente.Tipo = "composicion";
                }
                if (cardDestino == "*")
                {
                    existente.CardinalidadDestino = "*";
                }
                if (cardOrigen == "*")
                {
                    existente.CardinalidadOrigen = "*";
                }
            }
        }
    }
}