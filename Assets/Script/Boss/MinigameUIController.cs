using UnityEngine;

public class MinigameUIController : MonoBehaviour
{
    private PlayerController playerControllerRef;
    private GameObject sampahWorldObject;

    public void SetupMinigame(PlayerController player, GameObject sampahObj)
    {
        playerControllerRef = player;
        sampahWorldObject = sampahObj;
    }

    public void FinishMinigame()
    {
        if (playerControllerRef != null)
        {
            playerControllerRef.enabled = true;
        }

        if (sampahWorldObject != null)
        {
            Destroy(sampahWorldObject);
        }

        gameObject.SetActive(false);
    }
}