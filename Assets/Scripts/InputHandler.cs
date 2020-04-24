using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InputHandler : MonoBehaviour
{
    Locomotor locomotor = null;
    Bomber bomber = null;
    private GameObject turret;
    [SerializeField]
    private bool pcControl;
    public Joystick moveJoystick ,aimJoystick;
    private Vector3 aim;

    // Start is called before the first frame update
    void Start()
    {
        locomotor = GetComponent<Locomotor>();
        bomber = GetComponent<Bomber>();
        turret = this.transform.GetChild(2).gameObject;
        if (pcControl)
        {
            moveJoystick.gameObject.SetActive(false);
            aimJoystick.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        float h, v;
        if (!pcControl)
        {
            h = moveJoystick.Horizontal;
            v = moveJoystick.Vertical;
        }
        else
        {
            h = Input.GetAxisRaw("Horizontal");
            v = Input.GetAxisRaw("Vertical");
        }
        locomotor.MoveTo(h, v);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            bomber.SetBomb();
        }

        //mirar con el raton
        if (pcControl)
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            turret.GetComponent<Turret>().rotateTurret(ray);
            if (Input.GetMouseButtonDown(0))
            {
                turret.GetComponent<Turret>().doShot();
            }
        }
        else
        {
            aim = new Vector3(aimJoystick.Horizontal, 0, aimJoystick.Vertical) * 10;
            turret.GetComponent<Turret>().aimTurret(aim);
        }
    }
    public bool getIsPC() { return pcControl; }
    public void setIsPC(bool pcControl) { this.pcControl = pcControl; }
}
