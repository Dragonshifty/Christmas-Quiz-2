using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace FCQ.IO
{
    [System.Serializable]
    public class QuizQuestion
    {
        public string questionText;
        public string[] answers;
        public int answerIndex;
    }
}


