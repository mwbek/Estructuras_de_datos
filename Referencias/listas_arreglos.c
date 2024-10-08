//Librerias TAD
#include "listas.h"
//Librerias TAD"
#include <stdio.h>
#include <stdlib.h>
#include <stdbool.h>

//Constante de tamanio maximo para la lista
static const int TAMANIO_MAXIMO = 100;
/*
    Defino los elementos de las estructuras ListaRep y IteradorRep
*/

struct ListaRep
{
    TipoElemento *valores;  //Este va a ser el vector/array del "TipoElemento"
    int cantidad;           //Va a mantener la cantidad actual de elementos en la lista
};

struct IteradorRep
{
    Lista lista; //Guarda la lista que se le pase
    int posicionActual; //Mantiene la posicion de cada iteracion
};





/*
    Crea la lista vacia. Retorna el puntero de la misma
*/
Lista l_crear()
{
    //Asigno un espacio en memoria a la nueva lista
    Lista nueva_lista = (Lista) malloc(sizeof(struct ListaRep));
    //Inicializo la lista con el tamanio maximo y le paso el tamanio en bytes del "TipoElemento"
    nueva_lista->valores = calloc(TAMANIO_MAXIMO, sizeof(TipoElemento));
    //La lista a pesar de tener un tamanio maximo de 100 elementos, empieza con 0
    nueva_lista->cantidad = 0;

    //Retorno la lista
    return nueva_lista;
}

/*
    Determina si la lista esta vacia. Retorna "True" cuando esta vacia, "false" en caso contrario
*/
bool l_es_vacia(Lista lista)
{
    //Si la cantidad de items en la lista es 0 retorna "True"
    return lista->cantidad == 0;
}

/*
    Determina si la lista esta llena. Retorna "True" si lo esta, "False" en caso contrario
*/
bool l_es_llena(Lista lista)
{   
    //Si la cantidad de items en la lista es igual al tamanio maximo retorna "true"
    return lista->cantidad == TAMANIO_MAXIMO;

}

/*
    Determina la cantidad de elementos de la lista.
*/
int l_longitud(Lista lista)
{
    //Retorno la cantidad de items que hay actualmente en la lista
    return lista->cantidad;
}

/*
    Agrega un elemento al final de la lista, incrementando la cantidad de elementos de la misma
*/
void l_agregar(Lista lista, TipoElemento elemento)
{
    //Validacion por si la lista esta llena
    if (l_es_llena(lista) != true){
        //Si a la lista le queda espacio agrego un elemento mas al final
        lista->valores[lista->cantidad] = elemento;
        //Sumo uno a la cantidad de la lista
        lista->cantidad++;
    }

}

/*
    Borra un elemento de la lista. Recibe como parametro la lista y la clave a borrar
    En caso de tener claves repetidas borrara todas las ocurrencias
*/
void l_borrar(Lista lista, int clave)
{
    //Si la lista esta vacia sale de la funcion
    if (l_es_vacia(lista) == true)
    {
        return;
    }

    //Si la lista no esta vacia

    //Me paro en la primera posicion de la lista
    int pos = 0;
    int i; //Variable que uso para el FOR
    //Recorro toda la lista
    while (pos < lista->cantidad)
    {
        //Si la clave del valor hallado en la lista es igual a la clave pasada por parametro se entra al IF
        if (lista->valores[pos]->clave == clave)
        {
            //Recorro desde la ultima posicion hasta el final de la lista
            for (i = pos; i < lista->cantidad - 1; i++)
            {
                //Muevo para atras los valores
                lista->valores[i] = lista->valores[i+1];
            }
            //Elimino un elemento de la lista
            lista->cantidad--;
        }
        else
        {
            //Paso a la siguiente posicion
            pos++;
        }
    }


}

/*
    Busca un elemento en la lista recorriendola
    Si hay repetidos retorna la primer ocurrencia
    Si la clave a buscar no existe retorna NULL
*/
TipoElemento l_buscar(Lista lista, int clave)
{

    int pos = 0;

    //Con el while recorro toda la lista (si es necesario)
    while (pos < lista->cantidad)
    {
        if (lista->valores[pos]->clave == clave)
        {
            return lista->valores[pos];
        }
        pos++;
    }
    //Si sale del while es porque no encontro nada, por lo tanto retorno NULL
    return NULL;

}

/*
    Inserta un elemento en la posicion recibida por parametro
*/
void l_insertar(Lista lista, TipoElemento elemento, int pos)
{
    int i;
    //Recorro la lista al reves. Empiezo del ultimo elemento hasta la posicion en la que tengo que insertar
    //En este sentido <--- voy corriendo los valores de derecha a izquierda. 
    for (i = lista->cantidad; i >= pos && i > 0; i--)
    {
        //Voy corriendo cada elemento a la DERECHA.
        //La lista se recorre de derecha a izquierda <--- pero los elementos se mueven a la derecha --->
        /*
            Por ejemplo, si el ultimo elemento esta en la posicion 5
            se mueve a la posicion 6 y asi hasta la posicion que
            hay que insertar un nuevo elemento
        */
        lista->valores[i] = lista->valores[i-1];
    }
    //Pongo en la ultima posicion que quedo el elemento a insertar
    lista->valores[pos - 1] = elemento;
    lista->cantidad++;

}

/*
    Elimina una posicion de la lista sin importar el dato que esta en esa posicion
    A diferencia de la funcion "borrar" este no borra todas las ocurrencias
*/
void l_eliminar(Lista lista, int pos)
{
    int i;
    //Valido que la posicion que se quiere eliminar exista en la lista
    if (pos >= 1 && pos <= l_longitud(lista))
    {
        /*
            Recorro la lista de derecha a izquierda
            termino aca <---empiezo aca
            y para eliminar un elemento lo piso con el siguiente
            y voy desplazando los demas elementos una casilla a la izquierda
        */

        for (i = pos -1; i<lista->cantidad; i++)
        {
            //Voy moviendo los valores de derecha a izquierda

            /*
                Por ejemplo,
            */
            lista->valores[i] = lista->valores[i+1];
        }
        //Resto 1 a la cantidad
        lista->cantidad--;

    }

}

/*
    Retorna el elemento (dato) de la posicion recibida por parametro
*/
TipoElemento l_recuperar(Lista lista, int pos)
{
    return lista->valores[pos-1];
}

/*
    Muestra las CLAVES por pantalla
*/
void l_mostrarLista(Lista lista)
{
    int i;
    printf("Contenido de la lista");
    for (i = 0; i < lista->cantidad;i++)
    {
        printf(" %d", lista->valores[i]->clave);
    }
    printf("\n");
}


// Funciones de iterador de la lista

/*
    Inicializa el iterador para poder hacer un recorrido de la lista
*/
Iterador iterador(Lista lista)
{
    Iterador iter = (Iterador) malloc(sizeof(struct IteradorRep));
    iter->lista = lista;
    iter->posicionActual =0;
    return iter;
}

/*
    Retorna "True" si quedan elementos por recorrer, caso contrario "False"
*/
bool hay_siguiente(Iterador iterador)
{
    //Compara la posicion actual con la cantidad total.
    //Si la posicion actual es menor significa que hay un valor
    //siguiente por lo que devolveria "True"
    return iterador->posicionActual < iterador->lista->cantidad;
}

/*
    Retorna el proximo elemento de la lista
*/
TipoElemento siguiente(Iterador iterador)
{
    return iterador->lista->valores[iterador->posicionActual++];
}
