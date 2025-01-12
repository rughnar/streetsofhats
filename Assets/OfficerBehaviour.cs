using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OfficerBehaviour : MonoBehaviour
{
    public float arrestSpeed = 10f;
    private EnemyController enemyController;
    private PlayerAttack playerAttack;
    private Boolean playerOnSight = false;
    private Boolean playerStole = false;

    private Boolean proceedToArrest = false;

    void Awake()
    {
        playerAttack = FindObjectOfType<PlayerAttack>();
        enemyController = GetComponent<EnemyController>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("PlayerPresence"))
        {
            playerOnSight = true;
            if (playerStole)
            {
                enemyController.ToggleMoveNormally();
                proceedToArrest = true;
            }

        }
    }


    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("PlayerPresence")) playerOnSight = true;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("PlayerPresence"))
        {
            playerOnSight = false;
            if (playerStole)
            {
                enemyController.ToggleMoveNormally();
                proceedToArrest = false;
            }

        }
    }

    void Update()
    {
        Debug.Log("Player On Sight? " + playerOnSight);
        Debug.Log("Ïs Player stealing? " + playerAttack.Stealing());
        if (playerOnSight && (playerAttack.Stealing() || playerAttack.HasMoreThanOneHat()))
        {
            playerStole = true;
            proceedToArrest = true;
        }
        if (playerStole && proceedToArrest) ArrestPlayer();
    }


    private void ArrestPlayer()
    {
        enemyController.ToggleMoveNormally();
        Transform tf = playerAttack.gameObject.transform;
        float distance = Vector2.Distance(transform.position, tf.position);

        // Verifica si el jugador está dentro del rango de persecución

        // Mueve al enemigo hacia el jugador
        Vector2 direction = (tf.position - transform.position).normalized;
        transform.position = Vector2.MoveTowards(transform.position, tf.position, arrestSpeed * Time.deltaTime);
    }
}
