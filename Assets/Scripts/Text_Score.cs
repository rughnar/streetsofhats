using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Text_Score : MonoBehaviour
{
    
    TextMeshProUGUI texto;

    private void Awake()
    {
        texto = GetComponent<TextMeshProUGUI>();
    }

    void Start()
    {
        Debug.Log(PlayerPrefs.GetString("score").ToString());

        texto.text = PlayerPrefs.GetString("score").ToString();
    }
}
