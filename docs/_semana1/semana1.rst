Semana 1
===========
Esta semana vamos a repasar los últimos conceptos de programación estudiados
en sensores 1. El ejercicio propuesto busca integrar, de manera robusta,
sensores y actuadores a un motor de videojuegos como Unity, utilizando el
puerto de comunicaciones seriales.

Procedimiento
---------------
Retomaremos la guía del curso de sensores 1 que está
`aquí <https://drive.google.com/open?id=1HY9ocUXXVxhxCPJ6bSe0YpPXEPWudITRncw2FNWDZTU>`__ .

De este procedimiento nos interesan, sobre todo, los ejercicios 3 y 4.

Reto
------

El semestre pasado, un egresado del programa Ingeniería en Diseño de
Entretenimiento Digital me contó acerca de una experiencia interactiva
que está realizando con este `dispositivo <http://www.chafon.com/productdetails.aspx?pid=382>`__.
La idea de la experiencia es que al entrar a un probador de ropa, el
dispositivo detecte qué prendas está entrando el cliente y le sugiera otras
prendas con las cuales puede combinar las que tiene. Para lograr lo anterior,
se debe integrar el sensor al motor  de juegos Unity mediante la implementación
del protocolo que se describe en el siguiente `manual <https://drive.google.com/open?id=1uDtgNkUCknkj3iTkykwhthjLoTGJCcea>`__.

El reto consiste en implementar un programa en Unity, utilizando la biblioteca
Ardity, que permita interactuar con el sensor descrito. considere:

1. El protocolo es binario.
2. Es necesario modificar el código fuente de Ardity para detectar en qué
   momento se recibió un mensaje completo.
3. Tenga presente que no puede modificar el protocolo, ya está establecido.

.. note::
    IMPORTANTE: antes de comenzar a programar. Lea el código fuente de Ardity y escriba
    una estrategia para solucionar el problema. Discuta con sus compañeros.

4. Como no tenemos el sensor físico, vamos a simular su funcionamiento
   programando un arduino de tal manera que simule el protocolo
   dado.
5. En `este <https://drive.google.com/open?id=1iVr2Fiv8wXLqNyShr_EOplSvOJBIPqJP>`__
   archivo hay una secuencia de comandos y posibles respuestas del sensor.
   Vamos a utilizar esta información para realizar la simulación.



