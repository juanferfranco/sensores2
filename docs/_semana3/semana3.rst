Semana 3
===========
Esta semana vamos a modificar el mecanismo mediante el cual se utilizará el serial. Usaremos hilos. Los hilos permiten introducir e independizar flujos de instrucciones en los programas; sin embargo, 
también introducen problemas de sincronización con los cuales tendremos que aprender a lidiar correctamente. No pierdan de vista que las soluciones de integración por 
medio de corutinas e hilos que hemos estudiado pueden ser utilizadas en otras plataformas, en particular los hilos.

Las soluciones planteadas hasta el momento no poseen algunas características de robustez deseables en aplicaciones comerciales. Por tanto, esta semana también 
vamos a estudiar un pluggin open source para Unity que permite una comunicación más confiable. Por tanto, estudiaremos en detalle los conceptos necesarios 
para alcanzar niveles de robustez necesarios, no solo para los ejercicios de esta semana, sino también para otros escenarios similares. 

Objetivo
---------
1. Introducir y utilizar hilos y algunos mencanismos de sincronización y comunicación.
2. Integración de sensores y actuadores a Unity utilizando hilos.
3. Analizar qué elementos se deben añadir a una aplicación para hacer que sea robusta antes situaciones de error típicas en el uso de un puerto de comunicaciones.

Material de referencia
-----------------------
Los ejercicios de esta semana los realizaremos en base al material del blog de Alan Zucconi, parte 2. La guía de trabajo se encuentra 
en este `enlace <https://drive.google.com/open?id=1GFoobhnUdcnuXfgekqUguBN_Gb1G9CsVMwJIg9bb_Ck>`__.

También analizaremos el pluggin para Unity `Ardity <https://ardity.dwilches.com/>`__. La guía 
de trabajo está `aquí <https://drive.google.com/open?id=1HY9ocUXXVxhxCPJ6bSe0YpPXEPWudITRncw2FNWDZTU>`__ (pendiente).
