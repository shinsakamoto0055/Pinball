using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/*課題＊＊＊***********************************************************************************
Lesson5で作成したPinBallをもとに作成してください
UIのTextを使って得点を表示してください
ターゲット（大小の星と雲）にボールが衝突した時に得点を加算してください
ターゲットの種類によって取得できる点数を変えてください（例：小さい星は10点、大きい星は20点など）
得点は画面の右上に見やすく表示しましょう
***********************************************************************************************/

public class BallController : MonoBehaviour {

    //ボールが見える可能性のあるＺ軸の最大値
    private float visiblePosZ = -6.5f;

    //ゲームオーバーを表示するテキスト
    private GameObject gameoverText;

    //得点を表示するテキスト
    private GameObject pointsText;

    //得点初期化
    private int points = 0;

    // Use this for initialization
    void Start () {

        //シーン中のGameOberTextオブジェクトを取得
        this.gameoverText = GameObject.Find("GameOverText");
        //シーン中のPointsTextオブジェクトを取得
        this.pointsText = GameObject.Find("PointsText");

    }

    // Update is called once per frame

    void Update () {

        //ボールが画面外に出た場合
        if (this.transform.position.z < this.visiblePosZ) {

            //GameoverTextにゲームオーバーを表示
            this.gameoverText.GetComponent<Text>().text = "Game Over";

            Destroy(this.gameObject);
        }

        //PointsTextに得点を表示
        this.pointsText.GetComponent<Text>().text = points + "点";

    }
    //衝突時に呼ばれる関数
    void OnCollisionEnter(Collision other)
    {

        if (other.gameObject.tag == "SmallStarTag"){ //小さい星は5点
            points += 5;
            //Debug.Log("小さい星は5点");
        } else if (other.gameObject.tag == "LargeStarTag"){  //大きい星は20点
            points += 20;
            //Debug.Log("大きい星は20点");
        } else if (other.gameObject.tag == "BonusStarTag"){  //ボーナス星は10000点
            points += 10000;
            //Debug.Log("ボーナス星は10000点");
        } else if (other.gameObject.tag == "SmallCloudTag"){  //小さい雲は10点
            points += 10;
            //Debug.Log("小さい雲は10点");
        } else if (other.gameObject.tag == "LargeCloudTag"){  //大きい雲は40点
            points += 40;
            //Debug.Log("大きい雲は40点");
        }

    }

}