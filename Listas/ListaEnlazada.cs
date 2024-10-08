namespace Estructuras_de_datos.Listas
{


    public class ListaEnlazada : IListas
    {
        private class Nodo
        {
            public TipoElemento? _datos;
            public Nodo? _siguiente;
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

        public void borrar(int clave)
        {
            throw new NotImplementedException();
        }

        public TipoElemento? buscar(int clave)
        {
            throw new NotImplementedException();
        }

        public void insertar(TipoElemento elemento, int pos)
        {
            throw new NotImplementedException();
        }

        //Elimina la posicion pasada por parametro
        public void eliminar(int pos)
        {
            //Si la posicion no es valida, salgo de la funcion
            if (pos < 0 || pos >= _cantidad) return;




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
                Console.Write(aux._datos.GetClave() + " ");
                aux = aux._siguiente;
            }
            Console.Write("]\n");
            
        }
    }
}
