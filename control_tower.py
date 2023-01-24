from mesa import Agent
from ordered_set import OrderedSet
from datetime import timedelta
from util import PlaneStates
from plane import PlaneAgent
import datetime


class ControlTowerAgent(Agent):
    """ """

    def __init__(self, unique_id, model, planeTakeoffTime=5, planeLandingTime=5):
        super().__init__(unique_id, model)
        self.takeoffs = OrderedSet([])
        self.landings = OrderedSet([])
        self.runway1Available = True
        self.runway2Available = True
        self.startedTakeoff = None
        self.startedLanding = None
        self.takeoffTime = timedelta(seconds=planeTakeoffTime)
        self.landingTime = timedelta(seconds=planeLandingTime)

    def getPlanes(self):
        res = []
        for (content, x, y) in self.model.grid.coord_iter():
            for agent in content:
                if isinstance(agent, PlaneAgent):
                    res.append(agent)
        return res

    def reducer(self, p):
        if p.state == PlaneStates.RequestingLanding:
            self.landings |= [p]
        if p.state == PlaneStates.Emergency:
            self.landings = [p] | self.landings
        if p.state == PlaneStates.RequestingTakeoff:
            self.takeoffs |= [p]

    def step(self):
        planes = self.getPlanes()
        for p in planes:
            self.reducer(p)

        if (not self.runway1Available) and (datetime.datetime.now() >= (self.startedTakeoff + self.takeoffTime)):
            self.runway1Available = True
            self.startedTakeoff = None

        if (not self.runway2Available) and (datetime.datetime.now() >= (self.startedLanding + self.landingTime)):
            self.runway2Available = True
            self.startedLanding = None

        if len(self.takeoffs) > 0 and self.runway1Available:
            self.startedTakeoff = datetime.datetime.now()
            self.runway1Available = False
            nextToTakeOff = self.takeoffs[0]
            nextToTakeOff.nextState = PlaneStates.Flying
            # remove from set
            self.takeoffs -= [nextToTakeOff]
        if len(self.landings) > 0 and self.runway2Available:
            self.startedLanding = datetime.datetime.now()
            self.runway2Available = False
            nextToLand = self.landings[0]
            nextToLand.nextState = PlaneStates.Preparing
            # remove from set
            self.landings -= [nextToLand]

    def advance(self):
        planes = self.getPlanes()
        for p in planes:
            p.state = p.nextState
