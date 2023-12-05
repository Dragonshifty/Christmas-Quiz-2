using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class QuizQuestion
{
    public string questionText;
    public string[] answers;
    public int answerIndex;
}

[System.Serializable]
public class QuizData
{
    // public QuizQuestion[] questions;
    public List<QuizQuestion> questions;
}