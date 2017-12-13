//using UnityEngine;
//using UnityEngine.UI;
//using System.Collections;
//using System.Collections.Generic;

//public class ShatteredDisplayEffect : MonoBehaviour
//{
//    /// <summary>
//    /// 画面キャプチャー用カメラ
//    /// </summary>
//    public Camera CaptureCamera;

//    /// <summary>
//    /// エフェクト用のカメラ
//    /// </summary>
//    public Camera EffectCamera;

//    /// <summary>
//    /// 描画に使用するマテリアル。通常はDiffeseマテリアル。
//    /// </summary>
//    public Material UsedMaterial;

//    /// <summary>
//    /// 分割オブジェクト
//    /// </summary>
//    //public List<GameObject> ShatteredGameObjects;

//    /// <summary>
//    /// 
//    /// </summary>
//    public Slider DivisionSliderX;

//    /// <summary>
//    /// 
//    /// </summary>
//    public Slider DivisionSliderY;

//    /// <summary>
//    /// ポストエフェクト用レイヤー名称
//    /// </summary>
//    public string PostEffectLayer = "PostEffect";

//    /// <summary>
//    /// ガラスバラバラエフェクトの実行を開始します。
//    /// </summary>
//    public void OnExecute()
//    {
//        // キャプチャー開始
//        if (CaptureCamera.gameObject.GetComponent<RenderTextureCapture>() == null)
//        {
//            var capture = CaptureCamera.gameObject.AddComponent<RenderTextureCapture>();
//            capture.ResultHandler += new System.Action(OnCaptured);
//        }
//    }

//    /// <summary>
//    /// 画面キャプチャー完了時
//    /// </summary>
//    public void OnCaptured()
//    {
//        // キャプチャー終了
//        var capture = CaptureCamera.gameObject.GetComponent<RenderTextureCapture>();
//        var texture = capture.Texture;
//        Destroy(capture);

//        // 使用するテクスチャーの準備
//        this.UsedMaterial.mainTexture = texture;

//        // 3角形の点群を生成
//        var points = CreateShatteredPoints((int)DivisionSliderX.value, (int)DivisionSliderY.value);

        
//        // 点群からメッシューを生成(ドロネー分割)
//        Triangulator triangulator = new Triangulator();
//        var mesh = triangulator.CreateInfluencePolygon(points.ToArray());

//        // メッシュをすべて分割
//        var triangleMeshes = SubdivisionMesh(mesh);

//        // メッシュからGameObjectを生成
//        var objs = CreateShatteredGameObject(triangleMeshes);

//        // ルートを作成し、その下へすべて追加
//        //GameObject rootObj = new GameObject("ShatteredRoot");
//        //objs.ForEach(o => o.transform.SetParent(rootObj.transform));

//        // 地面を生成
//        //var groundCollider = CreateShatteredGroundCollider();
//        //groundCollider.transform.SetParent(rootObj.transform);

//        // ランダムで力を加える
//        foreach (var obj in objs)
//        {
//            var rigid = obj.GetComponent<Rigidbody>();
//            var powerx = Random.Range(-100f, 100f);
//            var powery = Random.Range(0f, 100f);
//            var powerz = Random.Range(-100f, 100f);
//            rigid.AddForce(new Vector3(powerx, powery, powerz));
//        }

//        //ShatteredGameObjects = objs;
//    }

//    /// <summary>
//    /// 
//    /// </summary>
//    void Update()
//    {
//        //if( ShatteredGameObjects == null ) {
//        //  return;
//        //}
//        //foreach( var obj in ShatteredGameObjects ) {
//        //  var rigid = obj.GetComponent<Rigidbody>();
//        //  rigid.AddForce(new Vector3(100, 0, 0));
//        //}
//    }

//    /// <summary>
//    /// 3角形の点群を生成します。
//    /// </summary>
//    /// <param name="divX"></param>
//    /// <param name="divY"></param>
//    /// <returns></returns>
//    List<Vector2> CreateShatteredPoints(int divX, int divY)
//    {
//        List<Vector2> points = new List<Vector2>();

//        // 必須点4点を追加
//        points.Add(new Vector2(0, 0));
//        points.Add(new Vector2(Screen.width, 0));
//        points.Add(new Vector2(0, Screen.height));
//        points.Add(new Vector2(Screen.width, Screen.height));

//        int xDiv = divX;
//        int yDiv = divY;

//        // 外周の点も追加
//        for (int y = 1; y < yDiv; y++)
//        {
//            double by = y * (Screen.height / yDiv);
//            points.Add(new Vector2((float)0, (float)by));
//            points.Add(new Vector2((float)Screen.width, (float)by));
//        }
//        for (int x = 1; x < xDiv; x++)
//        {
//            double bx = x * (Screen.width / xDiv);
//            points.Add(new Vector2((float)bx, (float)0));
//            points.Add(new Vector2((float)bx, (float)Screen.height));
//        }

//        // 適当に点を生成
//        for (int y = 0; y < yDiv; y++)
//        {
//            for (int x = 0; x < xDiv; x++)
//            {
//                double bx = x * (Screen.width / xDiv);
//                double by = y * (Screen.height / yDiv);
//                // 1つのグリッド内の点を生成
//                for (int i = 0; i < 1; i++)
//                {
//                    var px = Random.Range(0.0f, Screen.width / xDiv) + bx;
//                    var py = Random.Range(0.0f, Screen.height / yDiv) + by;
//                    points.Add(new Vector2((float)px, (float)py));
//                }
//            }
//        }

//        return points;
//    }

//    /// <summary>
//    /// 1メッシュから3角形メッシュへ変換します。
//    /// スクリーン座標系からワールド座標系に変換します。
//    /// </summary>
//    /// <param name="mesh"></param>
//    /// <returns></returns>
//    List<Mesh> SubdivisionMesh(Mesh mesh)
//    {
//        List<Mesh> meshes = new List<Mesh>();

//        // 厚みのある3角形ポリゴンメッシュへ変換
//        int index = 0;
//        int polygonCount = mesh.triangles.Length / 3;
//        for (int n = 0; n < polygonCount; n++)
//        {
//            var p1 = mesh.vertices[mesh.triangles[n * 3 + 0]];
//            var p2 = mesh.vertices[mesh.triangles[n * 3 + 1]];
//            var p3 = mesh.vertices[mesh.triangles[n * 3 + 2]];
//            var uv1 = mesh.uv[mesh.triangles[n * 3 + 0]];
//            var uv2 = mesh.uv[mesh.triangles[n * 3 + 1]];
//            var uv3 = mesh.uv[mesh.triangles[n * 3 + 2]];
//            string name = "SubdivisionMesh_" + index.ToString();
//            meshes.Add(GenTriangleMesh(name, p1, p2, p3, uv1, uv2, uv3));
//            ++index;
//        }
//        return meshes;
//    }

//    /// <summary>
//    /// 3角形のメッシュを生成します。
//    /// </summary>
//    /// <param name="name"></param>
//    /// <param name="p1"></param>
//    /// <param name="p2"></param>
//    /// <param name="p3"></param>
//    /// <param name="uv1"></param>
//    /// <param name="uv2"></param>
//    /// <param name="uv3"></param>
//    /// <returns></returns>
//    Mesh GenTriangleMesh(string name, Vector3 p1, Vector3 p2, Vector3 p3, Vector2 uv1, Vector2 uv2, Vector2 uv3)
//    {
//        float z1 = 10.0f;
//        float z2 = 10.1f;

//        Vector3[] vtx = new Vector3[6];
//        Vector2[] UV = new Vector2[6];

//        // 頂点座標の指定.
//        vtx[0] = EffectCamera.ScreenToWorldPoint(new Vector3(p1.x, p1.z, z1));
//        vtx[1] = EffectCamera.ScreenToWorldPoint(new Vector3(p2.x, p2.z, z1));
//        vtx[2] = EffectCamera.ScreenToWorldPoint(new Vector3(p3.x, p3.z, z1));
//        vtx[3] = EffectCamera.ScreenToWorldPoint(new Vector3(p1.x, p1.z, z2));
//        vtx[4] = EffectCamera.ScreenToWorldPoint(new Vector3(p2.x, p2.z, z2));
//        vtx[5] = EffectCamera.ScreenToWorldPoint(new Vector3(p3.x, p3.z, z2));

//        // UVの指定
//        UV[0] = new Vector2(uv1.x / Screen.width, uv1.y / Screen.height);
//        UV[1] = new Vector2(uv2.x / Screen.width, uv2.y / Screen.height);
//        UV[2] = new Vector2(uv3.x / Screen.width, uv3.y / Screen.height);
//        UV[3] = new Vector2(uv1.x / Screen.width, uv1.y / Screen.height);
//        UV[4] = new Vector2(uv2.x / Screen.width, uv2.y / Screen.height);
//        UV[5] = new Vector2(uv3.x / Screen.width, uv3.y / Screen.height);

//        // 三角形ごとの頂点インデックスを指定.
//        int[] idx = new int[8 * 3] {
//      0,1,2,
//      5,4,3,
//      0,3,4,
//      0,4,1,
//      1,4,5,
//      1,5,2,
//      2,5,3,
//      2,3,0
//    };

//        //Vector3[] dnml = new Vector3[idx.Length];
//        Vector3[] dvtx = new Vector3[idx.Length];
//        Vector2[] dUV = new Vector2[idx.Length];
//        int[] didx = new int[8 * 3];
//        int p = 0;
//        for (int n = 0; n < idx.Length / 3; ++n)
//        {
//            for (int m = 0; m < 3; m++)
//            {
//                dvtx[p + m] = vtx[idx[p + m]];
//                dUV[p + m] = UV[idx[p + m]];
//                didx[p + m] = p + m;
//            }
//            p += 3;
//        }

//        var mesh = new Mesh();
//        mesh.vertices = dvtx;
//        mesh.uv = dUV;
//        mesh.triangles = didx;
//        mesh.RecalculateNormals();
//        mesh.RecalculateBounds();

//        return mesh;
//    }

//    /// <summary>
//    /// メッシュからバラバラエフェクト用のGameObjectを作成します。
//    /// </summary>
//    /// <param name="triangleMeshes"></param>
//    /// <returns></returns>
//    List<GameObject> CreateShatteredGameObject(List<Mesh> triangleMeshes)
//    {
//        List<GameObject> objects = new List<GameObject>();

//        int index = 0;
//        foreach (var mesh in triangleMeshes)
//        {
//            GameObject parts = new GameObject("ShatteredParts_" + index.ToString());
//            parts.AddComponent<MeshFilter>();
//            parts.AddComponent<MeshRenderer>();
//            parts.AddComponent<MeshCollider>();
//            parts.GetComponent<MeshFilter>().sharedMesh = mesh;
//            parts.GetComponent<MeshFilter>().sharedMesh.name = name;
//            parts.GetComponent<MeshRenderer>().material = this.UsedMaterial;
//            parts.GetComponent<MeshCollider>().sharedMesh = mesh;
//            parts.AddComponent<Rigidbody>();
//            parts.AddComponent<InvisibleDestory>();
//            parts.layer = LayerMask.NameToLayer(this.PostEffectLayer);
//            objects.Add(parts);
//            ++index;
//        }
//        return objects;
//    }

//    /// <summary>
//    /// 地面の当たり判定用コライダーを作成します。
//    /// </summary>
//    /// <returns></returns>
//    GameObject CreateShatteredGroundCollider()
//    {
//        GameObject ground = new GameObject();
//        ground.name = "GroundCollider";
//        ground.layer = LayerMask.NameToLayer(this.PostEffectLayer);

//        var p1 = this.EffectCamera.ScreenToWorldPoint(new Vector3(0, 0, 10.0f));
//        var p2 = this.EffectCamera.ScreenToWorldPoint(new Vector3(Screen.width, 0, 10.0f));

//        var collider = ground.AddComponent<BoxCollider>();
//        collider.size = new Vector3((p2 - p1).magnitude, 1, 10);
//        collider.transform.up = this.EffectCamera.transform.up;
//        collider.transform.position = (p2 - p1) / 2.0f + p1;

//        return ground;
//    }

//}