using UnityEngine;

public class Gear : MonoBehaviour
{
    public ItemData.ItemType type;
    public float rate;
    public void Init(ItemData data)
    {
        name = "Gear" + data.itemId;
        transform.parent = GameManager.instance.player.transform;
        transform.localPosition = Vector3.zero;

        type = data.itemType;
        rate = data.damages[0];
        ApplyGear();
    }


    public void LevelUp(float rate)
    {
        this.rate = rate;
        ApplyGear();
    }

    void ApplyGear()
    {
        switch(type)
        {
            case ItemData.ItemType.IAS:
                RateUp();
                break;
            case ItemData.ItemType.shoe:
                SpeedUp();
                break;
        }
    }

    void RateUp()
    {
        Weapon[] weapons = transform.parent.GetComponentsInChildren<Weapon>();

        foreach (Weapon weapon in weapons)
        {
            switch (weapon.id)
            {
                case 20: // 피스톨
                    weapon.speed = 0.5f * (1f - rate);
                    break;
                default: // 다른무기

                    break;
            }
        }
    }

    void SpeedUp()
    {
        float speed = 5;
        GameManager.instance.player.speed = speed + speed * rate;
    }
}
