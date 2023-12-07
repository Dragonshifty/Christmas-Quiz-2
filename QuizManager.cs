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
    List<QuizQuestion> fullQuestionsList;
    List<QuizQuestion> testQuestions;
    List<QuizQuestion> tenQuestions;
    
    void Start() 
    {
        testQuestions = new List<QuizQuestion>();
        tenQuestions = new List<QuizQuestion>();
        GetTestList();
        RandomizeList(testQuestions);
        GetTen();
    }

    private void LoadQuestions()
    {
        StartCoroutine(iOManager.GetQuestionsListAsync(InitializeQuestionsList));
        
    }

    private void InitializeQuestionsList(List<QuizQuestion> loadedQuestions)
    {
        fullQuestionsList = loadedQuestions;
        Debug.Log("Loaded");
    }

    public void GetTestList()
    {
        for (int i = 0; i < 26; i++)
        {
            System.Random random = new System.Random();
            int index = random.Next(4);
            testQuestions.Add(new QuizQuestion
            {
                questionText = $"Question {i}",
                answers = new string[] { "AnswerOne", "AnswerTwo", "AnswerThree", "AnswerFour"},
                answerIndex = index
            });
        }
    }

    public void GetTen()
    {
        foreach (QuizQuestion qq in testQuestions)
        {
            int lastIndex = testQuestions.Count -1;
            tenQuestions.Add(testQuestions[lastIndex]);
            testQuestions.RemoveAt(lastIndex);
        }

        foreach (QuizQuestion qq2 in tenQuestions)
        {
            Debug.Log(qq2.questionText);
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

}
