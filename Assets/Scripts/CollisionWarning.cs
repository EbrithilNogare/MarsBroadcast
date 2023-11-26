using UnityEngine;

public class CollisionWarning : MonoBehaviour
{
    [SerializeField] NotificationController notificationController;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        notificationController.SetCloseSensors(true);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        notificationController.SetCloseSensors(false);
    }
}
