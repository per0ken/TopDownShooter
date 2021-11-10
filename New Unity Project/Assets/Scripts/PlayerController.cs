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
        if (Input.GetKeyUp(KeyCode.W))
        {
            if (animator != null)
            {
                animator.SetBool("IsRunning", false);
            }
        }
        if (Input.GetKey(KeyCode.W))
        {
                    if(animator!=null)
            {
                animator.SetBool("IsRunning", true);
            }
            transform.Translate(Vector3.forward * movementSpeed);
        }
    }
}
