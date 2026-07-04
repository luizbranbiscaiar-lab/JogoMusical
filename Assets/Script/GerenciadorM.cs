using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GerenciadorM : MonoBehaviour
{
    public static GerenciadorM instancia;

    [Header("Configuração de UI")]
    public Slider barraProgresso;
    public GameObject painelVitoria; // Arraste o seu Painel de Vitória aqui

    private int pontosAtuais = 0;
    private Vector3 escalaOriginal;

    void Awake() => instancia = this;

    void Start()
    {
        // Salva a escala original se a barra existir
        if (barraProgresso != null)
        {
            escalaOriginal = barraProgresso.transform.localScale;
        }

        // Garante que o painel de vitória comece escondido
        if (painelVitoria != null)
        {
            painelVitoria.SetActive(false);
        }
    }

    public void GanhouDo()
    {
        if (pontosAtuais < 15)
        {
            pontosAtuais++;
            AtualizarInterface(); // Atualiza a barra e o pulo de escala

            if (pontosAtuais >= 15)
            {
                MostrarVitoria();
            }
        }
    }

    public void PerdeuPonto()
    {
        if (pontosAtuais > 0)
        {
            pontosAtuais--;
            AtualizarInterface();
        }
    }

    void AtualizarInterface()
    {
        if (barraProgresso != null)
        {
            // Atualiza o valor do Slider
            barraProgresso.value = pontosAtuais;

            // Faz o efeito de pulo na escala
            barraProgresso.transform.localScale = escalaOriginal * 1.1f;

            // Cancela invocações anteriores para não bugar se ganhar pontos rápido
            CancelInvoke("ResetScale");
            Invoke("ResetScale", 0.1f);
        }
    }

    void ResetScale()
    {
        if (barraProgresso != null)
        {
            barraProgresso.transform.localScale = escalaOriginal;
        }
    }

    void MostrarVitoria()
    {
        // Salva o progresso no PlayerPrefs
        string idFase = "Venceu_M" + MenuController.mundoAtual + "_F" + MenuController.faseAtual;

        if (PlayerPrefs.GetInt(idFase, 0) == 0)
        {
            PlayerPrefs.SetInt(idFase, 1);
            string chaveMundo = "Mundo" + MenuController.mundoAtual + "_Completas";
            int total = PlayerPrefs.GetInt(chaveMundo, 0);
            PlayerPrefs.SetInt(chaveMundo, total + 1);
            PlayerPrefs.Save();
        }

        // Ativa o painel de vitória
        if (painelVitoria != null)
        {
            painelVitoria.SetActive(true);
            Time.timeScale = 0;
        }
    }

    // Função para o botão do painel de vitória chamar
    public void IrParaOMapa()
    {
        // IMPORTANTE: Se o nome da sua cena de mapa for diferente, mude aqui
        SceneManager.LoadScene("MenuMapa");
        Time.timeScale = 1;
    }
}
