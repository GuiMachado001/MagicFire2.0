using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private PlayerControls controls;
    private Vector2 moveInput;
    public float speed = 5f;
    private Animator animator;

    private void Awake()
    {
        controls = new PlayerControls();
        animator = GetComponent<Animator>();

        // Movimento
        controls.Player.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        controls.Player.Move.canceled += ctx => moveInput = Vector2.zero;

        // Ataque
        controls.Player.Attack.performed += ctx => Attack();
    }

    private void OnEnable() => controls.Player.Enable();
    private void OnDisable() => controls.Player.Disable();

    void Update()
    {
        Vector3 movement = new Vector3(moveInput.x, 0, moveInput.y);
        transform.Translate(movement * speed * Time.deltaTime);

        animator.SetFloat("Speed", movement.magnitude);
    }

    void Attack()
    {
        animator.SetTrigger("Attack");
    }
}
