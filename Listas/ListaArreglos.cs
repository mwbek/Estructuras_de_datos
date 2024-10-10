namespace Estructuras_de_datos.Listas
{







    public class ListaArreglos : IListas, IIterador
    {

        private TipoElemento[] _valores;
        private int _cantidad;
        private readonly int _tamanio_maximo;
        private int _posicion_actual;


        public ListaArreglos()
        {
            _valores = new TipoElemento[IListas.tamanio_maximo];
            _cantidad = 0;
            _tamanio_maximo = IListas.tamanio_maximo;
        }

        //Sobrecarga de constructor
        public ListaArreglos(int longitud)
        {
            _valores = new TipoElemento[longitud];
            _cantidad = 0;
            _tamanio_maximo = longitud;
        }

        public bool es_llena()
        {
            return _cantidad == _tamanio_maximo;
        }
        public bool es_vacia()
        {
            return _cantidad == 0;
        }
        public int longitud()
        {
            return _cantidad;
        }


        /// <summary>
        /// Agrega un elemento al final de la lista
        /// </summary>
        /// <param name="elemento"></param>
        public void agregar(TipoElemento elemento)
        {
            if (es_llena() == false)
            {
                _valores[_cantidad] = elemento;
                _cantidad++;
            }
        }

        /// <summary>
        /// Elimina todas las ocurrencias de una clave
        /// </summary>
        /// <param name="clave"></param>
        public void borrar(int clave)
        {
            if (es_llena()) return;

            int pos = 0;

            while (pos < _cantidad)
            {

                TipoElemento x = _valores[pos];
                //Si la clave coincido, borro y muevo todo una posicion atras
                if (x.Clave == clave)
                {
                    //Desplazo todo el arreglo a la izquierda
                    for (int i = pos; i < _cantidad - 1; i++)
                    {
                        _valores[i] = _valores[i + 1];

                    }
                    _cantidad--;
                }
                else
                {
                    pos++;
                }
            }
        }

        /// <summary>
        /// Busca un elemento en la lista recorriendola 
        /// </summary>
        /// <param name="clave"></param>
        /// <returns>
        /// Si hay repetidos retorna la primer ocurrencia
        /// Si la clave a buscar no existe retorna NULL
        /// </returns>
        /// <exception cref="NotImplementedException"></exception>
        public TipoElemento? buscar(int clave)
        {
            if (es_vacia() == false)
            {
                for (int i = 0; i < _cantidad; i++)
                {
                    TipoElemento x = _valores[i];

                    if (x.Clave == clave) return x;
                }
            }

            return null;
        }

        /// <summary>
        ///     Elimina una posicion de la lista sin importar el dato que esta en esa posicion
        ///     A diferencia de la funcion "borrar" este no borra todas las ocurrencias
        /// </summary>
        /// <param name="pos"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void eliminar(int pos)
        {

            if (es_vacia() == false)
            {
                if (pos >= 0 && pos < _cantidad)
                {
                    for (int i = pos; i < _cantidad - 1; i++)
                    {
                        _valores[i] = _valores[i + 1];
                    }
                    _cantidad--;
                }
            }
        }

        public void insertar(TipoElemento elemento, int pos)
        {

            if (es_llena() == false)
            {
                if (pos >= 0 && pos <= _tamanio_maximo)
                {
                    for (int i = _cantidad - 1; i >= pos && i >= 0; i--)
                    {
                        _valores[i + 1] = _valores[i];
                    }
                    _valores[pos] = elemento;
                    _cantidad++;
                }

            }

        }


        /// <summary>
        /// Retorna el elemento en la posicion pasada por parametro
        /// </summary>
        /// <param name="pos"></param>
        /// <returns></returns>
        public TipoElemento? recuperar(int pos)
        {
            if (es_vacia() == false)
            {
                if (pos >= 0 && pos < _cantidad)
                {
                    return _valores[pos];
                }
            }
            return null;
        }

        public void mostrar_lista()
        {
            if (_cantidad > 0)
            {
                Console.Write("Lista de arreglos: [ ");
                for (int i = 0; i < _cantidad; i++)
                {
                    Console.Write(_valores[i].Clave + " ");
                }
                Console.Write("]\n");
            }
        }

        //Funcion iterador
        public bool hay_siguiente()
        {
            //Mientras la posicion actual sea menor a la longitud significa que hay otro elemento mas
            return _posicion_actual < _cantidad;
        }

        public TipoElemento? siguiente()
        {
            return _valores[_posicion_actual++];
        }

    }
}
