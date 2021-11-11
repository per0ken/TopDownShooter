using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    float movementSpeed = 0.5f;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movementDirection = new Vector3(-horizontalInput, 0, -verticalInput);
        movementDirection.Normalize();

        Vector3 movement = new Vector3(0.0f, verticalInput, horizontalInput);

        if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.S))
        {
            if (animator != null)
            {
                animator.SetBool("IsRunning", false);
            }
        }

        //Rigidbody rb = GetComponent<Rigidbody>();
        //GetComponent<Rigidbody>().velocity = movement * movementSpeed;

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.S))
        {
                    if(animator!=null)
            {
                animator.SetBool("IsRunning", true);
            }
            transform.Translate(Vector3.forward * movementSpeed);
        }


        if (movementDirection != Vector3.zero)
        {
            transform.forward = movementDirection;
        }
    }
}
