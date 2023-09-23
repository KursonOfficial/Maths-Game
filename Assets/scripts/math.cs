using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class math : MonoBehaviour
{
  public Text text_prim,text_score;
  int score = 0;
  int answ;
  List<int> numb = new List<int>() { 0, 0, 0, 0 };
  public bool multiplayer;
  List<bool> sus = new List<bool>() { false, false, false }; 
  public GameObject win;
  void Start(){   
    text_score.text = "Score:"+ score.ToString();
    rand(0);
  }
  void rand(int dif){
    string prim = "if you see this in game something went wrong";
    answ = UnityEngine.Random.Range(1,4);

    if (dif == 0){

      do
        {numb[0]=UnityEngine.Random.Range(1,4);}
      while(answ==numb[0]);

      if (answ>numb[0])
        sus[0]= true;
      else
        sus[0]= false; 

    numb[1]=Math.Abs(answ-numb[0]);

    prim = numb[0].ToString() + SusToString(sus[0]) + numb[1].ToString() + "= ?";

  }
  if(text_prim.text != prim)
    text_prim.text = prim;
  else
    rand(dif);
}

  private string SusToString(bool sus){
  if (sus){
    return "+";
  }else{
    return "-";
  }
 }

    public void but0(int NumBut){
      if (answ == NumBut){
        score++;
        text_score.text = "Score:" + score.ToString();
        rand(0);
        if (multiplayer==true && score == Settings.ScoreToWin){
          
        }
      }else{
        score = 0;
        text_score.text = "Score:" + score.ToString();
        rand(0);
      }
  }
}