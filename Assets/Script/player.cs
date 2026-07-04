using UnityEngine;
using UnityEngine.InputSystem;

public class player : MonoBehaviour
{
    [Header("Configurações de Movimento")]
    public float velocidadeNoChao = 10f;
    public float velocidadeNoAr = 5f;
    public float forcaPulo = 12f;
    public float suavizacao = 0.1f;

    [Header("Configurações do Agachamento")]
    private bool estaAgachado = false;

    [Header("Detecção de Chão")]
    public Transform detectorChao;
    public float raioDeteccao = 0.4f;
    public LayerMask camadaChao;

    private Rigidbody2D rb;
    private bool estaNoChao;
    private float moveInput;
    private float finalInput;
    private float direcaoOlhar = 1f;
    private Vector2 velocidadeAuxiliar;
    private Animator anim;

    private int movendoHash = Animator.StringToHash("movendo");
    private int saltandoHash = Animator.StringToHash("saltando");
    private int agachadoHash = Animator.StringToHash("Agachado");
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.freezeRotation = true;
        rb.gravityScale = 3.5f;
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        estaNoChao = Physics2D.OverlapCircle(detectorChao.position, raioDeteccao, camadaChao);

        // --- NOVO INPUT SYSTEM ---
        float h = 0f;

        if (Keyboard.current != null)
        {
            if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed) h = 1f;
            else if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed) h = -1f;

            if (Keyboard.current.spaceKey.wasPressedThisFrame) Pular();

            // Define o estado de agachado
            estaAgachado = Keyboard.current.leftShiftKey.isPressed && estaNoChao;
        }

        // Unifica teclado e botões mobile
        finalInput = (h != 0) ? h : moveInput;

        // TRAVA DE MOVIMENTO: Zera o input se estiver agachado
        if (estaAgachado)
        {
            finalInput = 0f;
        }

        if (finalInput > 0) direcaoOlhar = 1f;
        else if (finalInput < 0) direcaoOlhar = -1f;

        float velAlvo = estaNoChao ? velocidadeNoChao : velocidadeNoAr;

        Vector2 targetVelocity = new Vector2(finalInput * velAlvo, rb.linearVelocity.y);
        rb.linearVelocity = Vector2.SmoothDamp(rb.linearVelocity, targetVelocity, ref velocidadeAuxiliar, suavizacao);

        // Atualiza os parâmetros do Animator
        anim.SetBool(movendoHash, finalInput != 0);
        anim.SetBool(saltandoHash, !estaNoChao);
        anim.SetBool(agachadoHash, estaAgachado);

        spriteRenderer.flipX = (direcaoOlhar < 0);
    }

    public void ApertouBotao(float dir) => moveInput = dir;
    public void SoltouBotao() => moveInput = 0;

    public void Pular()
    {
        if (estaNoChao && !estaAgachado)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, forcaPulo);
        }
    }
}