using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;
using Random = UnityEngine.Random;


public class vscomputer : MonoBehaviour
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
    public bool computer = false;

    public Image image;

    // Start is called before the first frame update
    void Start()
    {
        Setup();
    }

    // Update is called once per frame
    void Update()
    {
        if (computer == true)
        {
            ComputerAI();
        }
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

        // Debug.LogError("checkwin(xpos,ypos) = " + checkwin(xpos, ypos));
        if (checkwin(xpos, ypos) == 1)
        {
            Winer("Computer Win!!!!!!");
            Debug.Log("hello");
        }

        if (checkwin(xpos, ypos) == -1)
        {
            Winer("Player Win!!!!!!");

            Debug.Log("hello");
        }
    }

    public void playervscom()
    {
        Application.LoadLevel("computer");
        Setup();
        computer = false;
        WinerText.gameObject.SetActive(false);
        image.gameObject.SetActive(false);
    }

    public void quit()
    {
        Application.LoadLevel("Chon");
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
            Debug.Log("X thắng");
        }
    }

    private int checkwin(int x, int y)
    {
        if ((checkdoc(x, y) == 10) || (checkngang(x, y) == 10) || (checkcheophai(x, y) == 10) ||
            (checkcheotrai(x, y) == 10))
        {
            Debug.Log("X thắng");
            return 1;
        }

        if ((checkdoc(x, y) == -10) || (checkngang(x, y) == -10) || (checkcheophai(x, y) == -10) ||
            (checkcheotrai(x, y) == -10))
        {
            Debug.Log("O thắng");
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
            if (x - i < 0 || y + i > 10)
                break;

            if (max[x - i, y + i] == 1)
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


    /// <summary>
    /// /Computer AI
    /// </summary>
    public void ComputerAI()
    {
        int Diemmax = 0;
        int Diemtancong = 0;
        int Diemphongthu = 0;
        int Buocdi = 0;
        if (xo == 0 && dem == 0)
        {
            onclick(100);
        }

        if (xo == 0)
        {
            for (int i = 0; i < 11; i++)
            {
                for (int j = 0; j < 11; j++)
                {
                    if (max[i, j] == -1)
                    {
                        int AI = 0;
                        Diemtancong = tcdoc(i, j) + tcngang(i, j)+tccheophai(i,j)+tccheotrai(i,j);
                        Diemphongthu = ptdoc(i, j) + ptngang(i, j+pncheophai(i,j)+pncheotrai(i,j));
                        if (Diemphongthu > Diemtancong)
                        {
                            AI = Diemphongthu;
                        }
                        else
                        {
                            AI = Diemtancong;
                        }

                        if (Diemmax < AI)
                        {
                            Diemmax = AI;
                            Buocdi = i * 11 + j;
                        }

                        Debug.LogError("" + Diemtancong + "" + Buocdi);
                    }
                }
            }

            onclick(Buocdi);
        }
    }

    ///AI
    /// AI tấn công 
    private int tcngang(int i, int j)
    {
        int Diemtancongngang = 0;
        int Soquanta = 0;
        int Soquandichtrai = 0;
        int Soquandichphai = 0;
        int khoangcach = 0;
        for (int k = 1; k <= 4 && j < 11 - 5; k++)
        {
            if (max[i, j + k] == 0)
            {
                if (k == 1)
                    Soquanta++;
                khoangcach++;
            }
            else if (max[i, j + k] == 1)
            {
                Soquandichtrai++;
                khoangcach++;
                break;
            }
            else
                khoangcach++;
        }

        for (int k = 1; k < 4 && j > 4; k++)
        {
            if (max[i, j - k] == 0)
            {
                if (k == 1)
                    Soquanta++;
                khoangcach++;
            }
            else if (max[i, j - k] == 1)
            {
                Soquandichphai++;

                break;
            }
            else
                khoangcach++;

            break;
        }

        if (Soquandichtrai > 0 && Soquandichphai > 0 && khoangcach < 4)
            return 0;
        Diemtancongngang -= Soquandichtrai + Soquandichphai;
        Diemtancongngang += Soquanta;
        return Diemtancongngang;
    }

    private int tcdoc(int i, int j)
    {
        int Diemtancongdoc = 0;
        int Soquandichtren = 0;
        int Soquandichduoi = 0;
        int Soquanta = 0;
        int Khoangcach = 0;
        for (int k = 0; k < 4 && i < 11 - 5; k++)
        {
            if (max[i + k, j] == 0)
            {
                if (k == 1)
                    Soquanta++;
                Khoangcach++;
            }
            else if (max[i + k, j] == 1)
            {
                Soquandichtren++;
                break;
            }
            else
                Khoangcach++;
        }

        for (int k = 0; k <= 4 && i > 4; k++)
        {
            if (max[i - k, j] == 0)
            {
                if (k == 1)
                    Soquanta++;
                Khoangcach++;
            }
            else if (max[i - k, j] == 1)
            {
                {
                    Soquandichduoi++;
                    break;
                }
            }
            else
                Khoangcach++;
        }

        if (Soquandichtren > 0 && Soquandichduoi > 0 && Khoangcach < 4)
            return 0;
        Diemtancongdoc -= Soquandichduoi + Soquandichtren;
        Diemtancongdoc += Soquanta;
        return Diemtancongdoc;
    }

    private int tccheotrai(int i, int j)
    {
        int Diemtancongcheotrai = 0;
        int Soquanta = 0;
        int Soquandichtren = 0;
        int Soquandichduoi = 0;
        int KhoangCach = 0;
        for (int k = 1; k < 4 && i < 11 - 5 && j > 4; k++)
        {
            if (max[i + 1, j - 1] == 0)
            {
                if (k == 1)
                {
                    Soquanta++;
                    KhoangCach++;
                }

                else if (max[i + 1, j - 1] == 1)
                {
                    Soquandichtren++;
                    break;
                }
            }
            else
                KhoangCach++;

        }
        for (int k = 1; k < 4 && i >4 && j<11-5; k++)
        {
            if (max[i - 1, j + 1] == 0)
            {
                if (k == 1)
                {
                    Soquanta++;
                    KhoangCach++;
                }

                else if (max[i - 1, j + 1] == 1)
                {
                    Soquandichduoi++;
                    break;
                }
            }
            else
                KhoangCach++;

        }
        //bị chặn 2 đầu khoảng cánh ko đủ để tạo thành 5 nước 
        if (Soquandichduoi > 0 && Soquandichtren > 0 && KhoangCach < 4) 
            return 0;
        Diemtancongcheotrai -= Soquandichduoi + Soquandichtren;
        Diemtancongcheotrai += Soquanta;
        return Diemtancongcheotrai;
    }

    private int tccheophai(int i, int j)
    {
        int Diemtancongcheotrai = 0;
        int Soquanta = 0;
        int Soquandichtren = 0;
        int Soquandichduoi = 0;
        int KhoangCach = 0;
        for (int k = 1; k < 4 && i < 11 - 5 && j < 11 - 5; k++)
        {
            if (max[i + 1, j + 1] == 0)
            {
                if (k == 1)
                {
                    Soquanta++;
                    KhoangCach++;
                }

                else if (max[i + 1, j + 1] == 1)
                {
                    Soquandichtren++;
                    break;
                }
            }
            else
                KhoangCach++;

        }
        for (int k = 1; k < 4 && i >4 && j >4; k++)
        {
            if (max[i - 1, j - 1] == 0)
            {
                if (k == 1)
                {
                    Soquanta++;
                    KhoangCach++;
                }

                else if (max[i - 1, j - 1] == 1)
                {
                    Soquandichduoi++;
                    break;
                }
            }
            else
                KhoangCach++;

        }
          //bị chặn 2 đầu khoảng cánh ko đủ để tạo thành 5 nước 
        if (Soquandichduoi > 0 && Soquandichtren > 0 && KhoangCach < 4) 
        return 0;
        Diemtancongcheotrai -= Soquandichduoi + Soquandichtren;
        Diemtancongcheotrai += Soquanta;
        return Diemtancongcheotrai;
    }

    ///AI phòng thủ
    private int ptngang(int i, int j)
    {
        int DiemPhongThu = 0;
        int SoQuanTaTrai = 0;
        int SoQuanTaPhai = 0;
        int SoQuanDich = 0;
        int KhoangCachPhai = 0;
        int KhoangCachTrai = 0;
        bool ok = false;

        for (int k = 1; k <= 4 && j < 11 - 5; k++)
        {
            if (max[i, j + k] == 1)
            {
                if (k == 1)

                    SoQuanDich++;
            }
            else if (max[i, j + k] == 0)
            {
                if (k == 4)

                    SoQuanTaTrai++;
                break;
            }
            else
            {
                if (k == 1)
                    ok = true;

                KhoangCachPhai++;
            }
        }

        if (SoQuanDich == 3 && KhoangCachPhai == 1 && ok)

            ok = false;

        for (int k = 1; k <= 4 && j > 4; k++)
        {
            if (max[i, j - k] == 1)
            {
                if (k == 1)

                    SoQuanDich++;
            }
            else if (max[i, j - k] == 0)
            {
                if (k == 4)

                    SoQuanTaPhai++;
                break;
            }
            else
            {
                if (k == 1)
                    ok = true;

                KhoangCachTrai++;
            }
        }

        if (SoQuanDich == 3 && KhoangCachTrai == 1 && ok)

            if (SoQuanTaPhai > 0 && SoQuanTaTrai > 0 && (KhoangCachTrai + KhoangCachPhai + SoQuanDich) < 4)
                return 0;

        DiemPhongThu -= SoQuanTaPhai + SoQuanTaPhai;
        DiemPhongThu += SoQuanDich;

        return DiemPhongThu;
    }

    private int ptdoc(int i, int j)
    {
        int DiemPhongThu = 0;
        int SoQuanTaTrai = 0;
        int SoQuanTaPhai = 0;
        int SoQuanDich = 0;
        int KhoangCachTren = 0;
        int KhoangCạchDuoi = 0;
        bool ok = false;
        for (int dem = 1; dem <= 4 && i > 4; dem++)
        {
            if (max[i - dem, j] == 1)
            {
                if (dem == 1)
                    SoQuanDich++;
            }
            else if (max[i - dem, j] == 0)
            {
                if (dem == 4)

                    SoQuanTaPhai++;
                break;
            }
            else
            {
                if (dem == 1)
                    ok = true;

                KhoangCachTren++;
            }
        }

        if (SoQuanDich == 3 && KhoangCachTren == 1 && ok)
            ok = false;
        for (int dem = 1; dem <= 4 && i < 11 - 5; dem++)
        {
            if (max[i + dem, j] == 1)
            {
                if (dem == 1)

                    SoQuanDich++;
            }
            else if (max[i + dem, j] == 0)
            {
                if (dem == 4)

                    SoQuanTaTrai++;
                break;
            }
            else
            {
                if (dem == 1)
                    ok = true;

                KhoangCạchDuoi++;
            }
        }

        if (SoQuanDich == 3 && KhoangCạchDuoi == 1 && ok)

            if (SoQuanTaPhai > 0 && SoQuanTaTrai > 0 && (KhoangCachTren + KhoangCạchDuoi + SoQuanDich) < 4)
                return 0;

        DiemPhongThu -= SoQuanTaTrai + SoQuanTaPhai;
        DiemPhongThu += SoQuanDich;
        return DiemPhongThu;
    }
    
       private int  pncheophai(int i, int j)
    {
        
        int DiemPhongNgu = 0;
        int SoQuanTaTrai = 0;
        int SoQuanTaPhai = 0;
        int SoQuanDich = 0;
        int KhoangCachTren = 0;
        int KhoangCachDuoi = 0;
        bool ok = false;

        for (int k = 1; k <= 4 && i < 11 - 5 && j < 11 - 5; k++)
        {
            if (max[i + k, j + k] == 1)
            {
                if (k == 1)

                SoQuanDich++;
            }
            else
                if (max[i + k, j + k] == 0)
                {
                    if (k == 4)

                    SoQuanTaPhai++;
                    break;
                }
                else
                {
                    if (k == 1)
                        ok = true;

                    KhoangCachTren++;
                }
        }

        if (SoQuanDich == 3 && KhoangCachTren == 1 && ok)

        ok = false;
        for (int k = 1; k <= 4 && i > 4 && j > 4; k++)
        {
            if (max[i - k, j - k] == 1)
            {
                if (k == 1)

                SoQuanDich++;
            }
            else
                if (max[i - k, j - k] == 0)
                {
                    if (k == 4)

                    SoQuanTaTrai++;
                    break;
                }
                else
                {
                    if (dem == 1)
                        ok = true;

                    KhoangCachDuoi++;
                }
        }

        if (SoQuanDich == 3 && KhoangCachDuoi == 1 && ok)
            DiemPhongNgu -= 200;

        if (SoQuanTaPhai > 0 && SoQuanTaTrai > 0 && (KhoangCachTren + KhoangCachDuoi + SoQuanDich) < 4)
            return 0;

        DiemPhongNgu -= SoQuanTaPhai + SoQuanTaTrai;
        DiemPhongNgu += SoQuanDich;

        return DiemPhongNgu;

    }
    private int  pncheotrai(int i, int j)
    {
        int DiemPhongNgu = 0;
        int SoQuanTaTrai = 0;
        int SoQuanTaPhai = 0;
        int SoQuanDich = 0;
        int KhoangCachTren = 0;
        int KhoangCachDuoi = 0;
        bool ok = false;

        //lên
        for (int k = 1; k <= 4 && i > 4 && j < 11 - 5; k++)
        {

            if (max[i - k, j + k] == 1)
            {
                if (k == 1)

                SoQuanDich++;
            }
            else
                if (max[i - k, j + k] == 0)
                {
                    if (k == 4)

                    SoQuanTaPhai++;
                    break;
                }
                else
                {
                    if (dem == 1)
                        ok = true;

                    KhoangCachTren++;
                }
        }
            

        if (SoQuanDich == 3 && KhoangCachTren == 1 && ok)
            DiemPhongNgu -= 200;

        ok = false;
        for (int dem = 1; dem <= 4 && i < 11 - 5 && j > 4; dem++)
        {
            if (max[i + dem, j - dem] == 1)
            {
                if (dem == 1)

                SoQuanDich++;
            }
            else
                if (max[i + dem, j - dem] == 0)
                {
                    if (dem == 4)

                    SoQuanTaTrai++;
                    break;
                }
                else
                {
                    if (dem == 1)
                        ok = true;

                    KhoangCachDuoi++;
                }
        }

        if (SoQuanDich == 3 && KhoangCachDuoi == 1 && ok)

        if (SoQuanTaPhai > 0 && SoQuanTaTrai > 0 && (KhoangCachTren + KhoangCachTren + SoQuanDich) < 4)
            return 0;

        DiemPhongNgu -= SoQuanTaTrai + SoQuanTaPhai;
        DiemPhongNgu += SoQuanDich;

        return DiemPhongNgu;
    }
}