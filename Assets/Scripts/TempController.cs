using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TempController : MonoBehaviour
{
    public Slider slider;
    public StatsManager statsManager;
    public Button Button;

    private float MinAcceptedValue;
    private float MaxAcceptedValue;

    void Start()
    {
        slider.value = statsManager.Temperature;
        MinAcceptedValue = statsManager.MinTemperature;
        MaxAcceptedValue = statsManager.MaxTemperature;
    }

    void Update()
    {
        statsManager.Temperature = Mathf.Clamp01(statsManager.Temperature + 0.2f * Time.deltaTime); ;
    }

    public void AddToTemp(float value)
    {
        statsManager.Temperature = Mathf.Clamp01(statsManager.Temperature + value);

        AudioConnector.Instance.PlayIceSound();
        StartCoroutine(SoundCouritine());
    }

    public void SetValueOnSlider(float value)
    {
        slider.value = value;
    }

    IEnumerator SoundCouritine()
    {
        yield return new WaitForSeconds(3);
        Button.interactable = true;
    }
}
