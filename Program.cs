using Estructuras_de_datos;
using Estructuras_de_datos.Listas;
using Estructuras_de_datos.Tipo_de_dato;
using System.Xml.Linq;


class Program
{
    public static int Main()
    {


        ListaEnlazada<int> l = new ListaEnlazada<int>();

        l.Agregar(1);
        l.Agregar(2);
        l.Agregar(3);
        l.Agregar(4);
        l.Agregar(3);
        l.Agregar(3);

        l.Mostrar_lista();

        l.Borrar(2);

        l.Mostrar_lista();

        l.BorrarTodos(3);
        l.Mostrar_lista();




        return 0;

    }
}


