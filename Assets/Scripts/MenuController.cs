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

    public void OpenTutorial(string tutorialNumber)
    {
        SceneManager.LoadScene("Tutorial" + tutorialNumber);
    }

    public void EndTutorial()
    {
        SceneManager.LoadScene("Menu");
    }
}
