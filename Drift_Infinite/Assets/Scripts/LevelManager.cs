using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [Header("Level Prefabs")]
    public List<GameObject> levelPrefabs; // List of level prefabs

    [Header("Skybox Colors")]
    public List<Color> skyboxColors; // List of skybox colors for each level

    private int selectedLevelIndex = -1; // Stores selected level index

    public Animator animator; // Reference to animator

    private void Start()
    {
        SelectRandomLevel();
    }

    public void SelectRandomLevel()
    {
        if (levelPrefabs.Count == 0)
        {
            Debug.LogWarning("LevelManager: No level prefabs assigned!");
            return;
        }

        // Select a random level but do not activate it yet
        selectedLevelIndex = Random.Range(0, levelPrefabs.Count);

        // Set the correct animation bool based on selected level
        if (animator != null)
        {
            animator.SetBool("Level1", selectedLevelIndex == 0);
            animator.SetBool("Level2", selectedLevelIndex == 1);
            animator.SetBool("Level3", selectedLevelIndex == 2);
        }
        else
        {
            Debug.LogWarning("LevelManager: Animator not assigned!");
        }

        Debug.Log("Selected Level: " + levelPrefabs[selectedLevelIndex].name);
    }

    public void ActivateLevel()
    {
        if (selectedLevelIndex == -1) return; // No level selected

        // Disable all levels
        foreach (GameObject level in levelPrefabs)
        {
            level.SetActive(false);
        }

        // Activate selected level
        levelPrefabs[selectedLevelIndex].SetActive(true);

        // Change the skybox color
        if (selectedLevelIndex < skyboxColors.Count)
        {
            RenderSettings.skybox.SetColor("_Tint", skyboxColors[selectedLevelIndex]);
        }

        if (RenderSettings.skybox.HasProperty("_Exposure"))
        {
            RenderSettings.skybox.SetFloat("_Exposure", 0.5f);
        }
        else
        {
            Debug.LogWarning("No skybox color assigned for level index: " + selectedLevelIndex);
        }

        Debug.Log("Activated Level: " + levelPrefabs[selectedLevelIndex].name);
    }
}
