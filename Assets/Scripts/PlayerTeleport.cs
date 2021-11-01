
using UnityEngine;

public class PlayerTeleport : MonoBehaviour
{
    public Transform[] PointsToTeleport;
    // Start is called before the first frame update
    void Start()
    {
        TeleporTo(0);
    }

    public void TeleporTo(int index)
    {
        transform.position =  PointsToTeleport[index].position;
    }
}
