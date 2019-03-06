Semana 7
===========
Esta semana vamos a terminar la introducción a la programación de 
sistemas embebidos utilizando el sistema operativo de tiempo real 
FreeRTOS. Es bueno aclarar que tenemos varios objetivos en este recorrido:

1. Entender mejor cómo funciona la plataforma ESP32.
2. Adicionar más herramientas, además del framework de arduino, que permitan realizar 
   programas más complejos e interesantes y de esta manera poder utilizar sensores y 
   actuadores más complejo o con restricciones de tiempo real más fuertes.
3. Los elementos de FreeRTOS que estamos estudiando: tareas, colas y timers, permitirán 
   entender e implementar el patrón de objetos activos. Está técnica moderna de programación
   permite construir sistemas más robustos, resilientes y flexibles.

Para realizar los ejercicios de esta semana, utilizaremos la plataforma ESP32 y como referencia los 
ejercicios de la semana 6 correspondientes a la sección: 
`Más ejercicios con el API de FreeRTOS <../_semana6/semana6.html#mas-ejercicios-con-el-api-de-freertos>`__

colas
-------
Consulte el `API <https://www.freertos.org/a00018.html>`__ de FreeRTOS para aprender las posibles funciones 
que ofrece para interactuar con colas.

Aquí está el código de referencia:

.. code-block:: cpp
   :lineno-start: 1

    // variable para almacenar la referencia a la cola
    QueueHandle_t queue; 
    
    void setup() {
    
      Serial.begin(115200);
    
      // Creación de la cola, de 10 items cada uno del tamaño de un entero.
      queue = xQueueCreate( 10, sizeof( int ) );
    
      // verifica si fue posible crear la cola de lo contrario el programación
      // no debería continuar o debería tomar alguna acción al respecto ;)
      if(queue == NULL){
        Serial.println("Error creating the queue");
      }
    
    }
    // En este caso la tarea de arduino autoenviará y recibirá sus propios
    // mensajes 
    void loop() {
      int element;
      // Si no hay cola, no hago nada: esta es la acción que tomamos
      // si no hay cola.
      
      if(queue == NULL)return;
    
      // Generar 10 mensajes para enviar a la cola 
      for(int i = 0; i<10; i++){
        xQueueSend(queue, &i, portMAX_DELAY);
      }
    
      // Lee los 10 mensajes
      for(int i = 0; i<10; i++){
        xQueueReceive(queue, &element, portMAX_DELAY);
        Serial.print(element);
        Serial.print("|");
      }
    
      Serial.println();
      delay(1000);
    }

Tenga en cuenta que al leer datos de la cola, si esta está vacía, la tarea será bloqueada portMAX_DELAY, es decir,
indefinidamente hasta que lleguen datos. Note también que en FreeRTOS es posible preguntar si hay datos en la cola 
utilizando ``uxQueueMessagesWaiting``.

Soft Timers
-------------
En `este <https://www.freertos.org/FreeRTOS-Software-Timer-API-Functions.html>`__ enlace está el API de FreeRTOS para 
utilizar los soft timers.
