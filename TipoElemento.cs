namespace Estructuras_de_datos
{
    public class TipoElemento : ITipoElemento
    {
        private int _clave;
        private object? _valor;

        public TipoElemento(int clave)
        {
            _clave = clave;
            _valor = null;
        }

        public TipoElemento(int clave, object valor)
        {
            _clave = clave;
            _valor = valor;
        }

        //Getters
        public int GetClave() { return _clave; }
        public object? GetValor() { return _valor; }

       
    }
}
