using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public void StartLearning()
    {
        SceneManager.LoadScene("RobotVisionLab");
    }

    public void ExploreAR()
    {
        SceneManager.LoadScene("MappingZone");
    }

    public void OpenQuiz()
    {
        SceneManager.LoadScene("QuizScene");
    }

    public void QuitApplication()
    {
        Application.Quit();
    }
}