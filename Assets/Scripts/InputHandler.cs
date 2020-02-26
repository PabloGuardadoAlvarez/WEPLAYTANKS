using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    Locomotor locomotor = null;
    Bomber bomber = null;
    private GameObject turret;
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
        var h = Input.GetAxisRaw("Horizontal");
        var v = Input.GetAxisRaw("Vertical");
        locomotor.MoveTo(h, v);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            bomber.SetBomb();
        }
        
        //mirar con el raton
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        
        turret.GetComponent<Turret>().rotateTurret(ray);
        if (Input.GetMouseButtonDown(0))
        {
            turret.GetComponent<Turret>().doShot();
        }
    }
}
