using Estructuras_de_datos;
using Estructuras_de_datos.Listas;


class Program
{
    public static int Main()
    {

        ListaEnlazada l = new ListaEnlazada();

        l.agregar(new TipoElemento(1));
        l.agregar(new TipoElemento(2));
        l.agregar(new TipoElemento(3));
        l.agregar(new TipoElemento(4));


        l.mostrar_lista();

        l.insertar(new TipoElemento(5), 0);
        l.mostrar_lista();

        l.insertar(new TipoElemento(6), 2);
        l.mostrar_lista();

        l.insertar(new TipoElemento(7), 6);
        l.mostrar_lista();

        l.insertar(new TipoElemento(8), 10);
        l.mostrar_lista();

        return 0;

    }
}


