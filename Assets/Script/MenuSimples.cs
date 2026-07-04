using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuSimples : MonoBehaviour
{
    public GameObject painelPausa;
    public GameObject PausaBTN;// Arraste o seu Painel de Pausa aqui
    private bool jogoPausado = false;

    // Função para carregar cenas
    public void CarregarCena(string nomeCena)
    {
        Time.timeScale = 1f; // Garante que o jogo despause ao mudar de cena
        SceneManager.LoadScene(nomeCena);
    }

    // Função para fechar o jogo
    public void SairDoJogo()
    {
        Application.Quit();
    }

    // --- NOVA FUNÇÃO DE PAUSA ---
    public void AlternarPausa()
    {
        jogoPausado = !jogoPausado;

        if (jogoPausado)
        {
            Time.timeScale = 0f; // Congela o tempo do jogo (movimentos, física)
            if (painelPausa != null) painelPausa.SetActive(true);
            PausaBTN.SetActive(false);
        }
        else
        {
            Time.timeScale = 1f; // Volta o tempo ao normal
            if (painelPausa != null) painelPausa.SetActive(false);
            PausaBTN.SetActive(true);
        }
    }

    // Função para o botão "Continuar" dentro do painel
    public void Continuar()
    {
        jogoPausado = false;
        Time.timeScale = 1f;
        if (painelPausa != null) painelPausa.SetActive(false);
        PausaBTN.SetActive(true);
    }
}
