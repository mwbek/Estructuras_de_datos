namespace Estructuras_de_datos.Listas
{

    public interface IListas
    {
        const int tamanio_maximo = 100;

        bool es_vacia();
        bool es_llena();
        int longitud();
        void agregar(TipoElemento elemento);
        void borrar(int clave);
        TipoElemento? buscar(int clave);
        void insertar(TipoElemento elemento, int pos);
        void eliminar(int pos);
        TipoElemento? recuperar(int pos);
        void mostrar_lista();

    }


    public interface IIterador
    {
        bool hay_siguiente();
        TipoElemento? siguiente();
    }

}
