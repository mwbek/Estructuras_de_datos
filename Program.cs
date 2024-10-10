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


        while (l.hay_siguiente())
        {
            Console.WriteLine(l.siguiente()?.Clave);

        }


        return 0;

    }
}


