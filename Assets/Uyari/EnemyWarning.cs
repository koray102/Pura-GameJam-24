using UnityEngine;
using UnityEngine.UI;

public class EnemyWarning : MonoBehaviour
{
    public Transform playerTransform; // Oyuncunun transformu (Kameran�n oldu�u yerde oldu�unu varsayal�m)
    public Transform enemyTransform; // D��man�n transformu
    public Image exclamationMark; // �nlem i�areti
    public LineRenderer arrowLine; // Ok �izgisi

    void Update()
    {
        // D��man var m� kontrol et
        if (enemyTransform != null)
        {
            // D��man�n y�n�n� al ve �nlem i�aretini o y�ne do�ru �evir
            Vector3 direction = enemyTransform.position - playerTransform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            exclamationMark.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

            // Ok �izgisini g�ncelle
            arrowLine.SetPosition(0, playerTransform.position);
            arrowLine.SetPosition(1, enemyTransform.position);
        }
        else
        {
            // E�er d��man yoksa, �nlem i�aretini ve ok �izgisini devre d��� b�rak

        }
    }
}

