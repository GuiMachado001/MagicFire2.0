using UnityEngine;
using UnityEngine.InputSystem; // importante para o Input System

public class PlayerMovement : MonoBehaviour
{
    private PlayerControls controls;  // referência do input
    private Vector2 moveInput;
    public float speed = 5f;

    private void Awake()
    {
        controls = new PlayerControls();

        controls.Player.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        controls.Player.Move.canceled += ctx => moveInput = Vector2.zero;
    }

    private void OnEnable()
    {
        controls.Player.Enable();
    }

    private void OnDisable()
    {
        controls.Player.Disable();
    }

    void Update()
    {
        Vector3 movement = new Vector3(moveInput.x, 0, moveInput.y);
        transform.Translate(movement * speed * Time.deltaTime);
    }
}
