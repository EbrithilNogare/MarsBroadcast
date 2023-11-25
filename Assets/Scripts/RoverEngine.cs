using DG.Tweening;
using UnityEngine;

public class RoverEngine : MonoBehaviour
{
    public float moveTime;
    public float moveSpeed;
    public float rotateTime;
    public float angleOfMovement;

    public bool moving;

    void Start()
    {
        Move();
    }

    void Update()
    {

    }

    private void Move()
    {
        moving = true;
        float angleInRadians = Mathf.Deg2Rad * transform.eulerAngles.z;
        float y = Mathf.Cos(angleInRadians);
        float x = -Mathf.Sin(angleInRadians);

        Vector3 targetPosition = transform.position + new Vector3(x, y, 0) * moveSpeed;
        transform.DOMove(targetPosition, moveTime)
            .OnComplete(Rotate);
    }

    private void Rotate()
    {
        moving = false;
        Quaternion randomRotation = Quaternion.Euler(0, 0, Random.Range(0, angleOfMovement) - angleOfMovement / 2);
        transform.DORotate(randomRotation.eulerAngles, rotateTime)
            .SetEase(Ease.InOutQuad)
            .OnComplete(Move);
    }
}