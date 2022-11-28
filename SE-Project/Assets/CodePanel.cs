using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CodePanel : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI CodeText;
    [SerializeField]
    private string LineNumberColor;

    public void DummyTest()
    {
        string[] codes = { "#include hello.cpp", "int main(void)", "{", "    helloworld();","    return 0;","}"};
        SetCode(codes);
    }
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
            CodeText.text += codeLines[i]+"\n";
        }
    }
}
