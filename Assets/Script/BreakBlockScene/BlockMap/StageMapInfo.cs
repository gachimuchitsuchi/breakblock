using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageMapInfo
{
    public const int NUM_Of_STAGES = 6;
    public const int STAGE_ROW = 10;
    public const int STAGE_COL = 19;

    //0=None,1=Pyro,2=Hydro,3=Cryo,4=Electro,5=何も置かない 
    //ステージごとに2次元配列(10*19)でブロック番号をセット

    public int[,,] stageMap = new int[NUM_Of_STAGES, STAGE_ROW, STAGE_COL]
    {
        {
            { 5, 1, 1, 1, 5, 2, 2, 2, 5, 5, 5, 3, 3, 3, 5, 4, 4, 4, 5 },
            { 5, 1, 1, 1, 5, 2, 2, 2, 5, 5, 5, 3, 3, 3, 5, 4, 4, 4, 5 },
            { 5, 1, 1, 1, 5, 2, 2, 2, 5, 5, 5, 3, 3, 3, 5, 4, 4, 4, 5 },
            { 5, 1, 1, 1, 5, 2, 2, 2, 5, 5, 5, 3, 3, 3, 5, 4, 4, 4, 5 },
            { 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 },
            { 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 },
            { 5, 1, 1, 1, 5, 2, 2, 2, 5, 5, 5, 3, 3, 3, 5, 4, 4, 4, 5 },
            { 5, 1, 1, 1, 5, 2, 2, 2, 5, 5, 5, 3, 3, 3, 5, 4, 4, 4, 5 },
            { 5, 1, 1, 1, 5, 2, 2, 2, 5, 5, 5, 3, 3, 3, 5, 4, 4, 4, 5 },
            { 5, 1, 1, 1, 5, 2, 2, 2, 5, 5, 5, 3, 3, 3, 5, 4, 4, 4, 5 }
        },
        {
            { 5, 1, 5, 5, 5, 5, 5, 0, 5, 1, 5, 0, 5, 5, 5, 5, 5, 1, 5 },
            { 5, 1, 1, 5, 5, 5, 5, 0, 5, 1, 5, 0, 5, 5, 5, 5, 1, 1, 5 },
            { 5, 5, 1, 1, 5, 1, 1, 0, 5, 1, 5, 0, 1, 1, 5, 1, 1, 5, 5 },
            { 5, 5, 5, 1, 1, 5, 5, 5, 5, 1, 5, 5, 5, 5, 1, 1, 5, 5, 5 },
            { 5, 5, 5, 5, 5, 1, 5, 5, 5, 1, 5, 5, 5, 1, 5, 5, 5, 5, 5 },
            { 5, 5, 5, 5, 5, 5, 1, 5, 5, 1, 5, 5, 1, 5, 5, 5, 5, 5, 5 },
            { 5, 5, 5, 5, 5, 5, 5, 1, 5, 1, 5, 1, 5, 5, 5, 5, 5, 5, 5 },
            { 5, 1, 1, 1, 5, 5, 5, 5, 1, 1, 1, 5, 5, 5, 5, 1, 1, 1, 5 },
            { 5, 1, 1, 1, 0, 0, 0, 5, 5, 1, 5, 5, 0, 0, 0, 1, 1, 1, 5 },
            { 5, 1, 1, 1, 5, 5, 5, 1, 5, 1, 5, 1, 5, 5, 5, 1, 1, 1, 5 }
        },
        {
            { 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 },
            { 5, 2, 2, 2, 2, 5, 5, 2, 2, 2, 2, 2, 5, 5, 2, 2, 2, 2, 5 },
            { 5, 5, 5, 5, 5, 5, 5, 5, 5, 0, 5, 5, 5, 5, 5, 5, 5, 5, 5 },
            { 5, 5, 5, 5, 5, 2, 2, 2, 2, 0, 2, 2, 2, 2, 5, 5, 5, 5, 5 },
            { 5, 2, 2, 2, 5, 5, 5, 5, 5, 0, 5, 5, 5, 5, 5, 2, 2, 2, 5 },
            { 5, 5, 5, 5, 5, 5, 5, 0, 5, 5, 5, 0, 5, 5, 5, 5, 5, 5, 5 },
            { 5, 5, 5, 5, 5, 5, 5, 5, 2, 5, 2, 5, 5, 5, 5, 5, 5, 5, 5 },
            { 5, 5, 5, 5, 5, 5, 5, 0, 0, 5, 0, 0, 5, 5, 5, 5, 5, 5, 5 },
            { 5, 2, 2, 5, 2, 2, 5, 5, 5, 2, 5, 5, 5, 2, 2, 5, 2, 2, 5 },
            { 5, 2, 2, 5, 2, 2, 5, 5, 5, 2, 5, 5, 5, 2, 2, 5, 2, 2, 5 }
        },
        {
            { 5, 5, 5, 5, 5, 5, 5, 5, 5, 3, 5, 5, 5, 5, 5, 5, 5, 5, 5 },
            { 5, 5, 5, 5, 5, 5, 5, 5, 3, 3, 3, 5, 5, 5, 5, 5, 5, 5, 5 },
            { 5, 5, 3, 3, 5, 3, 3, 5, 5, 3, 5, 5, 3, 3, 5, 3, 3, 5, 5 },
            { 5, 5, 3, 5, 3, 3, 3, 5, 5, 5, 5, 5, 3, 3, 3, 5, 3, 5, 5 },
            { 5, 3, 3, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 3, 3, 5 },
            { 5, 0, 0, 3, 3, 0, 0, 3, 3, 0, 3, 3, 0, 0, 3, 3, 0, 0, 5 },
            { 5, 3, 5, 3, 5, 3, 5, 3, 5, 3, 5, 3, 5, 3, 5, 3, 5, 3, 5 },
            { 5, 5, 3, 5, 3, 5, 3, 5, 3, 5, 3, 5, 3, 5, 3, 5, 3, 5, 5 },
            { 5, 3, 5, 3, 5, 3, 5, 3, 5, 3, 5, 3, 5, 3, 5, 3, 5, 3, 5 },
            { 5, 5, 3, 5, 3, 5, 3, 5, 3, 5, 3, 5, 3, 5, 3, 5, 3, 5, 5 }
        },
        {
            { 5, 4, 5, 5, 5, 5, 5, 4, 5, 4, 5, 4, 5, 5, 5, 5, 5, 4, 5 },
            { 5, 5, 4, 4, 5, 5, 4, 5, 5, 4, 5, 5, 4, 5, 5, 4, 4, 5, 5 },
            { 5, 5, 4, 5, 5, 5, 5, 4, 5, 4, 5, 4, 5, 5, 5, 5, 4, 5, 5 },
            { 5, 4, 5, 5, 4, 5, 4, 5, 5, 4, 5, 5, 4, 5, 4, 5, 5, 4, 5 },
            { 5, 5, 5, 4, 5, 4, 5, 4, 5, 5, 5, 4, 5, 4, 5, 4, 5, 5, 5 },
            { 5, 4, 4, 5, 5, 5, 4, 5, 5, 5, 5, 5, 4, 5, 5, 5, 4, 4, 5 },
            { 5, 5, 5, 4, 5, 5, 5, 4, 5, 4, 5, 4, 5, 5, 5, 4, 5, 5, 5 },
            { 5, 5, 5, 5, 4, 5, 4, 5, 5, 4, 5, 5, 4, 5, 4, 5, 5, 5, 5 },
            { 5, 5, 5, 4, 5, 5, 5, 4, 5, 4, 5, 4, 5, 5, 5, 4, 5, 5, 5 },
            { 5, 5, 4, 5, 5, 5, 4, 5, 5, 4, 5, 5, 4, 5, 5, 5, 4, 5, 5 }
        },
        {
            { 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 },
            { 5, 5, 5, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 5, 5, 5 },
            { 5, 5, 5, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 5, 5, 5 },
            { 5, 5, 5, 0, 0, 0, 0, 0, 1, 1, 1, 0, 0, 0, 0, 0, 5, 5, 5 },
            { 5, 5, 5, 0, 0, 0, 0, 1, 1, 1, 1, 1, 0, 0, 0, 0, 5, 5, 5 },
            { 5, 5, 5, 0, 0, 0, 0, 1, 1, 1, 1, 1, 0, 0, 0, 0, 5, 5, 5 },
            { 5, 5, 5, 0, 0, 0, 0, 0, 1, 1, 1, 0, 0, 0, 0, 0, 5, 5, 5 },
            { 5, 5, 5, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 5, 5, 5 },
            { 5, 5, 5, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 5, 5, 5 },
            { 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 }
        }
    };
}
