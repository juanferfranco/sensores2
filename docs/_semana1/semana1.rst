Semana 1
===========
Durante esta semana vamos a introducir el curso y estudiar la plataforma de cómputo con la cual trabajaremos este semestre.
La presentación de la primera clase está aquí: 
`Clase 1.1 <https://drive.google.com/open?id=199nQxxbA0AIDNq_tzbS7AryIZM7NNT1Jq3RvPds9-c8>`__.

Objetivo
---------
1. Introducir el curso

2. Introducir y configurar la plataforma de cómputo 

Lecturas
---------
En los siguientes sitios web se describe las características de la plataforma a utilizar (tanto hardware como software):

1. Características generales de la plataforma `ESP32 <https://www.espressif.com/en/products/hardware/esp32/overview>`__.

2. Guía de programación `ESP-IDF <`https://esp-idf.readthedocs.io/en/latest/>`__.

3. Tarjeta de desarrollo: `ESP32-PICO-KIT V4 board <https://esp-idf.readthedocs.io/en/latest/hw-reference/modules-and-boards.html#esp32-pico-kit-v4>`__.

.. image:: ../_static/esp32-pico-kit-v4.jpeg
    :scale: 50 %

Ejercicio 1: Configuración de las herramientas
------------------------------------------------
Vamos a configurar el entorno de desarrollo como lo indica la guía de programación 
`ESP-IDF <https://esp-idf.readthedocs.io/en/latest/>`__. Específicamente, seguiremos los pasos indicados en la sección
`Get started <https://esp-idf.readthedocs.io/en/latest/get-started/index.html#>`__. Antes de continuar con los pasos
siguientes es fundamental que seamos capaces de configurar, compilar, programar y comunicarnos con la tarjeta. Todos los pasos
anteriores desde la línea de comandos (bueno, tal vez la comunicación serial no). Para las comunicaciones serial podemos
utilizar la terminal de arduino o programas como `Hercules <https://www.hw-group.com/software/hercules-setup-utility>`__
o `CoolTerm <http://freeware.the-meiers.org/>`__.

En la documentación anterior sugieren utilizar eclipse como entorno integrado de desarrollo. Como sé que hay personas que no
les gusta eclipse (alias Simón), también podemos utilizar Visual Studio Code (me está gustando más). 
El siguiente video en youtube muestra cómo configurar `visual studio code <https://www.youtube.com/watch?v=VPgEc8FUiqI>`__.
En la descripción del video está el enlace para descargar los archivos necesarios. Una tercera opción, 
la cual utilizaré tabién es `visualGDB <https://visualgdb.com/>`__; sin embargo, esta herramienta no es gratis, es para gente de modo.

Hola mundo sin IDE
^^^^^^^^^^^^^^^^^^^
En esta sesión ilustraré los pasos necesarios para probar la instalación de las herramientas desde la línea de comandos. 
Estas pruebas son fundamentales antes de pensar en la instalación de un IDE. 

1. Lo primero que necesitamos para trabajar con la plataforma ESP32 es una serie de herramientas que permitan traducir 
código de lenguaje C a código de máquina de la plataforma objetivo (*target*). Todo este proceso lo haremos directamente en el 
computador (*host*) que puede tener un sistema operativo Linux, Windows o MacOS. El resultado de la traducción será un archivo 
binario que contendrá instrucciones en código de máquina del ESP32. Estas instrucciones tendremos que almacenarlas en la memoria
no volátil del ESP32 (memoria flash). El proceso de almacenamiento se realiza con la ayuda de un *bootloader*. Un *bootloader* 
es un programa que se ejecutará en el ESP32 y tendrá por reposabilidades recibir la información del archivo binario y 
almancenarla en la memoria. Esta operación se realiza, en principio, mediante el puerto serial; 
sin embargo, podríamos hacer lo mismo utilizando otros medios tales como un interfaz ethernet, wifi, 
una memoria no volátile externa, entre otros. Pregunta Juanito: ¿Y cómo hacemos para ejercutar el bootloader? La respuesta larga
está `aquí <https://github.com/espressif/esptool/wiki/ESP32-Boot-Mode-Selection>`__. La respuesta corta: el pin GPIO0 debe 
estar en estado lógico bajo y mantenerse así mientras el ESP32 es reiniciado. Las siguiente figura muestra 
los *push buttons* de la tarjeta de desarrollo que permiten realizar la operación:

.. image:: ../_static/manualInterface.jpeg


Afortunadamente, la secuencia anterior de acciones se puede automatizar desde la interfaz 
USB-Serial mediante el *script* esptool.py. Este *script*, que hace parte del *toolchain*, envía la información 
de la traducción y controla las líneas RTS y DTR que a su vez permiten controlar el pin GPIO0 y el pin de reset del ESP32. 
La siguiente figura muestra en detalle el esquemático de la interfaz USB-Serial:

.. image:: ../_static/picoSerialInterface.jpeg


Volviendo de nuevo al asunto del *toolchain*, en la siguiente figura se observa el mismo concepto pero esta vez recordando 
lo estudiado en sensores 1 con la plataforma Arduino UNO.

.. image:: ../_static/arduinoToolchain.jpeg

En mi caso, la siguiente figura muestra una imagen del *toolchain*, para windows y precompilado por espressif, descomprimida:

.. image:: ../_static/toolchainInstall.jpeg

Esta estructura emula un entorno similar al que se enfrentaría un usuario de Linux o MacOS.

2. Ya habíamos dicho que en esta parte del proceso no utilizaremos un IDE. Por tanto, la herramienta para realizar todas 
las operaciones será una terminal de línea de comandos, en mi caso: ``D:\ESP32\msys32\mingw32.exe``. Antes de continuar con 
la descarga del *framwork* de desarrollo, vamos a crear una carpeta para almacenarlo. Utilice los comandos de terminal 
``mkdir -p ~/esp32`` para crear el directorio y ``cd ~/esp`` para cambiarse (navegar) de directorio.

3. Vamos a descargar el *framework* de desarrollo. Pregunta Juanito ¿Qué es un *framework*? Podemos entender un *framework* 
como una aplicación incompleta, es decir, la empresa espressif nos entrega parte de nuestra aplicación hecha, pero nosotros 
debemos completarla con la funcionalidad particular que deseamos. El *framework* se denomina ``ESP-IDF`` que 
quiere decir *Espressif IoT Development Framework*. Para descargarlo debemos abrir la terminal y ejecutar los siguientes 
comandos::

    cd ~/esp
    git clone --recursive https://github.com/espressif/esp-idf.git


El ESP-IDF se descargará en la carpeta ``~/esp/esp-idf``. La siguiente figura muestra donde está dicha carpeta:

.. image:: ../_static/IDF-Dir.jpeg

.. note::
    #. El símbolo ``~`` significa directorio raíz del usuario. 
    #. Si por algún motivo olvidó utilizar la opción ``--recursive``, es necesario ejecutar los siguientes comandos para bajar el *framework* completo::
        
        cd ~/esp/esp-idf
        git submodule update --init

4. Ahora debemos configurar la ruta donde está ubicado el ESP-IDF. Para ello, debemos crear un *script* en la carpeta 
``D:\ESP32\msys32\etc\profile.d`` que nombraremos ``export_idf_path.sh``. Escriba en el archivo::

    export IDF_PATH="D:/ESP32/msys32/home/JuanFernandoFrancoHi/esp/esp-idf"

Salvamos el *script* y CERRAMOS la terminal. Al abrir de nuevo la terminal y ejecutar el comando::
    
    printenv IDF_PATH

Debe aparecer la ruta previamente configurada. De lo contrario, será necesario verificar los pasos anteriores.

5. Creamos un projecto. Copiamos en el directorio ``~/esp`` uno de los ejemplos que vienen con el ESP-IDF así:

    cd ~/esp
    cp -r $IDF_PATH/examples/get-started/hello_world .

6. Conectamos el ESP32 al PC e identificamos el puerto serial asignado por el sistema operativo:

.. image:: ../_static/comport.jpeg

7. Vamos a configurar el ESP-IDF utilizando la herramienta ``menuconfig``::

    cd ~/esp/hello_world
    make menuconfig

Debe aparecer la siguiente ventana:

.. image:: ../_static/projectConfig.jpeg

Navegar al menú ``Serial flasher config`` > ``Default serial port`` para configurar el puerto serial y la velocidad:

.. image:: ../_static/serialPortSDKConfig.jpeg

Confirmar las selecciones con enter. No olvide salvar seleccionando ``< Save >`` y luego salir seleccionando ``< Exit >``.

8. Compilar y almacenar el programa en la memoria *flash*. En la terminal escribimos el comando::
    
    make flash

Este comando hace varias cosas: compilar la aplicación y todos los componentes del ESP-IDF, genera el bootloader, 
`la tabla de particiones <https://esp-idf.readthedocs.io/en/latest/api-guides/partition-tables.html>`__, los binarios de la aplicación y 
finalmente envía el binario al ESP32.

9. Una vez almacenado el binario de la aplicación en la memoria *flash*, podemos abrir una terminal serial a 115200 para observar el resultado.

10. Pregunta Juanito ¿Y esto toca hacerlo cada que creemos una aplicación? La respuesta es si y no. No es necesario 
bajar el ESP-IDF y configurarlo; sin embargo, si es recomendable seguir estos pasos: 

* Copiar un proyecto existente.
* Configurar el *framework*: ``make menuconfig``.
* Compilar el proyecto: ``make all``. Esto compila la aplicación, el bootloader y la tabla de partición.
* Grabar todo el proyecto: ``make flash``.
* Luego, compilar sólo la aplicación: ``make app``. Esto acelara el proceso al evitar compilarlo todo. 
* Luego, grabar sólo la aplicación: ``make app-flash``.

11. Pregunta Juanito ¿Y si Espressif actualiza el toolchain? Cambio el nombre del directorio de 
``D:\ESP32\msys32`` a ``D:\ESP32\msys32\mingw32_old`` y repito todo el procedimiento desde la descarga del *toolchain*

12. Pregunta Juanito ¿Y si Espressif no actualiza el toolchain pero si actualiza el ESP-IDF? cambio el direcorio ~/esp/esp-idf por ~/esp/esp-idf_old y clono 
de nuevo el ESP-IDF::

    cd ~/esp
    git clone --recursive https://github.com/espressif/esp-idf.git


Configuración de Visual Studio Code (VSC)
^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
A continuación describiré los pasos necesarios para configurar la herramienta. Esta sección supone que los pasos anteriores se siguieron y el resultado 
fué exitoso. Esto es importante porque la función de VSC es llamar automáticamente los mismos comandos que estamos llamando manualme nte. 

1. Lo primero que debemos hacer es descargar `visual studio code <https://code.visualstudio.com/>`__.

2. Luego se deben instalar algunas extensiones: C/C++ for Visual Studio Code, Native Debug (para el futuro, pero nosotros no 
utilizaremos el debugger porque no tenemos una interfaz JTAG), Serial Monitor como muestra la siguiente figura:

.. image:: ../_static/extensiones.jpeg

3. Ahora configuramos la terminal desde la que VSC llamará los comandos. Seleccionar ``File -> Preferences -> Settings`` y adicionar el siguiente texto a las 
preferencias actuales::
    
        "terminal.integrated.shell.windows": "D:/ESP32/msys32/usr/bin/bash.exe",
        "terminal.integrated.shellArgs.windows": [
            "--login",
        ],
        "terminal.integrated.env.windows": {
            "CHERE_INVOKING": "1",
            "MSYSTEM": "MINGW32",
        }
    
Es de notar la ruta de la aplicación ``bash.exe`` en mi sistema: ``D:/ESP32/msys32/usr/bin/bash.exe``. En mi caso, los *Settings* quedan así::

    {
        "terminal.integrated.shell.windows": "D:/ESP32/msys32/usr/bin/bash.exe",
        "terminal.integrated.shellArgs.windows": [
            "--login",
        ],
        "terminal.integrated.env.windows": {
            "CHERE_INVOKING": "1",
            "MSYSTEM": "MINGW32",
        },
        "arduino.path": "C:/Users/JuanFernandoFrancoHi/arduino-1.8.5-windows/arduino-1.8.5",
        "arduino.logLevel": "info", "arduino.enableUSBDetection": true, 
        "C_Cpp.intelliSenseEngine": "Tag Parser",
        "files.autoSave": "afterDelay",
        "python.pythonPath": "C:\\Users\\JuanFernandoFrancoHi\\AppData\\Local\\Programs\\Python\\Python36-32\\python.exe",
        "arduino.additionalUrls": [
            "https://git.oschina.net/dfrobot/FireBeetle-ESP32/raw/master/package_esp32_index.json",
            "http://arduino.esp8266.com/stable/package_esp8266com_index.json",
            "https://github.com/stm32duino/BoardManagerFiles/raw/master/STM32/package_stm_index.json",
            "https://raw.githubusercontent.com/VSChina/azureiotdevkit_tools/master/package_azureboard_index.json"
        ]
    }

4. Verificamos que la terminal esté correctamente configurada. Seleccionamos el menú ``View --> Output`` y finalmente clock en Terminal. El resutado debe ser 
similar al que muestra la figura:

.. image:: ../_static/terminal.jpeg

Iniciar un nuevo proyecto en Visual Studio Code
^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
1. Copiamos de la carpeta de ejemplos del ESP-IDF el proyecto hello_world::

    cd ~/esp
    mkdir vscode-workspace
    cd vscode-workspace
    cp -r $IDF_PATH/examples/get-started/hello_world .

2. copiamos la carpeta `.vscode <./.vscode.zip>`__ en el directorio hello_world

Ejercicio 2: análisis del ejemplo 
------------------------------------
