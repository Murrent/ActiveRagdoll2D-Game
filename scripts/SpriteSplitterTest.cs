using System.Collections.Generic;
using UnityEngine;

public class SpriteSplitterTest : MonoBehaviour
{
    int spriteWidth, spriteHeight;
    Color32[] fullSprite;
    [SerializeField] public List<Color32[]> chunks = new List<Color32[]>();
    bool[] wasChecked;

    public void UpdateTiles(int[] toUpdate)
    {
        Sprite sprite = GetComponent<SpriteRenderer>().sprite;
        fullSprite = sprite.texture.GetPixels32();

        wasChecked = new bool[fullSprite.Length];

        spriteWidth = sprite.texture.width;
        spriteHeight = sprite.texture.height;

        if (chunks.Count == 0)
        {
            for (int i = 0; i < fullSprite.Length; i++)
            {
                if (fullSprite[i].a != 0 && !wasChecked[i])
                {
                    Debug.Log("chunks: " + i);
                    chunks.Add(new Color32[fullSprite.Length]);
                    FloodFillPixels(new Vector2Int(i, chunks.Count - 1));
                }
            }

            if (chunks.Count > 0)
            {
                PolygonCollider2D[] colliders = GetComponents<PolygonCollider2D>();
                foreach (PolygonCollider2D c in colliders)
                    Destroy(c);

                for (int i = 0; i < chunks.Count; i++)
                {
                    int amount = 0;
                    for (int j = 0; j < chunks[i].Length; j++)
                    {
                        if (chunks[i][j].a != 0)
                            amount++;
                    }

                    Debug.Log("pixels in " + i + ": " + amount);
                    if (amount > 1)
                        CreateNewSpriteObject(i);
                }

                Destroy(gameObject);
            }
        }
    }

    void CreateNewSpriteObject(int chunkIndex)
    {
        GameObject obj = Instantiate(gameObject, transform.position, transform.rotation);

        Texture2D texture_ = new Texture2D(spriteWidth, spriteHeight, TextureFormat.ARGB32, false);
        texture_.filterMode = FilterMode.Point;

        texture_.SetPixels32(chunks[chunkIndex]);
        texture_.Apply();

        SpriteRenderer sr = obj.GetComponent<SpriteRenderer>();
        sr.sprite = Sprite.Create(texture_, sr.sprite.rect, new Vector2(0.5f, 0.5f), sr.sprite.pixelsPerUnit);

        if (!TryGetComponent<Rigidbody2D>(out Rigidbody2D body))
            obj.AddComponent<Rigidbody2D>();
        obj.AddComponent<PolygonCollider2D>();
        Destroy(obj.GetComponent<SpriteSplitterTest>());
        obj.AddComponent<SpriteSplitterTest>();
    }

    Vector2Int IntToVectorPos(int pos)
    {
        return new Vector2Int(pos % spriteWidth, pos / spriteHeight);
    }

    int Vec2ToInt(Vector2Int pos)
    {
        return pos.x + pos.y * spriteHeight;
    }

    void FloodFillPixels(Vector2Int pos)
    {
        if (!(pos.x >= 0 && pos.x < fullSprite.Length) || fullSprite[pos.x].a == 0)
            return;

        int _up = pos.x - spriteWidth, _down = pos.x + spriteWidth, _left = pos.x - 1, _right = pos.x + 1;
        if (_up >= 0 && _up < fullSprite.Length && !wasChecked[_up] && fullSprite[_up].a != 0)
        {
            wasChecked[_up] = true;
            chunks[pos.y][_up] = fullSprite[_up];
            FloodFillPixels(new Vector2Int(_up, pos.y));
        }

        if (_down >= 0 && _down < fullSprite.Length && !wasChecked[_down] && fullSprite[_down].a != 0)
        {
            wasChecked[_down] = true;
            chunks[pos.y][_down] = fullSprite[_down];
            FloodFillPixels(new Vector2Int(_down, pos.y));
        }

        if (_left >= 0 && _left < fullSprite.Length && pos.x % spriteWidth != 0 && !wasChecked[_left] &&
            fullSprite[_left].a != 0)
        {
            wasChecked[_left] = true;
            chunks[pos.y][_left] = fullSprite[_left];
            FloodFillPixels(new Vector2Int(_left, pos.y));
        }

        if (_right >= 0 && _right < fullSprite.Length && pos.x % spriteWidth != spriteWidth - 1 &&
            !wasChecked[_right] && fullSprite[_right].a != 0)
        {
            wasChecked[_right] = true;
            chunks[pos.y][_right] = fullSprite[_right];
            FloodFillPixels(new Vector2Int(_right, pos.y));
        }
    }
}