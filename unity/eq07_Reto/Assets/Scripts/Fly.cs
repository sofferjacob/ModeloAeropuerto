using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fly : MonoBehaviour
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
        currentPoint = waypoints.FlyList[waypointIdx];
    }
    public void land()
    {
        islanding = true;
        //Debug.Log("En el land");
    }


    void Update()
    {
        if (islanding)
        {
            if (waypointIdx < waypoints.FlyList.Count)
            {
                if (waypointIdx == 0)
                {
                    //waypoints.FlyList[0].transform.position
                    transform.position = Vector3.MoveTowards(waypoints.FlyList[0].transform.position, currentPoint.transform.position, speed * Time.deltaTime);
                }
                else
                {
                    transform.position = Vector3.MoveTowards(transform.position, currentPoint.transform.position, speed * Time.deltaTime);
                }
                if (Vector3.Distance(transform.position, waypoints.FlyList[waypointIdx].transform.position) <= stopDistance)
                {
                    //Debug.Log("Waypoint reached");
                    //Debug.Log(waypointIdx);
                    if (waypointIdx < waypoints.FlyList.Count - 1)
                    {
                        waypointIdx++;
                    }
                    else if (waypointIdx >= waypoints.FlyList.Count - 1)
                    {
                        waypointIdx = waypoints.FlyList.Count - 1;
                    }
                    if (waypointIdx == 2)
                    {
                        transform.Rotate(15, 0, 0);
                        transform.position = new Vector3(700, 200, 900);
                        islanding = false;
                    }
                    currentPoint = waypoints.FlyList[waypointIdx];
                }
            }
        }
    }
}
