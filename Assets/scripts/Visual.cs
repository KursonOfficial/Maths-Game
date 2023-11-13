using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Visual : MonoBehaviour
{
    [SerializeField] List<Image> ImgComboBars;
    [SerializeField] List<GameObject> ObgComboBars;
    [SerializeField] ParticleSystem Fire;
    public Text TextCombo;
    public MathManager MathManager;

    int ComboBarState;
    //������ ����� ��� ���������/���������� ���� 
    public void SetFire(bool set, int amount)
    {       
        if (set)
        {
            var FireEmission = Fire.emission;
            FireEmission.rateOverTime = 40f;
        }
        else
        {
            var FireEmission = Fire.emission;
            FireEmission.rateOverTime = 0f;
        }
    }
    public void UpdateComboBar(float Combo�ounting)
    {
        switch (ComboBarState)
        {
            case 0:
                ImgComboBars[0].fillAmount = Combo�ounting / 15f;
                break;
            case 1:
                ImgComboBars[1].fillAmount = (Combo�ounting - 15f) / 15f;
                break;
            case 2:
                ImgComboBars[2].fillAmount = (Combo�ounting - 30f) / 15f;
                break;
        }

        
    }
    public void SetComboBar(int state)
    {
        switch (state)
        {
            case 0:
                TextCombo.text = "COMBO x1";
                ComboBarState = state;
                ObgComboBars[0].SetActive(true);
                ObgComboBars[1].SetActive(false);
                ObgComboBars[2].SetActive(false);
                break;
            case 1:
                TextCombo.text = "COMBO x2";
                ComboBarState = state;
                ObgComboBars[0].SetActive(false);
                ObgComboBars[1].SetActive(true);
                ObgComboBars[2].SetActive(false);
                break;
            case 2:
                ComboBarState = state;
                ObgComboBars[1].SetActive(false);
                ObgComboBars[2].SetActive(true);
                break;
        }
    }
}
