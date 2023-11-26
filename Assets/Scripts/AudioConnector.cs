using UnityEngine;

public class AudioConnector : MonoBehaviour
{
    public AudioSource ambientSound;
    public AudioSource endSound;
    public AudioSource gasSound;
    public AudioSource iceSound;
    public AudioSource moveSound;
    public AudioSource rotateSound;
    public AudioSource sweepSound;

    private static AudioConnector _instance;

    private AudioSource[] allAudioSources;

    private void Start()
    {
    }

    public static AudioConnector Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<AudioConnector>();
                if (_instance == null)
                {
                    GameObject singletonObject = new GameObject("SingletonObject");
                    _instance = singletonObject.AddComponent<AudioConnector>();
                }
                DontDestroyOnLoad(_instance.gameObject);
            }
            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    public void PlayAmbientSound(bool active)
    {
        if (active)
            ambientSound.Play();
        else
            ambientSound.Stop();
    }

    public void PlayEndSound()
    {
        endSound.Play();
    }

    public void PlayGasSound()
    {
        gasSound.Play();
    }

    public void PlayIceSound()
    {
        iceSound.Play();
    }

    public void PlayMoveSound()
    {
        //if (active)
        moveSound.Play();
        //else
        //moveSound.Stop();
    }

    public void PlayRotateSound()
    {
        //if (active)
        rotateSound.Play();
        //else
        //moveSound.Stop();
    }

    public void PlaySweepSound()
    {
        sweepSound.Play();
    }

    public void StopSweepSound()
    {
        sweepSound.Stop();
    }

    public void StopAllSounds()
    {
        allAudioSources = FindObjectsOfType(typeof(AudioSource)) as AudioSource[];
        foreach (AudioSource audioS in allAudioSources)
        {
            audioS.Stop();
        }
    }
}
