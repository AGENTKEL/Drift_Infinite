using UnityEngine;

public class ScrollAnimator : MonoBehaviour
{
    public LevelManager levelManager;
    public GameObject canvas;
    public GameObject stopButton;

    public void TriggerLevelActivation()
    {
        if (levelManager != null)
        {
            levelManager.ActivateLevel();
            canvas.SetActive(false);
        }
        else
        {
            Debug.LogWarning("LevelAnimationHandler: LevelManager not assigned!");
        }

        // Disable this object after triggering the level activation
    }

    public void DeactivateStopButton()
    {
        stopButton.SetActive(false);
    }
    
    public void StopQueue()
    {
        canvas.SetActive(false);
    }
}
