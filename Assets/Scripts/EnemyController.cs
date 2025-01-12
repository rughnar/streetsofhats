using UnityEngine;
using System.Collections;
using UnityEngine.Assertions.Must;

public class EnemyController : MonoBehaviour
{
    public float attackPoints = 10;
    public float velocity = 1f;
    public float hp;
    public AudioClip destroy;
    public HatHolder _hatHolder;
    private Rigidbody2D _rb;
    private AudioManager _audioManager;
    private EnemyManager _enemyManager;
    private SpriteRenderer _spriteRenderer;
    private int flipX;

    private bool moveNormally = true;
    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _audioManager = FindObjectOfType<AudioManager>();
        _enemyManager = FindObjectOfType<EnemyManager>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _hatHolder = GetComponentInChildren<HatHolder>();
    }

    void FixedUpdate()
    {
        if (moveNormally)
        {
            flipX = _spriteRenderer.flipX ? -1 : 1;
            _rb.MovePosition(new Vector2(_rb.position.x + velocity * 0.01f * flipX, _rb.position.y));
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        /* if (other.gameObject.CompareTag("Dam"))
         {
             other.gameObject.GetComponent<DamController>().ReduceHealth((int)attackPoints);
             Destroy(gameObject, 0.05f);
         }
 */
    }

    public void ReduceHealth(float hp)
    {
        this.hp -= hp;
        if (this.hp <= 0)
        {
            //audioManager.PlaySFX(destroy);
            _enemyManager.EnemyTakenDown();

            Destroy(gameObject, 0.05f);
        }
        else
        {
            StartCoroutine(GoFromColorToColorIn(0.2f, Color.red, Color.white));
        }

    }


    IEnumerator GoFromColorToColorIn(float seconds, Color colorFrom, Color colorTo)
    {
        _spriteRenderer.color = colorFrom;
        yield return new WaitForSeconds(seconds);
        _spriteRenderer.color = colorTo;
    }

    void OnBecameInvisible()
    {
        Destroy(this.gameObject);
    }

    public GameObject GetStealed()
    {
        return _hatHolder.RemoveHat();
    }

    public bool HasHat()
    {
        return _hatHolder.HasHat();
    }

    public void FaceCenter()
    {
        if (transform.position.x >= 0) _spriteRenderer.flipX = true;
        else _spriteRenderer.flipX = false;
    }

    public void ToggleMoveNormally()
    {
        if (moveNormally) moveNormally = false;
        else moveNormally = true;
    }
}
