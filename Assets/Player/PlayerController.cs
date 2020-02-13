using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 3, rotationSpeed = 90;
    protected Rigidbody _rb;
    public LayerMask mouseMask;


    // Start is called before the first frame update
    void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        var h = Input.GetAxisRaw("Horizontal");
        var v = Input.GetAxisRaw("Vertical");

        // Mirar hacia la dirección en la que avanzo de forma suave
        // Mi vector forward apunte al vector de movimiento
        var mov = new Vector3(h, 0, v).normalized;
        _rb.velocity = mov * speed + Vector3.up * _rb.velocity.y;
    }
}
