using Estructuras_de_datos;
using Estructuras_de_datos.Listas;
using Estructuras_de_datos.Tipo_de_dato;
using System.Xml.Linq;


class Program
{
    public static int Main()
    {


        //ListaArreglos<int> l = new ListaArreglos<int>();

        //l.Agregar(1);
        //l.Agregar(2);
        //l.Agregar(3);
        //l.Agregar(4);
        //l.Agregar(3);
        //l.Agregar(3);
        //l.Agregar(11);
        //l.Agregar(11);


        //l.Insertar(11,0);
        //Console.WriteLine("Test 04\n Insertar pos 0");
        //l.Mostrar_lista();
        //Console.WriteLine("=======================================");
        //l.Insertar(22, 3);
        //Console.WriteLine("Test 05\n Insertar pos 3");
        //l.Mostrar_lista();
        //Console.WriteLine("=======================================");
        //l.Insertar(33,l.Capacidad);
        //Console.WriteLine("Test 06\n Insertar pos final");
        //l.Mostrar_lista();
        //Console.WriteLine("=======================================");
        //int x =l.Buscar(4);
        //Console.WriteLine("Test 07\n Buscar: " + x.ToString());
        //l.Mostrar_lista();
        //Console.WriteLine("=======================================");
        //x =l.Buscar(80);
        //Console.WriteLine("Test 08\n Buscar dato inexistente: " + x.ToString());
        //l.Mostrar_lista();
        //Console.WriteLine("=======================================");
        //x = l.Recuperar(4);
        //Console.WriteLine("Test 09\n Recuperar 4: " + x.ToString());
        //l.Mostrar_lista();
        //Console.WriteLine("=======================================");
        //x = l.Buscar(80);
        //Console.WriteLine("Test 10\n Recuperar dato inexistente: "+ x.ToString());
        //l.Mostrar_lista();
        //Console.WriteLine("=======================================");
        //l.BorrarTodos(11);
        //Console.WriteLine("Test 10\n Borrar todos los 11: "+ x.ToString());
        //l.Mostrar_lista();
        //Console.WriteLine("=======================================");






        ListaCursores<int> listaCursores = new ListaCursores<int>();

        listaCursores.Agregar(1);
        listaCursores.Agregar(2);
        listaCursores.Agregar(3);
        listaCursores.Agregar(4);
        listaCursores.Agregar(5);

        listaCursores.Mostrar_lista();

        listaCursores.Borrar(1);
        listaCursores.Mostrar_lista();

        listaCursores.Borrar(4);

        listaCursores.Mostrar_lista();

        listaCursores.Agregar(7);


        listaCursores.Mostrar_lista();

        listaCursores.Agregar(8);


        listaCursores.Mostrar_lista();

        Console.WriteLine(listaCursores.ToString());


        return 0;


    }
}


