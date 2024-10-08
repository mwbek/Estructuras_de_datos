//Librerias TAD
#include "listas.h"
//Librerias TAD
#include <stdio.h>
#include <stdlib.h>
#include <stdbool.h>

//Constante de tamanio maximo para la lista
static const int TAMANIO_MAXIMO = 1000000;
/*
    Defino los elementos de las estructuras ListaRep y IteradorRep
*/

/*
    Estructura que va a ir guardando los datos y 
    un lazo (nodo) al siguiente elemento de la lsita
*/
struct Nodo 
{
    TipoElemento datos;
    //Definicion RECURSIVA del tipo de dato
    struct Nodo *siguiente; //Indica la posicion del siguiente nodo
};

/*
    Estructura que va a hacer de lista
*/
struct ListaRep
{
    struct Nodo *inicio; //puntero AL PRIMER NODO de la lista. Luego cada nodo se encadena
    int cantidad;  //Va a mantener la cantidad actual de elementos en la lista
};

struct IteradorRep
{
    /*
        El iterador cambia. En el caso de los vectores antes tenia que recibir la lista 
        y en este caso solo recibe el PUNTERO AL PRIMER NODO. Luego ira recorriendo los nodos
    */
    struct Nodo *posicionActual;
};





/*
    Crea la lista vacia. Retorna el puntero de la misma
*/
Lista l_crear()
{
    /*
        Creo la lista enlazada y se "conectan" la cantidad maxima de nodos establecidas,
        en este caso son 100 nodos como maximo. A pesar que se crean los 100 nodos y se conectan
        entre si, estos no ocupan espacio en memoria porque estan vacios
    */
    Lista nueva_lista = (Lista) malloc(sizeof(struct ListaRep));

    //Se indica que el primer nodo apunta a NULL porque la lista esta vacia
    nueva_lista->inicio = NULL;
    nueva_lista->cantidad = 0;
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
    //Si la lista es llena se sale de la funcion
    if (l_es_llena(lista) == true) return;

    //Creo un nodo nuevo
    struct Nodo *nuevo_nodo = malloc(sizeof(struct Nodo));
    //Le asigno el dato
    nuevo_nodo->datos = elemento;
    /*
        Le asigno cual va a ser el nodo siguiente, 
        pero como esta funcion agrega el dato al final de todo no va a tener un nodo siguiente
    */
    nuevo_nodo->siguiente = NULL;

    //Verifica si es el primer nodo de la lista
    if (lista->inicio == NULL)
    {
        //Si la lista tiene como inicio NULL (significa que esta vacia) agrega el nuevo nodo como inicio
        lista->inicio = nuevo_nodo;
    }
    else
    {
        /*
            Si no es el primer nodo de la lista creo un nodo temporal
            el cual va contener todos los nodos desde la posicion de inicio hasta el ultimo
        */
        struct Nodo *temp2 = lista->inicio;
        //Recorro TODOS los nodos hasta encontrar el ultimo
        while (temp2->siguiente != NULL)
        {
            //Si el nodo actual no es el ultimo se pasa al siguiente
            temp2 = temp2->siguiente;
        }
        /*
            Una vez que salio del bucle significa que encontro el ultimo nodo
            y le asigna AL SIGUIENTE (un nodo "vacio") la direccion de memoria del nodo
            recien creado
        */
        temp2->siguiente = nuevo_nodo;
    }
    //Sumo uno a la cantidad porque se agrego un nuevo elemento/nodo
    lista->cantidad++;

}

/*
    Borra un elemento de la lista. Recibe como parametro la lista y la clave a borrar
    En caso de tener claves repetidas borrara todas las ocurrencias
*/
void l_borrar(Lista lista, int clave)
{
    if (l_es_vacia(lista) == true) return;

    //Copio la lista desde el primer nodo
    struct Nodo *actual = lista->inicio;

    //Mientras no se haya llegado al final de la lista y las claves coincidan
    //se borra el elemento y todas sus repeticiones
    while (actual != NULL && actual->datos->clave == clave)
    {
        lista->inicio = actual->siguiente;
        free(actual); //Libera el espacio en memoria del nodo que elimine
        lista->cantidad--;
        actual = lista->inicio;
    }

    while (actual != NULL && actual->siguiente != NULL)
    {
        if (actual->siguiente->datos->clave == clave)
        {
            //Guarda el siguiente nodo del actual
            struct Nodo *temp = actual->siguiente;
            //Actualiza en el nodo "actual" la ubicacion del siguiente nodo
            actual->siguiente = temp->siguiente;
            free(temp);
            lista->cantidad--;
        }
        else
        {
            actual = actual->siguiente;
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
    //Copio la lista
    struct Nodo *actual = lista->inicio;

    //Recorro toda la lista hasta el ultimo nodo
    while (actual != NULL)
    {
        //Si llego a encontrar la clave pedida antes de recorrer toda la lista
        //Retorno el dato y salgo de la fucion
        if (actual->datos->clave == clave)
        {
            return actual->datos;
        }
        //Pasa a la siguiente posicion de la lista
        actual = actual->siguiente;
    }
    //Si no encontro el dato retorna null
    return NULL;
}

/*
    Inserta un elemento en la posicion recibida por parametro
*/
void l_insertar(Lista lista, TipoElemento elemento, int pos)
{
    int i;

    if (l_es_llena(lista) == true) return;

    //Crea un nuevo nodo y le asigna espacio en memoria
    struct Nodo *nuevo_nodo = malloc(sizeof(struct Nodo));
    //Le carga el dato elemento
    nuevo_nodo->datos = elemento;
    /*
        Y le carga la ubicacion del siguiente nodo.
        Como por ahora no se inserto en ninguna ubicacion
        no tiene nodo siguiente
    */
    nuevo_nodo->siguiente = NULL;

    //Comprueba si se quiere insertar en la primer posicion
    if (pos == 1)
    {
        /*
            En caso que se quiera insertar en la primer posicion
            el nuevo nodo guarda los datos nuevos y apunta a la direccion del nodo "inicio",
            luego en la lista se modifica el nodo de inicio por el nuevo nodo
       
        */
        nuevo_nodo->siguiente = lista->inicio;
        lista->inicio = nuevo_nodo;
    }
    else
    {
        /*
            Copio el nodo a una variable temporal y guardo desde el primer nodo al ultimo
            es decir, copio la lista entera
        */

        struct Nodo *temp2 = lista->inicio;
        /*
            Voy recorriendo la lista entera hasta llegar al nodo/posicion 
            pasada por parametro
        */
        for (i = 0; i < pos -2; i++)
        {
            //Si no es la posicion pasada por parametro paso al siguiente nodo
            temp2 = temp2->siguiente;
        }
        //Inserto el nodo en la lista

        //1) guardo la direccion del nodo que voy a mover en el nodo nuevo
        nuevo_nodo->siguiente = temp2->siguiente;
        //2) remplazo el nodo original de la lista por el nuevo nodo
        temp2->siguiente = nuevo_nodo;
        //Nunca pierdo la direccion del nodo que voy a remplazar porque siempre
        //la guardo antes en el nodo nuevo
    }
    lista->cantidad++;
}

/*
    Elimina una posicion de la lista sin importar el dato que esta en esa posicion
    A diferencia de la funcion "borrar" este no borra todas las ocurrencias
*/
void l_eliminar(Lista lista, int pos)
{
    int i;

    if (l_es_vacia(lista) == true) return;

    //Copio toda la lista para poder trabajar en la funcion
    struct Nodo *actual = lista->inicio;
    
    //Verifico que la posicion pasada por parametro exista en la lista
    if (pos >= 1 && pos <= l_longitud(lista))
    {
        //Si tengo que eliminar la primer posicion
        if (pos == 1)
        {
            //Guardo como "nodo incial" el nodo siguiente al que tengo  que eliminar
            lista->inicio = actual->siguiente;
            //Libero en memoria el nodo que tengo que eliminar
            free(actual);
        }
        else
        {
            /*
                Si tengo que eliminar cualquier otra posicion que no sea la primera
                Recorro toda la lista hasta llegar a la posicion ANTERIOR a la pedida,
                por ejemplo, si tengo que eliminar la 5ta posicion me paro en la 4ta
            */
            for (i = 0; i < pos - 2; i++)
            {
                actual = actual->siguiente;
            }
            //Una vez que llegue a la posicion anterior a la que tengo que eliminar
            //Elimino y actualizo el dato "siguiente" en los nodos

            //Actual apunta al nodo en posicion (pos - 1)
            /*
                Creo un nodo temporal que apunta a la posicion siguiente en la que estoy.
                Es decir, si tengo que eliminar la posicion 5 ahora mismo estoy parado
                en la 4ta, por lo que actual->siguiente apunta la 5ta. Entonces
                "temp" apunta al nodo que tengo que eliminar
            */
            struct Nodo *temp = actual->siguiente; //Nodo en "pos"

            //actualizo el dato del siguiente nodo en el nodo actual (4ta posicion)
            /*
                actualizo la direccion del nodo siguiente. Por ejemplo,
                Actual esta parado en la posicion 4, mientras temp esta parado en la posicion 5.
                Entonces, actualizo la direccion del siguiente nodo a la posicion 4 con el siguiente
                nodo de la posicion 5 que voy a eliminar, es decir, el nodo 4 ahora tiene como direccion
                el nodo 6
            */
            actual->siguiente = temp->siguiente; //Nodo en "pos+1"
            free(temp);
        }
        lista->cantidad--;
    }

/*
                            LISTA ORIGINAL
                            ─────────────
            NODO                  
    DATO    SIGUIENTE        
   ┌───────┬─────────┐    ┌───────┬─────────┐    ┌───────┬─────────┐
   │   4   │    6    │    │   5   │    6    │    │   6   │    7    │
   │       │         │───►│       │         │───►│       │         │
   │       │         │    │       │         │    │       │         │
   └───────┴────┬────┘    └───────┴─────────┘    └───────┴─────────┘

                            ELIMINO NODO
                            ────────────
            NODO                  
    DATO    SIGUIENTE      ESTE NODO SE ELIMINA
   ┌───────┬─────────┐    ┌───────┬─────────┐    ┌───────┬─────────┐
   │   4   │    6    │    │   5   │    6    │    │   6   │    7    │
   │       │    ▲    │    │       │    │    │    │       │         │
   │       │    └────┼────┼───────┼────┘    │    │       │         │
   └───────┴────┬────┘    └───────┴─────────┘    └───────┴─────────┘
                │                                    ▲
                │                                    │
                └────────────────────────────────────┘




*/


}



/*
    Retorna el elemento (dato) de la posicion recibida por parametro
*/
TipoElemento l_recuperar(Lista lista, int pos)
{
    int i;
    /*
        Creo una estructura que va a COPIAR la lista entera
        A pesar que le paso solo "lista->inicio" se copia la lista entera
        porque la estructura tiene un tipo de dato recursivo, en este caso es "siguiente"
        entonces al copiar el primer nodo copio todos los nodos siguientes
    */
    struct Nodo *temp2 = lista->inicio;

    /*
        Una vez que tengo la copia de la lista sobre la cual trabajar la recorro Nodo por Nodo
        hasta llegar a la posicion pasada por parametro
    */
    for (i = 0; i < pos - 1; i++)
    {
        /*
            Remplazo el nodo de inicio por el siguiente
            hasta llegar a la posicion pedida, recien ahi
            puedo retornar el dato
        */
        temp2 = temp2->siguiente;
    }
    return temp2->datos;
}

/*
    Muestra las CLAVES por pantalla
*/
void l_mostrarLista(Lista lista)
{
    /*
        Copio la lista entera
    */
    struct Nodo *temp2 = lista->inicio;
    printf("Contenido de la lista: ");
    //Hasta no llegar al ultimo nodo no sale del bucle
    while (temp2 != NULL)
    {
        //Imprime la clave del nodo actual
        printf("%d ", temp2->datos->clave);
        //Pasa al siguiente nodo
        temp2 = temp2->siguiente;
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
    iter->posicionActual = lista->inicio;
    return iter;
}

/*
    Retorna "True" si quedan elementos por recorrer, caso contrario "False"
*/
bool hay_siguiente(Iterador iterador)
{   
    //Si es distinto de NULL significa que hay un elemento mas y devuelve "true"
    return (iterador->posicionActual != NULL);
}

/*
    Retorna el proximo elemento de la lista
*/
TipoElemento siguiente(Iterador iterador)
{
    TipoElemento actual = iterador->posicionActual->datos;
    iterador->posicionActual = iterador->posicionActual->siguiente;
    return actual;
}
