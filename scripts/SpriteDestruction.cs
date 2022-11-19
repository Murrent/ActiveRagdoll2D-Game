using System.Collections.Generic;
using UnityEngine;

public class SpriteDestruction : MonoBehaviour
{
    public void DamageSprite(int radius, Vector2 position)
    {
        Vector2 localHit = transform.InverseTransformPoint(position);
        SpriteRenderer renderer = GetComponent<SpriteRenderer>();
        Texture2D tex = renderer.sprite.texture;

        Vector2Int hitPixel = new Vector2Int(
            Mathf.RoundToInt((localHit.x + (tex.width / renderer.sprite.pixelsPerUnit) / 2) *
                             renderer.sprite.pixelsPerUnit),
            Mathf.RoundToInt((localHit.y + (tex.height / renderer.sprite.pixelsPerUnit) / 2) *
                             renderer.sprite.pixelsPerUnit));
        Debug.Log(hitPixel);
        //Replace texture
        Texture2D newTex = new Texture2D(tex.width, tex.height, TextureFormat.ARGB32, false);
        newTex.filterMode = tex.filterMode;
        Color32[] newPixels = tex.GetPixels32();
        List<int> outLine = new List<int>();
        for (int x = hitPixel.x - radius; x < hitPixel.x + radius; x++)
        {
            if (x >= 0 && x <= tex.width)
            {
                float diffX = x - hitPixel.x;
                for (int y = hitPixel.y - radius; y < hitPixel.y + radius; y++)
                {
                    if (y >= 0 && y <= tex.height)
                    {
                        float diffY = y - hitPixel.y;
                        float distance = Mathf.Sqrt(diffX * diffX + diffY * diffY);
                        if (distance < radius && x + y * tex.width >= 0 && x + y * tex.width < newPixels.Length)
                            newPixels[x + y * tex.width].a = 0;
                        if (distance == radius && x + y * tex.width >= 0 && x + y * tex.width < newPixels.Length)
                        {
                            outLine.Add(x + y * tex.width);
                        }
                    }
                }
            }
        }

        newTex.SetPixels32(newPixels);
        newTex.Apply();
        renderer.sprite = Sprite.Create(newTex, renderer.sprite.rect, new Vector2(0.5f, 0.5f),
            renderer.sprite.pixelsPerUnit);

        GetComponent<SpriteSplitterTest>().UpdateTiles(outLine.ToArray());
        //PROBLEM!!! if there are few pixels left, the polygon collider will create a bad collider. 
        //Also if initial shape is simple, the polygon collider refuse to make itself more complex
    }
}