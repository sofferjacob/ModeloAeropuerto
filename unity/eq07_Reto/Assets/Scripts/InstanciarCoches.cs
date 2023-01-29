using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstanciarCoches : MonoBehaviour
{
    public GameObject PrefabCoche;
    // Start is called before the first frame update
    void Start()
    {
        Instantiate(PrefabCoche, new Vector3(-20, 0, -20), Quaternion.Euler(0, 180, 0));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
