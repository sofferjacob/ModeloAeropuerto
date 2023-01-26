import random

import json
import websockets
import asyncio
from util import *
from model import AirportModel


new_model = AirportModel(get_param("N"), get_param("M"), get_param("NUM_OF_PLANES"), get_param("PLANE_TAKEOFF_TIME_SECONDS"), get_param("PLANE_LANDING_TIME_SECONDS"), get_param("PLANE_ARRIVAL_TIME_SECONDS_LOWER"),
                         get_param("PLANE_ARRIVAL_TIME_SECONDS_HIGHER"), get_param("PLANE_PREPARING_TIME_SECONDS_LOWER"), get_param("PLANE_PREPARING_TIME_SECONDS_HIGHER"))


async def handle_connection(websocket):
    CONNECTIONS.add(websocket)
    await websocket.send(json.dumps({"initialState": new_model.getStatus()}))
    try:
        await websocket.wait_closed()
    finally:
        CONNECTIONS.remove(websocket)


async def run_model():
    while True:
        new_model.step()
        await asyncio.sleep(random.random() * 2 + 1)


async def main():
    async with websockets.serve(handle_connection, get_param("MODEL_HOST", as_int=False), get_param("MODEL_PORT")):
        await run_model()
    data = new_model.datacollector.get_model_vars_dataframe()
    print(data)


if __name__ == "__main__":
    print(f"Model is running on port {get_param('MODEL_PORT')}")
    asyncio.run(main())
