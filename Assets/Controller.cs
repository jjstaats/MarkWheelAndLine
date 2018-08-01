using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class Controller : MonoBehaviour {

    public Image image;
    public Image line;
    public Image dot;

    public Vector2 wheelGuess;

    int index = 0;

    bool lineMode = false;
    List<string> fileNames = new List<string>();

   private void Start()
    {
        FileInfo[] fileInfo = new DirectoryInfo("Assets/Resources").GetFiles("*.jpg", SearchOption.AllDirectories);
        foreach (var info in fileInfo)
        {
            fileNames.Add(info.Name.Substring(0, info.Name.Length-4));
        }
        NextImage();
    }
    
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (lineMode)
            {
                NextImage();
            }
            else
            {
                wheelGuess = CursorControl.GetGlobalCursorPos();
                EnableLine();
            }
        } else if (Input.GetMouseButtonDown(1))
        {
            NextImage();
        }
        else if (Input.GetMouseButtonDown(2))
        {
            ResetMode();
        }
        if (!lineMode)
        {
            dot.transform.position = Input.mousePosition;
        }
        else
        {
            line.GetComponent<RectTransform>().sizeDelta = new Vector2(dot.transform.position.x - Input.mousePosition.x, 100);
        }
    }

    void EnableLine()
    {
        line.gameObject.SetActive(true);
        line.transform.position = Input.mousePosition;
        dot.transform.position = Input.mousePosition;
        lineMode = true;

    }

    void NextImage()
    {
        Texture2D texture = Resources.Load<Texture2D>(fileNames[index]);

        index++;
        var sprite = new Sprite();
        sprite = Sprite.Create(texture, new Rect(0f, 0f, texture.width, texture.height), new Vector2(0.5f, 0.5f), 100f);
        image.sprite = sprite;
        dot.transform.position = wheelGuess;
        CursorControl.SetGlobalCursorPos(wheelGuess);
        lineMode = false;
        line.gameObject.SetActive(false);
    }
    void ResetMode()
    {
        lineMode = false;
        line.gameObject.SetActive(false);
    }
}
