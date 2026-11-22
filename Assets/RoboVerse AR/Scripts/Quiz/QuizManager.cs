using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class QuizManager : MonoBehaviour
{
    [System.Serializable]
    public class Question
    {
        public string question;
        public string[] answers;
        public int correctAnswer;
    }

    [Header("UI References")]
    public TMP_Text questionText;
    public TMP_Text questionCounter;
    public TMP_Text feedbackText;

    public Button[] answerButtons;
    public Button nextButton;

    [Header("Button Colors")]
    public Color normalColor = new Color32(8, 124, 167, 255);
    public Color correctColor = new Color32(124, 255, 107, 255);
    public Color wrongColor = new Color32(255, 82, 82, 255);

    private Question[] questions;

    private int currentQuestion = 0;
    private int score = 0;

    private bool answerSelected = false;


    void Start()
    {
        CreateQuestions();

        nextButton.interactable = false;

        DisplayQuestion();
    }


    void CreateQuestions()
    {
        questions = new Question[]
        {
            new Question
            {
                question = "What is the main purpose of an autonomous robot?",
                answers = new string[]
                {
                    "To operate only with human control",
                    "To perform tasks independently",
                    "To work only as a computer",
                    "To display images"
                },
                correctAnswer = 1
            },

            new Question
            {
                question = "Which component helps a robot detect nearby objects?",
                answers = new string[]
                {
                    "Speaker",
                    "Sensor",
                    "Keyboard",
                    "Monitor"
                },
                correctAnswer = 1
            },

            new Question
            {
                question = "What is the purpose of a distance sensor?",
                answers = new string[]
                {
                    "To measure nearby distance",
                    "To play music",
                    "To store files",
                    "To change screen brightness"
                },
                correctAnswer = 0
            },

            new Question
            {
                question = "What does robot mapping help a robot understand?",
                answers = new string[]
                {
                    "Its battery brand",
                    "Its surrounding environment",
                    "Its programming language",
                    "Its screen size"
                },
                correctAnswer = 1
            },

            new Question
            {
                question = "What does an obstacle represent in robot navigation?",
                answers = new string[]
                {
                    "A blocked area",
                    "A destination",
                    "A sensor",
                    "A battery"
                },
                correctAnswer = 0
            },

            new Question
            {
                question = "What is the main purpose of path planning?",
                answers = new string[]
                {
                    "To select a route to a destination",
                    "To increase screen resolution",
                    "To play audio",
                    "To change robot color"
                },
                correctAnswer = 0
            },

            new Question
            {
                question = "Which algorithm is commonly used for pathfinding?",
                answers = new string[]
                {
                    "HTML",
                    "A*",
                    "CSS",
                    "SQL"
                },
                correctAnswer = 1
            },

            new Question
            {
                question = "What does the A* algorithm use to estimate the remaining distance?",
                answers = new string[]
                {
                    "Heuristic function",
                    "Audio function",
                    "Display function",
                    "Color function"
                },
                correctAnswer = 0
            },

            new Question
            {
                question = "What happens when a robot detects an obstacle?",
                answers = new string[]
                {
                    "It should consider a safe route",
                    "It must ignore the obstacle",
                    "It should delete its map",
                    "It must switch off permanently"
                },
                correctAnswer = 0
            },

            new Question
            {
                question = "What is a waypoint?",
                answers = new string[]
                {
                    "A point along a navigation route",
                    "A type of battery",
                    "A sound effect",
                    "A programming error"
                },
                correctAnswer = 0
            },

            new Question
            {
                question = "Which Unity component can draw a line between points?",
                answers = new string[]
                {
                    "Line Renderer",
                    "Audio Listener",
                    "Canvas Scaler",
                    "Mesh Filter"
                },
                correctAnswer = 0
            },

            new Question
            {
                question = "What does a Collider do in Unity?",
                answers = new string[]
                {
                    "Defines a shape for collision detection",
                    "Plays background music",
                    "Changes text color",
                    "Creates a new scene"
                },
                correctAnswer = 0
            },

            new Question
            {
                question = "What is the purpose of a Raycast?",
                answers = new string[]
                {
                    "To detect objects along a direction",
                    "To create a user account",
                    "To save a document",
                    "To change the camera color"
                },
                correctAnswer = 0
            },

            new Question
            {
                question = "What does AR stand for?",
                answers = new string[]
                {
                    "Automatic Rotation",
                    "Augmented Reality",
                    "Advanced Robot",
                    "Artificial Route"
                },
                correctAnswer = 1
            },

            new Question
            {
                question = "What does AR Foundation help developers create?",
                answers = new string[]
                {
                    "Cross-platform AR experiences",
                    "Database tables only",
                    "Audio files only",
                    "Web pages only"
                },
                correctAnswer = 0
            },

            new Question
            {
                question = "Why does a robot use sensors?",
                answers = new string[]
                {
                    "To gather information about its surroundings",
                    "To decorate its body",
                    "To store passwords",
                    "To increase monitor size"
                },
                correctAnswer = 0
            },

            new Question
            {
                question = "What is the destination in a navigation task?",
                answers = new string[]
                {
                    "The target location",
                    "The starting sensor",
                    "A blocked location",
                    "The robot's wheel"
                },
                correctAnswer = 0
            },

            new Question
            {
                question = "What is the role of a robot's environment map?",
                answers = new string[]
                {
                    "To represent its surroundings",
                    "To control the music",
                    "To change its material",
                    "To display quiz answers"
                },
                correctAnswer = 0
            },

            new Question
            {
                question = "Why should a robot avoid obstacles?",
                answers = new string[]
                {
                    "To navigate safely",
                    "To make the screen brighter",
                    "To increase text size",
                    "To change its name"
                },
                correctAnswer = 0
            },

            new Question
            {
                question = "What is the main goal of autonomous navigation?",
                answers = new string[]
                {
                    "To reach a target without continuous human control",
                    "To remain in one place forever",
                    "To display information only",
                    "To operate as a keyboard"
                },
                correctAnswer = 0
            }
        };
    }


    void DisplayQuestion()
    {
        answerSelected = false;

        nextButton.interactable = false;

        feedbackText.text = "";

        Question q = questions[currentQuestion];

        questionText.text = q.question;

        questionCounter.text =
            "QUESTION " + (currentQuestion + 1) +
            " / " + questions.Length;

        for (int i = 0; i < answerButtons.Length; i++)
        {
            answerButtons[i].interactable = true;

            answerButtons[i].image.color = normalColor;

            TMP_Text buttonText =
                answerButtons[i].GetComponentInChildren<TMP_Text>();

            buttonText.text = q.answers[i];

            int buttonIndex = i;

            answerButtons[i].onClick.RemoveAllListeners();

            answerButtons[i].onClick.AddListener(
                () => SelectAnswer(buttonIndex)
            );
        }
    }


    void SelectAnswer(int selectedAnswer)
    {
        if (answerSelected)
            return;

        answerSelected = true;

        Question q = questions[currentQuestion];

        bool isCorrect =
            selectedAnswer == q.correctAnswer;

        if (isCorrect)
        {
            score++;

            feedbackText.text = "CORRECT! WELL DONE!";
            feedbackText.color = correctColor;
        }
        else
        {
            feedbackText.text =
                "INCORRECT! Correct answer: " +
                q.answers[q.correctAnswer];

            feedbackText.color = wrongColor;
        }

        answerButtons[selectedAnswer].image.color =
            isCorrect ? correctColor : wrongColor;

        answerButtons[q.correctAnswer].image.color =
            correctColor;

        foreach (Button button in answerButtons)
        {
            button.interactable = false;
        }

        nextButton.interactable = true;
    }


    public void NextQuestion()
    {
        currentQuestion++;

        if (currentQuestion < questions.Length)
        {
            DisplayQuestion();
        }
        else
        {
            PlayerPrefs.SetInt("QuizScore", score);
            PlayerPrefs.SetInt("TotalQuestions", questions.Length);
            PlayerPrefs.Save();

            Debug.Log("Total scenes: " + SceneManager.sceneCountInBuildSettings);

            for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
            {
                Debug.Log("Scene " + i + ": " +
                    SceneUtility.GetScenePathByBuildIndex(i));
            }

            Debug.Log("Can load ResultsScene: " +
                Application.CanStreamedLevelBeLoaded("ResultsScene"));

            SceneManager.LoadScene("ResultsScene");
        }
    }
}