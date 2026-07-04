using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{
    [Header("Configurações de Objeto")]
    // Lista de prefabs (pode colocar inimigos, moedas, etc.)
    [SerializeField] private GameObject[] prefabsParaSpawnar;

    [Header("Configurações de Tempo")]
    [SerializeField] private float tempoMinimo = 1f;
    [SerializeField] private float tempoMaximo = 3f;

    void Start()
    {
        if (prefabsParaSpawnar.Length > 0)
        {
            StartCoroutine(SpawnCiclo());
        }
        else
        {
            Debug.LogWarning("Lista de prefabs vazia no Spawner!");
        }
    }

    IEnumerator SpawnCiclo()
    {
        while (true)
        {
            float tempoEspera = Random.Range(tempoMinimo, tempoMaximo);
            yield return new WaitForSeconds(tempoEspera);

            // Escolhe um índice aleatório da lista
            int indiceSorteado = Random.Range(0, prefabsParaSpawnar.Length);
            GameObject objetoEscolhido = prefabsParaSpawnar[indiceSorteado];

            // Instancia o objeto escolhido
            Instantiate(objetoEscolhido, transform.position, Quaternion.identity);
        }
    }
}
