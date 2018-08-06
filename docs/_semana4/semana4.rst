Semana 4
===========
Esta semanas continuaremos explorando el API de FreeRTOS. 

Objetivos
----------

1. Utilizar mecanismos de comunicación entre tareas.
2. Emplear los servicios ofrecidos por los temporizadores del sistema operativo para controlar acciones en el tiempo.

Ejercicios con el API de FreeRTOS
---------------------------------
Para realizar los siguientes ejercicio es necesario tener a la mano dos documentos:

1. `Tutorial oficial <https://www.freertos.org/Documentation/161204_Mastering_the_FreeRTOS_Real_Time_Kernel-A_Hands-On_Tutorial_Guide.pdf>`__.
2. La implementación de Espressif. `ESP-FREERTOS <https://esp-idf.readthedocs.io/en/latest/api-reference/system/freertos.html>`__.


Ejericio: comunicación entre tareas
^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^

Las colas o *Queues* son uno de los mecanismos de comunicación de FreeRTOS. Estas permiten comunicar tareas, tareas con 
interrupciones e interrupiones con tareas.

Las colcas almacenan una cantidad finita de items todos ellos del mismo tamaño. La longitud de la cola es la cantidad 
máxima de items que puede almacenar. Al momento de crear la cola se define el tamaño de los items y la longitud de la cola.

Las colas se utilizan como estructuras de datos FIFO (First In First Out). Los datos se escriben al final de la cola (*tail*) 
y se remueven del frente (*head*). Es posible escribir al frente de la cola para modificar datos que ya están presentes.

La siguiente figura ilustra cómo funciona una cola:

.. image:: ../_static/queues.jpeg

Los datos que se almacenan en la cola pueden comportarse por valor (copia byte por byte) o por referencia (se copia la 
dirección del puntero donde están los datos). El primer método es más costoso en términos de memoria, pero permite desacoplar 
mejor las tareas, haciendo más simple el manejo de la información. 

Otras características a considerar:

* Es usual que una cola tenga múltiples escritores y sólo un lector. Aún así, es posible usarlas con otros esquemas.
* Una tarea lectora se bloqueará si no hay datos en la cola. Es posible especificar el tiempo que durará bloqueada. Si 
  otra tarea o una interrupción envía datos a la cola, la tarea pasará automáticamente al estado lista para ejecución y por 
  tanto será candidata a tener una CPU cuando el *scheduler* así lo determine.
* Las tareas pueden bloquerse, y especificar también tiempos de bloqueo, al escribir una cola. Esto ocurre cuando no hay 
  más espacio disponible.
* Varias tareas escritoras pueden bloquearse al esperar espacio en una cola. Cuando el espacio esté disponible, la tarea de 
  más alta prioridad será desbloqueada y puesta en lista para correr. Si todas las tareas tienen la misma prioridad, la tarea 
  que lleve más tiempo esperando desbloqueada y puesta lista para correr.
* API para crear una cola :: 
  
    QueueHandle_t xQueueCreate( UBaseType_t uxQueueLength, UBaseType_t uxItemSize ); 

Actividades: 

* Realizar el ejemplo 10 del `Tutorial oficial <https://www.freertos.org/Documentation/161204_Mastering_the_FreeRTOS_Real_Time_Kernel-A_Hands-On_Tutorial_Guide.pdf>`__.
* Realizar el ejemplo 11.

Recuerde que en ambas actividades es de esperar un comportamiento diferente gracias a los dos CPUs. De igual manera, es 
necesario adaptar el código pues no tenemos acceso directo a la función main. Pregunta juanito: ¿Cómo adapto el código? 
Mire los ejemplos anteriores y compárelos con los códigos de la semana 3.
