using Estructuras_de_datos.Tipo_de_dato;
using System.Collections;

namespace Estructuras_de_datos.Listas
{


    public class ListaArreglos<T> : IListas<T>, IEnumerable
    {

        private readonly T[] _valores;
        private int _capacidad;
        private readonly int _tamanio_maximo;

        public int Capacidad => _capacidad;

        public ListaArreglos()
        {
            _valores = new T[IListas<T>.tamanio_maximo];
            _capacidad = 0;
            _tamanio_maximo = IListas<T>.tamanio_maximo;
        }

        //Sobrecarga de constructor
        public ListaArreglos(int longitud)
        {
            _valores = new T[longitud];
            _capacidad = 0;
            _tamanio_maximo = longitud;
        }


        public T this[int index]
        {
            get
            {
                return _valores[index];
            }
            set
            {
                _valores[index] = value;
            }
        }


        private bool Es_llena()
        {
            return _capacidad == _tamanio_maximo;
        }
        private bool Es_vacia()
        {
            return _capacidad == 0;
        }



        public void Agregar(T elemento)
        {
            if (Es_llena() == false)
            {
                _valores[_capacidad] = elemento;
                _capacidad++;
            }
        }


        public void BorrarTodos(T elemento)
        {
            if (Es_llena()) return;

            int pos = 0;

            while (pos < _capacidad)
            {

                T x = _valores[pos];
                //Si el elemento coincide, borro y muevo todo una posicion atras
                if (x != null && x.Equals(elemento))
                {
                    //Desplazo todo el arreglo a la izquierda
                    for (int i = pos; i < _capacidad - 1; i++)
                    {
                        _valores[i] = _valores[i + 1];

                    }
                    _capacidad--;
                }
                else
                {
                    pos++;
                }
            }
        }

        public T? Buscar(T elemento)
        {
            if (Es_vacia() == false)
            {
                for (int i = 0; i < _capacidad; i++)
                {
                    T x = _valores[i];

                    if (x!= null && x.Equals(elemento)) return x;
                }
            }

            //En los genericos no puedo retornar NULL, pero puedo retornar Default(T)
            return default;
        }

        public void Borrar(int pos)
        {

            if (Es_vacia() == false)
            {
                if (pos >= 0 && pos < _capacidad)
                {
                    for (int i = pos; i < _capacidad - 1; i++)
                    {
                        _valores[i] = _valores[i + 1];
                    }
                    _capacidad--;
                }
            }
        }

        public void Insertar(T elemento, int pos)
        {

            if (Es_llena() == false)
            {
                if (pos > 0 && pos < _tamanio_maximo)
                {
                    for (int i = _capacidad - 1; i >= pos-1 && i > 0; i--)
                    {
                        _valores[i + 1] = _valores[i];
                    }
                    _valores[pos] = elemento;
                    _capacidad++;
                }

            }

        }


        public T? Recuperar(int pos)
        {
            if (Es_vacia() == false)
            {
                if (pos >= 0 && pos < _capacidad)
                {
                    return _valores[pos];
                }
            }
            return default;
        }

        public void Mostrar_lista()
        {
            if (_capacidad > 0)
            {
                Console.Write("Lista de arreglos: [ ");
                for (int i = 0; i < _capacidad; i++)
                {
                    Console.Write(_valores[i]?.ToString() + " ");
                }
                Console.Write("]\n");
            }
        }


        public T[] To_array()
        {
            T[] arreglo = new T[_capacidad];
            if (_capacidad > 0)
            {
                
                for (int i = 0; i < _capacidad; i++)
                {
                    arreglo[i] = _valores[i];
                }
            }

            return arreglo;
        }

        public IEnumerator GetEnumerator()
        {
            return new ListaArreglos_Iterador<T>(this);
        }
    }

    file class ListaArreglos_Iterador<T> : IEnumerator
    {
        private int _posicion_actual;
        private readonly ListaArreglos<T> _lista;
        
        public ListaArreglos_Iterador(ListaArreglos<T> lista)
        {
            _lista = lista;
            _posicion_actual = -1;
        }
        public T? Current => _lista[_posicion_actual];

        public bool MoveNext()
        {
            _posicion_actual++;
            return _posicion_actual < _lista.Capacidad;
        }

        public void Reset()
        {
            _posicion_actual = -1;
        }

        Object? IEnumerator.Current => Current;

        
    }
}
