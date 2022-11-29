using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HighlightPreset
{
    class KeyWord
    {
        public KeyWord(string _key,string _color)
        {
            Key = _key;
            Color = _color;
        }
        public string Key;
        public string Color;
        public string GenerateText()
        {
            return " " + "<color=" + Color + ">" + Key + "</color>" + " ";
        }
    }
    List<KeyWord> Keywords = new List<KeyWord>();

    public string HandleLine(string line)
    {
        //Debug.Log("handling:" + line);
        string ret = line;
        foreach(KeyWord word in Keywords)
        {
            //Debug.Log("try replacing");
            ret = ret.Replace(word.Key, word.GenerateText());
        }
        return ret;
    }
    public static HighlightPreset CreateDefaultPreset()
    {
        HighlightPreset preset = new HighlightPreset();
        preset.Keywords.Add(new KeyWord("int", "#006633"));
        preset.Keywords.Add(new KeyWord("double", "#006633"));
        preset.Keywords.Add(new KeyWord("enum", "#006633"));
        preset.Keywords.Add(new KeyWord("float ", "#006633"));
        preset.Keywords.Add(new KeyWord("long", "#006633"));
        preset.Keywords.Add(new KeyWord("char", "#006633"));

        preset.Keywords.Add(new KeyWord("{", "#660033"));
        preset.Keywords.Add(new KeyWord("}", "#660033"));

        preset.Keywords.Add(new KeyWord("if", "#ffcc00"));
        preset.Keywords.Add(new KeyWord("else", "#ffcc00"));
        preset.Keywords.Add(new KeyWord("while", "#ffcc00"));
        preset.Keywords.Add(new KeyWord("do", "#ffcc00"));
        preset.Keywords.Add(new KeyWord("break", "#ffcc00"));
        return preset;
    }
}

public class CodePanel : MonoBehaviour
{
    [SerializeField]
    public TextMeshProUGUI CodeText;
    [SerializeField]
    private string LineNumberColor;
    HighlightPreset preset=HighlightPreset.CreateDefaultPreset();

    public void Clear()
    {
        CodeText.text = "";
    }
    public void SetCode(string[] codeLines)
    {
        Clear();
        for(int i=0; i<codeLines.Length; i++)
        {
            //line number
            CodeText.text += "<color=" + LineNumberColor + ">" + (i + 1) + "</color>" +" " ;
            //CodeText.text += codeLines[i] + "\n";
            CodeText.text += preset.HandleLine(codeLines[i])+"\n";
        }
    }
}
