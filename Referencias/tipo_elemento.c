#include <stdio.h>
#include <stdlib.h>
#include "tipo_elemento.h"

//Escribo las funciones
TipoElemento te_crear(int clave)
{
    TipoElemento te = (TipoElemento) malloc(sizeof(struct TipoElementoRep));
    te->clave = clave;
    te->valor = NULL;
    return te;
}

TipoElemento te_crearConValor(int clave, void* valor)
{
    TipoElemento te = (TipoElemento) malloc(sizeof(struct TipoElementoRep));
    te->clave = clave;
    te->valor = valor;
    return te;
}
