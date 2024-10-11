#ifndef TIPO_ELEMENTO_H
#define TIPO_ELEMENTO_H

#include <stdio.h>
#include <stdlib.h>

//Creo la estructura que va a almacenar los datos
struct TipoElementoRep
{
    int clave;
    void* valor; //Lo defino como puntero void porque no se que tipo de dato va a tener
};

//Creo el tipo de dato con la palabra reservada "typedef" basandome en la estructura creada antes
typedef struct TipoElementoRep *TipoElemento;
//El tipo de dato creado es un puntero de la estructura tipoElemento

//Defino las propiedades o funciones que va a tener

//Va a crear un tipo elemento solamente con la clave pero sin valor. Retorna un dato de TipoElemento
TipoElemento te_crear(int clave);

//Va a crear un tipo Elemento con clave y valor. Retorna un dato de TipoElemento
TipoElemento te_crearConValor(int clave, void* valor);


#endif