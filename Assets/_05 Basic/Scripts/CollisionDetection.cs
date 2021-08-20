using UnityEngine;

/// <summary>
/// 当たり判定をして何かさせるコンポーネント
/// </summary>
public class CollisionDetection : MonoBehaviour
{
    /// <summary>当たった時にこの色に変える</summary>
    public Color m_changedColor = default;  // Color も「データ型」である。public にしているので、Inspector から色を設定できる。
    /// <summary>初期色をここに保存しておく</summary>
    Color m_initialColor = default;
    /// <summary>SpriteRenderer コンポーネントを取得して入れておく変数</summary>
    SpriteRenderer m_sprite = default;

    void Start()
    {
        // SpriteRenderer コンポーネントを取得しておく
        m_sprite = GetComponent<SpriteRenderer>();
        // 色を戻す時のために、初期色を保存しておく
        m_initialColor = m_sprite.color;
    }

    /// <summary>
    /// 2D でコライダーとコライダーが衝突した時に呼び出される
    /// どちらかに Rigidbody が付いていないと呼ばれないことに注意
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(this.name + " と " + collision.gameObject.name + " が衝突した");
        // 色を変える
        m_sprite.color = m_changedColor;
    }

    /// <summary>
    /// 2D でトリガーとトリガー、またはトリガーとコライダーが接触した時に呼び出される
    /// どちらかに Rigidbody が付いていないと呼ばれないことに注意
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(this.name + " と " + collision.gameObject.name + " が接触した");
        // 色を変える
        m_sprite.color = m_changedColor;
    }

    /// <summary>
    /// 2D でトリガーが接触している状態から「離れた」時に呼び出される
    /// どちらかに Rigidbody が付いていないと呼ばれないことに注意
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log(this.name + " と " + collision.gameObject.name + " が離れた");
        // 色を初期色に戻す
        m_sprite.color = m_initialColor;
    }
}
