using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public GameObject[] wallArray;
    public GameObject[] grassArray;
    public GameObject[] landArray;
    public static int row = 100;
    public static int col = 100;
    public Transform mapholder;

//	public static List<Vector2> grassPositionList = new List<Vector2>();
	public static List<Grasslocation> grassPositionList = new List<Grasslocation>();
    public List<Vector2> positionList = new List<Vector2>();
    public static int grassMax = 6;
    public static int grassMin = 6;
	public static int grassLandCountMax=5;

	public struct Grasslocation
	{
		public Vector2 posGrass{ get; private set;}
		public int posGrasscol{ get; private set;}
		public int posGrassrow{ get; private set;}
		public Grasslocation(Vector2 pos,int col,int row){
			posGrass=pos;
			posGrasscol=col;
			posGrassrow=row;
			
		}

	}




    // Use this for initialization
    void Start()
    {
        initMap();

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void initMap()
    {
        mapholder = new GameObject("Map").transform;
        for (int i = 0; i < col; i++)
        {
            for (int j = 0; j < row; j++)
            {

                if (i == 0 || j == 0 || i == col - 1 || j == row - 1)
                {
                    int index = Random.Range(0, wallArray.Length);
                    GameObject map = Instantiate(wallArray[index], new Vector3(0.65f * i, 0.65f * j, 0f), Quaternion.identity) as GameObject;
                    map.transform.SetParent(mapholder);

                }
                else {
                    int index = Random.Range(0, landArray.Length);
                    GameObject map = Instantiate(landArray[index], new Vector3(0.65f * i, 0.65f * j, 0f), Quaternion.identity) as GameObject;
                    map.transform.SetParent(mapholder);

                }
               


            }
        }

        positionList.Clear();
        for (int i = 1; i < col-2; i++)
        {
            for (int j = 1; j < row-2; j++)
            {
                positionList.Add(new Vector2(i, j));
           
            }
        }

        

//		int grassLandCount = Random.Range (0, grassLandCountMax);
//
//
//		for (int glc = 0; glc < grassLandCount; glc++) {
//			int grassColCount = Random.Range(grassMin, grassMax+1);
//			int grassRowCount = Random.Range (grassMin, grassMax+1);
//			
//			int positionIndex = Random.Range(0, positionList.Count);
//			Vector2 pos = positionList[positionIndex];
//
//			grassPositionList.Add(new Grasslocation(pos,grassColCount,grassRowCount));
//
//
//
//			for (int i = 0; i < grassColCount; i++) {
//				for (int j = 0; j < grassRowCount; j++) {
//
//
//					float x = pos.x + i;
//					float y = pos.y + j;
//
//					if (x < col-1 && y < row-1) {
//						positionList.Remove(new Vector2(x, y));
//						int grassIndex = Random.Range(0, grassArray.Length);
//						GameObject go = GameObject.Instantiate(grassArray[grassIndex], 0.65f * new Vector2(x, y), Quaternion.identity);
//						go.transform.SetParent(mapholder);
//
//					}
//
//
//
//
//				}
//
//			}
//
//
//		}

    }

//	public static bool  check(Vector2 pos) {
//		bool YN = false;
//		for (int i = 0; i < grassPositionList.Count; i++) {
//			Grasslocation gloction = grassPositionList [i];
//
//			Vector2 gpos = gloction.posGrass;
//			Vector2 dpos = new Vector2 (gpos.x + gloction.posGrasscol - 1, gpos.y + gloction.posGrassrow - 1);
//
//
//			if (pos.x>=gpos.x && pos.x<=dpos.x && pos.y>=gpos.y && pos.y<=dpos.y)
//					{
//						YN=true;
//						goto end;
//
//					}
//				
//			
//
//
//
//		}
//
//		end:return YN;
//
//	}
}