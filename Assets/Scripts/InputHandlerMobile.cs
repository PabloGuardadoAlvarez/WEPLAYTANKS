using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandlerMobile : MonoBehaviour
{
    Locomotor locomotor = null;
    Bomber bomber = null;
    public GameObject player;
    private GameObject turret;
    public Joystick moveJoystick ,aimJoystick;
    private Vector3 aim;
    // Start is called before the first frame update
    void Start()
    {
        locomotor = player.GetComponent<Locomotor>();
        bomber = player.GetComponent<Bomber>();
        turret = player.transform.GetChild(0).gameObject;
        moveJoystick.gameObject.SetActive(true);
            aimJoystick.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        float h, v;
        h = moveJoystick.Horizontal;
        v = moveJoystick.Vertical;
        locomotor.MoveTo(h, v);
        //hacer boton pa las bombas
        aim = new Vector3(aimJoystick.Horizontal, 0, aimJoystick.Vertical) * 10;
        turret.GetComponent<Turret>().aimTurret(aim);
        //hacer boton pa disparar

    }
    public void shootMobile(){
      turret.GetComponent<Turret>().doShot();
   }
}
