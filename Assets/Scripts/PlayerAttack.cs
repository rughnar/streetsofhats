using UnityEngine.InputSystem;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public AudioClip attackClip;
    public AudioClip eatTreeClip;

    public GameObject weaponStickPrefab;
    public float throwVelocity = 16f;
    public int ammo = 10;
    public float timeBetweenTreeInteractions = 0.3f;
    private PlayerInputActions _playerActions;
    private InputAction _attack;
    private InputAction _collect;

    private AudioManager _audioManager;
    private Camera _cam;
    private UIController _uiController;

    private float lastTreeAttack = 0f;

    private void Awake()
    {
        _playerActions = new PlayerInputActions();
        _audioManager = FindObjectOfType<AudioManager>();
        _cam = FindObjectOfType<Camera>();
        _uiController = FindObjectOfType<UIController>();
        _uiController.SetLivesSilently(ammo);
    }


    private void OnEnable()
    {
        _attack = _playerActions.Player.Fire;
        _attack.Enable();
        _attack.performed += Attack;

        _collect = _playerActions.Player.Fire2;
        _collect.Enable();
    }

    private void OnDisable()
    {
        _attack.Disable();
    }

    private void Attack(InputAction.CallbackContext callbackContext)
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

    }


    void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Pino") && _collect.IsPressed() && Time.fixedTime - lastTreeAttack > timeBetweenTreeInteractions)
        {
            /*TreeController tree = other.gameObject.GetComponent<TreeController>();
            tree.ReduceHealth();
            ammo += tree.ammoToGive;
            _uiController.IncreaseAmmo(ammo);
            _audioManager.PlaySFX(eatTreeClip);
            lastTreeAttack = Time.fixedTime;
        */
        }

    }
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Dam") && _collect.IsPressed())
        {
            /*DamController dam = other.gameObject.GetComponent<DamController>();

            if (!dam.IsAtMaxHp())
            {
                dam.AddHealth(1);
                ammo -= 1;
                _uiController.DecreaseAmmo(ammo);
            }
            else
            {
                _uiController.KeepSameLivesValue();

            }
            */
        }
    }

}
