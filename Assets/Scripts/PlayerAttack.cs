using UnityEngine.InputSystem;
using UnityEngine;
using System.Collections;
using UnityEngine.Rendering;

public class PlayerAttack : MonoBehaviour
{
    public AudioClip attackClip;
    public AudioClip eatTreeClip;
    public float timeBetweenSteals = 0.3f;
    public float timeBetweenSecures = 0.3f;
    public float timeNecessaryToSteal = 0.2f;
    public float timeNecessaryToSecure = 0.2f;
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

    private void Awake()
    {
        _playerActions = new PlayerInputActions();
        _audioManager = FindObjectOfType<AudioManager>();
        _cam = FindObjectOfType<Camera>();
        _uiController = FindObjectOfType<UIController>();
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
                steal = StartCoroutine(Steal());

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

    private IEnumerator Steal()
    {
        Debug.Log("Stealing hat");
        yield return new WaitForSeconds(timeBetweenSteals);
        Debug.Log("Hat Stolen");
        //Hat hat = other.gameObject.GetComponent<HatWielder>().GetStealed();
        //hatsInPossesions.Add(hat);
        //UIController.AddToMultiplier(hat.getMultiplier());
        _steal.Reset();
        lastStealTime = Time.fixedTime;
        isStealing = false;
    }

    private IEnumerator Secure()
    {
        Debug.Log("Securing hat");
        yield return new WaitForSeconds(timeNecessaryToSecure);
        Debug.Log("Hat Secured");
        lastSecureTime = Time.fixedTime;
        //Hat hat = other.gameObject.GetComponent<HatWielder>().GetStealed();
        //hatsInPossesions.Add(hat);
        //UIController.AddToMultiplier(hat.getMultiplier());
        _secure.Reset();
        isSecuring = false;
    }




    /*private void Steal(InputAction.CallbackContext callbackContext)
    {
        if (ammo > 0)
        {
            GameObject ws = Instantiate(weaponStickPrefab, this.transform.position, Quaternion.identity);
            Rigidbody2D rb = ws.GetComponent<Rigidbody2D>();//
            Vector2 mousePos = _cam.ScreenToWorldPoint(Input.mousePosition);
            Vector2 lookDir = mousePos - rb.position;
            float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
            rb.rotation = angle;
            rb.velocity = lookDir.normalized * throwVelocity;
            Debug.Log("dispare");
            ammo -= 1;
            _uiController.DecreaseAmmo(ammo);
            _audioManager.PlaySFX(attackClip);
        }
        else
        {
            Debug.Log("Me quede sin ramitas");
        }

    }*/


    void OnCollisionStay2D(Collision2D other)
    {
        /*
        if (other.gameObject.CompareTag("HatWielder") && _collect.IsPressed() && Time.fixedTime - lastTreeAttack > timeBetweenTreeInteractions)
        {
            TreeController tree = other.gameObject.GetComponent<TreeController>();
            tree.ReduceHealth();
            ammo += tree.ammoToGive;
            _uiController.IncreaseAmmo(ammo);
            _audioManager.PlaySFX(eatTreeClip);
            lastTreeAttack = Time.fixedTime;

        }
        */
    }
}
