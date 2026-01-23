using UnityEngine;
using TMPro;

[ExecuteAlways]
[RequireComponent(typeof(TextMeshProUGUI))]
public class HalfCircleText : MonoBehaviour
{
    public float letterSpacing = 5f; // extra pixels tussen letters
    public float radius = 150f;
    [Range(1f, 360f)]
    public float arcDegrees = 180f;
    public bool invert = false;

    private TextMeshProUGUI tmp;

    void Awake()
    {
        tmp = GetComponent<TextMeshProUGUI>();
    }

    void OnEnable()
    {
        UpdateText();
    }

    void OnValidate()
    {
        UpdateText();
    }

    public void UpdateText()
    {
        if (tmp == null) return;

        // Force rebuild en haal text info
        tmp.ForceMeshUpdate();
        var textInfo = tmp.textInfo;
        int charCount = textInfo.characterCount;
        if (charCount == 0) return;

        // Heel belangrijk: originele (ongetransformeerde) vertices cachen
        var cachedMeshInfo = textInfo.CopyMeshInfoVertexData();

        // Bereken totale advance (breedte + letterSpacing), gebruik xAdvance waar handig
        float totalAdvance = 0f;
        float[] advances = new float[charCount];

        for (int i = 0; i < charCount; i++)
        {
            var c = textInfo.characterInfo[i];

            // Onzichtbare/whitespace chars krijgen een fallback advance
            if (!c.isVisible)
            {
                float adv = (c.xAdvance != 0f ? c.xAdvance : tmp.fontSize * 0.3f) + letterSpacing;
                advances[i] = adv;
                totalAdvance += adv;
                continue;
            }

            int meshIndex = c.materialReferenceIndex;
            int vertIndex = c.vertexIndex;

            // Gebruik cached (originele) vertices voor breedte
            var vertsOrig = cachedMeshInfo[meshIndex].vertices;

            float minX = Mathf.Min(vertsOrig[vertIndex + 0].x, vertsOrig[vertIndex + 1].x, vertsOrig[vertIndex + 2].x, vertsOrig[vertIndex + 3].x);
            float maxX = Mathf.Max(vertsOrig[vertIndex + 0].x, vertsOrig[vertIndex + 1].x, vertsOrig[vertIndex + 2].x, vertsOrig[vertIndex + 3].x);
            float width = maxX - minX;

            if (width <= 0f) width = (c.xAdvance != 0f ? c.xAdvance : tmp.fontSize * 0.3f);

            float advance = width + letterSpacing;
            advances[i] = advance;
            totalAdvance += advance;
        }

        if (totalAdvance <= 0f) return;

        float arcRad = arcDegrees * Mathf.Deg2Rad;
        float halfArc = arcRad / 2f;
        float cumulative = 0f;

        // Transformeer vanaf cached vertices en schrijf naar live mesh
        for (int i = 0; i < charCount; i++)
        {
            var c = textInfo.characterInfo[i];
            float advance = advances[i];

            if (!c.isVisible)
            {
                cumulative += advance;
                continue;
            }

            int meshIndex = c.materialReferenceIndex;
            int vertIndex = c.vertexIndex;

            var vertsOrig = cachedMeshInfo[meshIndex].vertices;
            var vertsLive = textInfo.meshInfo[meshIndex].vertices;

            // Center van het karakter op basis van originele quad
            Vector3 charMid = (vertsOrig[vertIndex + 0] + vertsOrig[vertIndex + 2]) * 0.5f;

            // Normale verdeling over de boog
            float mid = cumulative + advance * 0.5f;
            float t = Mathf.Clamp01(mid / totalAdvance);

            float angle = Mathf.Lerp(-halfArc, halfArc, t);
            if (invert) angle = -angle;

            float x = Mathf.Sin(angle) * radius;
            float y = Mathf.Cos(angle) * radius;
            float rotZ = -angle * Mathf.Rad2Deg;
            Quaternion rot = Quaternion.Euler(0f, 0f, rotZ);

            // Transformeer elke vertex vanaf de originele positie, niet vanaf vorige live mesh
            for (int v = 0; v < 4; v++)
            {
                Vector3 orig = vertsOrig[vertIndex + v] - charMid;
                vertsLive[vertIndex + v] = new Vector3(x, y, 0f) + rot * orig;
            }

            cumulative += advance;
        }

        // Schrijf gewijzigde meshes terug
        for (int i = 0; i < textInfo.meshInfo.Length; i++)
        {
            var mesh = textInfo.meshInfo[i].mesh;
            mesh.vertices = textInfo.meshInfo[i].vertices;
            mesh.triangles = textInfo.meshInfo[i].triangles; // veilig om opnieuw toe te wijzen
            tmp.UpdateGeometry(mesh, i);
        }
    }
}
