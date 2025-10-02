using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RestartButton : MonoBehaviour
{
    public void againButton()
    {
        Debug.Log("again");
        SceneManager.LoadScene("Level3");
    }

    public void returnButton()
    {
        Debug.Log("return");
        SceneManager.LoadScene("Start");
    }
}