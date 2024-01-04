// God class, needs compartmentalising

using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;
using FCQ.IO;

public class QuizManager : MonoBehaviour
{
    IOManager iOManager;
    Timer timer;
    SlopeCam slopeCam;
    List<QuizQuestion> fullQuestionsList;
    List<QuizQuestion> questionsForGame;
    List<Image> quizButtonImages;

    [Header("Components")]
    [SerializeField] TextMeshProUGUI questionBox;
    [SerializeField] TextMeshProUGUI answerZeroBox;
    [SerializeField] TextMeshProUGUI answerOneBox;
    [SerializeField] TextMeshProUGUI answerTwoBox;
    [SerializeField] TextMeshProUGUI answerthreeBox;
    [SerializeField] TextMeshProUGUI messageBox;
    [SerializeField] TextMeshProUGUI questionsLeftDisplay;
    [SerializeField] Button answerZeroButton;
    [SerializeField] Button answerOneButton;
    [SerializeField] Button answerTwoButton;
    [SerializeField] Button answerThreeButton;

    Image buttonZeroImage;
    Image buttonOneImage;
    Image buttonTwoImage;
    Image buttonThreeImage;
    
    [Header("Question Number")]
    [SerializeField] int questionNumberCount = 10;
    [SerializeField] Canvas gameCanvas;
    [SerializeField] Canvas gameButtonsCanvas;
    Mover mover;
    SFXViscount sFXViscount;
    ScoreKeep scoreKeep;
    int qNI; // questionNumberIndex
    int currentAnswerIndex;
    bool bootLoad = false;

    private bool isSecondTimerExecuting = false;

    void Start() 
    {
        iOManager = FindObjectOfType<IOManager>();
        mover = FindObjectOfType<Mover>();
        sFXViscount = FindObjectOfType<SFXViscount>();
        timer = FindObjectOfType<Timer>();
        scoreKeep = new ScoreKeep();
        slopeCam = FindObjectOfType<SlopeCam>();

        buttonZeroImage = answerZeroButton.GetComponent<Image>();
        buttonOneImage = answerOneButton.GetComponent<Image>();
        buttonTwoImage = answerTwoButton.GetComponent<Image>();
        buttonThreeImage = answerThreeButton.GetComponent<Image>();

        quizButtonImages = new List<Image>
        {
            buttonZeroImage,
            buttonOneImage,
            buttonTwoImage,
            buttonThreeImage
        };

        timer.CountdownReachedZero += Firstimer;
        timer.DelayTimeReachedZero += SecondTimer;

        InitialiseGame();
    }

    private void InitialiseGame()
    {
        SetQNI();
        EnableQuestionBoxes(false);
        questionsForGame = new List<QuizQuestion>();
        LoadQuestions(false);
    }

    private void OnTriggerEnter(Collider other) {
        DoMovement(false);
        gameCanvas.gameObject.SetActive(true);
        slopeCam.FocusCamera("game");
        Debug.Log("Entered");
    }

    private void SetQNI()
    {
        qNI = questionNumberCount - 1;
    }

    void Firstimer()
    {
        Debug.Log("First Timer");
        OutOfTime();
    }


    public void DoMovement(bool enable)
    {
        mover.allowMovement = enable;

    }

    private void QuestionsRemainingDisplay()
    {
        questionsLeftDisplay.text =  $"{questionsForGame.Count}";
    }

    private IEnumerator ExecuteSecondTimerWithDelay()
    {
        isSecondTimerExecuting = true;

        Debug.Log("Second Timer");

        yield return new WaitForSeconds(1f);

        if (qNI < 0)
        {
            EndGame();
        }
        else
        {
            GetNextQuestion();
        }

        isSecondTimerExecuting = false;
    }

    void SecondTimer()
    {
        if (isSecondTimerExecuting)
        {
            return;
        }

        StartCoroutine(ExecuteSecondTimerWithDelay());
    }

    public void ResetGame()
    {
        LoadQuestions(true);
        messageBox.text = "Questions reset";
    }

    public void LoadQuestions(bool reset)
    {
        if (fullQuestionsList == null || reset)
        {
           StartCoroutine(iOManager.GetQuestionsListAsync(InitializeQuestionsList)); 
           
        } else
        {
            GetQuestionsForQuiz();
        }     
        
    }

    private void InitializeQuestionsList(List<QuizQuestion> loadedQuestions)
    {
        fullQuestionsList = loadedQuestions;
        RandomizeList(fullQuestionsList);
        GetQuestionsForQuiz();
        Debug.Log("Loaded");
    }

    private void GetQuestionsForQuiz()
    {
        if (questionsForGame != null) questionsForGame.Clear();
        if (fullQuestionsList.Count > questionNumberCount)
        {
            for (int i = fullQuestionsList.Count - 1; i >= 0; i--)
            {
                if (questionsForGame.Count == questionNumberCount) break;

                questionsForGame.Add(fullQuestionsList[i]);
                fullQuestionsList.RemoveAt(i);
            }

            if (questionsForGame.Count == questionNumberCount) messageBox.text = "Questions for Game initialised";
        } else
        {
            Debug.Log("Not enough questions = resetting");
            LoadQuestions(true);
            GetQuestionsForQuiz();
        }
    }

    void RandomizeList<T>(List<T> list)
    {
        System.Random random = new System.Random();

        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = random.Next(n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }

    public void GetNextQuestion()
    {
        QuestionsRemainingDisplay();
        EnableQuestionBoxes(true);
        timer.StartTimer();
        ClearButtonColours();
        sFXViscount.PlaySFX("question");
        if (qNI > -1)
        {
            messageBox.text = "";
            questionsLeftDisplay.text = $"{qNI + 1}";
            questionBox.text = questionsForGame[qNI].questionText;
            answerZeroBox.text = questionsForGame[qNI].answers[0];
            answerOneBox.text = questionsForGame[qNI].answers[1];
            answerTwoBox.text = questionsForGame[qNI].answers[2];
            answerthreeBox.text = questionsForGame[qNI].answers[3];
            currentAnswerIndex = questionsForGame[qNI].answerIndex;
        }
        --qNI;
    }

    public void zeeroButtonPress()
    {
        timer.InterruptTimer();
        ReviewAnswer(0);
    }

    public void oneButtonPress()
    {
        timer.InterruptTimer();
        ReviewAnswer(1);
    }

    public void twoButtonPress()
    {
        timer.InterruptTimer();
        ReviewAnswer(2);
    }

    public void threeButtonPress()
    {
        timer.InterruptTimer();
        ReviewAnswer(3);
    }

    void ReviewAnswer(int givenAnswer)
    {
        int score = scoreKeep.Score;
        EnableQuestionBoxes(false);
        if (currentAnswerIndex == givenAnswer)
        {
            scoreKeep.Score++;
            messageBox.text = $"Correct - score: {score} of {questionNumberCount}";
            mover.CorrectAnswerAnimation();
            sFXViscount.PlayCorrectOrIncorrectSound(true);
            
        }
        else
        {
            messageBox.text = $"Incorrect - score: {score} of {questionNumberCount}";
            mover.IncorrectAnswerAnimation();
            sFXViscount.PlayCorrectOrIncorrectSound(false);
        }
        HighLightCorrect(currentAnswerIndex);
    }

    void OutOfTime()
    {
        messageBox.text = "Ran out of time";
        sFXViscount.PlayCorrectOrIncorrectSound(false);
        HighLightCorrect(currentAnswerIndex);
    }

    
    void HighLightCorrect(int correctIndex)
    {
        ChangeButtonColours(quizButtonImages[correctIndex]);
    }

    private void ClearButtonColours()
    {
    foreach (Image image in quizButtonImages)
    {
        image.color = Color.white;
    }

        // buttonZeroImage.color = Color.white;
        // buttonOneImage.color = Color.white;
        // buttonTwoImage.color = Color.white;
        // buttonThreeImage.color = Color.white;
    }

    private void ChangeButtonColours(Image image)
    {
        foreach (Image buttonImage in quizButtonImages)
        {
            if (image == buttonImage)
            {
                buttonImage.color = Color.green;
            } else
            {
                buttonImage.color = Color.red;
            }
        }
    }

    void EnableQuestionBoxes(bool isEnabled)
    {
        answerZeroButton.enabled = isEnabled;
        answerOneButton.enabled = isEnabled;
        answerTwoButton.enabled = isEnabled;
        answerThreeButton.enabled = isEnabled;
    }

    public void StartQuiz()
    {
        timer.goDelayTimer = true;
        ClearButtonColours();
        
        messageBox.text = "";
        scoreKeep.ResetScore();
        if (!bootLoad)
        {
            if (!timer.goTimer)
            {
                LoadQuestions(false);
                messageBox.text = "";
                GetNextQuestion();
                sFXViscount.PlaySFX("start");
            }
            bootLoad = true;
        } else
        {
            if (!timer.goTimer)
            {
                LoadQuestions(false);
                messageBox.text = "";
                GetNextQuestion();
                sFXViscount.PlaySFX("start");
            }
        }     
    }

    public void GoToQuiz()
    {
        mover.GoToQuiz();
        slopeCam.FocusCamera("game");
        slopeCam.gameTimeActivated = true;
        gameButtonsCanvas.gameObject.SetActive(false);
    }

    void EndGame()
    {
        int score = scoreKeep.Score;
        messageBox.text = $"Final score: {score} of {questionNumberCount}";
        SetQNI();
        timer.goDelayTimer = false;
        sFXViscount.PlaySFX("win");
        questionsLeftDisplay.text = $"{0}";
    }

    public void QuitQuiz()
    {
        timer.goDelayTimer = false;
        timer.goTimer = false;
        DoMovement(true);
        gameCanvas.gameObject.SetActive(false);
        gameButtonsCanvas.gameObject.SetActive(true);
        messageBox.text = "";
        slopeCam.gameTimeActivated = false;
        slopeCam.FocusCamera("follow");
        mover.MoveToAfterGameWaypoint();
    }

}
