
using System.Collections.Generic;
using UnityEngine;
using System;

public class DialoguePhrases : ScriptableObject
{
    public List<Phrase> phrases = new List<Phrase>();

    public string GetValueBykey(string key)
    {
        foreach (Phrase item in phrases)
        {
            if (item.key == key)
                return item.value;
        }

        return "Key not found";
    }

    [Serializable]
    public class Phrase
    {
        public string key;
        [TextArea(3, 10)]
        public string value;
    }
}

