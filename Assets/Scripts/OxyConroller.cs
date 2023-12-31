using DG.Tweening;
using System.Collections;
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

    void Start()
    {
        SetValueOnSlider(statsManager.Oxygen);
        MinAcceptedValue = statsManager.MinOxygen;
        MaxAcceptedValue = statsManager.MaxOxygen;
    }

    void Update()
    {
        statsManager.Oxygen = Mathf.Clamp01(statsManager.Oxygen + 0.05f * Time.deltaTime);
    }

    public void AddToOxygen(float value)
    {
        statsManager.Oxygen = Mathf.Clamp01(statsManager.Oxygen + value);
        AudioConnector.Instance.PlayGasSound();
        StartCoroutine(SoundCouritine());
    }

    IEnumerator SoundCouritine()
    {
        yield return new WaitForSeconds(3);
        Button.interactable = true;
    }

    DG.Tweening.Core.TweenerCore<Quaternion, Vector3, DG.Tweening.Plugins.Options.QuaternionOptions> rotatorTween;

    public void SetValueOnSlider(float value)
    {
        float z = Mathf.LerpAngle(minimumAngle, maximumAngle, value);
        if (rotatorTween != null) rotatorTween.Kill(false);
        rotatorTween = Rotator.DORotate(new Vector3(0, 0, z), 3f);
    }
}
