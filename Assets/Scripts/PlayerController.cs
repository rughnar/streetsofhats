using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public AudioClip hurt;

    [SerializeField] private float _hp = 100;

    private AudioManager audioManager;
    private GameManager gameManager;

    void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        audioManager = FindObjectOfType<AudioManager>();
    }

    public void ReduceHealth(float hp)
    {
        _hp -= hp;
        audioManager.PlaySFX(hurt);
        if (_hp <= 0)
        {
            gameManager.EndLevel();
        }
    }


}
