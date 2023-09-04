using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Animal
{
    public string my_Name { get; private set; }
    public int my_Weight { get; private set; }

    public Animal() { }
    public Animal(string kName , int kWeight)
    {
        my_Name = kName;
        my_Weight = kWeight;
    }
}

public class Test010Dlg : MonoBehaviour
{
    [SerializeField] Text txtResult;
    [SerializeField] InputField inputName;
    [SerializeField] InputField inputWeight;

    [SerializeField] Button btnAdd;
    [SerializeField] Button btnStart;
    [SerializeField] Button btnClear;
    [SerializeField] int maxCount;

    List<Animal> List_AnimalSave = new List<Animal>();

    private void Start()
    {
        btnAdd.onClick.AddListener(() => ClickAdd());
        btnStart.onClick.AddListener(() => ClickPrint());
        btnClear.onClick.AddListener(() => ClickClear());
    }

    void ClickAdd()
    {
        string kName = inputName.text;
        int kWeight = int.Parse(inputWeight.text);
        ClearInput();
        txtResult.text = "";

        if (List_AnimalSave.Count >= maxCount)
        {
            txtResult.text = "추가한 동물의 수가 너무 많습니다.";
            return;
        }

        if (kWeight < 0 || kWeight > 2000)
        {
            txtResult.text = "몸무게 오류";
            return;
        }

        Animal kAnimal = new Animal(kName, kWeight);
        List_AnimalSave.Add(kAnimal);
    }

    void ClickPrint()
    {
        txtResult.text = "";
        string sumName = "";
        int sumWeight = 0;

        if (List_AnimalSave.Count <= 0)
        {
            txtResult.text = "추가한 동물이 없습니다";
            return;
        }

        if (List_AnimalSave.Count > 1)
        {
            for(int i = 0; i < List_AnimalSave.Count; i++)
            {
                sumName += List_AnimalSave[i].my_Name;
                sumWeight += List_AnimalSave[i].my_Weight;
                if (i != List_AnimalSave.Count - 1) sumName += ",";
            }

            txtResult.text = string.Format("{0}의 무게의 합은 {1}kg 입니다.", sumName, sumWeight);
        }
        else txtResult.text = string.Format("{0}의 무게는 {1}kg 입니다.", List_AnimalSave[0].my_Name, List_AnimalSave[0].my_Weight);

        List_AnimalSave.Clear();
    }

    void ClickClear()
    {
        List_AnimalSave.Clear();
        txtResult.text = "";
        ClearInput();
    }

    void ClearInput()
    {
        inputName.text = "";
        inputWeight.text = "";
    }
}
