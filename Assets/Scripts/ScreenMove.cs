using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.InputSystem.DefaultInputActions;

public class ScreenMove : MonoBehaviour
{
    [SerializeField]
    private float _speed;
    private PlayerInput _playerActions;
    private Rigidbody2D _rigidbody;
    private Vector2 _moveInput;

    public Transform Rover;


    private void Awake()
    {
        _playerActions = new PlayerInput();
        _rigidbody = GetComponent<Rigidbody2D>();
        if (_rigidbody is null)
            Debug.Log("RigidBody is null!");
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    private void FixedUpdate()
    {
        _moveInput = _playerActions.Screen.Move.ReadValue<Vector2>();
        //_moveInput.y = 0f;
        _rigidbody.velocity = _moveInput * _speed;
        //transform.LookAt(Rover);


        // Given x and y coordinates
        float x = transform.position.x - Rover.transform.position.x;
        float y = transform.position.y - Rover.transform.position.y;

        // Calculate the angle in radians
        float angleInRadians = Mathf.Atan2(x, -y);

        // Convert radians to degrees
        float angleInDegrees = angleInRadians * (180 / Mathf.PI);

        transform.rotation = Quaternion.Euler(0, 0, angleInDegrees);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnEnable()
    {
        _playerActions.Screen.Enable();
    }

    private void OnDisable()
    {
        _playerActions.Screen.Disable();
    }
}
