using Estructuras_de_datos.Tipo_de_dato;
using System.Collections;


namespace Estructuras_de_datos.Listas
{
    public class Nodo<T>
    {
        public required T _datos;
        public Nodo<T>? SiguienteNodo;
        public int SiguientePos;
    }

}
