using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float jumpspeed;
    [SerializeField] private float speed;
    private Vector3 _moveDirection;
    private bool isGrounded;
    private Rigidbody rb;
    [SerializeField, Range(1,20)] private float mouseSensX;
    [SerializeField, Range(1,20)] private float mouseSensY;
    private Vector2 currentRotation;
    [SerializeField] private Transform lookAtPoint;
    [SerializeField, Range(-90, 0)] private float minViewAngle;
    [SerializeField, Range(0, 90)] private float maxViewAngle;
    [SerializeField] private Rigidbody bulletPrefab;
    [SerializeField] private float bulletForce;



    // Start is called before the first frame update
    void Start()
    {
        InputManager.Init(this);
        InputManager.SetGameControls();

        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.rotation * (speed * Time.deltaTime * _moveDirection);
        CheckGround();
    }

    public void SetMovementDirection(Vector3 currentDirection)
    {
        _moveDirection = currentDirection;
    }

    public void Jump()
    {
        if (isGrounded)
        {
            rb.AddForce(Vector3.up * jumpspeed, ForceMode.Impulse);
        }
    }

    private void CheckGround()
    {
       isGrounded = Physics.Raycast(transform.position, Vector3.down, GetComponent<Collider>().bounds.size.y);
        Debug.DrawRay(transform.position, Vector3.down * GetComponent<Collider>().bounds.size.y, Color.green, 0, false);
    }

    public void SetLookDirection(Vector2 readValue)
    {
        currentRotation.x += readValue.x * Time.deltaTime * mouseSensX;
        currentRotation.y += readValue.y * Time.deltaTime * mouseSensY;

        transform.rotation = Quaternion.AngleAxis(currentRotation.x, Vector3.up);
        currentRotation.y = Mathf.Clamp(currentRotation.y, minViewAngle, maxViewAngle);
        lookAtPoint.localRotation = Quaternion.AngleAxis(currentRotation.y, Vector3.right);


    }

    public void Shoot()
    {
        Rigidbody currentProjectile = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        currentProjectile.AddForce(lookAtPoint.forward * bulletForce, ForceMode.Impulse);
        Destroy(currentProjectile.gameObject, 3);
    }
}
