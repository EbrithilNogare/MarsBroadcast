using UnityEngine;

public class ScreenMove : MonoBehaviour
{
    [SerializeField]
    private float _speed;
    private PlayerInput _playerActions;
    private Rigidbody2D _rigidbody;
    private Vector2 _moveInput;
    private Animator _animator;

    public Transform Rover;

    private void Awake()
    {
        _playerActions = new PlayerInput();
        _animator = GetComponentInChildren<Animator>();
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
        _rigidbody.velocity = _moveInput * _speed;

        if (_moveInput != Vector2.zero)
        {
            _animator.SetBool("isMoving", true);
        }
        else
        {
            _animator.SetBool("isMoving", false);
        }

        float x = transform.position.x - Rover.transform.position.x;
        float y = transform.position.y - Rover.transform.position.y;

        float angleInRadians = Mathf.Atan2(x, -y);
        float angleInDegrees = angleInRadians * (180 / Mathf.PI);

        transform.rotation = Quaternion.Euler(0, 0, angleInDegrees);
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
