using Estructuras_de_datos.Listas;
using NUnit.Framework;


namespace UnitTest_Estructuras_de_datos
{
    class ListaEnlazada_Test
    {
        ListaEnlazada<int>? listaEnlazada;
        [SetUp]
        public void Setup()
        {
            listaEnlazada = new ListaEnlazada<int>();
            listaEnlazada.Agregar(1);
            listaEnlazada.Agregar(2);
            listaEnlazada.Agregar(3);
            listaEnlazada.Agregar(4);
            listaEnlazada.Agregar(3);
            listaEnlazada.Agregar(3);
            listaEnlazada.Agregar(11);
            listaEnlazada.Agregar(11);
        }

        [Test]
        public void Agregar_elementos()
        {

            int[] expected = { 1, 2, 3, 4, 3, 3, 11, 11 };
            int[] actual = listaEnlazada!.To_array();
            Assert.That(expected, Is.EqualTo(actual));
        }

        [Test]
        public void Borrar_elementos()
        {
            BorrarYVerificar(1, [2, 3, 4, 3, 3, 11, 11], "Borro primer elemento");
            BorrarYVerificar(4, [2, 3, 4, 3, 11, 11], "Borro un elemento intermedio");
            BorrarYVerificar(6, [2, 3, 4, 3, 11], "Borro el ultimo elemento");


        }

        // Método auxiliar para borrar y verificar el resultado
        private void BorrarYVerificar(int posicion, int[] expected, string mensaje)
        {
            listaEnlazada!.Borrar(posicion);
            int[] actual = listaEnlazada.To_array();
            Assert.That(actual, Is.EqualTo(expected), mensaje);
        }


    }

}

