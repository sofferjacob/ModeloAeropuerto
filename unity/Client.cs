// https://medium.com/unity-nodejs/websocket-client-server-unity-nodejs-e33604c6a006
using UnityEngine;
using WebSocketSharp;
using System.Collections.Generic;

public class Client : MonoBehaviour
{
    WebSocket ws;
    public GameObject plane;
    public int numPlanes;
    Dictionary<string, GameObject> planes = new Dictionary<String, GameObject>();
    bool firstMessage = true;

    private void Start()
    {
        ws = new WebSocket("ws://localhost:5678");
        ws.Connect();
        ws.OnMessage += (sender, e) =>
        {
            if (firstMessage) {
                firstMessage = false;
                var message = JsonUtility.FromJson<InitialState>(e.Data);
                foreach (var e in message.initialState) {
                    // TODO: Figure out where to instantiate the airplanes
                    if (e.state == "PlaneStates.Flying") {
                        planes[e.planeId] = Instantiate(plane, new Vector3(0, 0, 0));
                    } else {
                        // Runs if state is preparing, requesting takeoff / landing
                        planes[e.planeId] = Instantiate(plane, new Vector3(0, 0, 0));
                    }
                }
            } else {
                var m = JsonUtility.FromJson<PlaneState>(e.Data);
                if (m.state == "PlaneStates.Flying") {
                    planes[m.planeId].takeoff();
                } else if (m.state == "PlaneStates.Preparing") {
                    planes[m.plane].land();
                } else if (m.state == "PlaneStates.OutOfReach") {
                    // TODO: Destroy the plane object and remove from map
                }
            }
            Debug.Log("Message Received from "+((WebSocket)sender).Url+", Data : "+e.Data);
        };
    }
}