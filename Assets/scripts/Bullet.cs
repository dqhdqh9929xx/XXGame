using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float flySpeed;

    // Update is called once per frame
    void Update()
    {
        var newPosition = transform.position;
        newPosition.y += Time.deltaTime * flySpeed;
        transform.position = newPosition;
    }
}

