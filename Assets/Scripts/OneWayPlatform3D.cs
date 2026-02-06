using UnityEngine;

public class OneWayPlatform3D : MonoBehaviour
{
    private BoxCollider col;

    public float offset = 0.05f;

    void Start()
    {
        col = GetComponent<BoxCollider>();
        col.isTrigger = false;
    }

    void Update()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (!player) return;

        CharacterController cc = player.GetComponent<CharacterController>();
        if (!cc) return;

        float playerBottom = player.transform.position.y - cc.height / 2f;
        float platformTop = col.bounds.max.y;

        // Player ở DƯỚI → cho xuyên
        if (playerBottom < platformTop - offset)
        {
            col.enabled = false;
        }
        // Player ở TRÊN → đứng được
        else
        {
            col.enabled = true;
        }
    }
}