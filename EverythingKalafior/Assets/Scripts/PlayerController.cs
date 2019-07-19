using Assets.Scripts.Interfaces;
using Assets.Scripts.Serializable;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class PlayerController : MonoBehaviour, IPlayerController
{
    public Keys Keys;
    public PlayerConfig PlayerConfig;

    private Rigidbody2D rb;
    private BoxCollider2D col;
    private IInput input;
    private bool IsActive;

    public void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<BoxCollider2D>();
    }

    public void Freeze()
    {
    }

    public void Jump()
    {
    }

    public void Walk(Vector2 direction)
    {
    }

    void Start()
    {
    }

    void Update()
    {
        
    }

    private void Move()
    {
        Walk(new Vector2(input.GetHorizontalAxisValue(), 0));
    }
}
