using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHPController : MonoBehaviour
{

    [SerializeField] private int vida = 3;
    private UIController uiController;
    private AudioManager audioManager;
    private SpriteRenderer spriteRenderer;
    private Coroutine colorChange;
    private BoxCollider2D boxCollider2D;
    private bool invulnerable = false;

    void Awake()
    {
        uiController = FindObjectOfType<UIController>();
        uiController.SetLivesSilently(vida);
        audioManager = FindObjectOfType<AudioManager>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider2D = GetComponent<BoxCollider2D>();
    }

    public int GetVida()
    {
        return vida;
    }

    public void SetVida(int vida)
    {
        this.vida = vida;
    }

    public void restarVida(int cantidad)
    {
        this.vida -= cantidad;
        uiController.DecreaseLives(vida);
        if (colorChange != null) StopCoroutine(colorChange);
        StartCoroutine(GoFromColorToColorIn(0.15f, Color.red, Color.white));

        if (vida <= 0)
        {
            SceneManager.LoadScene("Score");
        }
    }

    public void SumarVida(int cantidad)
    {
        this.vida += cantidad;
        uiController.IncreaseLives(vida);

    }

    IEnumerator GoFromColorToColorIn(float seconds, Color colorFrom, Color colorTo)
    {
        spriteRenderer.color = colorFrom;
        yield return new WaitForSeconds(seconds);
        spriteRenderer.color = colorTo;
    }

    public void Invulnerabilizar()
    {
        StartCoroutine(VolverseInvulnerable());
    }

    IEnumerator VolverseInvulnerable()
    {
        boxCollider2D.enabled = false;
        invulnerable = true;
        StartCoroutine(GoBackAndForthAlphaness());
        yield return new WaitForSeconds(2f);
        invulnerable = false;
        boxCollider2D.enabled = true;
    }

    IEnumerator GoBackAndForthAlphaness()
    {
        while (invulnerable)
        {
            spriteRenderer.color = new Color(255, 255, 255, 0f);
            yield return new WaitForSeconds(0.17f);
            spriteRenderer.color = new Color(255, 255, 255, 1f);
            yield return new WaitForSeconds(0.17f);
        }
        yield return null;
    }

}

