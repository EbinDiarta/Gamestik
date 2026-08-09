using UnityEngine;

public class MiniBoss : MonoBehaviour
{
    public bool miniBossKalah = false;

    public void Kalah()
    {
        miniBossKalah = true;

        Debug.Log("Mini Boss kalah!");
    }
}