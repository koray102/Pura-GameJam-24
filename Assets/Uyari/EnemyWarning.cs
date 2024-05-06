using UnityEngine;
using UnityEngine.UI;

public class EnemyWarning : MonoBehaviour
{
    public Transform playerTransform; // Oyuncunun transformu (Kameranýn olduðu yerde olduðunu varsayalým)
    public Transform enemyTransform; // Düþmanýn transformu
    public Image exclamationMark; // Ünlem iþareti
    public LineRenderer arrowLine; // Ok çizgisi

    void Update()
    {
        // Düþman var mý kontrol et
        if (enemyTransform != null)
        {
            // Düþmanýn yönünü al ve ünlem iþaretini o yöne doðru çevir
            Vector3 direction = enemyTransform.position - playerTransform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            exclamationMark.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

            // Ok çizgisini güncelle
            arrowLine.SetPosition(0, playerTransform.position);
            arrowLine.SetPosition(1, enemyTransform.position);
        }
        else
        {
            // Eðer düþman yoksa, ünlem iþaretini ve ok çizgisini devre dýþý býrak

        }
    }
}

