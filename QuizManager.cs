using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class QuizManager : MonoBehaviour
{
    private string jsonFileLocation;
    private QuizData quizData;
    private List<QuizQuestion> questionsList;
    int questionIndex;
    InputStuffs inputStuffs;

    string jsonData;
    // [SerializeField] GameObject textObject;
    // TextMeshPro givenText;
    // string typeQuestion;
    // string typeAnswer;
    // int typeIndex;


    void Awake()
    {
        // givenText = textObject.GetComponent<TextMeshPro>();
        jsonFileLocation = Application.persistentDataPath + "/quizData.json";
        questionsList = new List<QuizQuestion>();
        inputStuffs = FindObjectOfType<InputStuffs>();

        // SetQuestions();
        // SaveToJson();
        ReadFromJson();
        // if (givenText == null)
        // {
        //     Debug.LogError("TextMeshProUGUI component not found on the specified textObject.");
        //     return;
        // }

        // Canvas canvas = GameObject.Find("AnswerTB").GetComponent<Canvas>();
        // secondInputField = canvas.transform.Find("Answers").GetComponent<InputField>();
        // {
        //     Debug.LogError("Canvas not found.");
        //     return;
        // }
        // Check();
    }


    // void SetQuestions()
    // {
    //     quizData = new QuizData
    //     {
    //         questions = new QuizQuestion[]
    //         {
    //             new QuizQuestion
    //             {
    //                 questionText = "What is the bla?",
    //                 answers = new string[] {"one", "two", "three", "four"},
    //                 answerIndex = 1
    //             },
    //             new QuizQuestion
    //             {
    //                 questionText = "What is the bla bla?",
    //                 answers = new string[] {"one", "two", "three", "four"},
    //                 answerIndex = 2
    //             }
    //         }
    //     };
    // }

    public void SaveToJson()
    {
        string jsonData = JsonUtility.ToJson(quizData);
        File.WriteAllText(jsonFileLocation, jsonData);
        Debug.Log("Quiz data saved to: " + jsonFileLocation);
    }

    void ReadFromJson()
    {
        if (File.Exists(jsonFileLocation))
        {
            string loadedJson = File.ReadAllText(jsonFileLocation);
            quizData = JsonUtility.FromJson<QuizData>(loadedJson);
            questionsList = quizData.questions;
            questionIndex = questionsList.Count;
            // InputStuffs inputStuffs = FindObjectOfType<InputStuffs>();
            
        }
        inputStuffs.ShowQuestionIndex(questionIndex);
    }

    public void NewQuestion()
    {
        if (quizData == null)
        {
            quizData = new QuizData();
            quizData.questions = new List<QuizQuestion>();
            
        } else
        {
            questionsList = quizData.questions;
        }

        questionsList.Add(new QuizQuestion
        {
            questionText = inputStuffs.Question,
            answers = new string[] { inputStuffs.AnswerZero, inputStuffs.AnswerOne, inputStuffs.AnswerTwo, inputStuffs.AnswerThree },
            answerIndex = inputStuffs.AnswerIndex
        });

        quizData.questions = questionsList;

        inputStuffs.ResetFields();
        inputStuffs.ShowQuestionIndex(questionsList.Count);
    }
}
