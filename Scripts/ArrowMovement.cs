using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowMovement : MonoBehaviour
{
    private Rigidbody arrowRb;

    private float moveSpeed;
    [SerializeField] private float minSpeed;
    [SerializeField] private float maxSpeed;
    [SerializeField] private float rotateSpeed;

    private float deathPositionZ = -5f;
    private float arrowRotateZ;
    private void Awake()
    {
        arrowRb = GetComponent<Rigidbody>();
    }
    void Start()
    {
        moveSpeed = Random.Range(minSpeed, maxSpeed);
    }

    void Update()
    {
        ArrowMoveDirection();
        ArrowRotate();
        ArrowDeath();
    }

    private void ArrowMoveDirection()
    {
        Vector3 direction = Vector3.back.normalized;
        arrowRb.AddForce(direction * moveSpeed * Time.deltaTime);
    }

    private void ArrowRotate()
    {
        float arrowRotateX = 180f;
        arrowRotateZ += rotateSpeed * Time.deltaTime;
        transform.rotation = Quaternion.Euler(arrowRotateX, 0, arrowRotateZ);
    }

    private void ArrowDeath()
    {
        if(transform.position.z <= deathPositionZ)
        {
            Destroy(gameObject);
        }
    }

}
