
using UnityEngine;

public class ButtonPressAnimation : MonoBehaviour
{
    // Start is called before the first frame update

    public float ScaleAnim;
    void Start()
    {
        var hash = iTween.Hash("scale", Vector3.one* ScaleAnim, "time", 1, "looptype", "pingpong", "EaseType", "easeInBack");
        iTween.ScaleTo(gameObject, hash);
    }
    
}
