using Estructuras_de_datos.Listas;
using NUnit.Framework;

namespace UnitTest_Estructuras_de_datos
{
    public class ListaArreglos_Test
    {
        ListaArreglos<int>? listaArreglos;
        [SetUp]
        public void Setup()
        {
            listaArreglos = new ListaArreglos<int>();
            listaArreglos.Agregar(1);
            listaArreglos.Agregar(2);
            listaArreglos.Agregar(3);
            listaArreglos.Agregar(4);
            listaArreglos.Agregar(3);
            listaArreglos.Agregar(3);
            listaArreglos.Agregar(11);
            listaArreglos.Agregar(11);
            //[1,2,3,4,3,3,11,11]
        }

         [Test]
        public void Agregar_elementos()
        {

            int[] expected = { 1, 2, 3, 4, 3, 3, 11, 11 };
            int[] actual = listaArreglos!.To_array();
            Assert.That(expected, Is.EqualTo(actual));
        }

        [Test]
        public void Borrar_elementos()
        {
            BorrarYVerificar(1, [2, 3, 4, 3, 3, 11, 11], "Borro primer elemento");
            BorrarYVerificar(4, [2, 3, 4, 3, 11, 11], "Borro un elemento intermedio");
            BorrarYVerificar(6, [2, 3, 4, 3, 11], "Borro el ultimo elemento");

        }

        private void BorrarYVerificar(int posicion, int[] expected, string mensaje)
        {
            listaArreglos!.Borrar(posicion);
            int[] actual = listaArreglos.To_array();
            Assert.That(actual, Is.EqualTo(expected), mensaje);
        }


        [Test]
        public void Insertar_elementos()
        {
            InsertarYVerificar(15, 1, [15,1, 2, 3, 4, 3, 3, 11, 11], "Inserto en la primer posicion");
            InsertarYVerificar(16, 3, [15, 1, 16, 2, 3, 4, 3, 3, 11, 11], "Inserto en una posicion intermedia");
            InsertarYVerificar(17, 1, [15, 1, 16, 2, 3, 4, 3, 3, 11, 17, 11], "Inserto en la ultima posicion");
        }

        private void InsertarYVerificar(int elemento, int posicion, int[] expected, string mensaje)
        {
            listaArreglos.Insertar(elemento, posicion);
            int[] actual = listaArreglos.To_array();
            Assert.That(actual, Is.EqualTo(expected), mensaje);
        }
    }
}