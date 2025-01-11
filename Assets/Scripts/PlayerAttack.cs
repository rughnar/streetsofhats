using UnityEngine.InputSystem;
using UnityEngine;
using System.Collections;
using UnityEngine.Rendering;
using System.Collections.Generic;

public class PlayerAttack : MonoBehaviour
{
    public AudioClip stealClip;
    public AudioClip secureClip;
    public float timeBetweenSteals = 0.3f;
    public float timeBetweenSecures = 0.3f;
    private PlayerInputActions _playerActions;
    private InputAction _steal;
    private InputAction _secure;
    private AudioManager _audioManager;
    private Camera _cam;
    private UIController _uiController;
    private float lastStealTime = 0f;
    private float lastSecureTime = 0f;
    private Coroutine steal;
    private Coroutine secure;

    private bool isStealing = false;
    private bool isSecuring = false;

    private HatHolder _hatHolder;

    private void Awake()
    {
        _playerActions = new PlayerInputActions();
        _audioManager = FindObjectOfType<AudioManager>();
        _cam = FindObjectOfType<Camera>();
        _uiController = FindObjectOfType<UIController>();
        _hatHolder = GetComponentInChildren<HatHolder>();
    }


    private void OnEnable()
    {
        _steal = _playerActions.Player.Fire;
        _steal.Enable();
        //_steal.performed += Steal;

        _secure = _playerActions.Player.Fire2;
        _secure.Enable();
    }

    private void OnDisable()
    {
        _steal.Disable();
        _secure.Disable();
    }


    void Update()
    {
        if (isStealing && _steal.WasReleasedThisFrame())
        {
            StopCoroutine(steal);
            isStealing = false;
        }
        if (isSecuring && _secure.WasReleasedThisFrame())
        {
            StopCoroutine(secure);
            isSecuring = false;
        }
    }
    void OnTriggerStay2D(Collider2D other)
    {
        if (_steal.IsPressed())
        {
            Debug.Log("In contact");
            if (other.gameObject.CompareTag("HatWielder") && !isStealing)
            {
                isStealing = true;
                steal = StartCoroutine(Steal(other.gameObject));

            }
        }


        if (other.gameObject.CompareTag("Stall") && _secure.IsPressed() && !isSecuring)
        {
            isSecuring = true;
            secure = StartCoroutine(Secure());
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (isStealing && other.gameObject.CompareTag("HatWielder"))
        {
            StopCoroutine(steal);
            Debug.Log("Stealing interrupted");
            isStealing = false;
        }
        if (isSecuring && other.gameObject.CompareTag("Stall"))
        {
            StopCoroutine(secure);
            Debug.Log("Securing interrupted");
            isSecuring = false;
        }
    }

    private IEnumerator Steal(GameObject gameObject)
    {
        Debug.Log("Stealing hat");
        yield return new WaitForSeconds(timeBetweenSteals);
        Debug.Log("Hat Stolen");
        GameObject hat = gameObject.GetComponent<EnemyController>().GetStealed();
        _hatHolder.AddHat(hat);
        //UIController.AddToMultiplier(hat.getMultiplier());
        _steal.Reset();
        lastStealTime = Time.fixedTime;
        isStealing = false;
    }

    private IEnumerator Secure()
    {
        Debug.Log("Securing hat");
        yield return new WaitForSeconds(timeBetweenSecures);
        Debug.Log("Hat Secured");
        lastSecureTime = Time.fixedTime;
        //GameObject hat = other. <HatWielder>().GetStealed();
        //hatsInPossesions.Add(hat);
        //UIController.AddToMultiplier(hat.getMultiplier());
        _secure.Reset();
        isSecuring = false;
    }


}
