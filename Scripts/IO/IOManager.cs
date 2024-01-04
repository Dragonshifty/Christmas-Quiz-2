using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

namespace FCQ.IO
{
    public class IOManager : MonoBehaviour
    {
        private string jsonFileLocation;
        private QuizData quizData;
        private List<QuizQuestion> questionsList;
        int questionIndex;

        private string jsonData;

        void Awake()
        {
            jsonFileLocation = Application.persistentDataPath + "/quizData.json";
            Debug.Log(jsonFileLocation);
            questionsList = new List<QuizQuestion>();
        }

        public void SaveToJson(List<QuizQuestion> updatedQuestions)
        {
            quizData.questions = updatedQuestions;
            string jsonData = JsonUtility.ToJson(quizData);
            File.WriteAllText(jsonFileLocation, jsonData);
            Debug.Log("Quiz data saved to: " + jsonFileLocation);
        }

        IEnumerator ReadFromJson()
        {
            if (File.Exists(jsonFileLocation))
            {
                // Debug.Log($"IOM");
                // jsonFileLocation = Application.persistentDataPath + "/quizData.json";
                string loadedJson = File.ReadAllText(jsonFileLocation);
                quizData = JsonUtility.FromJson<QuizData>(loadedJson);
                questionsList = quizData.questions;
                // Debug.Log($"IOM - {questionsList.Count}");
            }

            if (quizData == null)
            {
                quizData = new QuizData();
                quizData.questions = new List<QuizQuestion>();
                Debug.Log("Data Null");
            }

            yield return null;
        }

        public IEnumerator GetQuestionsListAsync(Action<List<QuizQuestion>> callback)
        {
            yield return StartCoroutine(ReadFromJson());
            callback?.Invoke(questionsList);
        }
    }
}

