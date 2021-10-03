using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float rotationSpeed;
    Vector3 deltaMovement;
    public Vector3 movementDirection = new Vector3(0, 1);
    public GameObject bulletPrefab = null;
    float shotCooldown = 0.6f;
    public float shotCooldownTimer = 0;

    void Start()
    {

    }

    void Update()
    {
        if (shotCooldown > 0)
        {
            shotCooldownTimer -= Time.deltaTime;
        }
        GetInputs();
    }

    void GetInputs()
    {
        deltaMovement.y = Input.GetAxis("Vertical");
        deltaMovement.x = Input.GetAxis("Horizontal");

        float magnitude = Mathf.Clamp01(deltaMovement.magnitude);
        deltaMovement.Normalize();

        transform.position += (deltaMovement * speed * magnitude * Time.deltaTime);

        if (deltaMovement != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(Vector3.forward, deltaMovement);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
            UpdateMovementDirection();
        }

        if (shotCooldownTimer <= 0 && Input.GetAxisRaw("Shoot") > 0)
        {
            GameObject bulletGo = Instantiate(bulletPrefab, transform.position + movementDirection * 0.5f, transform.rotation);
            Rigidbody2D bulletRb = bulletGo.GetComponent<Rigidbody2D>();
            // bulletRb.velocity = Vector2.up * 5;
            bulletRb.velocity = movementDirection * 5;
            shotCooldownTimer = shotCooldown;
        }
    }

    void UpdateMovementDirection()
    {
        movementDirection = new Vector3(Mathf.Round(deltaMovement.x), Mathf.Round(deltaMovement.y));
    }
}
