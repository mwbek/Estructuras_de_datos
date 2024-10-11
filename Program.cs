using Estructuras_de_datos;
using Estructuras_de_datos.Listas;
using Estructuras_de_datos.Tipo_de_dato;


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

        foreach (TipoElemento x in l)
        {
            Console.WriteLine(x.Clave);
        }
        //=====================
        ListaArreglos la = new ListaArreglos();
        la.agregar(new TipoElemento(1));
        la.agregar(new TipoElemento(2));
        la.agregar(new TipoElemento(3));
        la.agregar(new TipoElemento(4));

        la.mostrar_lista();

        foreach (TipoElemento x in la)
        {
            Console.WriteLine(x.Clave);
        }


        


        return 0;

    }
}


