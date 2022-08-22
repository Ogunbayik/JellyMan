using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    private PlayerAnimator playerAnimator;

    private float horizontalInput;
    private float verticalInput;

    [SerializeField] private float turnSpeed;
    [Header("Border Settings")]
    [SerializeField] private float minimumZ;
    [SerializeField] private float maximumZ;
    [SerializeField] private float minimumX;
    [SerializeField] private float maximumX;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        playerAnimator = GetComponent<PlayerAnimator>();
    }
    void Update()
    {
        if(GameManager.Instance.currentState == GameManager.GameStates.InGame)
        Movement();

        SetMovementBorder();
    }

    private void Movement()
    {
        horizontalInput = Input.GetAxis(TagManager.HORIZONTAL);
        verticalInput = Input.GetAxis(TagManager.VERTICAL);
        Vector3 moveDirection = new Vector3(horizontalInput, 0, verticalInput) * turnSpeed * Time.deltaTime;

        float currentSpeed = Mathf.Abs(horizontalInput) + Mathf.Abs(verticalInput);
        playerAnimator.MovementAnim(currentSpeed);
        controller.Move(moveDirection);
    }
    private void SetMovementBorder()
    {
        if(transform.position.z < minimumZ)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, minimumZ);
        }else if(transform.position.z > maximumZ)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, maximumZ);
        }

        if(transform.position.x < minimumX)
        {
            transform.position = new Vector3(minimumX, transform.position.y, transform.position.z);
        }
        else if(transform.position.x > maximumX)
        {
            transform.position = new Vector3(maximumX, transform.position.y, transform.position.z);
        }
    }
}
