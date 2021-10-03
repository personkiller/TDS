using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float rotationSpeed;
    Vector3 deltaMovement;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        GetInputs();
    }

    void GetInputs()
    {
        deltaMovement.y = Input.GetAxisRaw("Vertical");
        deltaMovement.x = Input.GetAxisRaw("Horizontal");

        float magnitude = Mathf.Clamp01(deltaMovement.magnitude);
        deltaMovement.Normalize();

        transform.position += (deltaMovement * speed * magnitude * Time.deltaTime);

        if (deltaMovement != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(Vector3.forward, deltaMovement);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }
    }
}
