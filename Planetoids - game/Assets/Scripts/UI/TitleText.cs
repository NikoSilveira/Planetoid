using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TitleText : MonoBehaviour
{
    public TMP_Text textComponent;
    private float time_coef, strength_coef;

    private void Start()
    {
        time_coef = 0.6f;
        strength_coef = 8f;
    }

    void Update()
    {
        textComponent.ForceMeshUpdate();
        var textInfo = textComponent.textInfo;

        for (int i=0; i<textInfo.characterCount; i++) //Iterate through each different character individually
        {
            var charInfo = textInfo.characterInfo[i];

            if (!charInfo.isVisible)
            {
                continue; //skip invisible characters
            }

            var verts = textInfo.meshInfo[charInfo.materialReferenceIndex].vertices;

            for(int j=0; j<4; j++) //Iterates through each of the 4 vertices of a char
            {
                var orig = verts[charInfo.vertexIndex + j];
                verts[charInfo.vertexIndex + j] = orig + new Vector3(0, Mathf.Sin(Time.time*time_coef + orig.x*0.01f) * strength_coef, 0);
            }
        }

        for (int i=0; i<textInfo.meshInfo.Length; i++)
        {
            var meshInfo = textInfo.meshInfo[i];
            meshInfo.mesh.vertices = meshInfo.vertices;
            textComponent.UpdateGeometry(meshInfo.mesh, i);
        }
    }
}
