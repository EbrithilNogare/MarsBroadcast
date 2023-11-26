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

    public void SetTemperature(bool visible)
    {
        temperature.SetActive(visible);
    }

    public void SetO2(bool visible)
    {
        o2.SetActive(visible);
    }

    public void SetCloseSensors(bool visible)
    {
        closeSensors.SetActive(visible);
    }
}
