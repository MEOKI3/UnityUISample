using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Student
{
    public string m_Name;
    public int m_KorScore;
    public int m_MathScore;
    public int m_EngScore;

    public int m_Sun { get { return m_KorScore + m_MathScore + m_EngScore; } }
    public float m_Avarage { get { return (float)m_Sun / 3; } }

    public Student() { }
    public Student(string kName , int kScore , int mScore , int eScore)
    {
        m_Name = kName;
        m_KorScore = kScore;
        m_MathScore = mScore;
        m_EngScore = eScore;
    }
}

public class Test012Dlg : MonoBehaviour
{
    [SerializeField] InputField[] inputs;

    [SerializeField] Text txtResult;
    [SerializeField] Button btnOK;
    [SerializeField] Button btnClear;

    List<Student> List_Student = new List<Student>();

    private void Start()
    {
        btnOK.onClick.AddListener(ClickOK);
        btnClear.onClick.AddListener(ClickClear);
    }

    void ClickOK()
    {
        if(isNull())
        {
            PrintError("일부 칸이 비어있습니다.");
            return;
        }

        if (isOverOrLow())
        {
            PrintError("일부 점수에 오류 발생");
            return;
        }

        string kName = inputs[0].text;
        int kKor = int.Parse(inputs[1].text);
        int kMath = int.Parse(inputs[2].text);
        int kEng = int.Parse(inputs[3].text);

        Student kStudent = new Student(kName, kKor, kMath, kEng);
        List_Student.Add(kStudent);

        txtResult.text = "";
        for (int i = 0; i < List_Student.Count; i++)
        {
            Student student = List_Student[i];
            txtResult.text += string.Format("{0} = 국어:{1}, 수학:{2}, 영어:{3}, 합계:{4}, 평균:{5}", 
                student.m_Name, student.m_KorScore, student.m_MathScore, student.m_EngScore, student.m_Sun, student.m_Avarage.ToString("F2"));
        }

        ClearInputs();
    }

    void ClickClear()
    {
        ClearInputs();
        txtResult.text = "";
        List_Student.Clear();
    }

    void ClearInputs()
    {
        for (int i = 0; i < inputs.Length; i++)
        {
            inputs[i].text = "";
        }
    }

    void PrintError(string errorMsg)
    {
        txtResult.text = errorMsg;
        ClearInputs();
    }

    bool isNull()
    {
        for (int i = 0; i < inputs.Length; i++)
        {
            if (string.IsNullOrEmpty(inputs[i].text)) return true;
        }

        return false;
    }

    bool isOverOrLow()
    {
        for (int i = 1; i < inputs.Length; i++)
        {
            int score = int.Parse(inputs[i].text);
            if (score > 100 || score < 0) return true;
        }

        return false;
    }
}
