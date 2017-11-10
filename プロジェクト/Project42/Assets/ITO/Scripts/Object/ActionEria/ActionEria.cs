using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum LR
{
    RIGHT,
    LEFT,
}

[ExecuteInEditMode]
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshRenderer))]

public class ActionEria : MonoBehaviour
{
    /// <summary>
    /// 左右
    /// </summary>
    [SerializeField]
    private LR lr;

    /// <summary>
    /// エリアポイントのプレファブ
    /// </summary>
    [SerializeField]
    private GameObject eriaPointPrefab;

    /// <summary>
    /// 一番上のポイント
    /// </summary>
    [SerializeField]
    private GameObject TopEriaPoint;

    /// <summary>
    /// 一番下のポイント
    /// </summary>
    [SerializeField]
    private GameObject BottomEriaPoint;

    /// <summary>
    /// エリアポイントのオブジェクトのリスト
    /// </summary>
    private List<GameObject> eriaPointObjects;

    /// <summary>
    /// コライダー
    /// </summary>
    private PolygonCollider2D collider;

    /// <summary>
    /// ランダムで生成する位置(X座標)の候補
    /// </summary>
    private List<float> randomInstantiatePos_X;
    private List<float> instantiatePosX = new List<float>();

    [SerializeField]
    Material mat;

    /// <summary>
    /// ポイントを配置できる最大値
    /// </summary>
    [SerializeField]
    private int maxPointIndex;

    /// <summary>
    /// ポイントが最大値あるかどうか
    /// </summary>
    public bool IsMaxPoint()
    {
        if (eriaPointObjects.Count == maxPointIndex+2)
        {
            return true;
        }
        else return false;
    }


    // Use this for initialization
    void Start()
    {
        eriaPointObjects = new List<GameObject>();
        //リストに初期配置されているポイントを格納
        eriaPointObjects.Add(TopEriaPoint);
        eriaPointObjects.Add(BottomEriaPoint);
        collider = GetComponent<PolygonCollider2D>();
        SetRandomInstantiatePos_X();
        //リストをコピー
        instantiatePosX = new List<float>(randomInstantiatePos_X);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// エリアポイントの生成
    /// </summary>
    public void InstantiateEriaPoint()
    {
        if (IsMaxPoint()) return;
        float posX = instantiatePosX[Random.Range(0, instantiatePosX.Count-1)];
        //生成した位置をリストから削除(同じ列にポイントが生成されないように)
        instantiatePosX.Remove(posX);
        //Y座標は上の壁から下の壁までの間をランダム
        float posY = Random.Range(GameObject.Find("BottomCollider").transform.position.y + 1,
            GameObject.Find("TopCollider").transform.position.y - 1);
        AddEriaPoint(new Vector2(posX, posY));
        //ポリゴンを作成
        CreatePolygon();
    }

    /// <summary>
    /// エリアポイントの追加
    /// </summary>
    private void AddEriaPoint(Vector2 position)
    {
        //一番下のポイントを削除
        eriaPointObjects.Remove(BottomEriaPoint);
        //指定した位置にエリアポイントを生成
        GameObject eriaP = Instantiate(eriaPointPrefab, position, Quaternion.identity);
        //このオブジェクトを親に
        eriaP.transform.parent = transform;
        //リストに追加
        eriaPointObjects.Add(eriaP);
        //並び替え
        eriaPointObjects.Sort(CompareByDistance);
        //一番下のポイントを追加
        eriaPointObjects.Add(BottomEriaPoint);
        SetColliderPoint();
    }

    private void SetColliderPoint()
    {
        //ポイントの座標を入れる配列を作成
        Vector2[] pos = new Vector2[eriaPointObjects.Count];
        //配列にオブジェクトの座標を格納
        for (int i = 0; i < eriaPointObjects.Count; i++)
        {
            pos[i] = eriaPointObjects[i].transform.position;
        }
        //コライダーのポイントにコピー
        collider.points = pos;
    }

    /// <summary>
    /// 生成位置の候補を設定する
    /// </summary>
    private void SetRandomInstantiatePos_X()
    {
        randomInstantiatePos_X = new List<float>();

        //壁のX座標を取得
        float LeftWallPos_X = GameObject.Find("RightCollider").transform.position.x;


        //生成位置の間隔
        float space = LeftWallPos_X / (maxPointIndex + 1);

        for (int i = 1; i <= maxPointIndex; i++)
        {
            if (lr == LR.RIGHT) randomInstantiatePos_X.Add(space * (i));

            else randomInstantiatePos_X.Add(-(space * (i)));
        }
    }

    /// <summary>
    /// ポリゴンの作成(エリアの描画)
    /// </summary>
    private void CreatePolygon()
    {
        Mesh mesh = new Mesh();
        //あたり判定のポイントの数
        int collisionIndex = collider.points.Length;

        Vector3[] vertices = new Vector3[collisionIndex];
        for (int i = 0; i < collisionIndex; i++)
        {
            vertices[i] = collider.points[i];
        }
        mesh.vertices = vertices;

        Vector2[] verticesXY = new Vector2[collisionIndex];
        for (int i = 0; i < collisionIndex; i++)
        {
            Vector3 pos = collider.points[i];
            verticesXY[i] = new Vector2(pos.x, pos.y);
        }
        Triangulator tr = new Triangulator(verticesXY, Camera.main.transform.position);
        int[] indices = tr.Triangulate();

        mesh.triangles = indices;

        //UVデータの設定は今回は省略
        mesh.uv = new Vector2[collisionIndex];

        mesh.RecalculateNormals();//法線の再計算
        mesh.RecalculateBounds();//バウンディングボリュームの再計算

        MeshRenderer renderer = GetComponent<MeshRenderer>();
        renderer.material = mat;
        MeshFilter filter = GetComponent<MeshFilter>();
        filter.mesh = mesh;
    }


    /// <summary>
    /// 1つのポイントからオブジェクトの距離を比較
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <returns></returns>
    private int CompareByDistance(GameObject a, GameObject b)
    {
        Vector2 a_Pos = a.transform.position;
        Vector2 b_Pos = b.transform.position;
        if (Vector2.Distance(eriaPointObjects[0].transform.position, a_Pos) >
            Vector2.Distance(eriaPointObjects[0].transform.position, b_Pos))
        {
            return 1;
        }
        if (Vector2.Distance(eriaPointObjects[0].transform.position, a_Pos) <
            Vector2.Distance(eriaPointObjects[0].transform.position, b_Pos))
        {
            return -1;
        }

        return 0;
    }

    /// <summary>
    /// エリアポイントの破壊
    /// </summary>
    /// <param name="point"></param>
    public void BreakEriaPoint(GameObject point)
    {

        //リストから削除
        eriaPointObjects.Remove(point);
        //生成位置の候補に追加
        instantiatePosX.Add(point.transform.position.x);
        SetColliderPoint();
        CreatePolygon();
        //削除
        Destroy(point);

    }
}
