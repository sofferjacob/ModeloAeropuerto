// https://medium.com/unity-nodejs/websocket-client-server-unity-nodejs-e33604c6a006
using UnityEngine;
using WebSocketSharp;
using System.Collections;
using System.Collections.Generic;

public class Client : MonoBehaviour
{
    WebSocket ws;
    public GameObject plane;        // <- prefab
    public GameObject flypoint;     // <- point were planes stay during the state "Flying"
    List<GameObject> planes = new List<GameObject>();

    private Queue<int> takeoff_list = new Queue<int>();
    private Queue<int> landing_list = new Queue<int>();

    bool firstMessage = true;

    private InitialState initialState;
    private int shouldInstantiate = 0;

    private void takeoff(int index)
    {
        planes[index].GetComponent<Plane>().takeoff();
    }
    private void landing(int index)
    {
        Debug.Log(index);
        planes[index].GetComponent<PlaneLanding>().land();
    }
    private void Start()
    {
        ws = new WebSocket("ws://localhost:5678");
        ws.Connect();

        ws.OnMessage += (sender, e) =>
        {
            Debug.Log("Message Received from " + ((WebSocket)sender).Url + ", Data : " + e.Data);
            if (firstMessage)
            {
                initialState = JsonUtility.FromJson<InitialState>(e.Data);
                //Debug.Log(initialState.initialState.Count);
                firstMessage = false;
            }
            else
            {
                var m = JsonUtility.FromJson<PlaneState>(e.Data);
                //Debug.Log("In else");
                //Debug.Log(m.state);
                if (m.state == "2")
                {
                    var index = int.Parse(m.planeId.Substring(1));
                    takeoff_list.Enqueue(index);

                }
                else if (m.state == "0")
                {
                    var index = int.Parse(m.planeId.Substring(1));
                    landing_list.Enqueue(index);
                }
                else if (m.state == "4")
                {
                    Debug.Log("in else 4");
                    // TODO: Destroy the plane object and remove from map
                    // GameObject.destroy()
                }
            }
        };
    }

    private void Update()
    {
        if (takeoff_list.Count != 0){
            var index = takeoff_list.Dequeue();
            takeoff(index);
        }
        if (landing_list.Count != 0){
            var index = landing_list.Dequeue();
            landing(index);
        }

        if (initialState == null || shouldInstantiate == initialState.initialState.Count) return;

        var m = initialState.initialState[shouldInstantiate];
        shouldInstantiate += 1;

        // TODO: Figure out where to instantiate the airplanes
        if (m.state == "2")
        {
            GameObject obj = (GameObject)Instantiate(plane, flypoint.transform.position, Quaternion.Euler(0, -30, 0));
            planes.Add(obj);
            //Debug.Log($"Items in list: {planes.Count}");
        }
        else
        {
            // Runs if state is preparing, requesting takeoff / landing
            GameObject obj = (GameObject)Instantiate(plane, new Vector3(203, 1, 122), Quaternion.Euler(0, -30, 0));
            planes.Add(obj);
            //Debug.Log($"Items in list: {planes.Count}");
        }
    }
}