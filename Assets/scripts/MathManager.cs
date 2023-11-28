using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MathManager : MonoBehaviour
{
    public Visual Visual;
    public Text TextExam, TextScore;
    int score = 0;
    int answer;
    int interim;
    int combo;

    public static float SpeedCountdown = 0.5f;
    public static int AddingToCombo = 1;


    [SerializeField] float ComboСounting = 0;

    [SerializeField] bool multiplayer, player1;
    //лист цифр в примере
    List<int> numb = new List<int>() { 0, 0, 0, 0 };
    //лист знаков в примере (плюс или минус)
    List<bool> symbol = new List<bool>() { false, false, false }; 

    [SerializeField] GameObject GameOver;
    
    [SerializeField] ParticleSystem Explode;

    IEnumerator CorCountdown;

    
    

    void Start()
    {
        TextScore.text = "Score:"+ score.ToString();
        rand(0);
        CorCountdown = ComboCountdown();
        StartCoroutine(CorCountdown);
    }

    //эта фунция нужна для генерации примера.
    //принимает сложность.   
    void rand(int difficulty)
    {
        string example = "if you see this in game something went wrong";
        answer = UnityEngine.Random.Range(1,4);

        if (difficulty == 0)
        {
            do
            {
                numb[0] = UnityEngine.Random.Range(1, 4); 
            }
            while (answer == numb[0]);

            if (answer > numb[0])
                symbol[0] = true;
            else
                symbol[0] = false;

            numb[1] = Math.Abs(answer - numb[0]);            
        }
        else if (difficulty == 1)
        {
            numb[0] = UnityEngine.Random.Range(1, 4);
            symbol[0] = UnityEngine.Random.Range(0, 2) == 1;
            do 
            {
                numb[1] = UnityEngine.Random.Range(1, 4);
            }
            while (CheckFor1());            

        }
        example = CobbleTogether(difficulty);
        if(TextExam.text != example)
            TextExam.text = example;
        else
            rand(difficulty);
    }

    bool CheckFor1()
    {

        if (symbol[0])
        {
            interim = numb[0] + numb[1];
        }
        else
        {
            interim = numb[0] - numb[1];
        }

        numb[2] = Math.Abs(answer - interim);

        if (answer > interim)
            symbol[1] = true;
        else
            symbol[1] = false;


        if ((Math.Abs(answer - interim) <= 3) && (Math.Abs(answer - interim) >= 1))
        {
            return false;
        }
        return true;
    }

    // эта функциця сшивает пример
    // (делает из всего строчку)
    // в зависимости от сложности
    string CobbleTogether(int dif)
    {
        if (dif == 0)
        {
            return numb[0].ToString() + SymbolToString(symbol[0]) + numb[1].ToString() + "= ?";
        }
        else 
        {           
            return $"{numb[0]}" + SymbolToString(symbol[0]) + $"{numb[1]}" + SymbolToString(symbol[1]) + $"{numb[2]}" + "= ?";            
        }
    }
    private string SymbolToString(bool symbol)
    {
        if (symbol)
        {
            return "+";
        }
        else
        {
            return "-";
        }
    }

    /* фунция button отвечает за то чтоб кнопки сверяли 
         * ответет с их значением и в зависимосте от ответа 
          прибавляли или убавляли очки (значенние кнопки выбирается в юнити)*/
    public void Button(int NumBut)
    {
        if (answer == NumBut)
        {
            score += combo+1;
            TextScore.text = "Score:" + score.ToString();
            CheckCombo(1);
            rand(combo);            
            if (multiplayer==true && score >= Settings.ScoreToWin)
            {
                GameOver.SetActive(true);
                if (player1 == false)
                {
                    GameOver.transform.rotation = new Quaternion(0f, 0f, 180f, 0);
                }
            }           
        }
        else
        {            
            score = 0;
            TextScore.text = "Score:" + score.ToString();
            CheckCombo(-2);
            rand(combo);          
        }
    }

    void CheckCombo(int Accuracy)
    {
        switch (Accuracy)
        {
            case 1:
                StartCoroutine(SmoothAdd(AddingToCombo));
                if (ComboСounting > 15 && combo == 0)
                {                    
                    Visual.SetComboBar(1);
                    StartCoroutine(SmoothAdd(1f));
                    combo = 1;
                    Visual.SetFire(true,0);
                    break;
                }
                if (ComboСounting > 30 && combo == 1)
                {
                    StartCoroutine(SmoothAdd(1f)); ;
                    combo = 1;
                    Visual.SetFire(true, 1);
                    break;
                }
                Visual.TextCombo.text = $"COMBO x{combo + 1}";
                break;

            case -1:               
                combo--;
                ComboСounting -= 1f;
                Visual.SetComboBar(combo);
                Explode.Play();
                Visual.SetFire(false,0);               
                break;

            case -2:
                if (combo > 0)
                {                   
                    Explode.Play();
                    Visual.SetFire(false,0);
                    Visual.SetComboBar(0);                    
                }
                Visual.TextCombo.text = "";
                combo = 0;                
                StartCoroutine(SmoothOver());
                break;
        }
    }
    //корутина котороя убавляет полоску комбо ._.
    public IEnumerator ComboCountdown()
    {
        while (true)
        {
            if (ComboСounting <= 15 && combo == 1 || ComboСounting <= 30 && combo == 2)
            {
                CheckCombo(-1);
            }
            if (ComboСounting >= 0)
            {
                ComboСounting -= Time.deltaTime*SpeedCountdown;
                Visual.UpdateComboBar(ComboСounting);
            }
            else
            {
                Visual.TextCombo.text = "";
            }          
            yield return null;
        }      
    }
    //корутина котороя постепенно повышает полоску комбо
    IEnumerator SmoothAdd(float toAdd)
    {
        StopCoroutine(CorCountdown);
        for (int i = 0; i < toAdd*5; i ++)
        {
            yield return new WaitForSeconds(0.05f/toAdd);
            ComboСounting += 0.2f;
            Visual.UpdateComboBar(ComboСounting);
        }
        StartCoroutine(CorCountdown);
    }
    //корутина заканчивает полоску комбо
    IEnumerator SmoothOver()
    {
        for (int i = 10; i > 0 ; i--)
        {
            yield return new WaitForSeconds(0.001f);
            ComboСounting -= ComboСounting/i;            
        }
        ComboСounting = 0 ;
    }
}

