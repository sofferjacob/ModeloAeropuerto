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

````
$ python main.py
````
