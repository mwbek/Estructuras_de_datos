//Librerias TAD
#include "listas.h"
//Librerias TAD

#include <stdio.h>
#include <stdlib.h>
#include <stdbool.h>

//Constante de tamanio maximo para la lista
static const int TAMANIO_MAXIMO = 101;
static const int NULO = -1;
/*
    Defino los elementos de las estructuras ListaRep y IteradorRep
*/

struct Nodo 
{
    TipoElemento datos;
    /*
        El siguiente, a diferencia de la lista con apuntadores, es un entero. 
        Esto es debido a que como se usa un vector para simular la memoria
        no guarda direcciones de memoria para saber donde esta el siguiente nodo
        sino que guarda indices del vector
    */
    int siguiente; //Indica la posicion del siguiente nodo
};

//Estructura de lsita
struct ListaRep
{
    struct Nodo *cursor; //Es un vector que contiene nodos
    int inicio; 
    int libre; //contiene los nodos libres para usar
    int cantidad;  //Va a mantener la cantidad actual de elementos en la lista
};

struct IteradorRep
{
    Lista lista;
    int posicionActual;
};


/*
  ┌───────┬─────────────────┬──────────────┐
  │       │                 │              │
  │INDICE │      DATO       │   SIGUIENTE  │ - Cada casillero del vector es un NODO
  │       │ (TipoElemento)  │              │ - Se encadenan todos los nodos como "libres" al crear la lista
  │       │                 │              │ - Libre apunta al primer nodo libre ("0" cuando la lista se crea)
  ├───────┼─────────────────┼──────────────┤ - El siguiente del ultimo nodo libre siempre apunta a NULO
  │       │                 │              │   (-1 en este caso porque los enteros no pueden manejar NULL)
  │   0   │     Dato 1      │      1       │
  │       │                 │              │
  ├───────┼─────────────────┼──────────────┤
  │       │                 │              │
  │   1   │     Dato 2      │      2       │
  │       │                 │              │
  ├───────┼─────────────────┼──────────────┤
  │       │                 │              │
  │   2   │     Dato 3      │      3       │
  │       │                 │              │
  ├───────┼─────────────────┼──────────────┤
  │       │                 │              │
  │   3   │     Dato 4      │      4       │
  │       │                 │              │
  ├───────┼─────────────────┼──────────────┤
  │       │                 │              │
  │   4   │     Dato 5      │      5       │
  │       │                 │              │
  ├───────┼─────────────────┼──────────────┤
  │       │                 │              │
  │   5   │     Dato 6      │    NULO      │
  │       │                 │              │
  └───────┴─────────────────┴──────────────┘

    EL CURSOR CONTIENE DOS LISTAS, UNA CON NODOS LIBRES Y OTRA CON DATOS
    
    - El cursor no es un array, el cursor es un espacio en memoria (simulado con un array)
      que contiene 2 listas

                                        CURSOR
┌───────────────────────────────────────────────┐
│    Inicio                                     │
│   ┌────────┐     ┌────────┐     ┌────────┐    │
│   │dato    ├─────┤dato    ├─────┤dato    │    │
│   └────────┘     └────────┘     └────────┘    │
│                                paso nodos  │  │
│ ▲                              que ya no   │  │
│ │   Libre                      se usan     │  │
│ │ ┌────────┐     ┌────────┐     ┌────────┐ ▼  │
│ │ │        ├─────┤        ├─────┤        │    │
│   └────────┘     └────────┘     └────────┘    │
│  paso nodos libres para usar                  │
└───────────────────────────────────────────────┘
Cuando agrego/inserto un dato el vector de libres
le pasa un nodo al vector de datos

Cuando elimino un dato el vector de datos le devuelve
los nodos a libre    


*/


/*
    Crea la lista vacia. Retorna el puntero de la misma
*/
Lista l_crear()
{
    int i;

    //Creo la LISTA la cual va a contener los datos y el siguiente nodo
    Lista nueva_lista = (Lista) malloc(sizeof(struct ListaRep));
    //Creo el CURSOR que va a contener a la lista
    nueva_lista->cursor = calloc(TAMANIO_MAXIMO, sizeof(struct Nodo));
    nueva_lista->cantidad = 0;

    //Encadeno todos los libres
    for (i = 0; i < TAMANIO_MAXIMO-2; i++)
    {
        nueva_lista->cursor[i].siguiente = i + 1;
    }
    
    //Al marcar el primer cursor como libre, se marcan todos los demas como libres
    nueva_lista->libre = 0; 
    nueva_lista->inicio = NULO; //El puntero de la lista no apunta a nada
    nueva_lista->cursor[TAMANIO_MAXIMO-1].siguiente = NULO; //El proximo casillero del ultimo cursor apunta a nulo

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
    int p; //Guarda el indice del nodo libre a usar
    //int q; //Guarda los indices de los nodos usados en la lista

    //Si la lista es llena se sale de la funcion
    if (l_es_llena(lista) == true) return;

    
    p = lista->libre; //Le pido a la lista que me de la posicion (indice) del primer nodo libre que encuentre

    /*
        Cambio el indice del siguiente libre, sino pierdo en enlace (o encadenamiento) con todos los demas libres.
        Por ejemplo, yo agarre el libre en el indice 2, como voy a usar ese libre para ponerle un dato
        tengo que pasar el libre en el indice 3 como el primer libre disponible
    */
    lista->libre = lista->cursor[p].siguiente;
    
    //Cargo los datos
    lista->cursor[p].datos = elemento; //Asigno el dato
    lista->cursor[p].siguiente = NULO; //Como agrego un dato AL FINAL de la lista, el siguiente al ultimo es NULO (-1)

    //Controlo que no sea el primero de la lista
    if (lista->inicio == NULO)
    {
        //Si es el primer elemento lo pone como inicio
        lista->inicio = p;
    }
    else
    {
        
        int q = lista->inicio; //Guardo la ubicacion del primer nodo
        //y hago un while para buscar el ultimo nodo
        while (lista->cursor[q].siguiente != NULO)
        {
            q = lista->cursor[q].siguiente;
        }
        //Una vez que tengo el ultimo nodo lo conecto al ultimo
        lista->cursor[q].siguiente = p; //Lo conecto con el ultimo
    }
    lista->cantidad++;

}


/*
    Inserta un elemento en la posicion recibida por parametro
*/
void l_insertar(Lista lista, TipoElemento elemento, int pos)
{
    int i;

    //Controlo que la lista no este llena
    if (l_es_llena(lista) == true) return;

    int p = lista->libre; //Tomo el primer nodo libre
    //Actualizo el siguiente nodo libre para pasarlo a primer nodo libre
    lista->libre = lista->cursor[p].siguiente;
    //Cargo lso datos en el nodo libre que acabo de pedir
    lista->cursor[p].datos = elemento;
    lista->cursor[p].siguiente = NULO;

    //Inserto el nodo nuevo

    //Controla si cambia el inicio
    if (pos == 1)
    {   
        /*
            Si lo tengo que insertar en la primer posicion, actualizo a donde apunta el nodo de inicio
            y luego remplazo por el nuevo nodo. Entonces el nodo nuevo apunta al ex-inicio
        */
        
        lista->cursor[p].siguiente = lista->inicio;
        lista->inicio = p;
    }
    else
    {
        //La variable TEMP tiene el indice de donde arrancan los nodos con datos
        int temp = lista->inicio;
        //Recorro toda la lista y me paro 1 posicion antes que la que tengo que insertar 
        //(Mismo procedimiento que con punteros)
        for (i = 0; i < pos - 2; i++)
        {
            temp = lista->cursor[temp].siguiente;
        }
        /*
            El nuevo nodo (p) apunta al siguiente de donde esta parado temp.
            Por ejemplo, si TEMP = 2 el nuevo nodo apunta a 3
        */
        lista->cursor[p].siguiente = lista->cursor[temp].siguiente;
        //Una vez que el nodo nuevo (p) apunta a 3 puedo hacer que temp apunte al nodo nuevo (p)
        lista->cursor[temp].siguiente = p;
    }
    lista->cantidad++;
}

/*
    Elimina una posicion de la lista sin importar el dato que esta en esa posicion
    A diferencia de la funcion "borrar" este no borra todas las ocurrencias
*/
void l_eliminar(Lista lista, int pos)
{
    if (l_es_vacia(lista) == true) return;

    int p,q,i;
    int actual = lista->inicio; //Guardo el indice del primer nodo

    if (pos >= 1 && pos <= l_longitud(lista))
    {
        if (pos == 1)
        {
            p = actual; //Guardo el indice del primer nodo en "p"
            lista->inicio = lista->cursor[actual].siguiente;
            lista->cursor[p].siguiente = lista->libre;
            lista->libre = p; //Devuelvo al libre el nodo que elimine
        }
        else
        {
            for (i = 0; i < pos - 2; i++)
            {
                /*
                    Voy recorriendo toda la lista, pasando de nodo en nodo
                    hasta llegar a la posicion anterior de la que me pasaron por parametro
                */
                //Cuando el bucle termine de recorrer, "Actual" va a apuntar al nodo en posicion (pos -1)
                actual = lista->cursor[actual].siguiente;
            }

            /*
                "p" guarda el indice del siguiente nodo en el que se paro actual. Ejemplo:
                Si tengo que eliminar el nodo 3, "actual" en este punto vale 2,
                por lo tanto con la siguiente instruccion "p" va a valer 3
                que 3 es la "pos" que pasaron por parametro
            */
            p = lista->cursor[actual].siguiente; //nodo en "pos"
            /*
                Ahora pido el siguiente de "p", es decir, si volvemos a "actual" 
                se pidio el siguiente del siguiente, por lo tanto
                el nodo de actual ahora apunta a 4,que seria el siguiente del siguiente de "actual"\
                cuando valia 2
            */
            lista->cursor[actual].siguiente = lista->cursor[p].siguiente; //nodo en (pos + 1)

            //Ahora tengo que pasar el nodo que estaba en la posicion 3 al vector de nodos libres


            //El nodo que elimine ahora apunta al que era el primer nodo libre
            lista->cursor[lista->libre].siguiente = p;
            //Y agrego al vector de libres el nodo eliminado
            lista->libre = p;  
        }
        lista->cantidad--;
    }
    
}


/*
    Muestra las CLAVES por pantalla
    NO FUNCIONA
*/
void l_mostrarLista(Lista lista)
{
    int q; //Va a guardar el nodo actual
    q = lista->inicio; //Guardo la ubicacion del primer nodo
    printf("Contenido de la lista: ");

    while (q != NULO)
    {
        printf("%d ", lista->cursor[q].datos->clave);
        //"q" guarda la posicion del siguiente nodo
        q = lista->cursor[q].siguiente;
    }

    printf("\n");
    

}

/*
    Busca un elemento en la lista recorriendola
    Si hay repetidos retorna la primer ocurrencia
    Si la clave a buscar no existe retorna NULL
*/
TipoElemento l_buscar(Lista lista, int clave)
{
    int q; //Va a guardar el inicio de la lista
    q = lista->inicio; //Esto va a devolver 0

    while (q != NULO)
    {
        if (lista->cursor[q].datos->clave == clave)
        {
            return lista->cursor[q].datos;
        }
        //Si no encuentra la clave, paso al siguiente nodo
        q = lista->cursor[q].siguiente;
    }
    //Si la clave no esta en la lista retorna NULL
    return NULL;
}

/*
    Retorna el elemento (dato) de la posicion recibida por parametro
*/
TipoElemento l_recuperar(Lista lista, int pos)
{
    int i;
    int actual; //Va a guardar la primera posicion de la lista

    actual = lista->inicio;

    for (i = 0; i < pos-1; i++)
    {
        actual = lista->cursor[actual].siguiente;
    }
    return lista->cursor[actual].datos;
}


/*
    Borra un elemento de la lista. Recibe como parametro la lista y la clave a borrar
    En caso de tener claves repetidas borrara todas las ocurrencias
*/
void l_borrar(Lista lista, int clave)
{
    if (l_es_vacia(lista) == true) return;

    int q;
    //Guardo el comienzo de la lista
    int p = lista->inicio;

    //Borro las claves que coinciden con el inicio
    while ((p != NULO) && (lista->cursor[p].datos->clave == clave))
    {
        //"q" guarda el nodo que voy a eliminar para despues liberarlo
        q = p; 
        //actualizo a donde punta el nodo de inicio
        lista->inicio = lista->cursor[p].siguiente;

        //Recupero el nodo en el libre para no perderlo
        //Actualizo el "siguiente" del nuevo nodo libre (el que acabo de eliminar)
        lista->cursor[q].siguiente = lista->libre;
        //Lo devuelvo a la lista de nodos libres
        lista->libre = q; //Libero el nodo que guarde en "q"

        //Descuento 1 y arranco de nuevo desde el inicio
        lista->cantidad--;

        //Vuelvo a intentar desde el inicio
        p = lista->inicio;
    }

    //Borro las claves en el resto de la lista
    p = lista->inicio; //Nodo actual --> indice "0"
    while ((q != NULO) && (lista->cursor[q].siguiente != NULO))
    {
        //Hacer nosotros
        //........... similar a punteros. NO OLVIDAR ENCADENAR EL LIBRE

        if (lista->cursor[p].datos->clave == clave)
        {
            p = lista->cursor[q].siguiente; //Posicion del nodo a eliminar
            lista->cursor[q].siguiente = lista->cursor[p].siguiente; //Posicion del siguiente nodo al nodo eliminar
            lista->cursor[lista->libre].siguiente = p;
            lista->libre = p;
            lista->cantidad--;
        }
        else
        {
            q = p; //Guardo el nodo anterior
            p = lista->cursor[p].siguiente; //Paso al siguiente nodo    
        }



    }

}

// Funciones de iterador de la lista

/*
    Inicializa el iterador para poder hacer un recorrido de la lista
*/
Iterador iterador(Lista lista)
{
    Iterador iter = (Iterador) malloc(sizeof(struct IteradorRep));
    iter->lista = lista;
    iter->posicionActual =lista->inicio;
    return iter;
}

/*
    Retorna "True" si quedan elementos por recorrer, caso contrario "False"
*/
bool hay_siguiente(Iterador iterador)
{   
    //NO SE SI FUNCIONA
    //Si es distinto de NULL significa que hay un elemento mas y devuelve "true"
    return (iterador->posicionActual != NULO);
}

/*
    Retorna el proximo elemento de la lista
    NO FUNCIONA
*/
TipoElemento siguiente(Iterador iterador)
{
    int pos = iterador->posicionActual;
    TipoElemento actual = iterador->lista->cursor[pos].datos;
    iterador->posicionActual = iterador->lista->cursor[pos].siguiente;
    return actual;
}
