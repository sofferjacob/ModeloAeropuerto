using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneLanding : MonoBehaviour
{
    private Transform currentPoint; // El waypoint al que nos moveremos
    private int waypointIdx = 0; // Indice de waypoint, indica la posicion del waypoint en el arreglo
    // private float minDistance = 0.1f; // Distancia minima, nos ayuda a identificar si ya llegamos al destino
    public float speed = 200.0F; // Velocidad a la que se mueve
    private float stopDistance = 3;

    private bool islanding = false;

    private GameObject waypoints_obj;
    private Waypoints waypoints;

    void Start()
    {
        waypoints_obj = GameObject.Find("Waypoints");
        waypoints = waypoints_obj.GetComponent<Waypoints>();
        currentPoint = waypoints.LandingList[waypointIdx];
    }
    public void land()
    {
        islanding = true;
        Debug.Log("En el land");
    }


    void Update()
    {
        if (islanding)
        {
            if (waypointIdx < waypoints.LandingList.Count)
            {
                if (waypointIdx == 0)
                {
                    //waypoints.FlyList[0].transform.position
                    transform.position = Vector3.MoveTowards(waypoints.LandingList[0].transform.position, currentPoint.transform.position, speed * Time.deltaTime);
                    transform.rotation = Quaternion.Euler(15, -125, 0);
                    speed = 200F;
                }
                else
                {
                    transform.position = Vector3.MoveTowards(transform.position, currentPoint.transform.position, speed * Time.deltaTime);
                }
                if (Vector3.Distance(transform.position, waypoints.LandingList[waypointIdx].transform.position) <= stopDistance)
                {
                    if (waypointIdx < waypoints.LandingList.Count - 1)
                    {
                        waypointIdx++;

                    }
                    else
                    {
                        waypointIdx = 0;
                        transform.rotation = Quaternion.Euler(0, -40, 0);
                        islanding = false;
                    }

                    if (waypointIdx == 3)
                    {
                        // Going to the hangar
                        transform.rotation = Quaternion.Euler(0, 153, 0);
                        speed = 50.0F;
                    }
                    currentPoint = waypoints.LandingList[waypointIdx];
                }
            }
        }
    }
}
