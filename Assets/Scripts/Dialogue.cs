using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue
{
    public string name;
    public bool isDialogueTree;

    //[TextArea(3, 10)]
    //public string[] sentences;
    //[TextArea(3, 10)]
    //public string[] yesSentences;
    //[TextArea(3, 10)]
    //public string[] noSentences;

    public Sentence[] sentences;
    public Sentence[] yesSentences;
    public Sentence[] noSentences;
}
