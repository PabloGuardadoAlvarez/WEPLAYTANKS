using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanTrack : MonoBehaviour
{
    private GameObject target;
    private Turret turret;
    private bool canShot;
    // Start is called before the first frame update
    void Start()
    {
        canShot = true;
        turret = GetComponent<Turret>();
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            turret.aimTurret(target.transform.position);
            RaycastHit hit;

            // Does the ray intersect any objects excluding the player layer
            if (Physics.Raycast(new Vector3(transform.position.x,transform.position.y + .65f, transform.position.z), transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
            {
                Debug.DrawRay(new Vector3(transform.position.x, transform.position.y + .65f, transform.position.z), transform.TransformDirection(Vector3.forward) * hit.distance, Color.black);
                if (hit.collider.gameObject.tag == target.gameObject.tag)
                {
<<<<<<< HEAD
                    transform.parent.GetComponent<CanPathFind>().setState(false);
=======
                    if (transform.parent.GetComponent<CanPathFind>().enabled == true) {
                        transform.parent.GetComponent<CanPathFind>().canContinue = false;
                    }
>>>>>>> 3ba3b496087660cd8d9faa0bca3bfb4a9fbcddb6
                    if (canShot)
                    {
                        StartCoroutine(shot());
                    }
                }
                else
                {
<<<<<<< HEAD
                    transform.parent.GetComponent<CanPathFind>().setState(true);
=======
                    if (transform.parent.GetComponent<CanPathFind>().enabled == true)
                    {
                        transform.parent.GetComponent<CanPathFind>().canContinue = true;
                    }
>>>>>>> 3ba3b496087660cd8d9faa0bca3bfb4a9fbcddb6
                    StopCoroutine(shot());
                }
            }
        }
    }

    IEnumerator shot()
    {

        gameObject.GetComponent<Turret>().doShot();
        canShot = false;
        yield return new WaitForSeconds(2f);
        canShot = true;
    }

    public void setTarget(GameObject target) { this.target = target; }
    public GameObject getTarget() { return target; }
}
