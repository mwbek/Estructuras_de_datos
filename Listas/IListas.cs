using System.Collections;

namespace Estructuras_de_datos.Listas
{

    public interface IListas<T> : IEnumerable
    {
        const int tamanio_maximo = 100;

        void Agregar(T elemento);
        T? Buscar(T elemento);
        void Insertar(T elemento, int pos);
        void BorrarTodos(T elemento);
        void Borrar(int pos);
        T? Recuperar(int pos);
        void Mostrar_lista();

    }


}
