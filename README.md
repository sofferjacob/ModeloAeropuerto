# Modelo Reto
Simulación del aeropuerto de la Ciudad de México usando un modelo multiágentes. Puede interactuar con clientes por medio de
WebSockets que dan el estado de la simulación en tiempo real.

## Archivos
* `main.py`: Archivo principal que ejecuta el servidor.
* `control_tower.py`: Agente de torre de control.
* `model.py`: Modelo
* `plane.py`: Agente de avión.
* `util.py`: Funciones de utilidad.

Adicionalmente se incluye un cliente de prueba en los archivos `client.html` y `client.js`.

## Configuración
Todos los parametros de la simulación se pueden configurar por medio de "environment variables", así como el host y puerto donde corre el servidor.
De no ser configurados, se usan valores predeterminados. A continuación la lista de los parametros que se pueden configurar:

* `MODEL_HOST`: Host donde corre el servidor. Por defecto es `localhost`.
* `MODEL_PORT`: Puerto donde corre el servidor.
* `CHANCE_OF_EMERGENCY`: Probabilidad de que un avión tenga una emergencia.
* `PLANE_PREPARING_TIME_SECONDS_LOWER`
* `PLANE_PREPARING_TIME_SECONDS_HIGHER`
* `PLANE_ARRIVAL_TIME_SECONDS_LOWER`
* `PLANE_ARRIVAL_TIME_SECONDS_HIGHER`
* `PLANE_TAKEOFF_TIME_SECONDS`
* `PLANE_LANDING_TIME_SECONDS`
* `N`: Alto del grid
* `M`: Ancho del grid
* `NUM_OF_PLANES`
* `DURATION_OF_SIMULATION_MINUTES`

## Uso

1. Descargar todos los archivos de multiagentes y el package de Unity.
2. Abrir un proyecto de Unity e importar el package.
3. Descargar Probuilder desde el package manager de Unity.
4. Descargar NuGet para Unity siguiendo estas instrucciones:
  - Open Package Manager window (Window | Package Manager)
  - Click + button on the upper-left of a window, and select "Add package from git URL..."
  - Enter the following URL and click Add button
  - [https://github.com/GlitchEnzo/NuGetForUnity.git?path=/src/NuGetForUnity](https://github.com/GlitchEnzo/NuGetForUnity.git?path=/src/NuGetForUnity)
5. Desde la ventana de NuGet en Unity, buscar e instalar _WebSocketSharp-netstandard_
6. Abrir la localización en donde se encuentran los archivos de multiagentes (en donde los descargaste).
7. **Opcional:** crear un ambiente virtual en python
  - ````python3 -m venv venv````
  - ````source venv/bin/activate````
8. **Opcional** :establecer variables de entorno para la ejecución. En caso de no establecerlas se utilizarán los valores por defecto.
- Las variables que se pueden establecer son las siguientes:
    - MODEL\_HOST: Host donde corre el servidor. Por defecto es localhost.
    - MODEL\_PORT: Puerto donde corre el servidor.
    - NUM\_OF\_PLANES: número de aviones en la simulación. Por defecto es 5.
    - CHANCE\_OF\_EMERGENCY: Probabilidad de que un avión tenga una emergencia.
    - PLANE\_PREPARING\_TIME\_SECONDS\_LOWER
    - PLANE\_PREPARING\_TIME\_SECONDS\_HIGHER
    - PLANE\_ARRIVAL\_TIME\_SECONDS\_LOWER
    - PLANE\_ARRIVAL\_TIME\_SECONDS\_HIGHER
    - PLANE\_TAKEOFF\_TIME\_SECONDS
    - PLANE\_LANDING\_TIME\_SECONDS
    - N: Alto del grid
    - M: Ancho del grid
    - DURATION\_OF\_SIMULATION\_MINUTES
- Para establecer una variable de entorno:
  - En Mac o Linux:
      - ````export NOMBRE\_DE\_LA\_VARIABLE=VALOR````
  - En Windows:
      - ````set NOMBRE\_DE\_LA\_VARIABLE=VALOR````
9. Correr los siguientes comandos:
  - ````pip install -r requirements.txt````
  - ````python main.py````
10. Correr el juego desde Unity en la escena "AirportScene\_JE"
