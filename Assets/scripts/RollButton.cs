using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SlotMachineRollWithFirework : MonoBehaviour
{
    public Image reel1Image;
    public Image reel2Image;
    public Image reel3Image;

    public Sprite redSprite;
    public Sprite yellowSprite;
    public Sprite blackSprite;

    // 3 biến text hiển thị số lần từng màu
    public TMP_Text redCountText;
    public TMP_Text yellowCountText;
    public TMP_Text blackCountText;

    public GameObject fireworkPrefab;
    public Transform spawnPoint;

    private string[] values = { "đỏ", "vàng", "đen" };

    private int redCount = 0;
    private int yellowCount = 0;
    private int blackCount = 0;

    private string[] finalResults = new string[3];

    public void OnRollButtonClicked()
    {
        for (int i = 0; i < 3; i++)
        {
            int randIndex = Random.Range(0, values.Length);
            finalResults[i] = values[randIndex];
        }

        StartCoroutine(SpinReel(reel1Image, 1f, finalResults[0], 0));
        StartCoroutine(SpinReel(reel2Image, 1.5f, finalResults[1], 1));
        StartCoroutine(SpinReel(reel3Image, 2f, finalResults[2], 2));
    }

    private IEnumerator SpinReel(Image reelImage, float duration, string finalValue, int reelIndex)
    {
        float elapsed = 0f;
        while (elapsed < duration)
        {
            int randomIndex = Random.Range(0, values.Length);
            SetImageByValue(reelImage, values[randomIndex]);
            elapsed += 0.1f;
            yield return new WaitForSeconds(0.1f);
        }

        SetImageByValue(reelImage, finalValue);

        if (reelIndex == 2)
        {
            CountColorsAndUpdate();
        }
    }

    private void SetImageByValue(Image img, string value)
    {
        switch (value)
        {
            case "đỏ":
                img.sprite = redSprite;
                break;
            case "vàng":
                img.sprite = yellowSprite;
                break;
            case "đen":
                img.sprite = blackSprite;
                break;
        }
    }

    private void CountColorsAndUpdate()
    {
        int redThisRoll = 0;
        int yellowThisRoll = 0;
        int blackThisRoll = 0;

        foreach (var val in finalResults)
        {
            if (val == "đỏ") redThisRoll++;
            else if (val == "vàng") yellowThisRoll++;
            else if (val == "đen") blackThisRoll++;
        }

        redCount += redThisRoll;
        yellowCount += yellowThisRoll;
        blackCount += blackThisRoll;

        UpdateCountTexts();

        if (yellowCount >= 3)
        {
            SpawnFirework();
            yellowCount = 0;
            UpdateCountTexts();
        }
    }

    private void UpdateCountTexts()
    {
        if (redCountText != null)
            redCountText.text = "Số lần đỏ: " + redCount.ToString();

        if (yellowCountText != null)
            yellowCountText.text = "Số lần vàng: " + yellowCount.ToString();

        if (blackCountText != null)
            blackCountText.text = "Số lần đen: " + blackCount.ToString();
    }

    private void SpawnFirework()
    {
        if (fireworkPrefab != null && spawnPoint != null)
        {
            Instantiate(fireworkPrefab, spawnPoint.position, Quaternion.identity);
            Debug.Log("Firework spawned!");
        }
        else
        {
            Debug.LogWarning("FireworkPrefab or SpawnPoint not assigned!");
        }
    }
}
