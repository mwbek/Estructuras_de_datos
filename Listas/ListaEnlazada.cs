using System.Collections;

namespace Estructuras_de_datos.Listas
{

    public class ListaEnlazada<T> : IListas<T>, IEnumerable
    {

        private int _capacidad;
        private readonly int _tamanio_maximo;
        public Nodo<T>? _inicio;



        //Contructor de la lista
        public ListaEnlazada()
        {
            _capacidad = 0;
            _tamanio_maximo = IListas<T>.tamanio_maximo;
            _inicio = null;
        }

        //Sobrecarga de constructor
        public ListaEnlazada(int tamanio_maximo)
        {
            _capacidad = 0;
            _tamanio_maximo = tamanio_maximo;
            _inicio = null;

        }

        public int Capacidad => _capacidad;


        public T? this[int Index]
        {
            get
            {
                if (Es_vacia() == false)
                {
                    if (Index >= 0 && Index < _capacidad)
                    {
                        return Recuperar(Index)!; //El signo de exclamacion (!) le indica al compilador que nosotros estamos seguros que nunca va a retornar un null
                    }
                }
                throw new ArgumentOutOfRangeException("Index fuera de rango");
            }
            set
            {
                if (Index >= 0 && Index < _capacidad)
                {
                    Insertar(value!, Index);
                }
                else
                {
                    throw new ArgumentOutOfRangeException("Index fuera de rango");
                }


            }
        }





        public bool Es_vacia()
        {
            return _capacidad == 0;
        }
        public bool Es_llena()
        {
            return _capacidad == _tamanio_maximo;
        }


        public void Agregar(T elemento)
        {
            if (Es_llena()) return;

            //Creo un nodo nuevo
            Nodo<T> nuevo_nodo = new Nodo<T>
            {
                _datos = elemento,
                SiguienteNodo = null
            };


            //Si el inicio es nulo, lo agrego primero
            if (_inicio == null)
            {
                _inicio = nuevo_nodo;

            }
            else
            {
                //Busco el ultimo nodo vacio y lo agrego
                Nodo<T> aux = _inicio;
                while (aux.SiguienteNodo != null)
                {
                    aux = aux.SiguienteNodo;
                }
                aux.SiguienteNodo = nuevo_nodo;
            }



            _capacidad++;

        }

        //Elimina la posicion pasada por parametro
        public void Borrar(int pos)
        {
            //Si la posicion no es valida, salgo de la funcion
            if (pos <= 0 || pos > _capacidad) return;
            if (Es_vacia() || _inicio == null) return;
            //Caso 1: Posicion 0
            if (pos == 1)
            {
                //1) Muevo el segundo nodo al inicio
                //2) Libero el nodo que elimino
                _inicio = _inicio.SiguienteNodo;

            }
            else
            {
                //1) Busco el nodo que voy a eliminar.
                //2) Obtengo la direccion del nodo anterior
                //3) Obtengo la direccion del nodo siguiente
                //4) Conecto el nodo anterior con el siguiente

                Nodo<T>? aux = _inicio;

                for (int i = 0; i < pos - 2; i++)
                {
                    if (aux.SiguienteNodo == null) return;
                    aux = aux.SiguienteNodo;
                }

                //La variable "aux" esta una posicion anterior al nodo a eliminar

                //La variable "aux2" esta en la posicion siguiente al nodo a eliminar
                Nodo<T>? aux2 = aux.SiguienteNodo?.SiguienteNodo;

                //Conecto el nodo anterior con el siguiente
                aux.SiguienteNodo = aux2;
            }

            _capacidad--;
        }

        /*Borra un elemento de la lista. Recibe como parametro la lista y la clave a borrar
        En caso de tener claves repetidas borrara todas las ocurrencias*/
        public void BorrarTodos(T elemento)
        {

            if (Es_vacia() || _inicio == null) return;
            Nodo<T>? actual = _inicio;

            //Caso 1: El primer elemento se debe borrar
            if (actual != null && actual._datos!.Equals(elemento))
            {
                _inicio = actual.SiguienteNodo;
                actual = _inicio;
                _capacidad--;
            }

            //Caso 2: Se debe borrar otro elemento
            while (actual != null && actual.SiguienteNodo != null)
            {

                if (actual.SiguienteNodo._datos!.Equals(elemento))
                {
                    Nodo<T> temp = actual.SiguienteNodo; //Guardo el nodo anterior
                    //Debo borrar el nodo donde se encuentra el programa
                    actual.SiguienteNodo = temp.SiguienteNodo;
                    _capacidad--;
                }
                else
                {
                    actual = actual?.SiguienteNodo;
                }
            }
        }

        /* Busca un elemento en la lista recorriendola, si hay repetidos retorna la primer ocurrencia
           y si la clave a buscar no existe retorna NULL
        */
        public T? Buscar(T elemento)
        {
            //Validaciones
            if (Es_vacia()) return default;

            Nodo<T>? aux = _inicio;

            while (aux != null)
            {
                T? te = aux._datos;
                if (te != null && te.Equals(elemento)) return te;
                aux = aux.SiguienteNodo;
            }

            return default;
        }

        public void Insertar(T elemento, int pos)
        {
            if (Es_llena()) return;
            if (pos < 0 || pos >= _tamanio_maximo) return;

            //Creo el nodo nuevo
            Nodo<T> nuevo_nodo = new Nodo<T>
            {
                _datos = elemento,
                SiguienteNodo = null
            };



            Nodo<T>? aux = _inicio;

            //Caso 1: La posicion excede la cantidad de elementos, agrego al final
            if (pos >= _capacidad)
            {
                Agregar(elemento);
                return;
            }
            //Caso 2: Inserto en la posicion 0
            else if (pos == 0)
            {
                nuevo_nodo.SiguienteNodo = _inicio;
                _inicio = nuevo_nodo;

            }
            else
            {
                //Me muevo hasta llegar al nodo anterior a donde tengo que insertar
                for (int i = 0; i < pos - 1; i++)
                {
                    aux = aux?.SiguienteNodo;
                }

                //El nodo donde estoy debe apuntar al nuevo, y el nuevo a donde esta apuntando
                //el nodo anterior

                nuevo_nodo.SiguienteNodo = aux?.SiguienteNodo;
                if (aux == null) return;
                aux.SiguienteNodo = nuevo_nodo;

            }
            _capacidad++;
        }

        public T? Recuperar(int pos)
        {
            if (pos < 0 || pos >= _tamanio_maximo) return default;
            if (Es_vacia()) return default;

            Nodo<T>? actual = _inicio;

            for (int i = 0; i < pos; i++)
            {

                actual = actual?.SiguienteNodo;


            }
            if (actual == null) return default;
            return actual._datos;
        }

        public void Mostrar_lista()
        {
            Nodo<T>? aux = _inicio;
            Console.Write("Lista enlazada: [ ");
            while (aux != null)
            {
                if (aux._datos == null) break;
                Console.Write(aux._datos.ToString() + " ");
                aux = aux.SiguienteNodo;
            }
            Console.Write("]\n");

        }

        public T[] To_array()
        {
            T[] arreglo = new T[_capacidad];
            Nodo<T>? aux = _inicio;
            int i = 0;
            while (aux != null)
            {
                arreglo[i] = aux._datos!;
                aux = aux.SiguienteNodo;
                i++;
            }
            return arreglo;
        }



        public IEnumerator GetEnumerator()
        {
            return new ListaEnlazada_Iterador<T>(_inicio);
        }
    }



    file class ListaEnlazada_Iterador<T> : IEnumerator
    {

        private Nodo<T>? _posicion_actual;
        private readonly Nodo<T>? _inicio;


        public T? Posicion_Actual
        {
            get
            {
                if (_posicion_actual != null)
                {
                    return _posicion_actual._datos;
                }
                return default;
            }
        }



        public ListaEnlazada_Iterador(Nodo<T>? inicio)
        {
            _inicio = inicio;
            _posicion_actual = null;
        }


        public object? Current => Posicion_Actual;

        public bool MoveNext()
        {
            if (_posicion_actual == null)
            {
                _posicion_actual = _inicio;
            }
            else
            {
                _posicion_actual = _posicion_actual.SiguienteNodo;
            }
            return _posicion_actual != null;
        }

        public void Reset()
        {
            _posicion_actual = null;
        }
    }
}
