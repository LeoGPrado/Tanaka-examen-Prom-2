using UnityEngine;

public class KitchenZone : BaseZone
{
    [Header("Prefabs")]
    [SerializeField] GameObject itemPrefab;
    [SerializeField] GameObject notePrefab;

    [Header("Cantidad fija")]
    [SerializeField] int notasASpawnear = 2;
    [SerializeField] int itemsASpawnear = 1;

    [Header("Altura de spawn")]
    [SerializeField] float alturaNotas = 1.2f;
    [SerializeField] float alturaItems = 0.3f;

    protected override void ExecuteSpawning()
    {
        int[] indices = new int[spawnPoints.Length];
        for (int i = 0; i < indices.Length; i++) indices[i] = i;
        for (int i = indices.Length - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            (indices[i], indices[j]) = (indices[j], indices[i]);
        }

        int cursor = 0;

        for (int i = 0; i < notasASpawnear; i++)
        {
            if (cursor >= spawnPoints.Length) break;
            Transform point = spawnPoints[indices[cursor++]];
            if (notePrefab != null && point != null)
            {
                Vector3 pos = point.position + Vector3.up * alturaNotas;
                Instantiate(notePrefab, pos, point.rotation, point);
            }
        }

        for (int i = 0; i < itemsASpawnear; i++)
        {
            if (cursor >= spawnPoints.Length) break;
            Transform point = spawnPoints[indices[cursor++]];
            if (itemPrefab != null && point != null)
            {
                Vector3 pos = point.position + Vector3.up * alturaItems;
                Instantiate(itemPrefab, pos, point.rotation, point);
            }
        }

    }
}