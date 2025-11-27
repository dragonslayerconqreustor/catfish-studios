
using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;

public enum SpawnMode
{
    InOrder,
    Random
}
public class SideScrollingManager : MonoBehaviour
{
    [Header("Spawn Mode")]
    [SerializeField] private SpawnMode backgroundSpawnMode = SpawnMode.InOrder;
    [SerializeField] private SpawnMode foregroundSpawnMode = SpawnMode.InOrder;

    [Header("Scroll Speeds")]
    [SerializeField] public float backgroundScrollSpeed = 2f;
    [SerializeField] public float foregroundScrollSpeed = 5f;

    [Header("Prefab Arrays")]
    [SerializeField] private GameObject[] backgroundPrefabs;
    [SerializeField] private GameObject[] foregroundPrefabs;

    [Header("Spawn Settings")]
    [SerializeField, Min(0)] private float SizeOfBackgroundPrefabs;
    [SerializeField, Min(0)] private float SizeOfForegroundPrefabs;
    [SerializeField, SerializeAs("Deprecated")] private float backgroundSpawnInterval;
    [SerializeField, SerializeAs("Deprecated")] private float foregroundSpawnInterval;
    [SerializeField] private Vector3 backgroundSpawnPosition = new Vector3(15f, 0f, 10f);
    [SerializeField] private Vector3 foregroundSpawnPosition = new Vector3(15f, 0f, 0f);
    [SerializeField] private float destroyPositionX = -15f;

    private float currentBackgroundSpeed;
    private float currentForegroundSpeed;

    private float backgroundSpawnTimer = 0f;
    private float foregroundSpawnTimer = 0f;

    private int currentBackgroundIndex = 0;
    private int currentForegroundIndex = 0;

    private List<ScrollableElement> activeElements = new List<ScrollableElement>();

    void Start()
    {
        currentBackgroundSpeed = backgroundScrollSpeed;
        currentForegroundSpeed = foregroundScrollSpeed;

        backgroundSpawnInterval = SizeOfBackgroundPrefabs / currentBackgroundSpeed;
        foregroundSpawnInterval = SizeOfForegroundPrefabs / currentForegroundSpeed;


        if (backgroundPrefabs.Length > 0)
            SpawnBackground();
        if (foregroundPrefabs.Length > 0)
            SpawnForeground();
    }

    void Update()
    {
     
        UpdateAllElementSpeeds();

        backgroundSpawnTimer += Time.deltaTime;
        foregroundSpawnTimer += Time.deltaTime;

        if (backgroundSpawnTimer >= backgroundSpawnInterval && backgroundPrefabs.Length > 0)
        {
            SpawnBackground();
            backgroundSpawnTimer = 0f;
        }

        if (foregroundSpawnTimer >= foregroundSpawnInterval && foregroundPrefabs.Length > 0)
        {
            SpawnForeground();
            foregroundSpawnTimer = 0f;
        }

    
        CleanupOffscreenElements();
    }

    private void SpawnBackground()
    {
        GameObject prefab = GetNextPrefab(backgroundPrefabs, ref currentBackgroundIndex, backgroundSpawnMode);
        if (prefab != null)
        {
            GameObject instance = Instantiate(prefab, backgroundSpawnPosition, Quaternion.identity);
            ScrollableElement element = instance.GetComponent<ScrollableElement>();

            if (element == null)
            {
                element = instance.AddComponent<ScrollableElement>();
            }

            element.Initialize(currentBackgroundSpeed, true);
            activeElements.Add(element);
        }
    }

    private void SpawnForeground()
    {
        GameObject prefab = GetNextPrefab(foregroundPrefabs, ref currentForegroundIndex, foregroundSpawnMode);
        if (prefab != null)
        {
            GameObject instance = Instantiate(prefab, foregroundSpawnPosition, Quaternion.identity);
            ScrollableElement element = instance.GetComponent<ScrollableElement>();

            if (element == null)
            {
                element = instance.AddComponent<ScrollableElement>();
            }

            element.Initialize(currentForegroundSpeed, false);
            activeElements.Add(element);
        }
    }

    private GameObject GetNextPrefab(GameObject[] prefabs, ref int currentIndex, SpawnMode mode)
    {
        if (prefabs.Length == 0) return null;

        GameObject selectedPrefab;

        if (mode == SpawnMode.InOrder)
        {
            selectedPrefab = prefabs[currentIndex];
            currentIndex = (currentIndex + 1) % prefabs.Length;
        }
        else 
        {
            selectedPrefab = prefabs[Random.Range(0, prefabs.Length)];
        }

        return selectedPrefab;
    }

    private void UpdateAllElementSpeeds()
    {
        foreach (ScrollableElement element in activeElements)
        {
            if (element != null)
            {
                if (element.IsBackground())
                {
                    element.SetScrollSpeed(currentBackgroundSpeed);
                }
                else
                {
                    element.SetScrollSpeed(currentForegroundSpeed);
                }
            }
        }
    }

    private void CleanupOffscreenElements()
    {
        for (int i = activeElements.Count - 1; i >= 0; i--)
        {
            if (activeElements[i] == null || activeElements[i].transform.position.x < destroyPositionX)
            {
                if (activeElements[i] != null)
                {
                    Destroy(activeElements[i].gameObject);
                }
                activeElements.RemoveAt(i);
            }
        }
    }

    public void SetBackgroundSpawnMode(SpawnMode mode)
    {
        backgroundSpawnMode = mode;
    }

    public void SetForegroundSpawnMode(SpawnMode mode)
    {
        foregroundSpawnMode = mode;
    }

    public void ToggleBackgroundSpawnMode()
    {
        backgroundSpawnMode = (backgroundSpawnMode == SpawnMode.InOrder) ? SpawnMode.Random : SpawnMode.InOrder;
        Debug.Log("Background Spawn Mode: " + backgroundSpawnMode);
    }

    public void ToggleForegroundSpawnMode()
    {
        foregroundSpawnMode = (foregroundSpawnMode == SpawnMode.InOrder) ? SpawnMode.Random : SpawnMode.InOrder;
        Debug.Log("Foreground Spawn Mode: " + foregroundSpawnMode);
    }
}
