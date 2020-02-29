using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CanPathFind : MonoBehaviour
{

    public GameObject player;
    private NavMeshPath path;
    private float elapsed = 0.0f;
    private int count;
    private Vector3 vectorFinal;
    // Start is called before the first frame update

    void Start()
    {
        path = new NavMeshPath();
        elapsed = 0.0f;
        count = 0;
    }

    // Update is called once per frame
    void Update()
    {

        NavMesh.CalculatePath(transform.position, player.transform.position, NavMesh.AllAreas, path);
        vectorFinal = path.corners[count] - transform.position;

        if (path.corners.Length > 0)
        {
            
            gameObject.GetComponent<Locomotor>().MoveTo(new Vector3(vectorFinal.x,0,vectorFinal.z));

            if (transform.position.x == path.corners[count].x && transform.position.z == path.corners[count].z)
            {
                count++;
            }
        }

    }
}
