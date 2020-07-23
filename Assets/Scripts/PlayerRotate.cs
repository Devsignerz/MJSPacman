using UnityEngine;

public class PlayerRotate : MonoBehaviour
{
    public float speed = .6f;
    public float rotateTarget = 0f;
    // Update is called once per frame
    void Update()
    {
        rotateTarget %= 360;

        if (Input.GetKeyDown(KeyCode.LeftArrow))
            rotateTarget -= 90f;
        if (Input.GetKeyDown(KeyCode.RightArrow))
            rotateTarget += 90f;
        if (Input.GetKeyDown(KeyCode.DownArrow))
            rotateTarget += 180f;

    }
    void FixedUpdate()
    {
        transform.rotation = Quaternion.Lerp(transform.rotation,
            Quaternion.Euler(transform.rotation.x, rotateTarget, transform.rotation.z),
            speed);
    }
}
