using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Caro : MonoBehaviour
{
    public Button[] caroclick;
    public GameObject[] turnXO;
    public Text WinerText;
    public Sprite[] icon;
    public int xo;
    public int[] khung;
    public int dem;
    public int[,] max = new int[11, 11];
    public bool player = true;

    public Image image;

    // Start is called before the first frame update
    void Start()
    {
        Setup();
    }

    // Update is called once per frame
    void Update()
    {
    }

    // private void Awake()
    // {
    //     for (int i = 0; i < caroclick.Length; i++)
    //     {
    //        caroclick[i] = transform.Find("GroupBtn/Button " + "(" + i + ")").GetComponent<Button>();
    //     }
    // }
    public void Setup()
    {
        xo = 0;
        dem = 0;
        player = true;
        // turnXO[0].SetActive(true);
        // turnXO[1].SetActive(false);
        for (int i = 0; i < caroclick.Length; i++)
        {
            caroclick[i].interactable = true;
            caroclick[i].GetComponent<Image>().sprite = null;
        }

        for (int j = 0; j < caroclick.Length; j++)
        {
            khung[j] = -1;
        }

        for (int h = 0; h < 11; h++)
        {
            for (int k = 1; k < 11; k++)
            {
                max[h, k] = -1;
            }
        }
    }

    public void playervsplayer()
    {
        Application.LoadLevel("vsplayer");
        Setup();
        player = false;
//     WinerText.gameObject.SetActive(false);
    }

    public void quit()
    {
        Application.LoadLevel("Chon");
    }

    public void onclick(int which)
    {
        caroclick[which].image.sprite = icon[xo];
        caroclick[which].interactable = false;
        khung[which] = xo;
        dem++;
        int xpos = (int) (which) / 11;
        int ypos = (int) (which) % 11;
        max[xpos, ypos] = xo;

        if (xo == 0)
        {
            xo = 1;
            turnXO[0].SetActive(false);
            turnXO[1].SetActive(true);
        }
        else
        {
            xo = 0;
            turnXO[0].SetActive(true);
            turnXO[1].SetActive(false);
        }

        Debug.LogError("checkwin(xpos,ypos) = " + checkwin(xpos, ypos));

        if (checkwin(xpos, ypos) == 1)
        {
            Winer(" O Win!!!!!!");
            Debug.Log("hello o ");
        }

        if (checkwin(xpos, ypos) == -1)
        {
            Winer(" X Win!!!!!!");
            Debug.Log("hello x");
        }
    }


    void Winer(string wintext)
    {
        WinerText.gameObject.SetActive(true);
        image.gameObject.SetActive(true);
        WinerText.text = wintext;

        WinerText.text = wintext;

        for (int i = 0; i < caroclick.Length; i++)
        {
            caroclick[i].interactable = false;
            Debug.Log("thắng");
        }
    }

    private int checkwin(int x, int y)
    {
        if ((checkdoc(x, y) == 10) || (checkngang(x, y) == 10) || (checkcheophai(x, y) == 10)||(checkcheotrai(x,y)==10))
        {
            return 1;
        }

        if ((checkdoc(x, y) == -10) || (checkngang(x, y) == -10) || (checkcheophai(x, y) == -10)||(checkcheotrai(x,y)==-10))
        {
            return -1;
        }

        return 0;
    }

    private int checkdoc(int x, int y)
    {
        int countTopO = 0;
        for (int i = x; i >= 0; i--)
        {
            if (max[i, y] == 1)
            {
                countTopO++;
            }
            else
            {
                break;
            }
        }

        int countButtonO = 0;
        for (int i = x + 1; i < 11; i++)
        {
            if (max[i, y] == 1)
            {
                countButtonO++;
            }
            else
            {
                break;
            }
        }

        int countTopX = 0;
        for (int i = x; i >= 0; i--)
        {
            if (max[i, y] == 0)
            {
                countTopX++;
            }
            else
            {
                break;
            }
        }

        int countButtonX = 0;
        for (int i = x + 1; i < 11; i++)
        {
            if (max[i, y] == 0)
            {
                countButtonX++;
            }
            else
            {
                break;
            }
        }

        if (countTopO + countButtonO == 5)
        {
            return -10;
        }

        if (countTopX + countButtonX == 5)
        {
            return 10;
        }

        return 0;
    }

    private int checkngang(int x, int y)
    {
        int countLeftO = 0;
        for (int i = y; i >= 0; i--)
        {
            if (max[x, i] == 1)
            {
                countLeftO++;
            }
            else
            {
                break;
            }
        }

        int countRightO = 0;
        for (int i = y + 1; i < 11; i++)
        {
            if (max[x, i] == 1)
            {
                countRightO++;
            }
            else
            {
                break;
            }
        }

        int countLeftX = 0;
        for (int i = y; i >= 0; i--)
        {
            if (max[x, i] == 0)
            {
                countLeftX++;
            }
            else
            {
                break;
            }
        }

        int countRightX = 0;
        for (int i = y + 1; i < 11; i++)
        {
            if (max[x, i] == 0)
            {
                countRightX++;
            }
            else
            {
                break;
            }
        }

        if (countLeftO + countRightO == 5)
        {
            return -10;
        }

        if (countLeftX + countRightX == 5)
        {
            return 10;
        }

        return 0;
    }

    private int checkcheophai(int x, int y)
    {
        int countTopO = 0;
        int countBottomO = 0;
        int countTopX = 0;
        int countBottomX = 0;
        for (int i = 0; i < 5; i++)
        {
            if (x - i < 0 || y - i < 0)
                break;
            if (max[x - i, y - i] == 1)
            {
                countTopO++;
            }
            else
            {
                break;
            }
        }

        for (int i = 1; i < 5; i++)
        {
            if (x + i > 10 || y + i > 10)
                break;
            if (max[x + i, y + i] == 1)
            {
                countBottomO++;
            }
            else
            {
                break;
            }
        }

        for (int i = 0; i < 5; i++)
        {
            if (x - i < 0 || y - i < 0)
                break;
            if (max[x - i, y - i] == 0)
            {
                countTopX++;
            }
            else
            {
                break;
            }
        }

        for (int i = 1; i < 5; i++)
        {
            if (x + i > 10 || y + i > 10)
                break;
            if (max[x + i, y + i] == 0)
            {
                countBottomX++;
            }
            else
            {
                break;
            }
        }

        if (countTopO + countBottomO == 5)
        {
            return -10;
        }

        if (countTopX + countBottomX == 5)
        {
            return 10;
        }

        return 0;
    }
    private int checkcheotrai(int x, int y)
    {
        int countTopO = 0;
        int countButtonO = 0;
        int countTopX = 0;
        int countButtonX = 0;
    
        for (int i = 0; i < 5; i++)
        {
            if (x + i > 10 || y - i < 0)
                break;
            if (max[x + i, y - i] == 1)
            {
                countTopO++;
            }
            else
            {
                break;
            }
        }
        for (int i = 1; i < 5; i++)
        {
            if (x -i < 0 || y + i > 10)
                break;
            
            if (max[x-i, y+i] == 1)
            {
                countButtonO++;
    
            }
            else
            {
                break;
            }
        }
        for (int i = 0; i < 5; i++)
        {
            if (x + i > 10 || y - i < 0)
                break;
            if (max[x + i, y - i] == 0)
            {
                countTopX++;
    
            }
            else
            {
                break;
            }
        }
        for (int i = 1; i < 5; i++)
        {
            if (x - i < 0 || y + i > 10)
                break;
            if (max[x - i, y + i] == 0)
            {
                countButtonX++;
    
            }
            else
            {
                break;
            }
        }
        if (countTopO + countButtonO == 5)
        {
            return -10;
        }
        if (countTopX + countButtonX == 5)
        {
            return 10;
        }
        return 0;
    }

    
}