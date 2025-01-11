using System.Collections;
using UnityEngine;
using TMPro;

public class UIController : MonoBehaviour
{
    public TMP_Text livesText;
    public TMP_Text ammoText;

    private Coroutine livesColorChange = null;
    private Coroutine ammoColorChange = null;

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
        livesText.text = "" + lives;
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



    public void SetAmmoSilently(int ammo)
    {
        ammoText.text = "" + ammo;
    }

    public void IncreaseAmmo(int ammo)
    {
        if (ammoColorChange != null) StopCoroutine(ammoColorChange);
        ammoColorChange = StartCoroutine(GoFromColorToColorIn(0.2f, Color.green, Color.white, ammoText));
        SetAmmoSilently(ammo);
    }

    public void DecreaseAmmo(int ammo)
    {
        if (ammoColorChange != null) StopCoroutine(ammoColorChange);
        ammoColorChange = StartCoroutine(GoFromColorToColorIn(0.2f, Color.red, Color.white, ammoText));
        SetAmmoSilently(ammo);
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
