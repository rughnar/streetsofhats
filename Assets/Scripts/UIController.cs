using System.Collections;
using UnityEngine;
using TMPro;

public class UIController : MonoBehaviour
{
    public TMP_Text livesText;
    public TMP_Text scoreText;
    public TMP_Text multiplierText;
    private Coroutine livesColorChange = null;
    private Coroutine multiplierColorChange = null;

    /*
        public void SetHPBar(float hp)
        {
            healthSlider.SetValueWithoutNotify(hp);
        }

        public void AddToHPBar(float hp)
        {
            healthSlider.SetValueWithoutNotify(healthSlider.value + hp);
        }
    */

    public void SetLivesSilently(int lives)
    {
        livesText.text = "Lives: " + lives;
    }

    public void IncreaseLives(int lives)
    {
        if (livesColorChange != null) StopCoroutine(livesColorChange);
        livesColorChange = StartCoroutine(GoFromColorToColorIn(0.2f, Color.green, Color.white, livesText));
        SetLivesSilently(lives);
    }

    public void DecreaseLives(int lives)
    {
        if (livesColorChange != null) StopCoroutine(livesColorChange);
        livesColorChange = StartCoroutine(GoFromColorToColorIn(0.2f, Color.red, Color.white, livesText));
        SetLivesSilently(lives);
    }


    public void SetMultiplierSilently(float multiplier)
    {
        multiplierText.text = multiplier + "x Multiplier";
    }

    public void IncreaseMultiplier(float multiplier)
    {
        if (multiplierColorChange != null) StopCoroutine(multiplierColorChange);
        multiplierColorChange = StartCoroutine(GoFromColorToColorIn(0.2f, Color.green, Color.white, multiplierText));
        SetMultiplierSilently(multiplier);
    }

    public void DecreaseMultiplier(float multiplier)
    {
        if (multiplierColorChange != null) StopCoroutine(multiplierColorChange);
        multiplierColorChange = StartCoroutine(GoFromColorToColorIn(0.2f, Color.red, Color.white, multiplierText));
        SetMultiplierSilently(multiplier);
    }



    public void SetScoreSilently(int score)
    {
        scoreText.text = "" + score;
    }


    IEnumerator GoFromColorToColorIn(float seconds, Color colorFrom, Color colorTo, TMP_Text text)
    {
        text.faceColor = colorFrom;
        yield return new WaitForSeconds(seconds);
        text.faceColor = colorTo;
    }

    public void KeepSameLivesValue()
    {
        if (livesColorChange != null) StopCoroutine(livesColorChange);
        livesColorChange = StartCoroutine(GoFromColorToColorIn(0.3f, Color.yellow, Color.white, livesText));

    }
}
