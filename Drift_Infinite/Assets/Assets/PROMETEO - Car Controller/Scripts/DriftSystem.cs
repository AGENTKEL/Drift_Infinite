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

    public GameObject driftEffectPrefab; // Particle effect prefab

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
            noDriftTimer = 0f; // Reset the no-drift timer

            driftTimer += Time.deltaTime;
            driftPoints += driftMultiplier; // Add drift points instantly
            UpdateDriftPointsUI();

            // Dynamically increasing time to gain multipliers
            float requiredDriftTime = 2f + (driftMultiplier - 1); // Takes longer for higher multipliers

            if (driftTimer >= requiredDriftTime)
            {
                driftMultiplier++;
                Instantiate(driftEffectPrefab, transform.position, Quaternion.identity); // Spawn effect
                driftTimer = 0f; // Reset drift timer
                UpdateDriftMultiplierUI();
            }
        }
        else
        {
            driftTimer = 0f; // Reset drift timer if not drifting
            noDriftTimer += Time.deltaTime;

            // Dynamically decreasing time before losing multiplier
            float decayTime = Mathf.Max(2f - ((driftMultiplier - 1) * 0.5f), 0.5f); // Takes less time to lose at high multipliers

            if (noDriftTimer >= decayTime && driftMultiplier > 1)
            {
                driftMultiplier--; // Decrease multiplier
                noDriftTimer = 0f; // Reset no-drift timer
                UpdateDriftMultiplierUI();
            }
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

    public void ResetDriftPoints()
    {
        driftPoints = 0;
        driftMultiplier = 1;
        UpdateDriftPointsUI();
        UpdateDriftMultiplierUI();
    }
}
