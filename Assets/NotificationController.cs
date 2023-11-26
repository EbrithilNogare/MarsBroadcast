using UnityEngine;

public class NotificationController : MonoBehaviour
{
    [SerializeField] GameObject temperature;
    [SerializeField] GameObject o2;
    [SerializeField] GameObject closeSensors;

    void Start()
    {
        temperature.SetActive(false);
        o2.SetActive(false);
        closeSensors.SetActive(false);
    }

    void SetTemperature(bool visible)
    {
        temperature.SetActive(visible);
    }

    void SetO2(bool visible)
    {
        o2.SetActive(visible);
    }

    void SetCloseSensors(bool visible)
    {
        closeSensors.SetActive(visible);
    }
}
