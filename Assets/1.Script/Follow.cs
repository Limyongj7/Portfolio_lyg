using UnityEngine;

public class Follow : MonoBehaviour // 체력바 캐릭터 고정 스크립트
{
    RectTransform rect;
    Vector3 offset;

    void Awake()
    {
        rect = GetComponent<RectTransform>();
    }

    void Start()
    {
        // 여기서 실행해야 GameManager와 Player가 준비된 상태임
        Vector3 playerScreenPos =
            Camera.main.WorldToScreenPoint(GameManager.instance.player.transform.position);

        offset = rect.position - playerScreenPos;
    }

    private void FixedUpdate()
    {
        Vector3 playerScreenPos =
            Camera.main.WorldToScreenPoint(GameManager.instance.player.transform.position);

        rect.position = playerScreenPos + offset;
    }
}

