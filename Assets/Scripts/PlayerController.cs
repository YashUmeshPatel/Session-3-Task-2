using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private PlayerInput playerInput;

    [SerializeField] GameObject player;

    [SerializeField] GameObject test1;
    [SerializeField] GameObject test2;

    [SerializeField] private float movementSpeed;

    Vector2 PressPoint;
    Quaternion StartRotaion;
    float SceneWidth;

    Vector2 direction;

    private void Awake()
    {
        playerInput = new PlayerInput();
    }

    private void Start()
    {
        SceneWidth = Screen.width;
    }

    private void OnEnable()
    {
        playerInput.PlayerController.Movement.Enable();
        //playerInput.PlayerController.Rotation.Enable();
        playerInput.PlayerController.Testing.Enable();

        //playerInput.PlayerController.Movement.started += movePlayer;
        playerInput.PlayerController.Movement.performed += movePlayer;
        playerInput.PlayerController.Movement.canceled += movePlayer;

        //playerInput.PlayerController.Rotation.performed += rotatePlayer;

        playerInput.PlayerController.Testing.performed += Test;
        
    }

    private void Test(InputAction.CallbackContext context)
    {
        //playerInput.PlayerController.Testing.
    }

    private void OnDisable()
    {
        playerInput.PlayerController.Movement.Disable();
        playerInput.PlayerController.Rotation.Disable();
    }

    void movePlayer(InputAction.CallbackContext context)
    {        
        direction = playerInput.PlayerController.Movement.ReadValue<Vector2>();

        Debug.Log(direction);
        
    }

    

    void Testing()
    {
        //test1.transform.rotation = Quaternion.Euler(Vector3.forward * Time.deltaTime * movementSpeed);
        test1.transform.Rotate(Vector3.forward * movementSpeed * Time.deltaTime);
        test2.transform.Rotate(Vector3.forward * movementSpeed * Time.deltaTime);
    }

    void rotatePlayer(InputAction.CallbackContext context)
    {
        if (playerInput.PlayerController.Rotation.IsPressed() == true)
        {            
            PressPoint = Mouse.current.position.ReadValue();
            StartRotaion = player.transform.rotation;
        }

        else if (Input.GetMouseButton(0))
        {
            float Difference = (Mouse.current.position.ReadValue() - PressPoint).x;
            player.transform.rotation = StartRotaion * Quaternion.Euler(Vector3.forward * (Difference / SceneWidth) * -360);
        }
    }

    private void Update()
    {
        player.transform.Translate(direction * movementSpeed * Time.deltaTime);
        Testing();
    }
}
