using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DriftSystem : MonoBehaviour
{
    [Header("Drift Points System")]
    public PrometeoCarController carController; // Reference to the car controller script
    public bool useDriftPoints = true; // Enable/disable drift points system
    public int driftPoints = 0; // Current drift points
    public TextMeshProUGUI driftPointsText; // UI text to display drift points
    public TextMeshProUGUI driftMultiplierText; // UI text to display drift multiplier
    public float pointsMultiplier = 1f; // Base multiplier
    public int driftMultiplier = 1; // Current drift multiplier
    
    public GameObject driftEffectPrefab;

    private float driftTimer = 0f; // Timer to track how long the car has been drifting
    private float noDriftTimer = 0f; // Timer to track how long the car has stopped drifting

    private void Update()
    {
        if (useDriftPoints && carController != null)
        {
            UpdateDriftPoints();
        }
    }

    private void UpdateDriftPoints()
    {
        if (carController.isDrifting)
        {
            // Reset no drift timer
            noDriftTimer = 0f;

            // Increment drift timer
            driftTimer += Time.deltaTime;

            // Add drift points instantly
            driftPoints += driftMultiplier; 
            UpdateDriftPointsUI();

            // Increase multiplier every 2 seconds
            if (driftTimer >= 2f)
            {
                driftMultiplier++;
                GameObject effect = Instantiate(driftEffectPrefab, transform.position, Quaternion.identity);
                driftTimer = 0f; // Reset drift timer
                UpdateDriftMultiplierUI();
            }
        }
        else
        {
            // Increase no-drift timer
            noDriftTimer += Time.deltaTime;

            if (noDriftTimer >= 2f && driftMultiplier > 1)
            {
                driftMultiplier--; // Decrease multiplier every 2 seconds of no drifting
                noDriftTimer = 0f; // Reset no-drift timer
                UpdateDriftMultiplierUI();
            }

            driftTimer = 0f; // Reset drift timer if not drifting
        }
    }

    private void UpdateDriftPointsUI()
    {
        if (driftPointsText != null)
        {
            driftPointsText.text = driftPoints.ToString();
        }
    }

    private void UpdateDriftMultiplierUI()
    {
        if (driftMultiplierText != null)
        {
            driftMultiplierText.text = "x" + driftMultiplier;
        }
    }

    // Method to reset drift points and multiplier
    public void ResetDriftPoints()
    {
        driftPoints = 0;
        driftMultiplier = 1;
        UpdateDriftPointsUI();
        UpdateDriftMultiplierUI();
    }
}
