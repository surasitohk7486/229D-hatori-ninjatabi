using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI : MonoBehaviour
{
    [SerializeField]
    public Player G_HP;
    public Player_NinjaM B_HP;
    public static int GHP;
    public static int BHP;
    public TextMeshProUGUI scoreTextG;
    public TextMeshProUGUI scoreTextB;
    void Update()
    {
        scoreTextG.text = "HP : " + G_HP.HP;
        scoreTextB.text = B_HP.HP + " : HP";
    }
}
