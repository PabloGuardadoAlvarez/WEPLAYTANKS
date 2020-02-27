using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    Locomotor locomotor = null;
    Bomber bomber = null;
    private GameObject turret;
    public Joystick moveJoystick ,aimJoystick;
    private Vector3 aim;

    // Start is called before the first frame update
    void Start()
    {
        locomotor = GetComponent<Locomotor>();
        bomber = GetComponent<Bomber>();
        turret = this.transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        var h = moveJoystick.Horizontal;
        var v = moveJoystick.Vertical;
        locomotor.MoveTo(h, v);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            bomber.SetBomb();
        }

        //mirar con el raton
        //var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //turret.GetComponent<Turret>().rotateTurret(ray);

        aim = new Vector3(aimJoystick.Horizontal,0,aimJoystick.Vertical) * 10;
        turret.GetComponent<Turret>().aimTurret(aim);


        if(Input.GetKeyDown(KeyCode.C))
        {
            turret.GetComponent<Turret>().doShot();
        }
    }
}
