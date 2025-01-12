using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OfficerBehaviour : MonoBehaviour
{
    public float arrestSpeed = 10f;
    private EnemyController enemyController;
    private PlayerController playerController;
    private Boolean playerOnSight = false;
    private Boolean playerStole = false;
    private Boolean proceedToArrest = false;

    private Boolean arresting = false;

    void Awake()
    {
        playerController = FindObjectOfType<PlayerController>();
        enemyController = GetComponent<EnemyController>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("PlayerPresence"))
        {
            playerOnSight = true;
            if (playerStole && !proceedToArrest && !arresting)
            {
                enemyController.MoveNormally(false);
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
                enemyController.MoveNormally(true);
                proceedToArrest = false;
            }

        }
    }

    void Update()
    {
        Debug.Log("Player On Sight? " + playerOnSight);
        Debug.Log("Ïs Player stealing? " + playerController.Stealing());
        if (playerOnSight && (playerController.Stealing() || playerController.HasMoreThanOneHat()))
        {
            playerStole = true;
            proceedToArrest = true;
        }
        if (playerStole && proceedToArrest) ArrestPlayer();
    }


    private void ArrestPlayer()
    {
        enemyController.MoveNormally(false);
        Transform tf = playerController.gameObject.transform;
        float distance = Vector2.Distance(transform.position, tf.position);

        // Verifica si el jugador está dentro del rango de persecución

        // Mueve al enemigo hacia el jugador
        Vector2 direction = (tf.position - transform.position).normalized;
        transform.position = Vector2.MoveTowards(transform.position, tf.position, arrestSpeed * Time.deltaTime);

        if (distance < 1 && !arresting)
        {
            StartCoroutine(Arresting());
        }
    }

    private IEnumerator Arresting()
    {
        arresting = true;
        yield return new WaitForSeconds(0.2f);
        playerController.GetArrested(1);
        playerStole = false;
        proceedToArrest = false;
        arresting = false;
        enemyController.MoveNormally(true);

    }
}
