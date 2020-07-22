using UnityEngine;

public class PlayerRotate : MonoBehaviour
{
    public float rotateTarget = 0f;
    // Update is called once per frame
    void Update()
    {
        rotateTarget %= 360;

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            rotateTarget -= 90f;
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            rotateTarget += 90f;
        }
    }
    void FixedUpdate()
    {
        transform.rotation = Quaternion.Lerp(transform.rotation,
            Quaternion.Euler(transform.rotation.x, rotateTarget, transform.rotation.z),
            .3f);
    }
}
