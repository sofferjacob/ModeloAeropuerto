from mesa import Model
from mesa.time import SimultaneousActivation
from mesa.space import MultiGrid
import random
from plane import PlaneAgent
from control_tower import ControlTowerAgent
from util import PlaneStates
from datetime import timedelta


class AirportModel(Model):
    """ """

    def __init__(self, N=20, M=20, numPlanes=5, planeTakeOffTime=5, planeLandingTime=5, planeArrivalTimeLower=10, planeArrivalTimeHigher=30, planePreparingTimeLower=10, planePreparingTimeHigher=30) -> None:
        self.schedule = SimultaneousActivation(self)
        self.num = 0  # iteration
        self.planeTakeOffTime = planeTakeOffTime
        self.planeLandingTime = planeLandingTime
        self.planeArrivalTimeLower = planeArrivalTimeLower
        self.planeArrivalTimeHigher = planeArrivalTimeHigher
        self.planePreparingTimeLower = planePreparingTimeLower
        self.planePreparingTimeHigher = planePreparingTimeHigher
        # Create space
        self.grid = MultiGrid(N, M, False)
        # Add the plane agents
        self.addPlaneAgents(numPlanes)
        # Add the control tower agent
        self.addControlTower(N, M)

    def addPlaneAgents(self, num):
        for i in range(num):
            arrivalTime = random.randint(
                self.planeArrivalTimeLower, self.planeArrivalTimeHigher)
            preparingTime = random.randint(
                self.planePreparingTimeLower, self.planePreparingTimeHigher)
            flying = bool(random.randint(0, 1))
            p = PlaneAgent(f'p{i}', self, PlaneStates.Flying if flying else PlaneStates.Preparing, timedelta(
                seconds=arrivalTime), timedelta(seconds=preparingTime))
            self.schedule.add(p)
            self.grid.place_agent(p, (0, 0))

    def getStatus(self):
        res = []
        for (content, x, y) in self.grid.coord_iter():
            for agent in content:
                if isinstance(agent, PlaneAgent):
                    data = {
                        "planeId": str(agent.unique_id),
                        "state": str(agent.state.value),
                    }
                    res.append(data)
        return res

    def addControlTower(self, N, M):
        ct = ControlTowerAgent(
            "CT", self, self.planeTakeOffTime, self.planeLandingTime)
        self.schedule.add(ct)
        self.grid.place_agent(ct, (N//2, M//2))

    def step(self):
        """Advance the model by one step."""
        self.num += 1
        self.schedule.step()
