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

public class computer : MonoBehaviour
{
    public int TurnCount;
    public int whoturn;
    public GameObject[] turnIcons;
    public Sprite[] playIcon;
    public Button[] Caroclick;
    public int[] markedSpaces;
    int[,] matrix = new int[11,11];
    public Text WinerText;
    public float delay;
    public int value;
    public bool player = true;
    public bool com = false;
    void Start()
    {
        GameSetup();
    }
    void GameSetup()
    {
        
        whoturn = 0;
        TurnCount = 0;
        player = true;
        com = false;
//        turnIcons[0].SetActive(true);
       // turnIcons[1].SetActive(false);
        for (int i = 0; i < Caroclick.Length; i++)
        {
            
            Caroclick[i].interactable = true;
            Caroclick[i].GetComponent<Image>().sprite = null;           
           
        }
        for(int j = 0; j < Caroclick.Length ; j++)
        {
            markedSpaces[j] = -1;
            
        }
        for (int h = 0; h < 11; h++)
        {
            for (int k = 1; k < 11; k++)
            {
                matrix[h,k] = -1;
            }
        }
    }
    
    void Update()
    {  
         if(com == true)
        { 
            computerplay();
        }   
    }
    void Winer(string wintext){

        WinerText.gameObject.SetActive(true);
      
        WinerText.text = wintext;
       
        WinerText.text = wintext;
       
        for(int i = 0; i<Caroclick.Length;i++)
        {
            Caroclick[i].interactable = false;
        }
    }
    public void carobutton(int WhichNumber)
    {
        Caroclick[WhichNumber].image.sprite = playIcon[whoturn];
        Caroclick[WhichNumber].interactable = false;
        markedSpaces[WhichNumber] = whoturn;
        TurnCount++;
        int xpos = (int)(WhichNumber) / 11;
        int ypos = (int)(WhichNumber) % 11;       
        matrix[xpos,ypos] = whoturn;
       
        if (whoturn == 0)
        {
            whoturn = 1;
            turnIcons[0].SetActive(false);
            turnIcons[1].SetActive(true);
        } else
        {
            whoturn = 0;
            turnIcons[0].SetActive(true);
            turnIcons[1].SetActive(false);
        }
        if (isEndGame(xpos,ypos) == 1)
        {
            Winer("Player X Win!!!!!!");
            if(com == true)
            {
            }
            else
            {
            }
        }
        if (isEndGame(xpos, ypos) == -1)
        {
            Winer("Player O Win!!!!!!");
          
        }
    }

#region checkendgame    
    private int isEndGame(int x, int y)
    {
        if ((isEndGameVer(x,y) == 10) || (isEndGameHori(x,y) == 10) || (isEndGamePrimory(x,y) == 10) || (isEndGameSub(x,y) == 10))
        {
            return 1;
        }
        if ((isEndGameVer(x,y) == -10) || (isEndGameHori(x,y) == -10) || (isEndGamePrimory(x,y) == -10) || (isEndGameSub(x,y) == -10))
        {
            return -1;
        }
        return 0;
    }
    private int isEndGameVer(int x, int y)
    {
        int countTopO = 0;
        for(int i = x; i >= 0 ; i--)
        {
            if(matrix[i,y] == 1)
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
            if (matrix[i, y] == 1)
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
            if (matrix[i, y] == 0)
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
            if (matrix[i, y] == 0)
            {
                countButtonX++;

            }
            else
            {
                break;
            }
        }
        if (countTopO + countButtonO == 5)
        { return -10; }
        if (countTopX + countButtonX == 5)
        { return 10; }
        return 0;
    }
    private int isEndGameHori(int x, int y)
    {
        int countLeftO = 0;
        for (int i = y; i >= 0; i--)
        {
            if (matrix[x, i] == 1)
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
            if (matrix[x, i] == 1)
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
            if (matrix[x, i] == 0)
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
            if (matrix[x, i] == 0)
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
        if(countLeftX + countRightX == 5)
        {
            return 10;
        }
        return 0;
    }
    private int isEndGamePrimory(int x, int y)
    {

        int countTopO = 0;
        for (int i = 0; i < 5; i++)
        {
            if (x - i < 0 || y - i < 0)
                break;
            if (matrix[x - i, y - i] == 1)
            {
                countTopO++;

            }
            else
            {
                break;
            }
        }
        int countButtonO = 0;
        for (int i = 1; i < 5; i++)
        {
            if (x + i > 10 || y + i > 10)
                break;
            
            if (matrix[x+i, y+i] == 1)
            {
                countButtonO++;

            }
            else
            {
                break;
            }
        }
        int countTopX = 0;
        for (int i = 0; i < 5; i++)
        {
            if (x - i < 0 || y - i < 0)
                break;
            if (matrix[x - i, y - i] == 0)
            {
                countTopX++;

            }
            else
            {
                break;
            }
        }
        int countButtonX = 0;
        for (int i = 1; i < 5; i++)
        {
            if (x + i > 10 || y + i > 10)
                break;
            if (matrix[x + i, y + i] == 0)
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
    private int isEndGameSub(int x, int y)
    {
        int countTopO = 0;
        for (int i = 0; i < 5; i++)
        {
            if (x + i > 10 || y - i < 0)
                break;
            if (matrix[x + i, y - i] == 1)
            {
                countTopO++;

            }
            else
            {
                break;
            }
        }
        int countButtonO = 0;
        for (int i = 1; i < 5; i++)
        {
            if (x - i < 0 || y + i > 20)
                break;

            if (matrix[x - i, y + i] == 1)
            {
                countButtonO++;
            }
            else
            {
                break;
            }
        }
        int countTopX = 0;
        for (int i = 0; i < 5; i++)
        {
            if (x + i > 10 || y - i < 0)
                break;
            if (matrix[x + i, y - i] == 0)
            {
                countTopX++;

            }
            else
            {
                break;
            }
        }
        int countButtonX = 0;
        for (int i = 1; i < 5; i++)
        {
            if (x - i < 0 || y + i > 10)
                break;
            if (matrix[x - i, y + i] == 0)
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
        if ( countTopX + countButtonX == 5)
        {
            return 10;
        }
        return 0;
    }


    public void playervscom()
    {
        GameSetup();
        com = false;
        player = true;
        WinerText.gameObject.SetActive(false);
       
    }
   
#endregion

public void computerplay()
{
    int DiemMax = 0;
    int DiemPhongNgu = 0;
    int DiemTanCong = 0;
    int Buocdi=0;
    if(whoturn == 0 && TurnCount==0)
    {
        carobutton(200);
    }
    if(whoturn == 0)
    {
        for (int i = 0; i < 11; i++)
        {
            for (int j = 0; j < 11; j++)
            {
                if (matrix[i, j] == -1 && !catTia(i,j))
                {
                    int DiemTam = 0;
                    DiemTanCong = duyetTC_Ngang(i, j) + duyetTC_Doc(i, j) + duyetTC_CheoXuoi(i, j) + duyetTC_CheoNguoc(i, j);
                    DiemPhongNgu = duyetPN_Ngang(i, j) + duyetPN_Doc(i, j) + duyetPN_CheoXuoi(i, j) + duyetPN_CheoNguoc(i, j);
                    //Debug.Log(duyetTC_Ngang(i, j)+ " "+ DiemPhongNgu);
                    if (DiemPhongNgu > DiemTanCong)
                    {
                        DiemTam = DiemPhongNgu;
                    }
                    else
                    {
                        DiemTam = DiemTanCong;
                    }
                    if (DiemMax < DiemTam)
                    {
                        DiemMax = DiemTam;
                        Buocdi = i*11 + j;
                    }
                }
            } 
        }
        carobutton(Buocdi);
    }
}
#region Cắt tỉa Alpha betal
    bool catTia(int i, int j)
    {
        if (catTiaNgang(i,j) && catTiaDoc(i,j) && catTiaCheoPhai(i,j) && catTiaCheoTrai(i,j))
            return true;

        return false;
    }

    bool catTiaNgang(int i, int j)
    {
        if (j <= 10 - 5)
            for (int k = 1; k <= 4; k++)
                if (matrix[i,j+k] != -1)
                    return false;

        if (j >= 4)
            for (int k = 1; k <= 4; k++)
                if (matrix[i,j-k] != -1)
                    return false;

        return true;
    }
    bool catTiaDoc(int i, int j)
    {
        if (i <= 20 - 5)
            for (int k = 1; k <= 4; k++)
                if (matrix[i+k,j] != -1)
                    return false;


        if (i >= 4)
            for (int k = 1; k <= 4; k++)
                if (matrix[i-k,j] != -1)
                    return false;

        return true;
    }
    bool catTiaCheoPhai(int i, int j)
    {
        if (i <= 20 - 5 && j >= 4)
            for (int k = 1; k <= 4; k++)
                if (matrix[i+k,j-k] != -1)
                    return false;

        if (j <= 20 - 5 && i >= 4)
            for (int k = 1; k <= 4; k++)
                if (matrix[i-k,j+k] != -1)
                    return false;

        return true;
    }
    bool catTiaCheoTrai(int i, int j)
    {
        if (i <= 20 - 5 && j <= 21 - 5)
            for (int k = 1; k <= 4; k++)
                if (matrix[i+k,j+k] != -1)
                    return false;

        if (j >= 4 && i >= 4)
            for (int k = 1; k <= 4; k++)
                if (matrix[i-k,j-k] != -1)
                    return false;

        return true;
    }
#endregion
#region AI
    private int[] MangDiemTanCong = new int[7] { 0, 4, 25, 246, 7300, 6561, 59049 };
    private int[] MangDiemPhongNgu = new int[7] { 0, 3, 24, 243, 2197, 19773, 177957 };
#region tancong
    private int  duyetTC_Ngang(int i, int j)
    {
        int DiemTanCong = 0;
        int SoQuanTa = 0;
        int SoQuanDichTren = 0;
        int SoQuanDichDuoi = 0;
        int Khoangtrong = 0;
        for (int dem = 1; dem <= 4 && j < 11 - 5; dem++)
        {

            if (matrix[i, j + dem] == 0)
            {
                if (dem == 1)
                    DiemTanCong += 37;

                SoQuanTa++;
                Khoangtrong++;
            }
            else
                if (matrix[i, j + dem] == 1)
                {
                    SoQuanDichTren++;
                    break;
                }
                else Khoangtrong++;
        }
        for (int dem = 1; dem <= 4 && j > 4; dem++)
        {
            if (matrix[i, j - dem] == 0)
            {
                if (dem == 1)
                    DiemTanCong += 37;

                SoQuanTa++;
                Khoangtrong++;

            }
            else
                if (matrix[i, j - dem] == 1)
                {
                    SoQuanDichDuoi++;
                    break;
                }
                else Khoangtrong++;
        }
            if (SoQuanDichTren > 0 && SoQuanDichDuoi > 0 && Khoangtrong < 4)
                return 0;

        DiemTanCong -= MangDiemPhongNgu[SoQuanDichTren + SoQuanDichDuoi];
        DiemTanCong += MangDiemTanCong[SoQuanTa];
        return DiemTanCong;
    }
    private int  duyetTC_Doc(int i, int j)
    {
        int DiemTanCong = 0;
        int SoQuanTa = 0;
        int SoQuanDichTren = 0;
        int SoQuanDichDuoi = 0;
        int Khoangtrong = 0;
        for (int dem = 1; dem <= 4 && i < 11 - 5; dem++)
        {

            if (matrix[i+dem, j] == 0)
            {
                if (dem == 1)
                    DiemTanCong += 37;

                SoQuanTa++;
                Khoangtrong++;
            }
            else
                if (matrix[i+dem, j] == 1)
                {
                    SoQuanDichTren++;
                    break;
                }
                else Khoangtrong++;
        }
        for (int dem = 1; dem <= 4 && i > 4; dem++)
        {
            if (matrix[i-dem, j] == 0)
            {
                if (dem == 1)
                    DiemTanCong += 37;

                SoQuanTa++;
                Khoangtrong++;

            }
            else
                if (matrix[i-dem, j] == 1)
                {
                    SoQuanDichDuoi++;
                    break;
                }
                else Khoangtrong++;
        }
            //bị chặn 2 đầu khoảng chống không đủ tạo thành 5 nước
            if (SoQuanDichTren > 0 && SoQuanDichDuoi > 0 && Khoangtrong < 4)
                return 0;

        DiemTanCong -= MangDiemPhongNgu[SoQuanDichTren + SoQuanDichDuoi];
        DiemTanCong += MangDiemTanCong[SoQuanTa];
        return DiemTanCong;

    }
    private int  duyetTC_CheoXuoi(int i, int j)
    {
        int DiemTanCong = 0;
        int SoQuanTa = 0;
        int SoQuanDichTren = 0;
        int SoQuanDichDuoi = 0;
        int Khoangtrong = 0;
        for (int dem = 1; dem <= 4 && j < 21 - 5 && i<21-5; dem++)
        {

            if (matrix[i+dem, j+dem] == 0)
            {
                if (dem == 1)
                    DiemTanCong += 37;

                SoQuanTa++;
                Khoangtrong++;
            }
            else
                if (matrix[i+dem, j+dem] == 1)
                {
                    SoQuanDichTren++;
                    break;
                }
                else Khoangtrong++;
        }
        for (int dem = 1; dem <= 4 && i > 4 && j > 4; dem++)
        {
            if (matrix[i-dem, j-dem] == 0)
            {
                if (dem == 1)
                    DiemTanCong += 37;

                SoQuanTa++;
                Khoangtrong++;

            }
            else
                if (matrix[i-dem, j-dem] == 1)
                {
                    SoQuanDichDuoi++;
                    break;
                }
                else Khoangtrong++;
        }
            //bị chặn 2 đầu khoảng chống không đủ tạo thành 5 nước
            if (SoQuanDichTren > 0 && SoQuanDichDuoi > 0 && Khoangtrong < 4)
                return 0;

        DiemTanCong -= MangDiemPhongNgu[SoQuanDichTren + SoQuanDichDuoi];
        DiemTanCong += MangDiemTanCong[SoQuanTa];
        return DiemTanCong;
    }
    private int  duyetTC_CheoNguoc(int i, int j)
    {
        int DiemTanCong = 0;
        int SoQuanTa = 0;
        int SoQuanDichTren = 0;
        int SoQuanDichDuoi = 0;
        int Khoangtrong = 0;
        for (int dem = 1; dem <= 4 && j < 21 - 5 && i > 4; dem++)
        {

            if (matrix[i-dem, j+dem] == 0)
            {
                if (dem == 1)
                    DiemTanCong += 37;

                SoQuanTa++;
                Khoangtrong++;
            }
            else
                if (matrix[i-dem, j+dem] == 1)
                {
                    SoQuanDichTren++;
                    break;
                }
                else Khoangtrong++;
        }
        for (int dem = 1; dem <= 4 && j > 4 && i <21 - 5; dem++)
        {
            if (matrix[i+dem, j-dem] == 0)
            {
                if (dem == 1)
                    DiemTanCong += 37;

                SoQuanTa++;
                Khoangtrong++;

            }
            else
                if (matrix[i+dem, j-dem] == 1)
                {
                    SoQuanDichDuoi++;
                    break;
                }
                else Khoangtrong++;
        }
            if (SoQuanDichTren > 0 && SoQuanDichDuoi > 0 && Khoangtrong < 4)
                return 0;

        DiemTanCong -= MangDiemPhongNgu[SoQuanDichTren + SoQuanDichDuoi];
        DiemTanCong += MangDiemTanCong[SoQuanTa];
        return DiemTanCong;

    }
#endregion
#region phongngu
     private int  duyetPN_Ngang(int i, int j)
    {
        int DiemPhongNgu = 0;
        int SoQuanTaTrai = 0;
        int SoQuanTaPhai = 0;
        int SoQuanDich = 0;
        int KhoangTrongPhai = 0;
        int KhoangTrongTrai = 0;
        bool ok = false;


        for (int dem = 1; dem <= 4 && j < 21 - 5; dem++)
        {
            if (matrix[i, j + dem] == 1)
            {
                if (dem == 1)
                    DiemPhongNgu += 9;
                SoQuanDich++;
            }
            else
                if (matrix[i, j + dem] == 0)
                {
                    if (dem == 4)
                        DiemPhongNgu -= 170;

                    SoQuanTaTrai++;
                    break;
                }
                else
                {
                    if (dem == 1)
                        ok = true;

                    KhoangTrongPhai++;
                }
        }

        if (SoQuanDich == 3 && KhoangTrongPhai == 1 && ok)
            DiemPhongNgu -= 200;

        ok = false;

        for (int dem = 1; dem <= 4 && j > 4; dem++)
        {
            if (matrix[i, j - dem] == 1)
            {
                if (dem == 1)
                    DiemPhongNgu += 9;

                SoQuanDich++;
            }
            else
                if (matrix[i, j - dem] == 0)
                {
                    if (dem == 4)
                        DiemPhongNgu -= 170;

                    SoQuanTaPhai++;
                    break;
                }
                else
                {
                    if (dem == 1)
                        ok = true;

                    KhoangTrongTrai++;
                }
        }

        if (SoQuanDich == 3 && KhoangTrongTrai == 1 && ok)
            DiemPhongNgu -= 200;

        if (SoQuanTaPhai > 0 && SoQuanTaTrai > 0 && (KhoangTrongTrai + KhoangTrongPhai + SoQuanDich) < 4)
            return 0;

        DiemPhongNgu -= MangDiemTanCong[SoQuanTaPhai + SoQuanTaPhai];
        DiemPhongNgu += MangDiemPhongNgu[SoQuanDich];

        return DiemPhongNgu;
    }
    private int  duyetPN_Doc(int i, int j)
    {
        
        int DiemPhongNgu = 0;
        int SoQuanTaTrai = 0;
        int SoQuanTaPhai = 0;
        int SoQuanDich = 0;
        int KhoangTrongTren = 0;
        int KhoangTrongDuoi = 0;
        bool ok = false;
        for (int dem = 1; dem <= 4 && i > 4; dem++)
        {
            if (matrix[i - dem, j] == 1)
            {
                if (dem == 1)
                    DiemPhongNgu += 9;

                SoQuanDich++;
            }
            else
                if (matrix[i - dem, j] == 0)
                {
                    if (dem == 4)
                        DiemPhongNgu -= 170;

                    SoQuanTaPhai++;
                    break;
                }
                else
                {
                    if (dem == 1)
                        ok = true;

                    KhoangTrongTren++;
                }
            }
        if (SoQuanDich == 3 && KhoangTrongTren == 1 && ok)
            DiemPhongNgu -= 200;
        ok = false;
        for (int dem = 1; dem <= 4 && i < 21 - 5; dem++)
        {
            if (matrix[i + dem, j] == 1)
            {
                if (dem == 1)
                    DiemPhongNgu += 9;

                SoQuanDich++;
            }
            else
                if (matrix[i + dem, j] == 0)
                {
                    if (dem == 4)
                        DiemPhongNgu -= 170;

                    SoQuanTaTrai++;
                    break;
                }
                else
                {
                    if (dem == 1)
                        ok = true;

                    KhoangTrongDuoi++;
                }
        }

        if (SoQuanDich == 3 && KhoangTrongDuoi == 1 && ok)
            DiemPhongNgu -= 200;

        if (SoQuanTaPhai > 0 && SoQuanTaTrai > 0 && (KhoangTrongTren + KhoangTrongDuoi + SoQuanDich) < 4)
            return 0;

        DiemPhongNgu -= MangDiemTanCong[SoQuanTaTrai + SoQuanTaPhai];
        DiemPhongNgu += MangDiemPhongNgu[SoQuanDich];
        return DiemPhongNgu;

    }
    private int  duyetPN_CheoXuoi(int i, int j)
    {
        
        int DiemPhongNgu = 0;
        int SoQuanTaTrai = 0;
        int SoQuanTaPhai = 0;
        int SoQuanDich = 0;
        int KhoangTrongTren = 0;
        int KhoangTrongDuoi = 0;
        bool ok = false;

        for (int dem = 1; dem <= 4 && i < 21 - 5 && j < 21 - 5; dem++)
        {
            if (matrix[i + dem, j + dem] == 1)
            {
                if (dem == 1)
                    DiemPhongNgu += 9;

                SoQuanDich++;
            }
            else
                if (matrix[i + dem, j + dem] == 0)
                {
                    if (dem == 4)
                        DiemPhongNgu -= 170;

                    SoQuanTaPhai++;
                    break;
                }
                else
                {
                    if (dem == 1)
                        ok = true;

                    KhoangTrongTren++;
                }
        }

        if (SoQuanDich == 3 && KhoangTrongTren == 1 && ok)
            DiemPhongNgu -= 200;

        ok = false;
        for (int dem = 1; dem <= 4 && i > 4 && j > 4; dem++)
        {
            if (matrix[i - dem, j - dem] == 1)
            {
                if (dem == 1)
                    DiemPhongNgu += 9;

                SoQuanDich++;
            }
            else
                if (matrix[i - dem, j - dem] == 0)
                {
                    if (dem == 4)
                        DiemPhongNgu -= 170;

                    SoQuanTaTrai++;
                    break;
                }
                else
                {
                    if (dem == 1)
                        ok = true;

                    KhoangTrongDuoi++;
                }
        }

        if (SoQuanDich == 3 && KhoangTrongDuoi == 1 && ok)
            DiemPhongNgu -= 200;

        if (SoQuanTaPhai > 0 && SoQuanTaTrai > 0 && (KhoangTrongTren + KhoangTrongDuoi + SoQuanDich) < 4)
            return 0;

        DiemPhongNgu -= MangDiemTanCong[SoQuanTaPhai + SoQuanTaTrai];
        DiemPhongNgu += MangDiemPhongNgu[SoQuanDich];

        return DiemPhongNgu;

    }
    private int  duyetPN_CheoNguoc(int i, int j)
    {
        int DiemPhongNgu = 0;
        int SoQuanTaTrai = 0;
        int SoQuanTaPhai = 0;
        int SoQuanDich = 0;
        int KhoangTrongTren = 0;
        int KhoangTrongDuoi = 0;
        bool ok = false;

        //lên
        for (int dem = 1; dem <= 4 && i > 4 && j < 21 - 5; dem++)
        {

            if (matrix[i - dem, j + dem] == 1)
            {
                if (dem == 1)
                    DiemPhongNgu += 9;

                SoQuanDich++;
            }
            else
                if (matrix[i - dem, j + dem] == 0)
                {
                    if (dem == 4)
                        DiemPhongNgu -= 170;

                    SoQuanTaPhai++;
                    break;
                }
                else
                {
                    if (dem == 1)
                        ok = true;

                    KhoangTrongTren++;
                }
        }
            

        if (SoQuanDich == 3 && KhoangTrongTren == 1 && ok)
            DiemPhongNgu -= 200;

        ok = false;
        for (int dem = 1; dem <= 4 && i < 21 - 5 && j > 4; dem++)
        {
            if (matrix[i + dem, j - dem] == 1)
            {
                if (dem == 1)
                    DiemPhongNgu += 9;

                SoQuanDich++;
            }
            else
                if (matrix[i + dem, j - dem] == 0)
                {
                    if (dem == 4)
                        DiemPhongNgu -= 170;

                    SoQuanTaTrai++;
                    break;
                }
                else
                {
                    if (dem == 1)
                        ok = true;

                    KhoangTrongDuoi++;
                }
        }

        if (SoQuanDich == 3 && KhoangTrongDuoi == 1 && ok)
            DiemPhongNgu -= 200;

        if (SoQuanTaPhai > 0 && SoQuanTaTrai > 0 && (KhoangTrongTren + KhoangTrongDuoi + SoQuanDich) < 4)
            return 0;

        DiemPhongNgu -= MangDiemTanCong[SoQuanTaTrai + SoQuanTaPhai];
        DiemPhongNgu += MangDiemPhongNgu[SoQuanDich];

        return DiemPhongNgu;
    }
#endregion

#endregion
}