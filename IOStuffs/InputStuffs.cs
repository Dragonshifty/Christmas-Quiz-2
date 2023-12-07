using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.UI;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using System;

namespace FCQ.IO
{
    public class InputStuffs : MonoBehaviour
    {
        IOManager iOManager;
        private List<QuizQuestion> questionsList;

        string question;
        string answerzero;
        string answerOne;
        string answerTwo;
        string answerThree;
        int answerIndexInput;
        int questionIndex;

        public TMP_InputField questionField;
        public TMP_InputField answerZeroField;
        public TMP_InputField answerOneField;
        public TMP_InputField answerTwoField;
        public TMP_InputField answerThreeField;
        public TMP_InputField answerIndexField;
        public Button saveButton;
        public Button scrollLeft;
        public Button scrollRight;
        public Button saveEdit;
        public TextMeshProUGUI questionIndexText;

        private void Awake() 
        {
            iOManager = FindObjectOfType<IOManager>();
        }

        void Start()
        {
            StartCoroutine(iOManager.GetQuestionsListAsync(InitializeQuestionsList));
            saveButton.enabled = false;
            scrollLeft.enabled = false;
            scrollRight.enabled = false;
            saveEdit.enabled = false;
            questionField.Select();
            questionField.ActivateInputField();
            questionField.onEndEdit.AddListener(SetQuestions);
            answerZeroField.onEndEdit.AddListener(SetAnswerZero);
            answerOneField.onEndEdit.AddListener(SetAnswerOne);
            answerTwoField.onEndEdit.AddListener(SetAnswerTwo);
            answerThreeField.onEndEdit.AddListener(SetAnswerThree);
            answerIndexField.onEndEdit.AddListener(SetAnswerIndex);
        }

        private void InitializeQuestionsList(List<QuizQuestion> loadedQuestions)
        {
            questionsList = loadedQuestions;
            Debug.Log("Loaded");
            questionIndex = questionsList.Count;
            ShowQuestionIndex(questionIndex);
        }

        public string Question
        {
            get { return question; }
            set { question = value; }
        }

        public string AnswerZero
        {
            get { return answerzero; }
            set { answerzero = value; }
        }

        public string AnswerOne
        {
            get { return answerOne; }
            set { answerOne = value; }
        }

        public string AnswerTwo
        {
            get { return answerTwo; }
            set { answerTwo = value; }
        }

        public string AnswerThree
        {
            get { return answerThree; }
            set { answerThree = value; }
        }

        public int AnswerIndexInput
        {
            get { return answerIndexInput; }
            set { answerIndexInput = value; }
        }


        public void SetQuestions(string value)
        {
            question = value;
            answerZeroField.Select();
        }

        public void SetAnswerZero(string value)
        {
            answerzero = value;
            answerOneField.Select();
        }

        public void SetAnswerOne(string value)
        {
            answerOne = value;
            answerTwoField.Select();
        }

        public void SetAnswerTwo(string value)
        {
            answerTwo = value;
            answerThreeField.Select();
        }

        public void SetAnswerThree(string value)
        {
            answerThree = value;
            answerIndexField.Select();
        }

        public void SetAnswerIndex(string stringValue)
        {
            int intValue = -1;

            if (int.TryParse(stringValue, out intValue))
            {
                answerIndexInput = intValue - 1;
                saveButton.Select();
            }
            else
            {
                answerIndexField.text = "";
                answerIndexField.Select();
                answerIndexField.ActivateInputField();
            }

            if (!(intValue >= 1 && intValue <= 4))
            {
                answerIndexField.text = "";
                answerIndexField.Select();
                answerIndexField.ActivateInputField();
            }

            CheckForEmptyFields();
        }

        public void NewQuestion()
        {
            
            questionsList.Add(new QuizQuestion
            {
                questionText = question,
                answers = new string[] { AnswerZero, AnswerOne, AnswerTwo, AnswerThree },
                answerIndex = AnswerIndexInput
            });

           ResetFields();
           ShowQuestionIndex(questionsList.Count);
        }

        public void SaveToJson()
        {
            iOManager.SaveToJson(questionsList);
            saveButton.enabled = false;
        }


        void CheckForEmptyFields()
        {
            if (questionField.text.Equals("") || questionField == null)
            {
                questionField.Select();
            }
            else if (answerZeroField.text.Equals("") || questionField == null)
            {
                answerZeroField.Select();
            }
            else if (answerOneField.text.Equals("") || questionField == null)
            {
                answerOneField.Select();
            }
            else if (answerTwoField.text.Equals("") || questionField == null)
            {
                answerTwoField.Select();
            }
            else if (answerThreeField.text.Equals("") || questionField == null)
            {
                answerThreeField.Select();
            }
            else if (answerIndexField.text.Equals("") || questionField == null)
            {
                answerIndexField.Select();
            }
            else
            {
                saveButton.enabled = true;
            }
        }

        public void ResetFields()
        {
            questionField.text = "";
            answerZeroField.text = "";
            answerOneField.text = "";
            answerTwoField.text = "";
            answerThreeField.text = "";
            answerIndexField.text = "";
            saveButton.enabled = false;
            questionField.Select();
            questionField.ActivateInputField();
            ShowQuestionIndex(questionsList.Count);
        }

        public void ShowQuestionIndex(int value)
        {
            questionIndexText.text = $"{value}";
        }

        public void EnableEdit()
        {
            if (!scrollLeft.enabled)
            {
                scrollLeft.enabled = true;
                scrollRight.enabled = true;
                saveEdit.enabled = true;
                Debug.Log("Edit Enabled");
            }
            else
            {
                scrollLeft.enabled = false;
                scrollRight.enabled = false;
                saveEdit.enabled = false;
                Debug.Log("Edit disabled");
                ResetFields();
            }
        }

        public void ScrollLeftButton()
        {
            int currentIndexText = int.Parse(questionIndexText.text);

            if (currentIndexText > 0) 
            {
                --currentIndexText;
                DisplayOldQuestion(currentIndexText);
            }
        }

        public void ScrollRightButton()
        {
            int currentIndexText = int.Parse(questionIndexText.text);

            if (currentIndexText < questionsList.Count -1)
            {
                ++currentIndexText;
                DisplayOldQuestion(currentIndexText);
            }
        }

        public void DisplayOldQuestion(int index)
        {
            questionField.text = questionsList[index].questionText;
            answerZeroField.text = questionsList[index].answers[0];
            answerOneField.text = questionsList[index].answers[1];
            answerTwoField.text = questionsList[index].answers[2];
            answerThreeField.text = questionsList[index].answers[3];
            answerIndexField.text = questionsList[index].answerIndex.ToString();
            questionIndexText.text = index.ToString();
        }

        public void SaveEditedQuestion()
        {
            int index = int.Parse(questionIndexText.text);
            questionsList[index].questionText = questionField.text;
            questionsList[index].answers[0] = answerZeroField.text;
            questionsList[index].answers[1] = answerOneField.text;
            questionsList[index].answers[2] = answerTwoField.text;
            questionsList[index].answers[3] = answerThreeField.text;
            questionsList[index].answerIndex = int.Parse(answerIndexField.text);
            SaveToJson();
        }
    }
}

