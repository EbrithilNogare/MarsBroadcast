using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    void Start()
    {

    }

    public void StartGame()
    {
        SceneManager.LoadScene("MainGame");
    }

    public void StartTutorial()
    {
        ///SceneManager.LoadScene("Tutorial");
    }
}
