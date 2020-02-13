using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAim : MonoBehaviour
{
    public GameObject bullet, puntaPistola;
    public GameObject target;

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z), Vector3.up);
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 1000))
        {
            Debug.DrawLine(transform.position, hit.point);
        }
        
    }

    

    IEnumerator disparar()
    {
        while (true)
        {
            var disparo = Instantiate(bullet);
            disparo.transform.position = puntaPistola.transform.position;
            var balaRb = disparo.GetComponent<Rigidbody>();
            balaRb.AddForce(transform.forward * 375f);
            yield return new WaitForSeconds(2f);

        }
    }
}
