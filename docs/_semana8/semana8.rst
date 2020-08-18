Semana 8 Unidad 3 - sensores y actuadores SPI
===============================================

Trayecto de acciones, tiempos y formas de trabajo
---------------------------------------------------

Actividad 4
######################
* Fecha: agosto 26 de 2020 - 10 a.m.
* Descripción: trabaja en el reto
* Recursos: ingresa al grupo de Teams
* Duración de la actividad: 1 hora 40 minutos.
* Forma de trabajo: grupal

Para transmitir información de variables usando un protocolo binario
necesitas obtener los bytes que componen una variable.

¿Cómo conseguir cada uno de los bytes que componen la variable?

* Para responder esta pregunta usa el programa ScriptCommunicator.
* Considera este código:

.. code-block:: cpp
    :linenos:

    void setup() {
      Serial.begin(115200);

    }

    void loop() {
      static uint16_t x = 0;

      if (Serial.available()) {
        if (Serial.read() == 0x73) {
          Serial.write((uint8_t)( x & 0x00FF ));
          Serial.write( (uint8_t)(x >> 8 ));
        }
      }
    }

Nota cómo la operación (x >> 8 ) permite conseguir el byte de mayor
peso del entero no signado de 16 bits x.

* Ahora intentemos la misma técnica para conseguir los bytes de un número en
  punto flotante.

.. code-block:: cpp
    :linenos:

    void setup() {
        Serial.begin(115200);
    }

    void loop() {
        float num = 1.1;

        if (Serial.available()) {
            if (Serial.read() == 0x73) {
                Serial.write((uint8_t)( num ));
                Serial.write( (uint8_t)(num >> 8 ));
                Serial.write( (uint8_t)(num >> 16 ));
                Serial.write( (uint8_t)(num >> 32 ));
            }
        }
    }

El 1.1 en punto flotante será el 3f 8c cc cd

* ¿Pudiste compilar el programa?

Nota que al intentar compilar, el compilador te dirá que no es posible
aplicar el operador >> al tipo float.

* Debemos entonces aplicar una técnica diferente para obtener los bytes
  del float:

.. code-block:: cpp
    :linenos:

    void setup() {
        Serial.begin(115200);
    }

    void loop() {
        // 45 60 55 d5
        // https://www.h-schmidt.net/FloatConverter/IEEE754.html
        static float num = 3589.3645;
    
        static uint8_t arr[4] = {0};

        if(Serial.available()){
            if(Serial.read() == 0x73){
                memcpy(arr,(uint8_t *)&num,4);
                Serial.write(arr,4);
            }
        }
    }

En este caso estamos guardando los 4 bytes que componen el float
en un arreglo, arr, para luego transmitir dicho arreglo.

* ¿En qué orden estamos transmitiendo los bytes, en bigEndian o en
  littleEndian?

* Para leer los datos en la aplicación en Unity necesitaremos hacer
  la acción opuesta, es decir, a partir de los 4 bytes debemos
  construir el número en punto flotante. Para hacerlo investiga
  `esta <https://docs.microsoft.com/en-us/dotnet/api/system.bitconverter?view=netframework-4.8>`__
  clase de C#.
* En qué orden debemos organizar los bytes para poder hacer la conversión?


Actividad 5
############################

* Fecha: agosto 26 a agosto 28 de 2020
* Descripción: experimentación para el reto
* Recursos: mira el ejercicio
* Duración de la actividad: 5 horas
* Forma de trabajo: individual con solución de dudas en tiempo real

Ejercicio
^^^^^^^^^^^^^^^^^
Estudia la actividad anterior en detalle.
Experimenta de la misma manera con Kodular. ¿Cómo puedes recibir
bytes en kodular? y una vez tengas los bytes cómo haces para convertir
a un número en punto flotante? DEBERÍAS resolver este problema antes
de seguir. Ten presente que deberás ser ingenioso porque es posible
que la solución no sea tan obvia.

Actividad 6
######################
* Fecha: agosto 28 de 2020 - 10 a.m.
* Descripción: trabaja en el reto
* Recursos: ingresa al grupo de Teams
* Duración de la actividad: 1 hora 40 minutos.
* Forma de trabajo: grupal

En esta sesión define cómo vas a realizar el RETO, plantea la arquitectura,
experimenta con partes que aún no sabes cómo funcionan. RESUELVE tus dudas
en tiempo real con el docente.