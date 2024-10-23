using System.Collections;

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
            for (int i = 0; i < _cursor.Length; i++)
            {
                //Inicializo el nodo
                _cursor[i] = new Nodo<T> { _datos = default! };
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

        //Agrego al final de la lista
        public void Agregar(T elemento)
        {
            if (EsLleno()) return;
            //La variable LIBRE me indica que nodo esta libre. Guardo el nodo nuevo en la posicion libre
            int p = _libre; //Guardo la posicion libre
            _libre = _cursor[p].SiguientePos; //Guardo al posicion del proximo libre [IMPORTANTE HACERLO ANTES DE TODO!!]

            _cursor[p]._datos = elemento;
            _cursor[p].SiguientePos = _NULO; //Como se agrega al final, no tiene siguiente


            //Caso 1: Primer elemento
            if (_inicio == _NULO)
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
            if (pos <= 0 || pos >= _tamanio_maximo) return;
            if (EsLleno()) return;

            int aux = _inicio;

            //Caso 1: Pos 0
            if (pos == 1)
            {
                _inicio = _cursor[_inicio].SiguientePos;
                _cursor[aux].SiguientePos = _libre;
                _libre = aux;
            }
            //Caso 2: Cualquier otra posicion
            else
            {
                for (int i = 0; i < pos - 2; i++)
                {
                    aux = _cursor[aux].SiguientePos;
                }

                //Cuando sale del bucle esta una posicion atras de la posicion que tengo que borrar
                int aux2 = _cursor[aux].SiguientePos; //Pos a borrar
                _cursor[aux].SiguientePos = _cursor[aux2].SiguientePos;
                _cursor[aux2].SiguientePos = _libre;
                _libre = aux2;
            }



            _cantidad--;
        }

        public void BorrarTodos(T elemento)
        {
            if (EsVacio()) return;

            int actual = _inicio;

            //Caso 1: El primer elemento se debe borrar
            if (actual != _NULO && _cursor[actual]._datos!.Equals(elemento))
            {
                _inicio = _cursor[actual].SiguientePos;
                _cursor[actual].SiguientePos = _libre;
                _libre = actual;
                _cantidad--;
            }

            //Caso 2: El resto de elementos
            while (actual != _NULO && _cursor[actual].SiguientePos != _NULO)
            {

                int siguiente = _cursor[actual].SiguientePos;
                T valor = _cursor[siguiente]._datos;
                if (valor!.Equals(elemento))
                {

                    _cursor[actual].SiguientePos = _cursor[siguiente].SiguientePos;
                    _cursor[siguiente].SiguientePos = _libre;
                    _libre = siguiente;
                    _cantidad--;

                }
                else
                {
                    actual = _cursor[actual].SiguientePos;
                }
                
            }
        }

        public T? Buscar(T elemento)
        {
            if (EsVacio()) return default;

            int actual = _inicio;

            while (actual != _NULO)
            {
                T valor = _cursor[actual]._datos;
                if (valor!.Equals(elemento))
                {
                    return valor;
                }
                actual = _cursor[actual].SiguientePos;
            }
            return default;
        }

        public void Insertar(T elemento, int pos)
        {

            if(EsLleno()) return;
            if(pos < 1 || pos > _tamanio_maximo) return;

            int actual = _inicio;

            //Pos libre
            int pos_libre = _libre;
            _libre = _cursor[_libre].SiguientePos;

            //Cargo el nodo en la lista
            _cursor[pos_libre]._datos = elemento;
            _cursor[pos_libre].SiguientePos = _NULO;


            //Caso 1: Pos 1
            if (pos == 1)
            {
                _cursor[pos_libre].SiguientePos = actual;
                _inicio = pos_libre;
            }
            else
            {
                int aux = _inicio;
                for (int i = 0; i < pos-2; i++)
                {
                    aux = _cursor[aux].SiguientePos;
                }

                _cursor[pos_libre].SiguientePos = _cursor[aux].SiguientePos;
                _cursor[aux].SiguientePos = pos_libre;
            }
            _cantidad++;
        }

        public void Mostrar_lista()
        {
            Console.Write("Lista Cursores: [ ");
            int actual = _inicio;
            while (actual != _NULO)
            {
                Console.Write(_cursor[actual]._datos + " ");
                actual = _cursor[actual].SiguientePos;
            }


            Console.Write("]\n");
        }

        public T? Recuperar(int pos)
        {
            throw new NotImplementedException();
        }

        public T[] To_array()
        {
            T[] arreglo = new T[_cantidad];
            int actual = _inicio;
            int i = 0;
            while (actual != _NULO)
            {
                arreglo[i] = _cursor[actual]._datos;
                actual = _cursor[actual].SiguientePos;
                i++;
            }
            return arreglo;
        }



        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
