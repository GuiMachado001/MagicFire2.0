using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private PlayerControls controls;
    private Vector2 moveInput;
    public float speed = 3f;

    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sprite;

    private bool hasSpeedParameter = false;
    private int speedHash;

    private void Awake()
    {
        controls = new PlayerControls();

        controls.Player.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        controls.Player.Move.canceled += ctx => moveInput = Vector2.zero;

        controls.Player.Attack.performed += ctx =>
        {
            if (anim != null)
                anim.SetTrigger("Attack");
        };
    }

    private void OnEnable() => controls.Player.Enable();
    private void OnDisable() => controls.Player.Disable();

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();

        if (anim != null)
        {
            foreach (var p in anim.parameters)
            {
                if (p.type == AnimatorControllerParameterType.Float && p.name == "Speed")
                {
                    hasSpeedParameter = true;
                    speedHash = Animator.StringToHash("Speed");
                    break;
                }
            }
        }
    }

    void Update()
    {
        if (hasSpeedParameter)
            anim.SetFloat(speedHash, Mathf.Abs(moveInput.x));

        if (moveInput.x > 0.1f) sprite.flipX = false;
        else if (moveInput.x < -0.1f) sprite.flipX = true;
    }

    void FixedUpdate()
    {
        if (rb != null)
            rb.velocity = new Vector2(moveInput.x * speed, rb.velocity.y);
        else
            transform.Translate(new Vector3(moveInput.x, 0f, 0f) * speed * Time.fixedDeltaTime);
    }
}
