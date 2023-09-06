using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class Student3
{
    public string m_Name = "";
    public int m_Kor = 0;
    public int m_Math = 0;
    public int m_Eng = 0;

    public int m_Sum { get { return m_Kor + m_Math + m_Eng; } }
    public float m_Avarage { get { return (float)m_Sum / 3; } }

    public Student3() { }
    public Student3(string kName , int kKor , int kMath , int kEng)
    {
        m_Name = kName;
        m_Kor = kKor;
        m_Math = kMath;
        m_Eng = kEng;
    }
}

public class Test014Dlg : MonoBehaviour
{
    [SerializeField] InputField[] inputs;
    [SerializeField] Text txtCurResult;
    [SerializeField] Text txtResult;

    [SerializeField] Button btnAdd;
    [SerializeField] Button btnOK;
    [SerializeField] Button btnOpen;
    [SerializeField] Button btnClear;

    List<Student3> List_Student = new List<Student3>();

    private void Start()
    {
        btnAdd.onClick.AddListener(ClickAdd);
        btnClear.onClick.AddListener(ClickClear);
        btnOK.onClick.AddListener(ClickOK);
        btnOpen.onClick.AddListener(ClickOpen);
    }

    float[] GetKorAvrAndSum()
    {
        int sum = 0;
        float avarage = 0;
        for (int i = 0; i < List_Student.Count; i++)
        {
            sum += List_Student[i].m_Kor;
        }

        avarage = (float)sum / List_Student.Count;
        float[] returnValue = { sum, avarage };

        return returnValue;
    }

    float[] GetMathAvrAndSum()
    {
        int sum = 0;
        float avarage = 0;
        for (int i = 0; i < List_Student.Count; i++)
        {
            sum += List_Student[i].m_Math;
        }

        avarage = (float)sum / List_Student.Count;
        float[] returnValue = { sum, avarage };

        return returnValue;
    }

    float[] GetEngAvrAndSum()
    {
        int sum = 0;
        float avarage = 0;
        for (int i = 0; i < List_Student.Count; i++)
        {
            sum += List_Student[i].m_Eng;
        }

        avarage = (float)sum / List_Student.Count;
        float[] returnValue = { sum, avarage };

        return returnValue;
    }

    bool InputIsNull()
    {
        for (int i = 0; i < inputs.Length; i++)
        {
            if (string.IsNullOrEmpty(inputs[i].text)) return true;
        }

        return false;
    }

    bool IsOverScore()
    {
        for (int i = 1; i < inputs.Length; i++)
        {
            int score = int.Parse(inputs[i].text);
            if (score > 100 || score < 0) return true;
        }

        return false;
    }

    void PrintError(string errorMsg)
    {
        txtResult.text = errorMsg;
        ClearInputs();
    }

    void ClearInputs()
    {
        for (int i = 0; i < inputs.Length; i++)
        {
            inputs[i].text = "";
        }
    }

    void ClickAdd()
    {
        if(InputIsNull())
        {
            PrintError("일부 입력값이 비어있습니다.");
            return;
        }

        if(IsOverScore())
        {
            PrintError("일부 입력값이 너무 높거나 낮습니다.");
            return;
        }

        string kName = inputs[0].text;
        int kKor = int.Parse(inputs[1].text);
        int kMath = int.Parse(inputs[2].text);
        int kEng = int.Parse(inputs[3].text);

        Student3 student = new Student3(kName, kKor, kMath, kEng);
        List_Student.Add(student);
        txtCurResult.text = "";
        for (int i = 0; i < List_Student.Count; i++)
        {
            Student3 kStudent = List_Student[i];
            txtCurResult.text += string.Format("{0}:{1},{2},{3}\n", kStudent.m_Name, kStudent.m_Kor, kStudent.m_Math, kStudent.m_Eng);
        }

        ClearInputs();
    }

    void ClickOK()
    {
        txtCurResult.text = "";
        txtResult.text = "";
        List_Student.Sort((x, y) => x.m_Sum > y.m_Sum ? -1 : 1);

        PrintResult();
        PrintKindResult();
        ClearInputs();
    }

    void PrintResult()
    {
        for (int i = 0; i < List_Student.Count; i++)
        {
            Student3 kStudent = List_Student[i];
            txtResult.text += string.Format("{0}:{1},{2},{3},합계:{4},평균:{5}\n",
                kStudent.m_Name, kStudent.m_Kor, kStudent.m_Math, kStudent.m_Eng, kStudent.m_Sum, kStudent.m_Avarage.ToString("F2"));
        }
    }

    void PrintKindResult()
    {
        float[] kor = GetKorAvrAndSum();
        float[] math = GetMathAvrAndSum();
        float[] eng = GetEngAvrAndSum();

        txtResult.text += string.Format("국어({0},{1}) , 수학({2},{3}) , 영어({4},{5})",
            kor[0], kor[1].ToString("F2"), math[0], math[1].ToString("F2"), eng[0], eng[1].ToString("F2"));
    }

    void ClickOpen()
    {
        List<string[]> List_Data = new List<string[]>();
        TextAsset txtAsset = Resources.Load<TextAsset>("Table/test");

        if (txtAsset == null) return;

        StringReader sr = new StringReader(txtAsset.text);
        string data = sr.ReadLine();
        while(data != null)
        {
            string[] datas = data.Split("\t");
            if (datas.Length == 0) continue;

            List_Data.Add(datas);
            data = sr.ReadLine();
        }

        sr.Close();

        for (int i = 0; i < List_Data.Count; i++)
        {
            string kName = List_Data[i][0];
            int kKor = int.Parse(List_Data[i][1]);
            int kMath = int.Parse(List_Data[i][2]);
            int kEng = int.Parse(List_Data[i][3]);

            Student3 student = new Student3(kName, kKor, kMath, kEng);
            List_Student.Add(student);
        }

        txtCurResult.text = "";
        for (int i = 0; i < List_Student.Count; i++)
        {
            Student3 kStudent = List_Student[i];
            txtCurResult.text += string.Format("{0}:{1},{2},{3}\n", kStudent.m_Name, kStudent.m_Kor, kStudent.m_Math, kStudent.m_Eng);
        }
    }

    void ClickClear()
    {
        txtCurResult.text = "";
        txtResult.text = "";
        ClearInputs();
    }
}
