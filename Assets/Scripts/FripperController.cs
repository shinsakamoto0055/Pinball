using UnityEngine;
using System.Collections;

public class FripperController : MonoBehaviour {

    //HingeJointコンポーネントを入れる
    private HingeJoint myHingeJoynt;

    //初期の傾き
    private float defaultAngle = 20;

    //弾いたときの傾き
    private float flickAngle = -20;

    //画面の有効範囲（左画面）
    Rect rect = new Rect(0, 0, 400, 1280);
//    Rect rect_L = new Rect(400, 400, 800, 1280);

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
    }
    public void SetAngle(float angle) {
        JointSpring jointSpr = this.myHingeJoynt.spring;
        jointSpr.targetPosition = angle;
        this.myHingeJoynt.spring = jointSpr;
    }

}
