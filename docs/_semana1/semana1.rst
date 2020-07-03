Semana 1
===========

Unidad 1: repaso sensores 1
-----------------------------
En esta unidad vamos a repasar los conceptos fundamentales de sensores 1 y
cómo se aplican, en el contexto de sensores 1, los principios de la orientación
a objetos, la programación de operaciones de entrada-salida y la comunicación
entre hilos.

Ten presente que en esta unidad, se hará especialmente énfasis en que seas capaz
de explicar los principios de funcionamiento del software que escribas.

.. warning::
   ES MUY IMPORTANTE QUE LEAS CON DETENIMIENTO LA RÚBRICA DE EVALUACIÓN DE ESTA
   UNIDAD ANTES DE COMENZAR.

Propósitos de aprendizaje
^^^^^^^^^^^^^^^^^^^^^^^^^^
Identificar y expliar los principios de programación aprendidos en programación
y diseño orientado a objetos y sensores 1.

Identificar y explicar la arquitectura de los programas del controlador
y la plataforma interactiva integrados por medio de un protocolo de comunicación
binario

Verificar el software del controlador y la aplicación interactiva
por separado y de manera conjunta.

Actividad de aprendizaje
^^^^^^^^^^^^^^^^^^^^^^^^^^

Se realizará las SEMANAS 1,2 y 3

Lee con detenimiento el código de honor y luego los pasos que
debes seguir para evidenciar esta actividad.

Código de honor
###################
Para realizar este reto se espera que hagas lo siguiente:

* Colabora con tus compañeros cuando así se indique.
* Trabaja de manera individual cuando la actividad así te lo
  proponga.
* Usa solo la documentación oficial del framework del controlador
  y de la plataforma interactiva.
* NO DEBES utilizar sitios en Internet con soluciones o ideas para
  abordar el problema.
* NO DEBES hacer uso de foros.
* ¿Entonces qué hacer si no me funciona algo? Te propongo que
  experimentes, crea hipótesis, experimenta de nuevo, observa y concluye.
* NO OLVIDES, este curso se trata de pensar y experimentar NO de
  BUSCAR soluciones en Internet.

Enunciado
##########

Un egresado del programa Ingeniería en Diseño de
Entretenimiento Digital me contó acerca de una experiencia interactiva
que está realizando con este `dispositivo <http://www.chafon.com/productdetails.aspx?pid=382>`__.
La idea de la experiencia es que al entrar a un probador de ropa, el
dispositivo detecte qué prendas está entrando el cliente y le sugiera otras
prendas con las cuales puede combinar las que tiene. Para lograr lo anterior,
se debe integrar el sensor al motor  de juegos Unity mediante la implementación
del protocolo que se describe en el siguiente `manual <https://drive.google.com/open?id=1uDtgNkUCknkj3iTkykwhthjLoTGJCcea>`__.

El reto consiste en implementar un programa en Unity, utilizando la biblioteca
Ardity modificada en sensores 1 para soportar protocolos binarios y que permita
interactuar con el sensor descrito. Considere:

#. El protocolo es binario.
#. Recuerda que debemos añadir soporte para tu protocolo binario a Ardity.
#. Ten presente que no puedes modificar el protocolo establecido por el fabricante del sensor.

   .. note::
      IMPORTANTE: antes de comenzar a programar. Estudia con detenimiento el código fuente de
      Ardity.

#. Como no tenemos el sensor físico, vamos a simular su funcionamiento
   programando un arduino de tal manera que simule el protocolo
   dado.
#. En `este <https://drive.google.com/open?id=1iVr2Fiv8wXLqNyShr_EOplSvOJBIPqJP>`__
   archivo hay una secuencia de comandos y posibles respuestas del sensor.
   Vamos a utilizar esta información para realizar la simulación.
#. Tu programa en Unity debe tener interfaz de usuario, puedes usar la consola para
   depurar la aplicación, pero al final toda la interacción con el sensor la debes
   realizar en tu interfaz de usuario.

¿Qué debes entregar?
######################

* Crea una carpeta, la llamaras principal. 
* En la carpeta principal guarda una copia de la `rúbrica <https://docs.google.com/spreadsheets/d/1gNrBcmxv6xMgaKTbgmCqJbJqNA8Xa_5QOmXu61plN5o/edit?usp=sharing>`__
  con tu autoevaluación.
* En la carpeta principal guarda un archivo .pdf donde colocarás dos cosas:
  
  * UN ENLACE a tu ONE DRIVE donde estará alojado el video de sustentación.
  * Una tabla de contenidos que indique el instante de tiempo en el cual se
    pueden encontrar cada una de las secciones solicitadas en el video.

* Comprime la carpeta principal en formato .ZIP
* Entrega el archivo .ZIP `aquí <https://upbeduco-my.sharepoint.com/:f:/g/personal/juanf_franco_upb_edu_co/EmXf22H3PgNArlDNcs9xJVYBlBCHQKSy5FzdoiNd0F5NkA>`__
  antes de la sesión del 24 de julio a las 10 a.m. En esta sesión realizaremos la retroalimentación final.

¿Qué deberá tener el video de sustentación?
#############################################

* Máximo 40 minutos: debes planear el video tal como aprendiste en segundo semestre
  en tu curso de narrativa audiovisual.
* Cuida la calidad del audio y del video.
* Sección 1: introducción, donde dirás tu nombre y si realizaste el RETO
  completo. Si no terminaste indica claramente qué te faltó y por qué.
* Sección 2: identifica y explica de manera clara, precisa, completa y correctamente todos
  los principios de orientación a objetivos, cómo se gestionan las excepciones y cuándo
  se pueden presentar, cómo se programan las operaciones de entrada salida y cómo se realiza
  la comunicación con el hilo principal.
* Sección 3: con respecto al software del controlador y el computador explica de manera clara, 
  precisa y completa las partes, su función y sus propiedades; las relaciones entre las partes,
  su función y sus propiedades.
* Sección 4: muestra que tus programas compilan correctamente y sin errores
  o advertencias problemáticas. Explica tus programas
* Sección 5: explica cómo vas a probar cada programa por separado y en conjunto 
* Sección 6: muestra que tu programa funciona según los escenarios de prueba
  identificados.
* Tus explicaciones deben ser claras, precisas y completas. No olvides planear 
  bien tu video de sustentación.
* Para las explicaciones de las secciones 2 y 3 te recomiendo que utilices una aplicación
  de WhiteBoard como `esta <https://www.microsoft.com/en-us/p/microsoft-whiteboard/9mspc6mp8fm4?activetab=pivot:overviewtab>`__. 

Trayecto de acciones, tiempos y formas de trabajo
^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^

Fase 1 (motivación)
######################

* Fecha: julio 8 de 2020 - 10 a.m.
* Descripción: asiste al encuentro sincrónico donde se introducirá la actividad de
  aprendizaje de la unidad 1.
* Recursos: ingresa al grupo de `Teams <https://teams.microsoft.com/l/team/19%3a802f001ad22e4792bb8e26792169bd1f%40thread.tacv2/conversations?groupId=1e58096b-6ed5-4a65-8cf9-799db7a28b81&tenantId=618bab0f-20a4-4de3-a10c-e20cee96bb35>`__
* Duración de la actividad: 30 minutos
  que resuelvas tus dudas en tiempo real.
* Forma de trabajo: grupal

Fase 2 (diagnóstico-repaso)
############################

* Fecha: julio 8 de 2020 - 10:30 a.m
* Descripción: lee con detenimiento la unidad 1, busca tu material de sensores 1,
  pregunta si tienes dudas.
* Recursos: 
  
  * Te dejo el `enlace <https://sensores1.readthedocs.io/es/v2020.10/_semana11/semana11.html>`__ 
    al curso de sensores 1 versión 2020-10. Allí podrás consultar
    todo lo que necesitas para que tu reto funciones, pero recuerda, el reto de este
    reto (al ser un repaso) es que expliques en detalle!
  * Ingresa al grupo de `Teams <https://teams.microsoft.com/l/team/19%3a802f001ad22e4792bb8e26792169bd1f%40thread.tacv2/conversations?groupId=1e58096b-6ed5-4a65-8cf9-799db7a28b81&tenantId=618bab0f-20a4-4de3-a10c-e20cee96bb35>`__
    para que resuelvas dudas en tiempo real con el docente.

* Duración de la actividad: 1 hora 10 minutos
* Forma de trabajo: individual con solución de dudas en tiempo real

Fase 3 (fundamentación)
#############################

* Fecha: julio 8 a 10 de 2020
* Descripción: repasa el material de semestres pasados
* Recursos: 

   * Te dejo el `enlace <https://sensores1.readthedocs.io/es/v2020.10/_semana11/semana11.html>`__ 
     al curso de sensores 1 versión 2020-10 desde la semana 11.
   * `Aquí <https://docs.google.com/presentation/d/1uHoIzJGHLZxLbkMdF1o_Ov14xSD3wP31-MQtsbOSa2E/edit?usp=sharing>`__
     tienes la guía de instalación de Ardity.
   * No olvides consultar la documentación oficial de `Ardity <https://ardity.dwilches.com/>`__.
   * A medida que repasa lo visto en sensores 1 te recomiendo que hagas los propio con los conceptos
     de programación orientada a objetos que no recuerdes.

* Duración de la actividad: 1 hora de trabajo autónomo para que comiences integrando Unity con arduino.
* Forma de trabajo: individual

Fase 4 (ejercicios y discusión)
##################################

* Fecha: julio 8 a 10 de 2020
* Descripción: continua con el repaso. Acuerda reuniones con tus compañeros para trabajar de manera *colaborativa*
* Recursos: trabaja con el enlace a sensores 1 de la fase 1.
* Duración de la actividad: 4 horas de trabajo autónomo y colaborativo. Acuerda reuniones con tus compañeros.
* Forma de trabajo: individual y colaborativa.

Fase 5 (retroalimentación): 
#############################

* Fecha: julio 10 de 2020 - 10 a.m.
* Descripción: encuentro sincrónico para aclarar tus dudas
* Recursos: 
  
  * Ingresa al grupo de `Teams <https://teams.microsoft.com/l/team/19%3a802f001ad22e4792bb8e26792169bd1f%40thread.tacv2/conversations?groupId=1e58096b-6ed5-4a65-8cf9-799db7a28b81&tenantId=618bab0f-20a4-4de3-a10c-e20cee96bb35>`__
  * Corrige tus ejercicios (acciones de mejora)

* Duración de la actividad: 1 hora y 40 minutos para que aclares tus dudas en tiempo real.
* Forma de trabajo: colaborativo con solución de dudas en tiempo real y 
  trabajo individual en la acción de mejora.
