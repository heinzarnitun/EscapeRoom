using UnityEngine;

public class MouthOverride : MonoBehaviour
{
    private SkinnedMeshRenderer smr;
    public int mouthBlendShapeIndex = 1;  // Usually mouth open blendshape index

    void Start()
    {
        smr = GetComponent<SkinnedMeshRenderer>();
        if (smr == null)
            Debug.LogError("No SkinnedMeshRenderer found on this GameObject!");
    }

    void LateUpdate()
    {
        if (smr != null)
        {
            smr.SetBlendShapeWeight(mouthBlendShapeIndex, 0f);
        }
    }
}
