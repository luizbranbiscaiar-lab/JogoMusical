using UnityEngine;

public class PMoedas : MonoBehaviour
{
    [SerializeField] private float velocidade = 5f;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb != null) rb.linearVelocity = Vector2.left * velocidade;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Checa se a tag desta moeda é a que o Menu enviou
            if (gameObject.CompareTag(MenuController.tagAlvoDaFase))
            {
                GerenciadorM.instancia.GanhouDo();
                Debug.Log("Nota Correta!");
            }
            else if (gameObject.CompareTag("Dó") || gameObject.CompareTag("Ré") ||
                     gameObject.CompareTag("Mi") || gameObject.CompareTag("Inimigo"))
            {
                GerenciadorM.instancia.PerdeuPonto();
                Debug.Log("Nota Errada!");
            }
            Destroy(gameObject);
        }

        if (other.CompareTag("Obstaculo")) Destroy(gameObject);
    }
}
