using EPOOutline;
using UnityEngine;
using Values;

namespace Turrets
{
    public class ZoneVisual : MonoBehaviour
    {
        private const string FillColorName = "_PublicColor";

        private MeshRenderer meshRenderer;
        private Color initialFrontColor;
        private Color initialBackColor;

        public Outlinable Outlinable;

        public TransformValue PlayerTransform;
        public float MinDistanceFromPlayer;
        public float MaxDistanceFromPlayer;

        private float GetDistanceRatio()
        {
            var distance = Vector3.Distance(PlayerTransform.Value.position, transform.position);

            return Mathf.Clamp((distance - MinDistanceFromPlayer) / (MaxDistanceFromPlayer - MinDistanceFromPlayer), 0f,
                1f);
        }

        private void Awake()
        {
            meshRenderer = GetComponent<MeshRenderer>();
            initialFrontColor = Outlinable.FrontParameters.FillPass.GetColor(FillColorName);
            initialBackColor = Outlinable.FrontParameters.FillPass.GetColor(FillColorName);
        }

        private void Update()
        {
            var frontColor = new Color(initialFrontColor.r, initialFrontColor.g, initialFrontColor.b,
                Mathf.Lerp(initialFrontColor.a, 0f, GetDistanceRatio()));
            Outlinable.FrontParameters.Color = frontColor;
            Outlinable.FrontParameters.FillPass.SetColor(FillColorName, frontColor);

            var backColor = new Color(initialBackColor.r, initialBackColor.g, initialBackColor.b,
                Mathf.Lerp(initialBackColor.a, 0f, GetDistanceRatio()));
            Outlinable.BackParameters.Color = backColor;
            Outlinable.BackParameters.FillPass.SetColor(FillColorName, backColor);

            meshRenderer.enabled = frontColor.a > 0 || backColor.a > 0;
        }
    }
}