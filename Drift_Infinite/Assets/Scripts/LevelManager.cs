using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [Header("Level Prefabs")]
    public List<GameObject> levelPrefabs; // List of level prefabs

    [Header("Skybox Colors")]
    public List<Color> skyboxColors; // List of skybox colors for each level

    private void Start()
    {
        ActivateRandomLevel();
    }

    void ActivateRandomLevel()
    {
        if (levelPrefabs.Count == 0)
        {
            Debug.LogWarning("LevelManager: No level prefabs assigned!");
            return;
        }

        // Disable all levels
        foreach (GameObject level in levelPrefabs)
        {
            level.SetActive(false);
        }

        // Select a random level
        int randomIndex = Random.Range(0, levelPrefabs.Count);
        levelPrefabs[randomIndex].SetActive(true);

        // Change the skybox color
        if (randomIndex < skyboxColors.Count)
        {
            RenderSettings.skybox.SetColor("_Tint", skyboxColors[randomIndex]);
        }
        
        if (RenderSettings.skybox.HasProperty("_Exposure"))
        {
            RenderSettings.skybox.SetFloat("_Exposure", 0.4f);
        }
        else
        {
            Debug.LogWarning("No skybox color assigned for level index: " + randomIndex);
        }

        Debug.Log("Activated Level: " + levelPrefabs[randomIndex].name);
    }
}
