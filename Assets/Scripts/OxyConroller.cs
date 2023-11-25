using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OxyConroller : MonoBehaviour
{
    public Transform Rotator;
    public Button Button;
    public StatsManager statsManager;

    [SerializeField]
    private float MinAcceptedValue;
    [SerializeField]
    private float MaxAcceptedValue;

    private float minimumAngle = 120;
    private float maximumAngle = -30;

    private float probability = 0.04f;

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
        AudioConnector.Instance.PlayGasSound();
        StartCoroutine(SoundCouritine());

        //slider.value = statsManager.Oxygen;
    }

    IEnumerator SoundCouritine()
    {
        //Print the time of when the function is first called.
        Debug.Log("Started Coroutine at timestamp : " + Time.time);

        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(3);
        Button.interactable = true;
        //After we have waited 5 seconds print the time again.

    }

    public void SetValueOnSlider(float value)
    {
        float z = Mathf.LerpAngle(minimumAngle, maximumAngle, value);
        Rotator.DORotate(new Vector3(0, 0, z), 3f);
        //slider.value = value;
    }
}
