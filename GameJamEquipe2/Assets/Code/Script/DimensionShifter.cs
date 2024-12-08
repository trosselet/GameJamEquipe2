using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using Random = UnityEngine.Random;

public class DimensionShifter : MonoBehaviour
{

    [SerializeField, Header("References")] private FullScreenPassRendererFeature shifterEffect;
    [SerializeField] private Material shifterMaterial;
    [SerializeField] private Volume postProcVolume;

    [SerializeField, Header("Stats")] private float duration = 1.0f;
    
    private Material mMatCopy;
    private float mElapsedTime = 0.0f;
    private bool mIsTransitionning = false;
    private bool mIsFrom = true;
    private LensDistortion mPaniniProj;
    private ChromaticAberration mChromaticAberration;
    
    //Color change
    public LinkedList<KeyValuePair<RenderTexture, GameObject>> mColorsList;
    public LinkedListNode<KeyValuePair<RenderTexture, GameObject>> mCurrentColor;
    
    //Textures 2d
    [SerializeField] private RenderTexture mYellowTexture;
    [SerializeField] private RenderTexture mRedTexture;
    [SerializeField] private RenderTexture mBlueTexture;
    
    //Parents
    [SerializeField, Header("Parents")] private GameObject yellowParent;
    [SerializeField] private GameObject redParent;
    [SerializeField] private GameObject blueParent;
    
    void Start()
    {
        mMatCopy = new Material(shifterMaterial);
        
        postProcVolume.profile.TryGet(out mPaniniProj);
        postProcVolume.profile.TryGet(out mChromaticAberration);
        
        mColorsList = new LinkedList<KeyValuePair<RenderTexture, GameObject>>();
        //mColorsList.AddLast();
        mColorsList.AddLast(new KeyValuePair<RenderTexture, GameObject>(mBlueTexture, blueParent));
        mColorsList.AddLast(new KeyValuePair<RenderTexture, GameObject>(mRedTexture, redParent));
        mColorsList.AddLast(new KeyValuePair<RenderTexture, GameObject>(mYellowTexture, yellowParent));
        mCurrentColor = mColorsList.First;
        
        shifterEffect.passMaterial = mMatCopy;
        shifterEffect.SetActive(true);
    }

    private void OnDestroy()
    {
        shifterEffect.passMaterial = shifterMaterial;
    }

    private void StartTransition()
    {
        mMatCopy.SetFloat("_RandSeed", Random.Range(4.5f, 7.0f));
        mIsTransitionning = true;
    }

    public void ChangeColor()
    {
        if (mIsTransitionning) return;
        
        mMatCopy.SetTexture("_OldCam", mCurrentColor.Value.Key);
        
        //loop through all children
        DisableChildren(mCurrentColor.Value.Value, false);
        
        if (mCurrentColor.Next == null) mCurrentColor = mColorsList.First;
        else mCurrentColor = mCurrentColor.Next;
        
        DisableChildren(mCurrentColor.Value.Value, true);
        
        mMatCopy.SetTexture("_NewCam", mCurrentColor.Value.Key);
        StartTransition();
    }

    // Update is called once per frame
    void Update()
    {
        if (mIsTransitionning == false) return;
        
        if (mElapsedTime >= duration)
        {
            mElapsedTime = 0.0f;
            mIsTransitionning = false;
            return;
        }

        float ratio = mElapsedTime / duration;
        mMatCopy.SetFloat("_Progress", 6 * ratio);
        mPaniniProj.intensity.value = -Mathf.Abs(Mathf.Sin(ratio * Mathf.PI)/1.30f);
        mChromaticAberration.intensity.value = Mathf.Abs(Mathf.Sin(ratio * Mathf.PI)*20);
        mElapsedTime += Time.deltaTime;
    }

    private void DisableChildren(GameObject parent, bool active)
    {
        foreach (Transform child in parent.transform)
        {
            child.GetComponent<MeshCollider>().enabled = active;
        }
    }
}
