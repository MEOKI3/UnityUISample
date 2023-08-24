using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Test002Dlg : MonoBehaviour
{
    [SerializeField] InputField input_Score;
    [SerializeField] Text txt_Result;

    [SerializeField] Button btn_OK;
    [SerializeField] Button btn_Clear;

    private void Start()
    {
        btn_OK.onClick.AddListener(() => ClickOK());
    }

    void ClickOK()
    {
        int kScore = int.Parse(input_Score.text);

        if (kScore < 0 || kScore > 100) return;

        int checkScore = kScore / 10;

        if(checkScore == 10 || checkScore == 9) txt_Result.text = "A";
        else if(checkScore == 8) txt_Result.text = "B";
        else if(checkScore == 7) txt_Result.text = "C";
        else if(checkScore == 6) txt_Result.text = "D";
        else txt_Result.text = "F";

        //switch(kScore / 10)
        //{
        //    case 10:
        //    case 9:
        //        txt_Result.text = "A";
        //        break;
        //    case 8:
        //        txt_Result.text = "B";
        //        break;
        //    case 7:
        //        txt_Result.text = "C";
        //        break;
        //    case 6:
        //        txt_Result.text = "D";
        //        break;
        //    default:
        //        txt_Result.text = "F";
        //        break;
        //}
    }

    void ClickClear()
    {
        txt_Result.text = "Result";
    }
}
