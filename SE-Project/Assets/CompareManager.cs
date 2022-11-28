using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
public class ProgramPair
{
    public string first;
    public string second;
    public ProgramPair(string _first,string _second)
    {
        first = _first;
        second = _second;
    }
}
public class UnionSet
{
    private int[] parent;
    private int groupNum;

    public UnionSet(int n)
    {
        parent = new int[n];
        for (int i = 0; i < n; ++i)
        {
            parent[i] = i;
        }
        groupNum = n;
    }

    public int Find(int i)
    {
        while (i != parent[i])
        {
            parent[i] = parent[parent[i]];
            i = parent[i];
        }
        return i;
    }

    public void Unite(int a, int b)
    {
        int x = Find(a);
        int y = Find(b);
        if (x == y) return;
        parent[x] = y;
        groupNum--;
    }

    public bool InSameSet(int a, int b)
    {
        return Find(a) == Find(b);
    }

    public int GetGroups()
    {
        return groupNum;
    }

    public Dictionary<int, List<int>> GetEachGroup()
    {
        Dictionary<int, List<int>> res = new Dictionary<int, List<int>>();
        for (int i = 0; i < parent.Length; ++i)
        {
            int gid = Find(parent[i]);
            if (!res.ContainsKey(gid))
                res.Add(gid, new List<int>());
            res[gid].Add(i);
        }
        return res;
    }
}
public class CompareManager : MonoBehaviour
{
    public string csvRoute;
    public string dirRoute;
    public HashSet<string> programSet;
    public Dictionary<string,int> GetID;
    public Dictionary<int, string> GetProgram;
    public List<ProgramPair> programPairs;
    public UnionSet programUnionSet;

    public CodePanel codePanel_L;
    public CodePanel codePanel_R;
    public int CurrentIndex = 0;
    private void Start()
    {
        programSet = new HashSet<string>();
        programPairs = new List<ProgramPair>();
        csvRoute = AppDataHolder.instance.CSVRoute;
        ReadCSVFile();
        CurrentIndex = 0;
        ComparePair(programPairs[CurrentIndex]);
    }
    private void ReadCSVFile()
    {
        dirRoute=Path.GetDirectoryName(csvRoute);
        try
        {
            string[] lines = File.ReadAllLines(csvRoute);
            foreach (string line in lines)
            {
                string[] tokens = line.Split(",");
                bool available = false;
                foreach (string token in tokens)
                {
                    if (token != "file1" && token != "file2")
                    {
                        available = true;
                        programSet.Add(token);
                    }
                }
                if (available)
                {
                    programPairs.Add(new ProgramPair(tokens[0], tokens[1]));
                }
            }
        }
        catch (System.Exception)
        {
            AppDataHolder.instance.Message = "读取CSV文件时发生错误，请检查您的CSV文件格式以及是否被其他程序占用";
            SceneManager.LoadScene("Error");
        }
        //init dictionaries
        GetID = new Dictionary<string,int>();
        GetProgram = new Dictionary<int, string>();
        int id = 0;
        foreach(string programName in programSet)
        {
            GetID[programName] = id;
            GetProgram[id] = programName;
            ++id;
        }
        //create union set
        programUnionSet = new UnionSet(programSet.Count);

        //foreach(string token in programSet)
        //{
        //    Debug.Log(token);
        //}
        //foreach(ProgramPair pair in programPairs)
        //{
        //    Debug.Log(pair.first + " " + pair.second);
        //}
    }
    private void ComparePair(ProgramPair pair)
    {
        string LPath = dirRoute + "/" + pair.first;
        string RPath = dirRoute + "/" + pair.second;
        try
        {
            string[] ProgramLeft = File.ReadAllLines(LPath);
            codePanel_L.SetCode(ProgramLeft);
        }
        catch (System.Exception)
        {
            AppDataHolder.instance.SwitchErrorScene("无法读取程序源代码，请检查文件路径：\n" + LPath);
        }
        try
        {
            string[] ProgramRight = File.ReadAllLines(RPath);
            codePanel_R.SetCode(ProgramRight);
        }
        catch (System.Exception)
        {
            AppDataHolder.instance.SwitchErrorScene("无法读取程序源代码，请检查文件路径：\n" + RPath);
        }
    }
    private void NextPair()
    {
        CurrentIndex++;
        if (CurrentIndex >= programPairs.Count)
        {
            OutputFile();
            
        }
        else
        {
            ComparePair(programPairs[CurrentIndex]);
        }
    }
    public void SetEqual()
    {
        if (CurrentIndex >= programPairs.Count)
            return;
        ProgramPair currentPair = programPairs[CurrentIndex];
        programUnionSet.Unite(GetID[currentPair.first],GetID[currentPair.second]);
        //Debug.Log("united" + GetID[currentPair.first] + " and " + GetID[currentPair.second]);
        NextPair();
    }
    public void SetInEqual()
    {
        NextPair();
    }
    public void SetIDontKnow()
    {
        NextPair();
    }
    public void OutputFile()
    {
        string outputPath = dirRoute + "/out.csv";
        List<string> outputLines=new List<string>();
        outputLines.Add("file1,file2");
        for(int i = 0; i < programSet.Count; i++)
        {
            for (int j = i + 1; j < programSet.Count; j++)
            {
                Debug.LogWarning("Finding " + i + " " + j);
                if (programUnionSet.Find(i) == programUnionSet.Find(j))
                {
                    string line = GetProgram[i] + "," + GetProgram[j];
                    outputLines.Add(line);
                }
            }
        }
        try
        {
            File.WriteAllLines(outputPath, outputLines.ToArray());
        }
        catch (System.Exception)
        {
            AppDataHolder.instance.SwitchErrorScene("输出结果时发生错误，请检查文件路径：\n" + outputPath);
        }
        AppDataHolder.instance.SwitchFinishScene("判断成功，请访问文件路径：\n" + outputPath + "\n获取结果");
    }

    public void LargerText()
    {
        codePanel_L.CodeText.fontSize += 1;
        codePanel_R.CodeText.fontSize += 1;
    }
    public void SmallerText()
    {
        codePanel_L.CodeText.fontSize -= 1;
        codePanel_R.CodeText.fontSize -= 1;
    }
}
