﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyController : MonoBehaviour
{
    public Text moneyText1, moneyText2,
                moneyText3, moneyText4;
    public int money;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        string text = "$" + money;
        moneyText1.text = text;
        moneyText2.text = text;
        moneyText3.text = text;
        moneyText4.text = text;
    }
}