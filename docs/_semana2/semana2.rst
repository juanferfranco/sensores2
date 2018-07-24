Semana 2
===========
Esta semana comenzaremos a explorar conceptos avanzados de programación de sistemas embebidos. En particular, mediante 
el uso de sistemas operativos de tiempo real (RTOS); sin embargo, antes de comenzar a utilizar las abstracciones que un 
RTOS nos ofrece, debemos comprender cómo funciona.

Objetivos
---------
1. Entender cómo funciona un sistema operativo de tiempo real.
2. Programar utilizando tareas.

Algo de teoría
---------------
Los RTOS son una evolución de la arquitectura de programación clásica *backgroud/foreground* tan conocida por 
nosotros (si, arduino). La idea entonces de un RTOS es ofrecernos un ambiente de programación con múltiples *background* 
funcionando de manera concurrente, es decir, es como tener un programa de arduino con múltiples ciclos ``loop()`` 
concurrentes.

El siguiente código muestra un ejemplo típico de una arquitectura *background/foreground*:

.. code-block:: c
   :lineno-start: 1

    // background code:
    
    #include <stdint.h>
    #include "bsp.h"

    int main() {
        BSP_init();
        while (1) {
            BSP_ledGreenOn();
            BSP_delay(BSP_TICKS_PER_SEC / 4U);
            BSP_ledGreenOff();
            BSP_delay(BSP_TICKS_PER_SEC * 3U / 4U);
        }
        return 0;
    }

.. code-block:: c
   :lineno-start: 1

    // foreground code: blocking version

    #include <stdint.h>  /* Standard integers. WG14/N843 C99 Standard */
    #include "bsp.h"
    #include "TM4C123GH6PM.h" /* the TM4C MCU Peripheral Access Layer (TI) */

    /* on-board LEDs */
    #define LED_BLUE  (1U << 2)

    static uint32_t volatile l_tickCtr;

    void SysTick_Handler(void) {
        ++l_tickCtr;
    }

    void BSP_init(void) {
        SYSCTL->RCGCGPIO  |= (1U << 5); /* enable Run mode for GPIOF */
        SYSCTL->GPIOHBCTL |= (1U << 5); /* enable AHB for GPIOF */
        GPIOF_AHB->DIR |= (LED_RED | LED_BLUE | LED_GREEN);
        GPIOF_AHB->DEN |= (LED_RED | LED_BLUE | LED_GREEN);
        SystemCoreClockUpdate();
        SysTick_Config(SystemCoreClock / BSP_TICKS_PER_SEC);
        __enable_irq();
    }

    uint32_t BSP_tickCtr(void) {
        uint32_t tickCtr;
        __disable_irq();
        tickCtr = l_tickCtr;
        __enable_irq();
        return tickCtr;
    }

    void BSP_delay(uint32_t ticks) {
        uint32_t start = BSP_tickCtr();
        while ((BSP_tickCtr() - start) < ticks) {
        }
    }

    void BSP_ledGreenOn(void) {
        GPIOF_AHB->DATA_Bits[LED_GREEN] = LED_GREEN;
    }

    void BSP_ledGreenOff(void) {
        GPIOF_AHB->DATA_Bits[LED_GREEN] = 0U;
    }

Es importante notar que el código anterior es bloqueante (Pregunta Juanito: ¿Qué es eso?). La función 
``BSP_delay(BSP_TICKS_PER_SEC / 4U);`` consume todos los recursos de CPU en espera ocupada, es decir, esperamos 
a que pase el tiempo sin hacer nada más. A esto también lo llamamos ``polling``. Recordar que al día de hoy conocemos 
una excelente técnica de programación para lidiar con el problema anterior: lás máquinas de estado:

.. code-block:: c
   :lineno-start: 1

    // background code: non-blocking version 
    int main() {
        BSP_init();
        while (1) {
            /* Blinky polling state machine */
            static enum {
                INITIAL,
                OFF_STATE,
                ON_STATE
            } state = INITIAL;
            static uint32_t start;
            switch (state) {
                case INITIAL:
                    start = BSP_tickCtr();
                    state = OFF_STATE; /* initial transition */
                    break;
                case OFF_STATE:
                    if ((BSP_tickCtr() - start) > BSP_TICKS_PER_SEC * 3U / 4U) {
                        BSP_ledGreenOn();
                        start = BSP_tickCtr();
                        state = ON_STATE; /* state transition */
                    }
                    break;
                case ON_STATE:
                    if ((BSP_tickCtr() - start) > BSP_TICKS_PER_SEC / 4U) {
                        BSP_ledGreenOff();
                        start = BSP_tickCtr();
                        state = OFF_STATE; /* state transition */
                    }
                    break;
                default:
                    //error();
                    break;
            }
        }
        //return 0;
    }

Antes de continuar debemos repasar un concepto fundamental: las condiciones de carrera. Estas condiciones se presentan 
cuando dos entidades concurrentes compiten por un recurso haciendo que el estado del recurso dependa de la secuencia en 
la cual se accede. El siguiente ejemplo 
ilustrará este asunto:

.. code-block:: c 
   :lineno-start: 1

    #include "TM4C123GH6PM.h"
    #include "bsp.h"

    int main() {
        SYSCTL->RCGCGPIO  |= (1U << 5); /* enable Run mode for GPIOF */
        SYSCTL->GPIOHBCTL |= (1U << 5); /* enable AHB for GPIOF */
        GPIOF_AHB->DIR |= (LED_RED | LED_BLUE | LED_GREEN);
        GPIOF_AHB->DEN |= (LED_RED | LED_BLUE | LED_GREEN);

        SysTick->LOAD = SYS_CLOCK_HZ/2U - 1U;
        SysTick->VAL  = 0U;
        SysTick->CTRL = (1U << 2) | (1U << 1) | 1U;

        SysTick_Handler();

        __enable_irq();
        while (1) {
            GPIOF_AHB->DATA = GPIOF_AHB->DATA | LED_GREEN;
            GPIOF_AHB->DATA = GPIOF_AHB->DATA & ~LED_GREEN;
        }
        //return 0;
    }

.. code-block:: c 
   :lineno-start: 1

    /* Board Support Package */
    #include "TM4C123GH6PM.h"
    #include "bsp.h"

    __attribute__((naked)) void assert_failed (char const *file, int line) {
        /* TBD: damage control */
        NVIC_SystemReset(); /* reset the system */
    }

    void SysTick_Handler(void) {
        GPIOF_AHB->DATA_Bits[LED_BLUE] ^= LED_BLUE;
    }

.. code-block:: c 
   :lineno-start: 1

    #ifndef __BSP_H__
    #define __BSP_H__

    /* Board Support Package for the EK-TM4C123GXL board */

    /* system clock setting [Hz] */
    #define SYS_CLOCK_HZ 16000000U

    /* on-board LEDs */
    #define LED_RED   (1U << 1)
    #define LED_BLUE  (1U << 2)
    #define LED_GREEN (1U << 3)

    #endif // __BSP_H__

Observemos el código generado por el compilador para las expresiones que encienden y apagan el LED verde:

.. code-block:: asm
   :lineno-start: 1

    18                GPIOF_AHB->DATA = GPIOF_AHB->DATA | LED_GREEN;
    000003d4:   4B09                ldr        r3, [pc, #0x24]
    000003d6:   F8D333FC            ldr.w      r3, [r3, #0x3fc]
    000003da:   4A08                ldr        r2, [pc, #0x20]
    000003dc:   F0430308            orr        r3, r3, #8
    000003e0:   F8C233FC            str.w      r3, [r2, #0x3fc]
    19                GPIOF_AHB->DATA = GPIOF_AHB->DATA & ~LED_GREEN;
    000003e4:   4B05                ldr        r3, [pc, #0x14]
    000003e6:   F8D333FC            ldr.w      r3, [r3, #0x3fc]
    000003ea:   4A04                ldr        r2, [pc, #0x10]
    000003ec:   F0230308            bic        r3, r3, #8
    000003f0:   F8C233FC            str.w      r3, [r2, #0x3fc]

Consideremos el caso en el cual el LED azul está apagado y el LED verde encendido. El procesador comenzará 
a ejecutar las siguientes instrucciones que apagarán el LED verde:

.. code-block:: asm
   :lineno-start: 1

    19                GPIOF_AHB->DATA = GPIOF_AHB->DATA & ~LED_GREEN;
    000003e4:   4B05                ldr        r3, [pc, #0x14]
    000003e6:   F8D333FC            ldr.w      r3, [r3, #0x3fc]
    000003ea:   4A04                ldr        r2, [pc, #0x10]
    000003ec:   F0230308            bic        r3, r3, #8
    000003f0:   F8C233FC            str.w      r3, [r2, #0x3fc]

Justo antes de ejecutar la instrucción ``000003ec: F0230308 bic r3, r3, #8`` ocurre una interrupción 
``SysTick_Handler``. Dicha interrupción enciende y apaga el LED azul cada 500 ms. En este caso el LED azul se 
encenderá. Por tanto, al salir de la interrupción, tanto el LED azul como el verde estarán encendidos. Tenga en cuenta 
que el LED azul se apagará en 500 ms. La instrucción ``000003ec: F0230308 bic r3, r3, #8`` se ejecuta y sorpresivamente 
ambos LEDs se apagan (Dice Juanito: ¿Qué pasó?). Acaba de presentarse una ``condición de carrera``.

Para enteder lo anterior, debemos analizar con cuidado el contenido del registro r3 y del puerto de entrada/salida 
justo antes de la ejecución de ``000003ec: F0230308 bic r3, r3, #8``. En ese punto ``r3 = 0x00000008`` y 
``GPIOF = 0x00000008``. Esto es así porque estamos leyendo en el registro r3 el contenido del puerto GPIOF y en este 
momento el LED verde (bit 3) está encendido. Una vez se ejecuta la interrupción, el puerto cambia (``GPIOF = 0x0000000C``) 
ya que tanto el LED azul como el verde están encendidos. Luego de la interrupción se ejcuta la instrucción  
``000003ec: F0230308 bic r3, r3, #8`` haciendo ``r3 = 0x00000000``. Note que en este momento el valor de r3 no 
está considerando el estado del LED azul. En consecuencia, al ejecutar ``000003f0: F8C233FC str.w r3, [r2, #0x3fc]`` 
el puerto ``GPIOF`` tomará el valor de r3 y ambos LEDs se apagarán. (Pregunta Juanito: ¿Y cómo se puede arreglar esto?). 
El problema ocurre porque la lectura del puerto, su modificación y posterior escritura NO ES ``ATÓMICA``. Entonces para 
solucionar el problema podemos atacarlo de dos maneras: haciendo que la lectura, modificación y escritura del recurso sea 
atómica ("indivisible") o evitando compartir el recurso. 

Estrategia atómica:

.. code-block:: c
   :lineno-start: 1

    while (1) {
        __disable_irq();
        GPIOF_AHB->DATA = GPIOF_AHB->DATA | LED_GREEN;
        __enable_irq();
        
        __disable_irq();
        GPIOF_AHB->DATA = GPIOF_AHB->DATA & ~LED_GREEN;
        __enable_irq();
    }

Estrategia no recurso compartido:

.. code-block:: c
   :lineno-start: 1

    while (1) {
        GPIOF_AHB->DATA_Bits[LED_GREEN] = LED_GREEN;
        GPIOF_AHB->DATA_Bits[LED_GREEN] = 0U;
    }

La última estrategia permite acceder de manera individual y sólo con una operación de escritura los bits del puerto 
de entrada salida. La estrategia funciona gracias a una "jugada" en hardware. La siguiente figura muestra una línea de 
dirección y de datos dedicada a cada bit del puerto de entrada salida:

.. image:: ../_static/gpioAtomic.jpeg
   :scale: 50 %

Las líneas de dirección habilitan la escritura del bit. Por tanto, si se desea escribir el bit 2 del puerto, en las 
línea correspondientes del bus de direcciones debemos colocar el valor 0x010 y escribir en el bus de datos un 0x0000000004. 
Note que en los ejemplos anteriores, al ejecutar la instrucción ``000003f0: F8C233FC str.w r3, [r2, #0x3fc]`` estamos 
escribiendo el valor del registro r3 en todos los bits del puerto GPIOF porque el valor 0x3FC en las líneas correspondientes 
del bus de direcciones habilita cada bit del puerto GPIOF.

A continuación se observa el código generado por el compilador al emplear la estrategia del recurso no compartido:

.. code-block:: asm
   :lineno-start: 1

    19                GPIOF_AHB->DATA_Bits[LED_GREEN] = LED_GREEN;
    000003d4:   4B0E                ldr        r3, [pc, #0x38]
    000003d6:   2208                movs       r2, #8
    000003d8:   621A                str        r2, [r3, #0x20]

La instrucción ``ldr r3, [pc, #0x38]`` carga la dirección del puerto GPIOF en el registro 3 (0x4005D000), ``movs r2, #8`` 
carga un 8 en en r2 y finalmente ``str r2, [r3, #0x20]`` escribe un 8 en la dirección 0x4005D000 + 0x20, es decir,  
se escribe un 1 en el bit 3 (no olvide que se numeran desde 0) del puerto GPIOF correspondiente al LED verde.

El siguiente código muestra cómo está declarado el puerto GPIOF en lenguaje C:

.. code-block:: c
   :lineno-start: 1

    typedef struct {                                    /*!< GPIOA Structure                                                       */
    __IO uint32_t  DATA_Bits[255];                    /*!< GPIO bit combinations                                                 */
    __IO uint32_t  DATA;                              /*!< GPIO Data                                                             */
    __IO uint32_t  DIR;                               /*!< GPIO Direction                                                        */
    __IO uint32_t  IS;                                /*!< GPIO Interrupt Sense                                                  */
    __IO uint32_t  IBE;                               /*!< GPIO Interrupt Both Edges                                             */
    __IO uint32_t  IEV;                               /*!< GPIO Interrupt Event                                                  */
    __IO uint32_t  IM;                                /*!< GPIO Interrupt Mask                                                   */
    __IO uint32_t  RIS;                               /*!< GPIO Raw Interrupt Status                                             */
    __IO uint32_t  MIS;                               /*!< GPIO Masked Interrupt Status                                          */
    __O  uint32_t  ICR;                               /*!< GPIO Interrupt Clear                                                  */
    __IO uint32_t  AFSEL;                             /*!< GPIO Alternate Function Select                                        */
    __I  uint32_t  RESERVED1[55];
    __IO uint32_t  DR2R;                              /*!< GPIO 2-mA Drive Select                                                */
    __IO uint32_t  DR4R;                              /*!< GPIO 4-mA Drive Select                                                */
    __IO uint32_t  DR8R;                              /*!< GPIO 8-mA Drive Select                                                */
    __IO uint32_t  ODR;                               /*!< GPIO Open Drain Select                                                */
    __IO uint32_t  PUR;                               /*!< GPIO Pull-Up Select                                                   */
    __IO uint32_t  PDR;                               /*!< GPIO Pull-Down Select                                                 */
    __IO uint32_t  SLR;                               /*!< GPIO Slew Rate Control Select                                         */
    __IO uint32_t  DEN;                               /*!< GPIO Digital Enable                                                   */
    __IO uint32_t  LOCK;                              /*!< GPIO Lock                                                             */
    __I  uint32_t  CR;                                /*!< GPIO Commit                                                           */
    __IO uint32_t  AMSEL;                             /*!< GPIO Analog Mode Select                                               */
    __IO uint32_t  PCTL;                              /*!< GPIO Port Control                                                     */
    __IO uint32_t  ADCCTL;                            /*!< GPIO ADC Control                                                      */
    __IO uint32_t  DMACTL;                            /*!< GPIO DMA Control                                                      */
    } GPIOA_Type;
    #define GPIOF_AHB_BASE                  0x4005D000UL
    #define GPIOF_AHB                       ((GPIOA_Type              *) GPIOF_AHB_BASE)

Más adelante veremos que existe una tercera técnica para controlar el acceso atómico o exclusivo a los recursos compartidos. 
Dicha opción es ofrecida por un RTOS mediante semáfaros de exclusión mutua o *mutex*. Por lo pronto retomemos la 
discusión sobre la arquitectura de múltiples *backgrounds* que ofrece un RTOS. Pregunta Juanito: ¿Cómo es posible esta magia?

Retomemos el funcionamiento de una arquitectura *background/foreground* como ilustra la figura:

.. image:: ../_static/fore-back-gound.jpeg
   :scale: 50 %

El código que enciende y apaga el LED corre en el *background*. Cuando ocurre la interrupción ``SysTick_Handler`` el 
*background* será "despojado" de la CPU de la cual se apropiará (*preemption*) el servicio de atención a 
la interrupción o ``ISR`` en el *foreground*. Una vez termine la ejecución de la ISR, el *backgound* retomará justo en el 
punto en el cual fue "desalojado" (preempted). Note también que la comunicación entre el *background/foreground* se realiza 
por medio de la variable ``l_tickCtr``. Adicionalmente, observe como la función BSP_tickCtr accede la variable. 
Pregunta el profe a Juanito: ¿Por qué se hace de esa manera?



.. note::
    Los ejemplos anterior son tomados de un excelente curso ofrecido por `Miro Samek <http://www.state-machine.com/quickstart/>`__.


