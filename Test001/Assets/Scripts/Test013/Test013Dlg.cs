using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class Student2
{
    public string m_Name = "";
    public int m_Kor = 0;
    public int m_Math = 0;
    public int m_Eng = 0;

    public int sum { get { return m_Kor + m_Math + m_Eng; } }
    public float avarage { get { return (float)sum / 3; } }

    public Student2() { }
    public Student2(string kName , int kKor , int kMath , int kEng)
    {
        m_Name = kName;
        m_Kor = kKor;
        m_Math = kMath;
        m_Eng = kEng;
    }
}

public class Test013Dlg : MonoBehaviour
{
    [SerializeField] InputField[] score_Inputs;
    [SerializeField] Text txtResult;
    [SerializeField] Text txtCurResult;

    [SerializeField] Button btnAdd;
    [SerializeField] Button btnOK;
    [SerializeField] Button btnClear;
    [SerializeField] Button btnOpen;

    List<Student2> List_Student = new List<Student2>();

    private void Start()
    {
        btnAdd.onClick.AddListener(ClickAdd);
        btnOK.onClick.AddListener(ClickOK);
        btnClear.onClick.AddListener(ClickClear);
        btnOpen.onClick.AddListener(ClickOpen);
    }

    void ClickAdd()
    {
        if(InputsIsNull())
        {
            PrintError("일부 칸이 비어있습니다.");
            return;
        }

        string kName = score_Inputs[0].text;
        int kor = int.Parse(score_Inputs[1].text);
        int math = int.Parse(score_Inputs[2].text);
        int eng = int.Parse(score_Inputs[3].text);
        Student2 kStudent = new Student2(kName, kor, math, eng);

        if(IsScoreOver(kStudent))
        {
            PrintError("일부 점수 오류.");
            return;
        }

        List_Student.Add(kStudent);
        txtCurResult.text = "";
        for (int i = 0; i < List_Student.Count; i++)
        {
            Student2 kitem = List_Student[i];
            txtCurResult.text += string.Format("{0}:{1},{2},{3}\n", kitem.m_Name, kitem.m_Kor, kitem.m_Math, kitem.m_Eng);
        }

        ClearInputs();
    }

    void ClickOK()
    {
        if (List_Student.Count <= 0)
        {
            PrintError("아무런 학생도 리스트에 없습니다.");
            return;
        }

        txtResult.text = "";
        List_Student.Sort((x, y) => x.sum > y.sum ? -1 : 1);
        for (int i = 0; i < List_Student.Count; i++)
        {
            Student2 kitem = List_Student[i];
            txtResult.text += string.Format("{0}:{1},{2},{3},합계:{4},평균:{5}\n",
                kitem.m_Name, kitem.m_Kor, kitem.m_Math, kitem.m_Eng , kitem.sum , kitem.avarage.ToString("F2"));
        }

        ClearInputs();
    }

    void ClickOpen()
    {
        List<string[]> list_Data = new List<string[]>();

        TextAsset txtAsset = Resources.Load<TextAsset>("Table/test");
        if (txtAsset == null) return;
        StringReader sr = new StringReader(txtAsset.text);
        string inputData = sr.ReadLine();
        while(inputData != null)
        {
            string[] datas = inputData.Split("\t");
            if (datas.Length == 0)
                continue;

            list_Data.Add(datas);
            inputData = sr.ReadLine();
        }
        sr.Close();

        for (int i = 0; i < list_Data.Count; i++)
        {
            string kName = list_Data[i][0];
            int kor = int.Parse(list_Data[i][1]);
            int math = int.Parse(list_Data[i][2]);
            int eng = int.Parse(list_Data[i][3]);
            Student2 student = new Student2(kName, kor, math, eng);

            List_Student.Add(student);
        }

        txtCurResult.text = "";
        for (int i = 0; i < List_Student.Count; i++)
        {
            Student2 kitem = List_Student[i];
            txtCurResult.text += string.Format("{0}:{1},{2},{3}\n", kitem.m_Name, kitem.m_Kor, kitem.m_Math, kitem.m_Eng);
        }

        ClearInputs();
    }

    void ClickClear()
    {
        txtCurResult.text = "";
        txtResult.text = "";
        List_Student.Clear();
        ClearInputs();
    }

    void ClearInputs()
    {
        for (int i = 0; i < score_Inputs.Length; i++)
        {
            score_Inputs[i].text = "";
        }
    }

    void PrintError(string errorMsg)
    {
        ClearInputs();
        txtResult.text = errorMsg;
    }

    bool InputsIsNull()
    {
        for (int i = 0; i < score_Inputs.Length; i++)
        {
            if (string.IsNullOrEmpty(score_Inputs[i].text)) return true;
        }

        return false;
    }

    bool IsScoreOver(Student2 kStudent)
    {
        if (kStudent.m_Kor < 0 || kStudent.m_Kor > 100) return true;
        if (kStudent.m_Math < 0 || kStudent.m_Math > 100) return true;
        if (kStudent.m_Eng < 0 || kStudent.m_Eng > 100) return true;

        return false;
    }
}
