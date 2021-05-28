using UnityEngine;

public class BombObject
{
    private GameObject obj;

    public BombObject(string objName, int imgIndex, int depth,
        float posX, float posY, float scaleX, float scaleY)
    {
        Sprite resSprite = Resources.Load<Sprite>(BombObject_GetImagePath(imgIndex));

        obj = new GameObject(objName);

        obj.AddComponent(typeof(SpriteRenderer));
        obj.GetComponent<SpriteRenderer>().sprite = resSprite;
        obj.GetComponent<SpriteRenderer>().sortingOrder = depth;

        obj.transform.position = new Vector3(posX, posY);
        obj.transform.localScale = new Vector3(scaleX, scaleY);
    }

    public float BombObject_MoveDown (float speed)
    {
        obj.transform.position += new Vector3(0f, speed);
        return obj.transform.position.y;
    }

    public void BombObject_Destroy()
    {
        Object.Destroy(obj);
    }

    private string BombObject_GetImagePath (int index)
    {
        switch (index)
        {
            case 1: return "SpriteBomb_Blue";
            case 2: return "SpriteBomb_Green";
            case 3: return "SpriteBomb_Magenta";
            case 4: return "SpriteBomb_Orange";
            case 5: return "SpriteBomb_Red";
            default: return "";
        }
    }

}
