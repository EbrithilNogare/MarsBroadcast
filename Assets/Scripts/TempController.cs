using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TempController : MonoBehaviour
{
    public Slider slider;
    public StatsManager statsManager;

    private float MinAcceptedValue;
    private float MaxAcceptedValue;

    private float probability = 0.01f;

    // Start is called before the first frame update
    void Start()
    {
        slider.value = statsManager.Temperature;
        MinAcceptedValue = statsManager.MinTemperature;
        MaxAcceptedValue = statsManager.MaxTemperature;
    }

    // Update is called once per frame
    void Update()
    {
        if (Random.value < probability)
        {
            statsManager.Temperature = Mathf.Clamp01(statsManager.Temperature + 0.01f); ;
        }
    }

    public void AddToTemp(float value)
    {
        statsManager.Temperature = Mathf.Clamp01(statsManager.Temperature + value);
        //slider.value = statsManager.Temperature;
    }

    public void SetValueOnSlider(float value)
    {
        slider.value = value;
    }
}
