using UnityEngine;
using System.Collections;

public class FripperController : MonoBehaviour {

    //HingeJointコンポーネントを入れる
    private HingeJoint myHingeJoynt;

    //初期の傾き
    private float defaultAngle = 20;

    //弾いたときの傾き
    private float flickAngle = -20;

    //画面の有効範囲
    Rect rect_L = new Rect(0, 0, Screen.width/2, Screen.height);        //左画面範囲
    Rect rect_R = new Rect(Screen.width / 2, Screen.width / 2, Screen.width, Screen.height);    //右画面範囲

    // Use this for initialization
    void Start () {

        //HingeJointコンポーネント取得
        this.myHingeJoynt = GetComponent<HingeJoint>();

        //フリッパーの傾きを設定
        SetAngle(this.defaultAngle);
	
	}

    // Update is called once per frame
    void Update()
    {
        /*
                    //左矢印キーを押した時左フリッパーを動かす
                    if (Input.GetKeyDown(KeyCode.LeftArrow) && tag == "LeftFripperTag"){
                        SetAngle(this.flickAngle);
                    }
                    //右矢印キーを押した時右フリッパーを動かす
                    if (Input.GetKeyDown(KeyCode.RightArrow) && tag == "RightFripperTag"){
                        SetAngle(this.flickAngle);
                    }
                    //矢印キー話されたときフリッパーを元に戻す
                    if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow)) {
                        SetAngle(this.defaultAngle);
                    }
        */

        //***********************************************************************************************************
        //発展課題：スマートフォンでも動かせるようにマルチタッチに対応しましょう
        //***********************************************************************************************************


        //Input.touchCount の数だけタップ判定の処理をおこなう
        for (int i = 0; i < Input.touchCount; i++)
        {

            //タッチした座標を取得
            Touch _touch = Input.GetTouch(i);
            Vector2 newVec = new Vector2(_touch.position.x, Screen.height - _touch.position.y);

            //タッチ情報判定
            switch (Input.touches[i].phase) {

                //画面をタッチした
                case TouchPhase.Began:

                    //画面右側 && タグが右フリッパーである
                    if (newVec.x >= rect_R.xMin && newVec.x < rect_R.xMax && tag == "RightFripperTag") {
                        SetAngle(this.flickAngle);
                        Debug.Log("右フリッパー開始");
                    }
                    //画面左側がタップ開始された && タグが左フリッパーである
                    if (newVec.x >= rect_L.xMin && newVec.x < rect_L.xMax && tag == "LeftFripperTag") {
                        SetAngle(this.flickAngle);
                        Debug.Log("左フリッパー開始");
                    }
                        break;

                //タッチを離した
                case TouchPhase.Ended:

                    //画面右側 && タグが右フリッパーである
                    if (newVec.x >= rect_R.xMin && newVec.x < rect_R.xMax && tag == "RightFripperTag") {
                        SetAngle(this.defaultAngle);
                        Debug.Log("右フリッパー終了");
                    }
                    //画面左側がタップ開始された && タグが左フリッパーである
                    if (newVec.x >= rect_L.xMin && newVec.x < rect_L.xMax && tag == "LeftFripperTag") {
                        SetAngle(this.defaultAngle);
                        Debug.Log("左フリッパー終了");
                    }
                    break;

            }
        }

        /*修正前スクリプト******************************************************************************************************

                    //Input.touchCount の数だけタップ判定の処理をおこなう
                    for (int i = 0; i < Input.touchCount; i++) {

                    Touch _touch = Input.GetTouch(i);
                    Vector2 newVec = new Vector2(_touch.position.x, Screen.height - _touch.position.y);

                    //画面右側がタップ開始された && タグが右フリッパーである
                    if (newVec.x >= rect_R.xMin && newVec.x < rect_R.xMax && newVec.y >= rect_R.yMin && newVec.y < rect_R.yMax 
                        && Input.touches[i].phase == TouchPhase.Began 
                        && tag == "RightFripperTag"){

                        SetAngle(this.flickAngle);
        //                Debug.Log("右フリッパー開始");
                    }

                    //画面左側がタップ開始された && タグが左フリッパーである
                    if (newVec.x >= rect_L.xMin && newVec.x < rect_L.xMax && newVec.y >= rect_L.yMin && newVec.y < rect_L.yMax 
                        && Input.touches[i].phase == TouchPhase.Began 
                        && tag == "LeftFripperTag"){

                        SetAngle(this.flickAngle);
        //                Debug.Log("左フリッパー開始");
                    }

                   //画面右側がタップ終了された && タグが右フリッパーである
                    if (newVec.x >= rect_R.xMin && newVec.x < rect_R.xMax && newVec.y >= rect_R.yMin && newVec.y < rect_R.yMax 
                        && Input.touches[i].phase == TouchPhase.Ended 
                        && tag == "RightFripperTag"){

                        SetAngle(this.defaultAngle);
        //                Debug.Log("右フリッパー終了");
                    }
                    //画面左側がタップ終了された && タグが左フリッパーである
                    if (newVec.x >= rect_L.xMin && newVec.x < rect_L.xMax && newVec.y >= rect_L.yMin 
                        && newVec.y < rect_L.yMax && Input.touches[i].phase == TouchPhase.Ended 
                        && tag == "LeftFripperTag"){

                        SetAngle(this.defaultAngle);
        //                Debug.Log("左フリッパー終了");
                    }

        //****************************************************************************************************************

                int fingerCount = 0;
                foreach (Touch touch in Input.touches)
                {
                    if (touch.phase != TouchPhase.Ended && touch.phase != TouchPhase.Canceled)
                        fingerCount++;
        //          Debug.Log("User has " + fingerCount + " finger(s) touching the screen");
                }


                if (Input.touchCount > 0){

                    Touch _touch = Input.GetTouch(0);
                    Vector2 newVec = new Vector2(_touch.position.x, Screen.height - _touch.position.y);

                    //タッチしていない場合
                    if (fingerCount == 0) {

                            SetAngle(this.defaultAngle);
                            Debug.Log(66);
                    }

                    //１タッチしている場合
                    if (fingerCount == 1) {
                        //タップ判定
                        if (Input.touches[0].phase == TouchPhase.Began) {  //指１本でタップした

                            //Rectとタッチの位置を判定
                            if (newVec.x >= rect.xMin && newVec.x < rect.xMax && newVec.y >= rect.yMin && newVec.y < rect.yMax)
                            {
                                if (tag == "LeftFripperTag")
                                {
                                    SetAngle(this.flickAngle);
                                    Debug.Log("左タッチ１");
                                }
                            } else {
                                if (tag == "RightFripperTag") {
                                    SetAngle(this.flickAngle);
                                    Debug.Log("右タッチ１");
                                }
                            }
                        } else if (Input.touches[0].phase == TouchPhase.Ended) {    //指１本でタップを離した
                                SetAngle(this.defaultAngle);
                                Debug.Log("タッチ放す１");
                        }
                    }

                    //２タッチしている場合
                    if (fingerCount == 2) {

                        if (Input.touches[1].phase == TouchPhase.Began) {
                            SetAngle(this.flickAngle);
                            Debug.Log("２タッチ");
                        }
                    }
                }
            */
        //****************************************************************************************************************

    }

    public void SetAngle(float angle) {
        JointSpring jointSpr = this.myHingeJoynt.spring;
        jointSpr.targetPosition = angle;
        this.myHingeJoynt.spring = jointSpr;
    }

}
