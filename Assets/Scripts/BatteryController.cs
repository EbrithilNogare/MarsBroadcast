using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BatteryController : MonoBehaviour
{
    public Slider Slider;
    public StatsManager statsManager;

    // Start is called before the first frame update
    void Start()
    {
        Slider.value = statsManager.Battery;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AddToTemp(float value)
    {
        statsManager.Battery = Mathf.Clamp01(statsManager.Temperature + value);
        //slider.value = statsManager.Temperature;
    }

    public void SetValueOnSlider(float value)
    {
        Slider.value = value;
    }
}
