using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OxyConroller : MonoBehaviour
{
    public Transform Rotator;
    public StatsManager statsManager;

    [SerializeField]
    private float MinAcceptedValue;
    [SerializeField]
    private float MaxAcceptedValue;

    private float minimumAngle = 120;
    private float maximumAngle = -30;

    private float probability = 0.01f;

    // Start is called before the first frame update
    void Start()
    {
        SetValueOnSlider(statsManager.Oxygen);
        MinAcceptedValue = statsManager.MinOxygen;
        MaxAcceptedValue = statsManager.MaxOxygen;
    }

    // Update is called once per frame
    void Update()
    {
        if (Random.value < probability)
        {
            statsManager.Oxygen = Mathf.Clamp01(statsManager.Oxygen + 0.01f);
        }
    }

    public void AddToOxygen(float value)
    {
        statsManager.Oxygen = Mathf.Clamp01(statsManager.Oxygen + value);
        //slider.value = statsManager.Oxygen;
    }

    public void SetValueOnSlider(float value)
    {
        float z = Mathf.LerpAngle(minimumAngle, maximumAngle, value);
        Rotator.DORotate(new Vector3(0, 0, z), 0.5f);
        //slider.value = value;
    }
}
