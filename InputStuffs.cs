using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.UI;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using System;

public class InputStuffs : MonoBehaviour
{
    string question;
    string answerzero;
    string answerOne;
    string answerTwo;
    string answerThree;
    int answerIndex;
    int questionIndex;
    
    public TMP_InputField questionField;
    public TMP_InputField answerZeroField;
    public TMP_InputField answerOneField;
    public TMP_InputField answerTwoField;
    public TMP_InputField answerThreeField;
    public TMP_InputField answerIndexField;
    public TMP_InputField questionIndexField;
    public Button saveButton;

    void Start()
    {
        saveButton.enabled = false;
        questionField.Select();
        questionField.ActivateInputField();
        questionField.onEndEdit.AddListener(SetQuestions);
        answerZeroField.onEndEdit.AddListener(SetAnswerZero);
        answerOneField.onEndEdit.AddListener(SetAnswerOne);
        answerTwoField.onEndEdit.AddListener(SetAnswerTwo);
        answerThreeField.onEndEdit.AddListener(SetAnswerThree);
        answerIndexField.onEndEdit.AddListener(SetAnswerIndex);
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
        set {answerOne = value; }
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

    public int AnswerIndex
    { 
        get { return answerIndex; }
        set { answerIndex = value; }
    }


    public void SetQuestions(string value)
    {
        question = value;
        answerZeroField.Select();
        answerZeroField.ActivateInputField();
    }

    public void SetAnswerZero(string value)
    {
        answerzero = value;
        answerOneField.Select();
        answerOneField.ActivateInputField();
    }

    public void SetAnswerOne(string value)
    {
        answerOne = value;
        answerTwoField.Select();
        answerTwoField.ActivateInputField();
    }

    public void SetAnswerTwo(string value)
    {
        answerTwo = value;
        answerThreeField.Select();
        answerThreeField.ActivateInputField();
    }

    public void SetAnswerThree(string value)
    {
        answerThree = value;
        answerIndexField.Select();
        answerIndexField.ActivateInputField();
    }

    public void SetAnswerIndex(string stringValue)
    {
        int intValue = -1;

        if (int.TryParse(stringValue, out intValue))
        {
            answerIndex = intValue;
            saveButton.Select();
        } else
        {
            answerIndexField.text = "";
            answerIndexField.Select();
            answerIndexField.ActivateInputField();
        }

        if (!(intValue >= 0 && intValue <= 3))
        { 
            answerIndexField.text = "";
            answerIndexField.Select();
            answerIndexField.ActivateInputField();
        }

        CheckForEmptyFields();
    }


    void CheckForEmptyFields()
    {
        if (questionField.text.Equals("") || questionField == null)
        {
            questionField.Select();
            questionField.ActivateInputField();
            return;
        } else if (answerZeroField.Equals("") || questionField == null)
        {
            answerZeroField.Select();
            answerZeroField.ActivateInputField();
        } else if (answerOneField.Equals("") || questionField == null)
        {
            answerOneField.Select();
            answerOneField.ActivateInputField();
        } else if (answerTwoField.Equals("") || questionField == null)
        {
            answerTwoField.Select();
            answerTwoField.ActivateInputField();
        } else if (answerThreeField.Equals("") || questionField == null)
        {
            answerThreeField.Select();
            answerThreeField.ActivateInputField();
        } else if (answerIndexField.Equals("") || questionField == null)
        {
            answerIndexField.Select();
            answerIndexField.ActivateInputField();
        } else
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
    }

    public void ShowQuestionIndex(int value)
    {
        questionIndexField.text = $"{value}";
    }

    IEnumerator ActivateAnswerZeroField()
    {
        yield return null;

        answerZeroField.Select();
        answerZeroField.ActivateInputField();
    }


}
