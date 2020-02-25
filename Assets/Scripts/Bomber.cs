using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomber : MonoBehaviour
{
    private float height = 0.3f;
    public GameObject bomb;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetBomb()
    {
        var position = GetComponent<Transform>().transform.position;
        bomb.transform.position = new Vector3(position.x, height, position.z);
        Instantiate(bomb);
    }
}
