using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private PlayerInput playerInput;

    [SerializeField] GameObject player;

    [SerializeField] GameObject projectilePrefab;

    [SerializeField] private float movementSpeed;

    public FixedJoystick fixedJoystick;

    Vector2 pressPoint;
    Quaternion startRotation;
    
    float sceneWidth;
    bool flag = false;

    Vector2 direction;

    private void Awake()
    {
        playerInput = new PlayerInput();
    }

    private void Start()
    {
        sceneWidth = Screen.width;
    }

    private void Update()
    {       
        player.transform.Translate(direction * Time.deltaTime * movementSpeed, Camera.main.transform);
        PlayerMovementJoyStick();
    }

    private void OnEnable()
    {
        playerInput.PlayerController.Enable();

        playerInput.PlayerController.Shoot.started += CreatingBullet;

        playerInput.PlayerController.Movement.performed += movePlayer;
        playerInput.PlayerController.Movement.canceled += movePlayer;

        playerInput.PlayerController.Rotate.started += rotatePlayer;
        playerInput.PlayerController.Rotate.performed += rotatePlayer;

        playerInput.PlayerController.RotateTouch.started += rotatePlayer;
        playerInput.PlayerController.RotateTouch.performed += rotatePlayer;
    }

    private void OnDisable()
    {
        playerInput.PlayerController.Disable();

        playerInput.PlayerController.Shoot.started -= CreatingBullet;

        playerInput.PlayerController.Movement.performed -= movePlayer;
        playerInput.PlayerController.Movement.canceled -= movePlayer;

        playerInput.PlayerController.Rotate.started -= rotatePlayer;
        playerInput.PlayerController.Rotate.performed -= rotatePlayer;

        playerInput.PlayerController.RotateTouch.started -= rotatePlayer;
        playerInput.PlayerController.RotateTouch.performed -= rotatePlayer;
    }

    void movePlayer(InputAction.CallbackContext context)
    {        
        direction = playerInput.PlayerController.Movement.ReadValue<Vector2>();

        Debug.Log(direction);       
    }

    void rotatePlayer(InputAction.CallbackContext context)
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            if (context.phase == InputActionPhase.Started)
            {
                pressPoint = context.ReadValue<Vector2>();
                startRotation = player.transform.rotation;
            }
            else if (context.phase == InputActionPhase.Performed && flag == false)
            {
                float Difference = (context.ReadValue<Vector2>() - pressPoint).x;
                player.transform.rotation = startRotation * Quaternion.Euler(Vector3.forward * (Difference / sceneWidth) * -360);

                Debug.Log("MainDifference " + Difference);
                Debug.Log("MainRotation " + startRotation * Quaternion.Euler(Vector3.forward * (Difference / sceneWidth) * -360));
            }
        }
    }

    void PlayerMovementJoyStick()
    {
        if (fixedJoystick.Vertical != 0 && fixedJoystick.Horizontal != 0)
        {
            flag = true;
            float verticalDirection = fixedJoystick.Vertical;
            float horizontalDirection = fixedJoystick.Horizontal;

            Vector2 direction = new Vector2(horizontalDirection, verticalDirection);

            player.transform.Translate(direction * Time.deltaTime * movementSpeed);
        }
        else
            flag = false;
    }

    public void CreatingBullet(InputAction.CallbackContext context)
    {
        Instantiate(projectilePrefab, player.transform.position, player.transform.rotation);
    }

    public void CreatingBulletButton()
    {
        Instantiate(projectilePrefab, player.transform.position, player.transform.rotation);
    }
}
