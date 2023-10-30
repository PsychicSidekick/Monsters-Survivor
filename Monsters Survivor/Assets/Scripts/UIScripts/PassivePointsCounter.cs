using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PassivePointsCounter : MonoBehaviour
{
    public int availablePoints = 0;
    public int pointsSpent = 0;

    public TMP_Text counterText;

    public void AddAvailablePoints(int value)
    {
        availablePoints += value;
        UpdateCounterText();
    }

    public void ResetPoints()
    {
        AddAvailablePoints(pointsSpent);
        pointsSpent = 0;
        UpdateCounterText();
    }

    private void UpdateCounterText()
    {
        counterText.text = availablePoints.ToString();
    }
}
