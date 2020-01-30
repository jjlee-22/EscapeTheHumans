using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    public float speed;
}

private Void Start()
{
    rb = GetComponent<Rigidbody>();
}

private void FixedUpdate()
{
    // Gets input for movement
    float moveHorizonal = Input.GetAxis("Horizontal");
    float moveVertical = Input.GetAxis("Vertical");

    Vector3 movement = new Vector3(moveHorizonal, 0.0f, moveVertical);

    // Applies movement to the rigidfbody
    rb.AddForce(movement*speed);
}
