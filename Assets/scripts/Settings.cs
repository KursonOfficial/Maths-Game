using System;
using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using Unity.VisualScripting;

public class Settings : MonoBehaviour
{
    public static int ScoreToWin;
    public GameObject input_obg;
    public Text ScoreLableText;
    public GameObject ErrorScore;
    public GameObject AllSettings;

    private void Start() // настройка статики
    {if (Storage._ScoreToWin != 0) ScoreToWin = Storage._ScoreToWin; else ScoreToWin = 50;}

    public void DropDownScore(int option){
        switch(option){
            case 0:
                input_obg.SetActive(false);
                ScoreToWin = 50;
                ErrorScore.SetActive(false);
                ScoreLableText.text = ScoreToWin.ToString();
            break;
            case 1:
                input_obg.SetActive(false);
                ScoreToWin = 100;
                ErrorScore.SetActive(false);
                ScoreLableText.text = ScoreToWin.ToString();
            break;
            case 2:
                input_obg.SetActive(true);
                ScoreLableText.text = "своё";
            break;
        }            
    }
    public void SetScoreToWin(string number){
        if (number.Length > 0 && Convert.ToInt32(number)>0){
            ScoreToWin = Convert.ToInt32(number);
            Storage._ScoreToWin = Convert.ToInt32(number); // занёс в статику значение выйгрыша
            ErrorScore.SetActive(false);
            ScoreLableText.text = ScoreToWin.ToString();
        }else
            ErrorScore.SetActive(true);
    }
    public void SetSettingsActive(){
        AllSettings.SetActive(true);
        ScoreLableText.text = ScoreToWin.ToString();
    } 
     
}