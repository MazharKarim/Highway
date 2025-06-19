using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        SceneManager.LoadScene("GameScene");
    }
    public void ChangeCar()
    {
        SceneManager.LoadScene("CarSelection");
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
