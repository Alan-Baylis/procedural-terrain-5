using UnityEngine;

public class Tile : MonoBehaviour {
    public GameObject top;
    public GameObject bottom;

    Vector3 topSize;
    Vector3 bottomSize;
    Vector3 startingBottomScale;

    void Awake() {
        topSize = top.GetComponentInChildren<Renderer>().bounds.size;
        bottomSize = bottom.GetComponentInChildren<Renderer>().bounds.size;
        startingBottomScale = bottom.transform.localScale;    
    }

    public void UpdateHeight(float height) {
        Vector3 newPosition = transform.position;
        newPosition.y = height;
        transform.position = newPosition;

        //Vector3 newBottomScale = bottom.transform.localScale;
        //newBottomScale.y = startingBottomScale.y * (height - bottomSize.y);
        //bottom.transform.localScale = newBottomScale;
    }
}
