using UnityEngine;
using UnityEngine.UI;

public class ConfiguradorFase : MonoBehaviour
{
    [Header("Configurações da Fase")]
    public int numeroDaFase;
    public string nomeDaCena;
    public string tagDaMoeda;

    [Header("Visual da Missão")]
    public Sprite iconeDoItem; // Arraste aqui a Moeda, Estrela, Chave, etc.

    [TextArea(3, 10)]
    public string descricaoMissao;

    // Função que deve ser vinculada ao "On Click()" do botão no Inspector
    public void EnviarDadosParaOMenu()
    {
        // Busca o controlador do menu na cena
        MenuController menu = FindFirstObjectByType<MenuController>();

        if (menu != null)
        {
            menu.DefinirFase(numeroDaFase);
            menu.DefinirCena(nomeDaCena);
            menu.DefinirTagMoeda(tagDaMoeda);

            // Envia o texto e o ícone específicos desta fase para o painel
            menu.AbrirDetalhes(descricaoMissao, iconeDoItem);
        }
        else
        {
            Debug.LogError("MenuController não encontrado na cena!");
        }
    }
}
