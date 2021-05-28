using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    const   int   FPS = 60;
    const   float BASE_SPEED = -0.05f;
   
    public  float gameIndexSpeed = 1f;
    private float playerStep     = 1.6f;
    private int   playerPosition = 0;
    private int   gameStep       = 0;

    private GameObject objPlayer;
    private System.Random objRandom = new System.Random();

    private float bombCreateY  = 6f;
    private float bombDestroyY = -6f;
    private int   bombCount    = 0;

    private List<ActionObject> listObjField = new List<ActionObject>();
    private List<BombObject>   listObjBomb  = new List<BombObject>();

    void Start()
    {
        // 1. STATIC
        staticObjCreate ("objBackground",  "grass", 1,    0f, 0f, 5f, 5f);
        staticObjCreate ("objBorderLeft",  "line",  3, -4.1f, 0f, 5f, 5f);
        staticObjCreate ("objBorderRigth", "line",  3,  4.1f, 0f, 5f, 5f);

        // 2. ACTION
        ActionObject objField_1 = 
            new ActionObject ("objField_1", "chess", 2, 0.05f, 0f, 2f, 2f);

        ActionObject objField_2 = 
            new ActionObject ("objField_2", "chess", 2, 0.05f, 
            objField_1.ActionObject_GetSpriteHeightInUnit(), 2f, 2f);

        listObjField.Add(objField_1);
        listObjField.Add(objField_2);

        // THIS
        objPlayer = GameObject.Find("objPlayer");
        objPlayer.GetComponent<SpriteRenderer>().sortingOrder = 4;
        objPlayer.transform.position = new Vector3(0f, -4f);

        // SETTINGS
        Application.targetFrameRate = FPS;
    }

    void Update()
    {
        // 1. set count game step
        gameStep++;


        // 2. move down game field
        foreach (ActionObject obj in listObjField)
        {
            obj.ActionObject_MoveDown(BASE_SPEED * gameIndexSpeed);
        }

        // 3. move down bomb

        for (int i = 0; i < bombCount; i++)
        {
            BombObject obj = listObjBomb[i];

            if (obj.BombObject_MoveDown(BASE_SPEED * gameIndexSpeed) < bombDestroyY)
            {
                obj.BombObject_Destroy();
                listObjBomb.RemoveAt(i);
                bombCount--;
            }
        }

        if (gameStep > (120 / gameIndexSpeed))
        {
            gameStep = 0;
            int index = objRandom.Next(5) + 1;
            float posX = playerStep * (objRandom.Next(-2, 2) + 1);

            BombObject bombt =
                new BombObject("objBomb_1", index, 5, posX, bombCreateY, 4f, 4f);

            bombCount++;
            listObjBomb.Add(bombt);
        }

        if ((Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            && playerPosition > -2)
        {
            playerPosition--;
            objPlayer.transform.position += new Vector3(-(playerStep), 0f);
        }
        else if((Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            && playerPosition < 2)
        {
            playerPosition++;
            objPlayer.transform.position += new Vector3(playerStep, 0f);
        }
    }

    private void staticObjCreate(string name, string imgPath, int depth,
        float posX, float posY, float scaleX, float scaleY)
    {
        Sprite resSprite = Resources.Load<Sprite>(imgPath);

        GameObject obj = new GameObject(name);
        obj.AddComponent(typeof(SpriteRenderer));
        obj.GetComponent<SpriteRenderer>().sprite = resSprite;
        obj.GetComponent<SpriteRenderer>().sortingOrder = depth;

        obj.transform.position = new Vector3(posX, posY);
        obj.transform.localScale = new Vector3(scaleX, scaleY);
    }
}
