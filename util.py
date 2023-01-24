from enum import IntEnum
import os


class PlaneStates(IntEnum):
    Preparing = 0,
    RequestingTakeoff = 1,
    Flying = 2,
    Emergency = 3,
    OutOfReach = 4,
    RequestingLanding = 5,


# Constants
# CHANCE_OF_EMERGENCY = 50
# PLANE_PREPARING_TIME_SECONDS_LOWER = 10
# PLANE_PREPARING_TIME_SECONDS_HIGHER = 30
# PLANE_ARRIVAL_TIME_SECONDS_LOWER = 10
# PLANE_ARRIVAL_TIME_SECONDS_HIGHER = 30
# PLANE_TAKEOFF_TIME_SECONDS = 5
# PLANE_LANDING_TIME_SECONDS = 5
# N = 20
# M = 20
# NUM_OF_PLANES = 5
# DURATION_OF_SIMULATION_MINUTES = 1
CONNECTIONS = set()

_defaults = {
    "CHANCE_OF_EMERGENCY": 50,
    "PLANE_PREPARING_TIME_SECONDS_LOWER": 10,
    "PLANE_PREPARING_TIME_SECONDS_HIGHER": 30,
    "PLANE_ARRIVAL_TIME_SECONDS_LOWER": 10,
    "PLANE_ARRIVAL_TIME_SECONDS_HIGHER": 30,
    "PLANE_TAKEOFF_TIME_SECONDS": 5,
    "PLANE_LANDING_TIME_SECONDS": 5,
    "N": 20,
    "M": 20,
    "NUM_OF_PLANES": 5,
    "DURATION_OF_SIMULATION_MINUTES": 1,
    "MODEL_HOST": "localhost",
    "MODEL_PORT": 5678,
}


def get_param(name, as_int=True):
    if name in os.environ:
        if as_int:
            return int(os.environ[name])
        else:
            return os.environ[name]
    else:
        return _defaults[name]
