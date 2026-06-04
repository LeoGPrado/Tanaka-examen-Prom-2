using System.Collections.Generic;
using UnityEngine;

public class ProceduralPuzzleAssigner : MonoBehaviour
{
    public static ProceduralPuzzleAssigner Instance;

    [Header("Puzzle A - Cocina")]
    public Transform[] spotsNoteA;
    public Transform[] spotsKeyA;
    public GameObject codeNote;
    public GameObject key;

    [Header("Puzzle B - Fusibles")]
    public Transform[] spotsNoteB;
    public Transform[] spotsDrawingB;
    public GameObject fuseNotes;
    public GameObject drawing;

    [Header("Puzzle C - Caja Fuerte")]
    public Transform[] spotsNoteC;
    public Transform[] spotsDrawingC;
    public GameObject noteSolution;
    public GameObject drawingC;

    public string seed;
    private List<GameObject> spawneds = new List<GameObject>();


    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        NewGame();
    }

    public void NewGame()
    {
        DeleteAll();
        seed = "";

        string Avar = GeneratePuzzleA();
        string Bvar = GeneratePuzzleB();
        string Cvar = GeneratePuzzleC();

        seed = Avar + Bvar + Cvar;

        PlayerPrefs.SetString("Seed", seed);
        Debug.Log("Seed: " + seed);
    }

    string GeneratePuzzleA()
    {

        float randomA = Random.value;
        Debug.Log("RandomA :" + randomA);

        if (randomA > 0.5f)
        {
            Spawn(spotsNoteA, codeNote);
            return "A1";
        }
        else
        {
            Spawn(spotsKeyA, key);
            return "A2";
        }
        
    }

    string GeneratePuzzleB()
    {
        float randomB = Random.value;
        Debug.Log("RandomB :" + randomB);

        if (randomB > 0.5f)
        {
            return "B1";
        }
        else
        {
            float randomB2 = Random.value;
            Debug.Log("RandomB2 :" + randomB2);

            if (randomB2 > 0.5f)
                Spawn(spotsNoteB, fuseNotes);
            else
                Spawn(spotsDrawingB, drawing);

            return "B2";
        }
    }

    string GeneratePuzzleC()
    {
        float randomC = Random.value;
        Debug.Log("RandomC: " + randomC);

        if (randomC > 0.5f)
        {
            float randomC2 = Random.value;
            Debug.Log("Random C2: " + randomC2);

            if (Random.value > 0.5f)
                Spawn(spotsNoteC, noteSolution);
            else
                Spawn(spotsDrawingC, drawingC);

            return "C1";
        }
        else
        {
            return "C2";
        }
    }

    void Spawn(Transform[] spots, GameObject prefab)
    {
        if (spots.Length == 0 || prefab == null) return;

        List<Transform> activeSpots = new List<Transform>();
        for (int i1 = 0; i1 < spots.Length; i1++)
        {
            if (spots[i1] != null)
            {
                activeSpots.Add(spots[i1]);
            }
        }

        int i = Random.Range(0, activeSpots.Count);
        Transform chosenSpot = activeSpots[i];

        GameObject obj = Instantiate(prefab, chosenSpot.position, chosenSpot.rotation);
        spawneds.Add(obj);

        for (int j = 0; j < spots.Length; j++)
            spots[j].gameObject.SetActive(false);
    }


    void DeleteAll()
    {
        for (int i = 0; i < spawneds.Count; i++)
            if (spawneds[i] != null) Destroy(spawneds[i]);
        spawneds.Clear();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
