using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Monster
{
    public int m_Hp { get; private set; }
    public string m_Name { get; private set; }
    public bool isDead { get; private set; }

    public Monster() { }
    public Monster(int kHp , string kName)
    {
        m_Hp = kHp;
        m_Name = kName;
        isDead = false;
    }

    public void AddHP(int value)
    {
        m_Hp += value;

        if (m_Hp > 100) m_Hp = 100;
        else if (m_Hp <= 0)
        {
            m_Hp = 0;
            isDead = true;
        }
    }
}

public class Test011Dlg : MonoBehaviour
{
    [SerializeField] InputField inputName;
    [SerializeField] InputField inputHP;

    [SerializeField] Button btnAdd;
    [SerializeField] Button btnOK;
    [SerializeField] Button btnClear;

    [SerializeField] Text txtCurResult;
    [SerializeField] Text txtResult;

    List<Monster> List_Monster = new List<Monster>();

    private void Start()
    {
        btnAdd.onClick.AddListener(ClickAdd);
        btnOK.onClick.AddListener(ClickOK);
        btnClear.onClick.AddListener(ClickClear);
    }

    void ClickAdd()
    {
        int kHp = int.Parse(inputHP.text);
        string kName = inputName.text;

        if(kHp < 0 || kHp > 100)
        {
            txtResult.text = "체력 오류";
            return;
        }

        ClearInput();

        Monster kMonster = new Monster(kHp, kName);
        List_Monster.Add(kMonster);
        List_Monster.Sort((Monster a, Monster b) => a.m_Hp > b.m_Hp ? -1 : 1);

        string str = "";
        txtCurResult.text = "";
        txtResult.text = "";

        for (int i = 0; i < List_Monster.Count; i++)
        {
            str += string.Format("({0} , {1})", List_Monster[i].m_Name, List_Monster[i].m_Hp);

            if (i != List_Monster.Count - 1) str += ",";
        }
        txtCurResult.text = str;
    }

    void ClickOK()
    {
        txtResult.text = "";
        txtCurResult.text = "";

        List_Monster.Sort((Monster a , Monster b) => a.m_Hp > b.m_Hp ? 1 : -1);

        for (int i = 0; i < List_Monster.Count; i++)
        {
            List_Monster[i].AddHP(-80);
        }

        for (int i = 0; i < List_Monster.Count; i++)
        {
            txtResult.text += string.Format("{0} , 남은체력 : {1} ({2})\n", 
                List_Monster[i].m_Name, List_Monster[i].m_Hp , (List_Monster[i].isDead) ? "사망" : "생존");
        }

        List_Monster.Clear();
    }

    void ClickClear()
    {
        ClearInput();
        txtCurResult.text = "";
        txtResult.text = "";
        List_Monster.Clear();
    }

    void ClearInput()
    {
        inputHP.text = "";
        inputName.text = "";
    }
}
