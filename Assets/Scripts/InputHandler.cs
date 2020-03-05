﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    Locomotor locomotor = null;
    Bomber bomber = null;
    private GameObject turret;
    public GameObject player;
    public Joystick moveJoystick ,aimJoystick;
    private Vector3 aim;

    // Start is called before the first frame update
    void Start()
    {
        locomotor = player.GetComponent<Locomotor>();
        bomber = player.GetComponent<Bomber>();
        turret = player.transform.GetChild(0).gameObject;
        moveJoystick.gameObject.SetActive(false);
        aimJoystick.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        float h, v;
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");
        locomotor.MoveTo(h, v);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            bomber.SetBomb();
        }
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        turret.GetComponent<Turret>().rotateTurret(ray);
        if (Input.GetMouseButtonDown(0))
        {
            turret.GetComponent<Turret>().doShot();
        }
    }
}
