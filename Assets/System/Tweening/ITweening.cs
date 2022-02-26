using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public interface ITweening
{
	void TweenPos(Transform objTransform, Vector3 targetPosition, float speed, Action onStart = null, Action onComplete = null, bool ignoreTimeScale = false);
	void TweenPosX(Transform objTransform, float targetX, float speed, Action onStart = null, Action onComplete = null, bool ignoreTimeScale = false);
	void TweenPosY(Transform objTransform, float targetY, float speed, Action onStart = null, Action onComplete = null, bool ignoreTimeScale = false);

	void TweenAnchorPos(RectTransform rectTransform, Vector3 targetPosition, float speed, Action onStart = null, Action onComplete = null, bool ignoreTimeScale = false);
	void TweenAnchorPosX(RectTransform rectTransform, float targetX, float speed, Action onStart = null, Action onComplete = null, bool ignoreTimeScale = false);
	void TweenAnchorPosY(RectTransform rectTransform, float targetY, float speed, Action onStart = null, Action onComplete = null, bool ignoreTimeScale = false);

	void TweenFade(Image image, float targetAlpha, float speed, Action onStart = null, Action onComplete = null, bool ignoreTimeScale = false);
	
	void TweenColor(Image image, Color targetColor, float speed, Action onStart = null, Action onComplete = null, bool ignoreTimeScale = false);
	void TweenColor(Text text, Color targetColor, float speed, Action onStart = null, Action onComplete = null, bool ignoreTimeScale = false);
	void TweenColor(TextMeshProUGUI text, Color targetColor, float speed, Action onStart = null, Action onComplete = null, bool ignoreTimeScale = false);

	void TweenTextNumber(Text text, float startingValue, float targetValue, float speed, Action onStart = null, Action onComplete = null, bool ignoreTimeScale = false);
	void TweenTextNumber(TextMeshProUGUI text, float startingValue, float targetValue, float speed, Action onStart = null, Action onComplete = null, bool ignoreTimeScale = false);
}