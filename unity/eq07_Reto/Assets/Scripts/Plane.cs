using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plane : MonoBehaviour
{

    // we reference the Waypoints class {TakeOffList, LandingList}
    private Transform currentPoint; // El waypoint al que nos moveremos
    private int waypointIdx = 0; // Indice de waypoint, indica la posicion del waypoint en el arreglo
    // private float minDistance = 0.1f; // Distancia minima, nos ayuda a identificar si ya llegamos al destino
    public float speed = 200.0F; // Velocidad a la que se mueve
    private float stopDistance = 3;

    private bool istakingoff = false;
    private GameObject waypoints_obj;
    private Waypoints waypoints;

    void Start()
    {
        waypoints_obj = GameObject.Find("Waypoints");
        waypoints = waypoints_obj.GetComponent<Waypoints>();
        currentPoint = waypoints.TakeOffList[waypointIdx];
    }

    public void takeoff()
    {
        istakingoff = true;
        //Debug.Log("En el takeoff");
    }

    void Update()
    {
        // check islanding and istakingoff
        if (istakingoff)
        {
            if (waypointIdx < waypoints.TakeOffList.Count)
            {
                if (waypointIdx == 0)
                {
                    //Starting at the hangar
                    transform.position = Vector3.MoveTowards(waypoints.TakeOffList[0].transform.position, currentPoint.transform.position, speed * Time.deltaTime);
                    transform.rotation = Quaternion.Euler(0, -63, 0);
                    speed = 50F;
                }
                else
                {
                    transform.position = Vector3.MoveTowards(transform.position, currentPoint.transform.position, speed * Time.deltaTime);
                }
                if (Vector3.Distance(transform.position, waypoints.TakeOffList[waypointIdx].transform.position) <= stopDistance)
                {
                    if (waypointIdx < waypoints.TakeOffList.Count - 1)
                    {
                        waypointIdx++;
                    }
                    else
                    {
                        waypointIdx = 0;
                        transform.rotation = Quaternion.Euler(0, -40, 0);
                        istakingoff = false;
                    }

                    if (waypointIdx == 3)
                    {
                        // Taking off
                        transform.rotation = Quaternion.Euler(-10, 55, 0);
                        speed = 200.0F;
                    }
                    currentPoint = waypoints.TakeOffList[waypointIdx];
                }
            }
        }
    }
}
