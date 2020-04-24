﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CanPathFind : MonoBehaviour
{

    private GameObject[] players;
    private GameObject target;
    private NavMeshPath path;
    private CanTrack tracking;
    private int count;
    private Vector3 vectorFinal;
    public bool canContinue;

    // Start is called before the first frame update

    void Start()
    {
        path = new NavMeshPath();
        count = 0;
        changeStateToTrue();
        tracking = transform.GetChild(2).gameObject.GetComponent<CanTrack>();
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            if (players.Length > 0)
            {
                target = players[0];
                tracking.setTarget(target);
            }
        }
        else if (target != null && canContinue)
        {
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
    }

    public void changeStateToFalse() {
        canContinue = false;
    }

    public void changeStateToTrue()
    {
        canContinue = true;
    }

    public void setPlayers(GameObject[] players) { this.players = players; }
    public GameObject[] getPlayers() { return players; }
}
