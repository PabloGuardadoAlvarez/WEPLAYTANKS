using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CanPathFind : MonoBehaviour
{

    private GameObject[] players;
    private GameObject target;
    private NavMeshPath path;
    private int count;
    private Vector3 vectorFinal;
    private bool canContinue = true;

    // Start is called before the first frame update

    void Start()
    {
        path = new NavMeshPath();
        count = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(target == null)
            target = players[0];

        if (target != null && canContinue)
        {
            GetComponent<TrailEffect>().setEmitter(true);
            NavMesh.CalculatePath(transform.position, target.transform.position, NavMesh.AllAreas, path);
            vectorFinal = path.corners[count] - transform.position;

            if (path.corners.Length > 0)
            {

                gameObject.GetComponent<Locomotor>().MoveTo(new Vector3(vectorFinal.x, 0, vectorFinal.z));

                if (transform.position.x == path.corners[count].x && transform.position.z == path.corners[count].z)
                {
                    count++;
                }
            }
        }
        else
        {
            GetComponent<TrailEffect>().setEmitter(false);
        }
    }

    public void setState(bool state) { canContinue = state; }

    public bool getState() { return canContinue; }

    public void setPlayers(GameObject[] players) { this.players = players; }
    public GameObject[] getPlayers() { return players; }
}
