using UnityEngine;

public class ModelChange : MonoBehaviour
{
    public GameObject[] bodys;
    public GameObject[] heads;
    public GameObject[] weapons;

    private void Start()
    {
        Change();
    }

    public void Change()
    {
        if(bodys != null)
        {
            foreach (var body in bodys)
            {
                body.SetActive(false);
            }

            int number = Random.Range(0, bodys.Length);
            bodys[number].SetActive(true);
        }

        if (heads != null)
        {
            foreach (var head in heads)
            {
                head.SetActive(false);
            }

            int number = Random.Range(0, heads.Length);
            heads[number].SetActive(true);
        }

        if (weapons != null)
        {
            foreach (var weapon in weapons)
            {
                weapon.SetActive(false);
            }

            int number = Random.Range(0, weapons.Length);
            weapons[number].SetActive(true);
        }
    }
}
