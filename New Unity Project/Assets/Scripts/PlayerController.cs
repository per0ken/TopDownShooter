using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    float movementSpeed = 0.5f;
    private Animator animator;

    // Start is called before the first frame update
    void Start() => animator = GetComponent<Animator>();

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movementDirection = new Vector3(-horizontalInput, 0, -verticalInput);
        movementDirection.Normalize();

        bool isMoving = horizontalInput != 0 || verticalInput != 0;

        if(animator != null)
            animator.SetBool("IsRunning", isMoving);
         
        if (isMoving)
            transform.Translate(Vector3.forward * movementSpeed);

        if (movementDirection != Vector3.zero)
            transform.forward = movementDirection;
    }
}
