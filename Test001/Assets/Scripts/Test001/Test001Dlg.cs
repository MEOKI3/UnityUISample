using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Test001Dlg : MonoBehaviour
{
    [SerializeField] Button btn_OK;
    [SerializeField] Button btn_Clear;

    [SerializeField] Text txt_Result;

    private void Start()
    {
        btn_OK.onClick.AddListener(() =>
        {
            OnClick_OK();
        });
    }

    int Sum(int a , int b)
    {
        return a + b;
    }

    void Swap(int a , int b)
    {
        int temp = a;
        a = b;
        b = temp;
    }

    void Swap2(ref int a , ref int b)
    {
        int temp = a;
        a = b;
        b = temp;
    }

    void OnClick_OK()
    {
        txt_Result.text = "";
        int a = 100;
        int b = 200;

        int sum = Sum(10, 20);
        txt_Result.text += string.Format("гу╟Х = {0}\n", sum);
        txt_Result.text += string.Format("-------------------------------------\n", sum);

        txt_Result.text += string.Format("a = {0} , b = {1}\n", a, b);
        Swap(a, b);
        txt_Result.text += string.Format("a = {0} , b = {1}\n", a, b);
        Swap2(ref a, ref b);
        txt_Result.text += string.Format("a = {0} , b = {1}\n", a, b);

        txt_Result.text += string.Format("-------------------------------------\n", sum);
    }
    
    void OnClick_Clear()
    {

    }
}
