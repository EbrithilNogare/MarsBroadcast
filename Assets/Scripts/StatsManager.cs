using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class StatsManager : MonoBehaviour
{
    public delegate void TempChangeDelegate(float newObject);
    public event TempChangeDelegate OnTempChange;

    public delegate void OxygenChangeDelegate(float newObject);
    public event TempChangeDelegate OnOxyChange;

    public delegate void BatteryChangeDelegate(float newObject);
    public event TempChangeDelegate OnBatteryChange;

    public delegate void ErrorChangeDelegate(string newObject);
    public event ErrorChangeDelegate OnErrorChange;
    [Header("RUNNING")]
    public bool TimeRunning = true;
    [Header("ERROR")]
    public string _Error = "";
    public string Error
    {
        get { return _Error; }
        set
        {
            // Check if the value is different
            if (_Error != value)
            {
                // Set the new value
                _Error = value;

                // Trigger the event
                OnErrorChange(value);
            }
        }
    }
    public GameObject WinScreen;
    public GameObject LoserScreen;

    [Header("TEMPERATURE")]
    public float _Temperature;
    public float Temperature
    {
        get { return _Temperature; }
        set
        {
            // Check if the value is different
            if (_Temperature != value)
            {
                // Set the new value
                _Temperature = value;

                // Trigger the event
                OnTempChange(value);
            }
        }
    }
    public float MinTemperature;
    public float MaxTemperature;
    public TempController TempController;
    [Space(10)]

    [Header("OXYGEN")]
    public float _Oxygen;
    public float Oxygen
    {
        get { return _Oxygen; }
        set
        {
            // Check if the value is different
            if (_Oxygen != value)
            {
                // Set the new value
                _Oxygen = value;

                // Trigger the event
                OnOxyChange(value);
            }
        }
    }

    public float MinOxygen;
    public float MaxOxygen;
    public OxyConroller OxyConroller;
    [Space(10)]

    [Header("BATTERY")]
    public float _Battery;
    public float DecreaseValue;
    public BatteryController BatteryController;
    public float Battery
    {
        get { return _Battery; }
        set
        {
            // Check if the value is different
            //if (_Oxygen != value)
            //{
            // Set the new value
            _Battery = value;

            // Trigger the event
            OnBatteryChange(value);
            //}
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        this.OnTempChange += HandleTempChange;
        this.OnOxyChange += HandleOxyChange;
        this.OnBatteryChange += HandleBatteryChange;
        this.OnErrorChange += HandleErrorChange;
    }

    // Update is called once per frame
    void Update()
    {
        if (TimeRunning)
        {
            Battery -= 0.0001f;

        }
        //Battery -= 0.001f;
    }

    private void HandleTempChange(float newValue)
    {
        TempController.SetValueOnSlider(newValue);
        if (Temperature > MaxTemperature || Temperature < MinTemperature)
        {
            Error = "Temperature outside of bounds!";
        }
    }

    private void HandleOxyChange(float newValue)
    {
        OxyConroller.SetValueOnSlider(newValue);
        if (Oxygen > MaxOxygen || Oxygen < MinOxygen)
        {
            Error = "Oxygen outside of bounds!";
        }
    }

    private void HandleBatteryChange(float newValue)
    {
        BatteryController.SetValueOnSlider(newValue);
        if (Battery <= 0f)
        {
            Debug.Log("You WON!!");
            WinScreen.SetActive(true);
        }
    }

    private void HandleErrorChange(string newValue)
    {
        Debug.Log("Error accure due: " + newValue);
        TimeRunning = false;
        if (Battery > 0f) LoserScreen.SetActive(true);
    }

    public void ResetGame()
    {
        SceneManager.LoadScene("MainGame");
    }

    //private void OnValidate()
    //{
    //    Oxygen = _Oxygen;
    //    Temperature = _Temperature;
    //}
}
