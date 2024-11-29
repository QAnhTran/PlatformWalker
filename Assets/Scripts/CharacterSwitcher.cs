using UnityEngine;

public class CharacterSwitch : MonoBehaviour
{
    public GameObject otherCharacter; // Nhân vật để chuyển đổi sang
    private bool isCurrentCharacterActive = true; // Trạng thái: nhân vật hiện tại đang hoạt động

    void Start()
    {
        // Nhân vật này bắt đầu ở trạng thái hoạt động, nhân vật khác bị ẩn
        if (isCurrentCharacterActive)
        {
            gameObject.SetActive(true);
            otherCharacter.SetActive(false);
        }
    }

    void Update()
    {
        // Nhấn phím T để chuyển đổi nhân vật
        if (Input.GetKeyDown(KeyCode.T))
        {
            SwitchCharacter();
        }
    }

    private void SwitchCharacter()
    {
        if (isCurrentCharacterActive)
        {
            // Ẩn nhân vật hiện tại, hiển thị nhân vật khác
            otherCharacter.transform.position = transform.position; // Chuyển vị trí
            otherCharacter.transform.rotation = transform.rotation; // Chuyển hướng
            otherCharacter.SetActive(true);
            gameObject.SetActive(false);
        }
        else
        {
            // Hiển thị nhân vật hiện tại, ẩn nhân vật khác
            transform.position = otherCharacter.transform.position; // Chuyển vị trí
            transform.rotation = otherCharacter.transform.rotation; // Chuyển hướng
            gameObject.SetActive(true);
            otherCharacter.SetActive(false);
        }

        // Cập nhật trạng thái
        isCurrentCharacterActive = !isCurrentCharacterActive;
    }
}
