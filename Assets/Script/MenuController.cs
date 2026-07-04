using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    [Header("Painéis Pais")]
    public GameObject painelMundos;
    public GameObject subMenuFases;
    public GameObject painelMissao;

    [Header("UI")]
    public TextMeshProUGUI txtMundo;
    public TextMeshProUGUI txtDescricao;
    public Image imgIconeMissao;

    [Header("Progressão Automática")]
    public Button[] botoesMundos;

    [Header("Título de cada mundo")]
    public string[] titulosMundos;

    public static string tagAlvoDaFase;
    public static int mundoAtual;
    public static int faseAtual;

    private string cenaParaCarregar;

    void Start()
    {
        VerificarBloqueioMundos();
    }

    public void VerificarBloqueioMundos()
    {
        for (int i = 0; i < botoesMundos.Length; i++)
        {
            if (botoesMundos[i] == null)
                continue;

            if (i == 0)
            {
                botoesMundos[i].interactable = true;
            }
            else
            {
                int fasesDoMundoAnterior = PlayerPrefs.GetInt("Mundo" + i + "_Completas", 0);

                botoesMundos[i].interactable = (fasesDoMundoAnterior >= 3);
            }
        }
    }

    public void SelecionarMundo(int numeroMundo)
    {
        mundoAtual = numeroMundo;

        if (txtMundo != null)
        {
            int index = numeroMundo - 1;

            if (index >= 0 && index < titulosMundos.Length)
            {
                txtMundo.text = titulosMundos[index];
            }
            else
            {
                txtMundo.text = "Mundo " + numeroMundo;
            }
        }

        painelMundos.SetActive(false);
        subMenuFases.SetActive(true);
    }

    public void DefinirFase(int numeroFase)
    {
        faseAtual = numeroFase;
    }

    public void DefinirCena(string nomeCena)
    {
        cenaParaCarregar = nomeCena;
    }

    public void DefinirTagMoeda(string tag)
    {
        tagAlvoDaFase = tag;
    }

    public void AbrirDetalhes(string missao, Sprite icone)
    {
        if (txtDescricao != null)
            txtDescricao.text = missao;

        if (imgIconeMissao != null)
        {
            if (icone != null)
            {
                imgIconeMissao.sprite = icone;
                imgIconeMissao.gameObject.SetActive(true);
            }
            else
            {
                imgIconeMissao.gameObject.SetActive(false);
            }
        }

        painelMissao.SetActive(true);
    }

    public void VoltarAoMapa()
    {
        subMenuFases.SetActive(false);
        painelMissao.SetActive(false);
        painelMundos.SetActive(true);

        VerificarBloqueioMundos();
    }

    public void IniciarFase()
    {
        if (!string.IsNullOrEmpty(cenaParaCarregar))
        {
            SceneManager.LoadScene(cenaParaCarregar);
        }
    }
}