
using UnityEngine;
using UnityEngine.UI;


public class ImlecAnimation : MonoBehaviour
{
    private Color alpha;
    private Image image;

    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();  

        alpha = image.color;

        Yan();
    }

    private void Yan()
    {
        if (alpha.a < 1)
        {
            alpha.a += 0.05f;
            image.color = alpha;

            Invoke("Yan", 0.05f);
        }
        else
        {
            Invoke("Son", 0.05f);
        }
    }

    private void Son()
    {
        if (alpha.a > 0)
        {
            alpha.a -= 0.05f;
            image.color = alpha;

            Invoke("Son", 0.05f);
        }
        else
        {
            Invoke("Yan", 0.05f);
        }
    }
}
