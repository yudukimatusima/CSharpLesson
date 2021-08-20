using UnityEngine;

/// <summary>
/// 左クリックで指定したプレハブを生成するコンポーネント
/// </summary>
public class StarGenerator : MonoBehaviour
{
    /// <summary>左クリックで生成するオブジェクト</summary>
    [SerializeField] GameObject m_prefab = default; // 
    /* ↑ GameObject 型の変数を Inspector からセットできる。
     * Scene 上（Hierarchy 上）のオブジェクトまたは Project ウィンドウ内のアセットを指定できる。
     * ほとんどの場合は Project ウインドウからプレハブを指定する。 */
    /// <summary>オブジェクトを自分からどれくらいずらした位置に生成するか設定する</summary>
    [SerializeField] Vector3 m_offset = default;

    void Update()
    {
        // 左クリックが押されたら
        if (Input.GetButtonDown("Fire1"))
        {
            Debug.Log("Fire1 が押された");
            // プレハブから GameObject を生成し、設定されたオフセットにずらす
            GameObject go = Instantiate(m_prefab, this.transform.position, this.transform.rotation);
            go.transform.position += m_offset;
        }
    }
}
