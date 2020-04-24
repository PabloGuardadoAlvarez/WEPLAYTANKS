using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField]
    private GameObject linkedPortal;
    private Transform spawnLocation;

    // Start is called before the first frame update
    void Start()
    {
        spawnLocation = transform.GetChild(0).transform;
        if (linkedPortal != null){
            linkedPortal.GetComponent<Portal>().setLinkedPortal(this.gameObject);
        }
    }

    public GameObject getLinkedPortal() { return linkedPortal;  }
    public void setLinkedPortal(GameObject linkedPortal) { this.linkedPortal = linkedPortal; }

    public Vector3 getSpawnLocation() { return spawnLocation.transform.position; }
    // Update is called once per frame
    void Update()
    {
        
    }
}
