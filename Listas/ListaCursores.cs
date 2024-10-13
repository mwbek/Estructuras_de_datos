using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Estructuras_de_datos.Listas
{
    public class ListaCursores<T> : IListas<T>, IEnumerable
    {


        //Estructura de una Lista de cursores
        private Nodo<T>[] _cursor;
        private int _inicio;
        private int _libre;
        private int _cantidad;
        private const int _NULO = -1;
        private int _tamanio_maximo;

        public int Cantidad => _cantidad;


        public ListaCursores()
        {
            _tamanio_maximo = IListas<T>.tamanio_maximo;
            _cursor = new Nodo<T>[_tamanio_maximo];

            //Encadeno todos los nodos libres
            for (int i = 0;i < _cursor.Length; i++)
            {
                //Inicializo el nodo
                _cursor[i] = new Nodo<T> { _datos = default};
                _cursor[i].SiguientePos = i + 1;
            }

            //Inicializo las variables
            _inicio = _NULO; //El inicio no tiene ningun valor
            _libre = 0; //La primer posicion esta libre
            _cursor[_tamanio_maximo - 1].SiguientePos = _NULO; //El ultimo elemento es nulo

        }
        public ListaCursores(int capacidad)
        {
            _tamanio_maximo = capacidad;
            _cursor = new Nodo<T>[_tamanio_maximo];

            //Encadeno todos los nodos libres
            for (int i = 0; i < _cursor.Length; i++)
            {
                _cursor[i].SiguientePos = i + 1;
            }

            //Inicializo las variables
            _inicio = _NULO; //El inicio no tiene ningun valor
            _libre = 0; //La primer posicion esta libre
            _cursor[_tamanio_maximo - 1].SiguientePos = _NULO; //El ultimo elemento es nulo
            _cantidad = 0;

        }


        private bool EsVacio()
        {
            return _cantidad == 0;
        }
        private bool EsLleno()
        {
            return _cantidad == _tamanio_maximo;
        }


        public void Agregar(T elemento)
        {
            if (EsLleno()) return;
            //La variable LIBRE me indica que nodo esta libre. Guardo el nodo nuevo en la posicion libre
            int p = _libre; //Guardo la posicion libre
            _libre = _cursor[p].SiguientePos; //Guardo al posicion del proximo libre [IMPORTANTE HACERLO ANTES DE TODO!!]

            _cursor[p]._datos = elemento;
            _cursor[p].SiguientePos = _NULO; //Como se agrega al final, no tiene siguiente


            //Caso 1: Primer elemento
            if(_inicio == _NULO)
            {
                //Aca no guardo nodos sino que la posicion de cada nodo
                _inicio = p;
            }
            //Caso 2: Resto de elementos
            else
            {
                int aux = _inicio; //Guardo la posicion del primer nodo
                while (_cursor[aux].SiguientePos != _NULO)
                {
                    aux = _cursor[aux].SiguientePos;
                }
                //Cuando sale del while encontro el ultimo nodo
                _cursor[aux].SiguientePos = p;
            }

            _cantidad++;
        }

        public void Borrar(int pos)
        {
            throw new NotImplementedException();
        }

        public void BorrarTodos(T elemento)
        {
            throw new NotImplementedException();
        }

        public T? Buscar(T elemento)
        {
            throw new NotImplementedException();
        }

        public void Insertar(T elemento, int pos)
        {
            throw new NotImplementedException();
        }

        public void Mostrar_lista()
        {
            Console.Write("Lista Cursores: [ ");
            for (int i = 0; i < _cantidad; i++)
            {
                Console.Write(_cursor[i]._datos + " ");
            }
            Console.Write("]\n");
        }

        public T? Recuperar(int pos)
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
