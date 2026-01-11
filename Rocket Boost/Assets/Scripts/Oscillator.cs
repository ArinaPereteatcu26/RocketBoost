using UnityEngine;

public class Oscillator : MonoBehaviour
{
    [SerializeField] Vector3 movementVector;
    [SerializeField] float speed;
    
    Vector3 startPosisition;
    Vector3 endPosition;
    float movementFactor;



    void Start()
    {
        startPosisition = transform.position;
        endPosition = startPosisition + movementVector;
    }

    void Update()
    {
        movementFactor = Mathf.PingPong(Time.time * speed, 1f);
        transform.position = Vector3.Lerp(startPosisition, endPosition, movementFactor);
    }
}
