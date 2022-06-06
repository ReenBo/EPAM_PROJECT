using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DPlayerController : MonoBehaviour
{
    private Animator animator;
    private Rigidbody body;
    public float speed = 2f;

    void Start()
    {
        animator = GetComponent<Animator>();
        body = GetComponent<Rigidbody>();
    }

    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 directionVector = new Vector3(v, 0, h);
        animator.SetFloat("SpeedD", Vector3.ClampMagnitude(directionVector, 1).magnitude);
        body.velocity = Vector3.ClampMagnitude(directionVector,1) * speed;
    }
}
