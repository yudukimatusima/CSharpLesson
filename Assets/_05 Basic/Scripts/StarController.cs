using UnityEngine;

/// <summary>
/// Star のオブジェクトを制御するコンポーネント
/// 画面下に消えたら自分自身を消す（正式な言い方としては「破棄する」という）
/// </summary>
public class StarController : MonoBehaviour
{
    void Update()
    {
        // 画面下に消えたら（y 座標が -10 より小さくなったら）自分自身を破棄する
        if (this.transform.position.y < -10f)
        {
            Debug.Log(this.name + "を破棄する");
            Destroy(this.gameObject);
        }
    }

    /// <summary>
    /// 他のコライダーと衝突した時に呼び出される
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 衝突した相手に "Destructable" タグが付いていたら
        if (collision.gameObject.tag == "Destructable")
        {
            Debug.Log("衝突相手と自分自身を破棄する");
            // 衝突相手を破棄して
            Destroy(collision.gameObject);
            // 自分自身も破棄する
            Destroy(this.gameObject);
        }
    }
}
