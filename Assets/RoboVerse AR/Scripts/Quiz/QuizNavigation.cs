using UnityEngine;
using UnityEngine.SceneManagement;

public class QuizNavigation : MonoBehaviour
{
    public void RetryQuiz()
    {
        SceneManager.LoadScene("QuizScene");
    }
}