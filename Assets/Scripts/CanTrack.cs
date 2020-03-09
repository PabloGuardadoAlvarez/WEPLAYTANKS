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
        if(target != null)
            turret.aimTurret(target.transform.position);
        RaycastHit hit;

        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.black);
            if (hit.collider.gameObject.tag == target.gameObject.tag)
            {
                gameObject.transform.parent.GetComponent<CanPathFind>().canContinue = false;
                if (canShot) {
                    StartCoroutine(shot());
                }
            }
            else {
                gameObject.transform.parent.GetComponent<CanPathFind>().canContinue = true;
                StopCoroutine(shot());
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
