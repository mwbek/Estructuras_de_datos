using Estructuras_de_datos.Tipo_de_dato;
using System.Collections;

namespace Estructuras_de_datos.Listas
{

    public interface IListas : IEnumerable
    {
        const int tamanio_maximo = 100;

        bool es_vacia();
        bool es_llena();
        int longitud();
        void agregar(TipoElemento elemento);
        TipoElemento? buscar(int clave);
        void borrar(int clave);
        void insertar(TipoElemento elemento, int pos);
        void eliminar(int pos);
        TipoElemento? recuperar(int pos);
        void mostrar_lista();

    }


}
