using UnityEngine;

public class Controller : MonoBehaviour
{
    public static Controller instance;
    public float horizontal;
    public float vertical;

    public bool jumpDown;
    public bool jumpHold;

    public void MajuDown() => horizontal = 1;
    public void MundurDown() => horizontal = -1;
    public void StopMove() => horizontal = 0;

    public void LoncatDown()
    {
        if (Sound.instance != null)
        {
            Sound.instance.PlaySFX(Sound.instance.jump);
        }
        jumpDown = true;
        jumpHold = true;
    }

    public void LoncatUp()
    {
        jumpHold = false;
    }

    void LateUpdate()
    {
        jumpDown = false;
    }
}