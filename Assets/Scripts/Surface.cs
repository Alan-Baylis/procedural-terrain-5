using System.Collections.Generic;
using UnityEngine;

public class Surface : MonoBehaviour {
    public Transform placeholder;
    public GameObject tilePrefab;

    public Vector2 placeholderSize = new Vector2(100, 100);
    Vector2 _placeholderSize;

    [Range(1, 20)]
    public int tileSize = 10;
    int _tileSize;

    [Range(1, 10)]
    public int maxTileHeight = 3;
    int _maxTileHeight;

    [Range(1, 10)]
    public int maxSteps = 3;
    int _maxSteps;

    float placeholderBorder = 5;
    Transform tileParent;
    List<Tile> tiles;

    void Awake() {
        placeholder.gameObject.SetActive(true);
        UpdatePlaceholder();

        _placeholderSize = placeholderSize;
        _tileSize = tileSize;
        _maxTileHeight = maxTileHeight;
        _maxSteps = maxSteps;
    }

    void Update() {
        if (placeholderSize != _placeholderSize) {
            UpdatePlaceholder();
            _placeholderSize = placeholderSize;
        }

        if (tileSize != _tileSize) {
            GenerateTiles();
            _tileSize = tileSize;
        }

        if (maxTileHeight != _maxTileHeight) {
            ResizeTiles();
            _maxTileHeight = maxTileHeight;
        }

        if (maxSteps != _maxSteps) {
            ResizeTiles();
            _maxSteps = maxSteps;
        }
    }

    void UpdatePlaceholder() {
        placeholder.localScale = new Vector3(
            placeholderSize.x + placeholderBorder,
            placeholder.localScale.y,
            placeholderSize.y + placeholderBorder);

        GenerateTiles();
    }

    void GenerateTiles() {
        if (tileParent != null) {
            Destroy(tileParent.gameObject);
        }

        tileParent = (new GameObject("Tiles")).transform;
        tileParent.parent = transform;
        tiles = new List<Tile>();

        float xPadding = (placeholderSize.x % tileSize) / 2.0f;
        float zPadding = (placeholderSize.y % tileSize) / 2.0f;

        for (int x = 0; x + tileSize <= placeholderSize.x; x += tileSize) {
            float xPos = x + xPadding + tileSize / 2.0f - placeholderSize.x / 2.0f;

            for (int z = 0; z + tileSize <= placeholderSize.y; z += tileSize) {
                GameObject tile = Instantiate(tilePrefab);
                float zPos = z + zPadding + tileSize / 2.0f - placeholderSize.y / 2.0f;
                tile.transform.position = new Vector3(xPos, 0, zPos);
                tile.transform.localScale = Vector3.one * tileSize;
                tile.transform.parent = tileParent;
                tiles.Add(tile.GetComponent<Tile>());
            }
        }

        ResizeTiles();
    }

    public void ResizeTiles() {
        float stepSize = maxTileHeight / (float)maxSteps;

        foreach (Tile tile in tiles) {
            float tileHeight = Random.Range(1, maxSteps + 1) * stepSize;
            tile.UpdateHeight(tileHeight);
        }
    }
}
