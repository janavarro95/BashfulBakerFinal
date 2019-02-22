using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue {

    public string name;

    [TextArea(3, 10)]
    public string[] sentences;


    public Dialogue()
    {

    }

    /// <summary>
    /// Allows for constructor in code.
    /// </summary>
    /// <param name="Name">The name of the speaker?</param>
    /// <param name="Sentences">The sentences to say.</param>
    public Dialogue(string Name,string[] Sentences)
    {
        this.name = Name;
        this.sentences = Sentences;
    }
}
