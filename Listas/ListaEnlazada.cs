namespace Estructuras_de_datos.Listas
{


    public class ListaEnlazada : IListas
    {
        private class Nodo
        {
            public TipoElemento? _datos;
            public Nodo? _siguiente;

            ~Nodo() { }
        }


        private int _cantidad;
        private readonly int _tamanio_maximo;
        private Nodo? _inicio;

        //Contructor de la lista
        public ListaEnlazada()
        {
            _cantidad = 0;
            _tamanio_maximo = IListas.tamanio_maximo;
            _inicio = null;
        }

        public bool es_vacia()
        {
            return _cantidad == 0;
        }

        public bool es_llena()
        {
            return _cantidad == _tamanio_maximo;
        }

        public int longitud()
        {
            return _cantidad;
        }

        public void agregar(TipoElemento elemento)
        {
            if (es_llena()) return;

            //Creo un nodo nuevo
            Nodo nuevo_nodo = new Nodo();
            nuevo_nodo._datos = elemento;
            nuevo_nodo._siguiente = null;

            //Si el inicio es nulo, lo agrego primero
            if(_inicio == null) _inicio = nuevo_nodo;
            else
            {
                //Busco el ultimo nodo vacio y lo agrego
                Nodo aux = _inicio;
                while (aux._siguiente != null)
                {
                    aux = aux._siguiente;
                }
                aux._siguiente = nuevo_nodo;
            }

            _cantidad++;

        }

        //Elimina la posicion pasada por parametro
        public void eliminar(int pos)
        {
            //Si la posicion no es valida, salgo de la funcion
            if (pos < 0 || pos >= _cantidad) return;
            if (es_vacia() || _inicio == null) return;
            //Caso 1: Posicion 0
            if (pos == 0)
            {
                //1) Muevo el segundo nodo al inicio
                //2) Libero el nodo que elimino
                _inicio = _inicio._siguiente;

            }
            else
            {
                //1) Busco el nodo que voy a eliminar.
                //2) Obtengo la direccion del nodo anterior
                //3) Obtengo la direccion del nodo siguiente
                //4) Conecto el nodo anterior con el siguiente

                Nodo? aux = _inicio;

                for (int i = 0; i < pos - 1; i++)
                {
                    if (aux._siguiente == null) return;
                    aux = aux._siguiente;
                }

                //La variable "aux" esta una posicion anterior al nodo a eliminar

                //La variable "aux2" esta en la posicion siguiente al nodo a eliminar
                Nodo? aux2 = aux._siguiente?._siguiente;

                //Conecto el nodo anterior con el siguiente
                aux._siguiente = aux2;
            }

            _cantidad--;
        }

        /*
        Borra un elemento de la lista. Recibe como parametro la lista y la clave a borrar
        En caso de tener claves repetidas borrara todas las ocurrencias
        */
        public void borrar(int clave)
        {

            if(es_vacia()) return;

            Nodo? actual = _inicio;

            //Caso 1: El primer elemento se debe borrar
            if(actual?._datos?.Clave == clave)
            {
                _inicio = actual._siguiente;
                actual = _inicio;
            }

            //Caso 2: Se debe borrar otro elemento
            while (actual != null)
            {
                Nodo? temp = actual; //Guardo el nodo anterior en caso de entrar al IF
                if(actual?._siguiente?._datos?.Clave == clave)
                {
                    //Debo borrar el nodo donde se encuentra el programa
                    actual = actual._siguiente; //Este desaparece
                    temp._siguiente = actual._siguiente;
                }

                actual = actual?._siguiente;
            }

            


            _cantidad--;

        }

        /* Busca un elemento en la lista recorriendola, si hay repetidos retorna la primer ocurrencia
           y si la clave a buscar no existe retorna NULL
        */

        public TipoElemento? buscar(int clave)
        {
            //Validaciones
            if(es_vacia()) return null;

            Nodo? aux = _inicio;

            while(aux != null)
            {
                TipoElemento? te = aux._datos;
                if (te?.Clave == clave) return te;
                aux = aux._siguiente;
            }

            return null;
        }

        public void insertar(TipoElemento elemento, int pos)
        {
            throw new NotImplementedException();
        }


        public TipoElemento? recuperar(int pos)
        {
            throw new NotImplementedException();
        }

        public void mostrar_lista()
        {
            Nodo? aux = _inicio;
            Console.Write("Lista enlazada: [ ");
            while (aux != null)
            {
                if (aux._datos == null) break;
                Console.Write(aux._datos.Clave + " ");
                aux = aux._siguiente;
            }
            Console.Write("]\n");
            
        }
    }
}
