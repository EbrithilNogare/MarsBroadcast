using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OxyConroller : MonoBehaviour
{
    public Slider slider;
    public StatsManager statsManager;

    [SerializeField]
    private float MinAcceptedValue;
    [SerializeField]
    private float MaxAcceptedValue;

    // Start is called before the first frame update
    void Start()
    {
        slider.value = statsManager.Oxygen;
        MinAcceptedValue = statsManager.MinOxygen;
        MaxAcceptedValue = statsManager.MaxOxygen;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AddToOxygen(float value)
    {
        statsManager.Oxygen = Mathf.Clamp01(statsManager.Oxygen + value);
        //slider.value = statsManager.Oxygen;
    }

    public void SetValueOnSlider(float value)
    {
        slider.value = value;
    }
}
