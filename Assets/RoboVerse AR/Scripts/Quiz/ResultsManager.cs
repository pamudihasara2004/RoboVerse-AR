using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class ResultsManager : MonoBehaviour
{
    [Header("Score UI")]
    public TMP_Text scoreText;
    public TMP_Text percentageText;

    [Header("Performance UI")]
    public TMP_Text performanceText;

    [Header("Buttons")]
    public Button retryButton;
    public Button homeButton;

    [Header("Scene Names")]
    public string quizSceneName;
    public string homeSceneName;

    void Start()
    {
        DisplayResults();

        retryButton.onClick.AddListener(RetryQuiz);
        homeButton.onClick.AddListener(GoToHome);
    }

    void DisplayResults()
    {
        int score = PlayerPrefs.GetInt("QuizScore", 0);
        int totalQuestions = PlayerPrefs.GetInt("TotalQuestions", 20);

        float percentage = 0f;

        if (totalQuestions > 0)
        {
            percentage = (float)score / totalQuestions * 100f;
        }

        scoreText.text = score + " / " + totalQuestions;

        percentageText.text = percentage.ToString("F0") + "%";

        if (percentage >= 80)
        {
            performanceText.text = "Excellent Work!";
        }
        else if (percentage >= 60)
        {
            performanceText.text = "Great Job!";
        }
        else if (percentage >= 40)
        {
            performanceText.text = "Good Effort!";
        }
        else
        {
            performanceText.text = "Keep Practicing!";
        }
    }

    public void RetryQuiz()
    {
        PlayerPrefs.DeleteKey("QuizScore");
        PlayerPrefs.DeleteKey("TotalQuestions");

        SceneManager.LoadScene(quizSceneName);
    }

    public void GoToHome()
    {
        SceneManager.LoadScene(homeSceneName);
    }
}