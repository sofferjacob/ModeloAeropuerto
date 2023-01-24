from mesa import Agent
from util import PlaneStates, CONNECTIONS, get_param
import websockets
import datetime
import random
import json


class PlaneAgent(Agent):
    """ """

    def __init__(self,
                 unique_id,
                 model,
                 state,
                 arrivalTime=None,
                 preparingTime=None,
                 destination="MEX"):
        super().__init__(unique_id, model)
        self.state = state
        self.arrivalTime = arrivalTime
        self.preparingTime = preparingTime
        self.nextState = state
        self.destination = destination
        self.timePrepStarted = None
        self.timeFlightStarted = None

    def step(self):
        if self.state == PlaneStates.Preparing:
            if self.timePrepStarted == None:
                self.timePrepStarted = datetime.datetime.now()
            if datetime.datetime.now() >= (self.timePrepStarted + self.preparingTime):
                self.nextState = PlaneStates.RequestingTakeoff
                self.timePrepStarted = None
        elif self.state == PlaneStates.Flying:
            if self.destination != "MEX":
                self.nextState = PlaneStates.OutOfReach
            else:
                if self.timeFlightStarted == None:
                    self.timeFlightStarted = datetime.datetime.now()
                if datetime.datetime.now() >= (self.timeFlightStarted + self.arrivalTime):
                    if random.randint(0, get_param("CHANCE_OF_EMERGENCY")) == 1:
                        self.nextState = PlaneStates.Emergency
                    else:
                        self.nextState = PlaneStates.RequestingLanding
                    self.timeFlightStarted = None

    def advance(self):
        if self.state != self.nextState:
            message = {
                "planeId": str(self.unique_id),
                "state": str(self.nextState),
            }
            websockets.broadcast(CONNECTIONS, json.dumps(message))
        self.state = self.nextState
