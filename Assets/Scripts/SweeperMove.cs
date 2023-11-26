using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class SweeperMove : MonoBehaviour
{
    [SerializeField]
    private float speed;
    private PlayerInput _playerActions;
    private Rigidbody2D _rigidbody;
    private Vector2 _moveInput;
    private Animator _animator;
    [SerializeField] private SurfaceController tileMapController;

    //public Transform Sweeper;
    public Camera camera;


    // Start is called before the first frame update
    void Start()
    {
        _playerActions = new PlayerInput();
        _animator = GetComponentInChildren<Animator>();
        _rigidbody = GetComponent<Rigidbody2D>();
        if (_rigidbody is null)
            Debug.Log("RigidBody is null!");
    }

    // Update is called once per frame
    private void FixedUpdate()
    {

    }

    DG.Tweening.Core.TweenerCore<Vector3, Vector3, DG.Tweening.Plugins.Options.VectorOptions> tween;
    public void SweeperWalk(CallbackContext context)
    {
        if (!context.started) return;

        if (_animator.GetBool("IsSweeping")) return;

        if (EventSystem.current.IsPointerOverGameObject(0) || EventSystem.current.IsPointerOverGameObject(-1)) return;
        Vector2 position = Mouse.current.position.ReadValue();

        float cameraSize = camera.orthographicSize;
        Vector3 worldMousePosition = camera.ScreenToWorldPoint(new Vector3(position.x, position.y, cameraSize));
        Vector2Int coordinate = new Vector2Int((int)Mathf.Floor(worldMousePosition.x), (int)Mathf.Floor(worldMousePosition.y));

        _animator.SetBool("IsMoving", true);

        float x = transform.position.x - coordinate.x;
        float y = transform.position.y - coordinate.y;
        float angleInRadians = Mathf.Atan2(y, x);
        float angleInDegrees = angleInRadians * (180f / Mathf.PI);
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angleInDegrees + 180));

        tween.Kill(false);
        tween = transform.DOMove(new Vector3(coordinate.x, coordinate.y, transform.position.z), speed).SetSpeedBased().OnComplete(() =>
        {
            _animator.SetBool("IsMoving", false);
            TrySweep(coordinate.x, coordinate.y);
        });
    }

    public void TrySweep(int x, int y)
    {
        if (tileMapController.IsThisTileFootStep(x, y))
        {
            _animator.SetBool("IsSweeping", true);
            AudioConnector.Instance.PlaySweepSound();

            // wait cca 3 sec
            StartCoroutine(SweepCouritine(x, y));
        }
    }

    IEnumerator SweepCouritine(int x, int y)
    {
        yield return new WaitForSeconds(3);
        tileMapController.CleanTile(x, y);
        _animator.SetBool("IsSweeping", false);
        AudioConnector.Instance.StopSweepSound();
    }
}
