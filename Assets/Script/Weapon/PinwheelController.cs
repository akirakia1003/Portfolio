using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinwheelController : BaseWeapon
{
    // ���ˉ�
    int reflectionCount = 5;
    // �J�����\���͈�
    Vector2 cameraSize;

    // Start is called before the first frame update
    void Start()
    {
        // �����_���ȕ����Ɍ������Ĕ�΂�
        forward = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
        forward.Normalize();

        // �J�����\���͈́i�����j
        float aspect = Screen.width / (float)Screen.height;
        cameraSize = new Vector2(Camera.main.orthographicSize * aspect, Camera.main.orthographicSize);

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // ���ˉ񐔏���
        if (0 > reflectionCount)
        {
            Destroy(gameObject);
            return;
        }

        // �Ǎۂňړ��ʂ𔽎�
        Vector2 camera = Camera.main.transform.position;
        Vector2 start = new Vector2(camera.x - cameraSize.x, camera.y - cameraSize.y);
        Vector2 end = new Vector2(camera.x + cameraSize.x, camera.y + cameraSize.y);
        Vector2 pos = rigidbody2d.position;

        // ��ʊO�̔���
        if (pos.x <= start.x || end.x <= pos.x)
        {
            forward.x *= -1;
            reflectionCount--;
        }
        if (pos.y <= start.y || end.y <= pos.y)
        {
            forward.y *= -1;
            reflectionCount--;
        }

        // ��]
        transform.Rotate(new Vector3(0, 0, 1000 * Time.deltaTime));
        // �ړ�
        rigidbody2d.position += forward * stats.MoveSpeed * Time.deltaTime;
    }

    // �g���K�[���Փ˂�����
    private void OnTriggerEnter2D(Collider2D collision)
    {
        attackEnemy(collision);
    }
}
