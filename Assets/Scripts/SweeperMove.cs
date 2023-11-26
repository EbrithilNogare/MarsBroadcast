using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
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

        if (EventSystem.current.IsPointerOverGameObject(0) || EventSystem.current.IsPointerOverGameObject(-1)) return;
        Vector2 position = Mouse.current.position.ReadValue();

        float cameraSize = camera.orthographicSize;
        Vector3 worldMousePosition = camera.ScreenToWorldPoint(new Vector3(position.x, position.y, cameraSize));
        Vector2Int coordinate = new Vector2Int((int)Mathf.Round(worldMousePosition.x), (int)Mathf.Round(worldMousePosition.y));

        _animator.SetBool("IsMoving", true);

        tween.Pause();
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
            tileMapController.CleanTile(x, y);
            //_animator.SetBool("IsSweeping", false);
        }
    }
}
