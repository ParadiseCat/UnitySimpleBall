using UnityEngine;

public class ActionObject
{
    const   float pixelPerUnit       = 100f;
    const   float halfCameraUnits    = 5f;
    private float spriteHeigthInUnit = 0f;

    private GameObject obj;

    public ActionObject(string objName, string imgPath, int depth,
        float posX, float posY, float scaleX, float scaleY)
    {
        Sprite resSprite = Resources.Load<Sprite>(imgPath);

        obj = new GameObject(objName);

        obj.AddComponent(typeof(SpriteRenderer));
        obj.GetComponent<SpriteRenderer>().sprite = resSprite;
        obj.GetComponent<SpriteRenderer>().sortingOrder = depth;

        obj.transform.position = new Vector3(posX, posY);
        obj.transform.localScale = new Vector3(scaleX, scaleY);

        spriteHeigthInUnit = obj.GetComponent<SpriteRenderer>().sprite.rect.height 
            * scaleY / pixelPerUnit;
    }

    public float ActionObject_GetSpriteHeightInUnit()
    {
        return spriteHeigthInUnit;
    }

    public void ActionObject_MoveDown(float speed)
    {
        obj.transform.position += new Vector3(0f, speed);

        if(obj.transform.position.y < -(spriteHeigthInUnit / 2f + halfCameraUnits))
        {
            obj.transform.position += new Vector3(0f, spriteHeigthInUnit * 2f);
        }
    }
}
