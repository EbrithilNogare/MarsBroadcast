using UnityEngine;
using UnityEngine.UI;

public class BatteryController : MonoBehaviour
{
    public Slider Slider;
    public StatsManager statsManager;

    void Start()
    {
        Slider.value = statsManager.Battery;
    }

    public void AddToTemp(float value)
    {
        statsManager.Battery = Mathf.Clamp01(statsManager.Temperature + value);
    }

    public void SetValueOnSlider(float value)
    {
        Slider.value = value;
    }
}
