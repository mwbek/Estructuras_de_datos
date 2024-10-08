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

        Console.WriteLine("Cantidad elementos: " + l.longitud());

        l.mostrar_lista();


        l.eliminar(1);
        l.mostrar_lista();


        l.agregar(new TipoElemento(7));
        l.agregar(new TipoElemento(5));
        l.agregar(new TipoElemento(7));
        l.agregar(new TipoElemento(18));
        l.agregar(new TipoElemento(1));

        l.mostrar_lista();

        l.borrar(7);

        l.mostrar_lista();

        l.borrar(1);

        l.mostrar_lista();




        return 0;

    }
}


