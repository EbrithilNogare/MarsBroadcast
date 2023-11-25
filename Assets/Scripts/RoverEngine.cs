using DG.Tweening;
using UnityEngine;

public class RoverEngine : MonoBehaviour
{
    public float moveTime;
    public float moveSpeed;
    public float rotateTime;
    public float angleOfMovement;

    public bool moving;

    public StatsManager stats;

    private bool endGame = false;

    void Start()
    {
        Move();
    }

    void Update()
    {
        // Perform the 2D raycast


        // Draw a debug line in the editor
        //Debug.DrawLine(transform.position, transform.position + new Vector3(0, 0, 1) * 10f, Color.green);



        float angleInRadians = Mathf.Deg2Rad * (transform.eulerAngles.z + 5);
        float y = Mathf.Cos(angleInRadians);
        float x = -Mathf.Sin(angleInRadians);

        RaycastHit2D hit1 = Physics2D.Raycast(transform.position, new Vector3(x, y, 0), 20f, 1 << 8);
        Debug.DrawLine(transform.position, new Vector3(hit1.point.x, hit1.point.y, 0), Color.green);

        angleInRadians = Mathf.Deg2Rad * (transform.eulerAngles.z - 5);
        y = Mathf.Cos(angleInRadians);
        x = -Mathf.Sin(angleInRadians);

        RaycastHit2D hit2 = Physics2D.Raycast(transform.position, new Vector3(x, y, 0), 20f, 1 << 8);
        Debug.DrawLine(transform.position, new Vector3(hit2.point.x, hit2.point.y, 0), Color.green);
        //Debug.DrawLine(transform.position, new Vector3(x, y, 0) * 1000f, Color.green);

        // Check if the ray hits something
        if (hit1.collider != null && hit2.collider != null &&
            hit1.collider.transform.gameObject.tag == "Screen" &&
            hit2.collider.transform.gameObject.tag == "Screen")
        {
            //Debug.Log("Ray hit: " + hit1.collider.name + hit2.collider.name);
        }
        else
        {
            //Debug.Log("ROVER SAW US!!");
            stats.Error = "ROVER SAW US!!";
            endGame = true;
        }
    }

    private void Move()
    {
        if (endGame) { return; }
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
        if (endGame) { return; }
        moving = false;
        Quaternion randomRotation = Quaternion.Euler(0, 0, Random.Range(0, angleOfMovement) - angleOfMovement / 2);
        transform.DORotate(randomRotation.eulerAngles, rotateTime)
            .SetEase(Ease.InOutQuad)
            .OnComplete(Move);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        stats.Error = "ROVER SAW US!!";
        endGame = true;
    }
}