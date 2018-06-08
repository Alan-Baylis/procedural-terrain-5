using UnityEngine;

public class Tile : MonoBehaviour {
    public Transform top;
    public Transform bottom;

    float topHeight;

    void Awake() {
        topHeight = top.GetComponent<Renderer>().bounds.size.y;
    }

    public void UpdateHeight(float height) {
        gameObject.SetActive(height != 0);

        float bottomHeight = height - topHeight;

        Vector3 bottomPosition = bottom.localPosition;
        bottomPosition.y = bottomHeight / 2;
        bottom.localPosition = bottomPosition;

        Vector3 bottomScale = bottom.localScale;
        bottomScale.y = bottomHeight;
        bottom.localScale = bottomScale;

        Vector3 topPosition = top.localPosition;
        topPosition.y = bottomHeight + topHeight / 2;
        top.localPosition = topPosition;
    }
}
