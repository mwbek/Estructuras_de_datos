namespace Estructuras_de_datos
{
    public class TipoElemento
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
        public int Clave { get { return _clave; } }
        public object? Valor {  get { return _valor; } }

       
    }
}
