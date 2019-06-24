using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SimpleFSM
{
    public class PolygonDestructionTransition : IStateTransition
    {
        private GameObject _destructionObj;
        private float _fadeTime;
        private List<Material> _fadeMaterialList;

        public PolygonDestructionTransition(float fadeTime)
        {
            var destructionPrefab = Resources.Load<GameObject>("TransitionPolygonSphere");
            _destructionObj = UnityEngine.Object.Instantiate(destructionPrefab);
            var renderers = _destructionObj.GetComponentsInChildren<MeshRenderer>();
            _fadeTime = fadeTime;
            _fadeMaterialList = new List<Material>();
            for (int i = 0; i < renderers.Length; i++)
            {
                var mat = renderers[i].material;
                if (mat.HasProperty("_Threshold"))
                {
                    _fadeMaterialList.Add(mat);
                }
            }
        }

        public IEnumerable Enter()
        {
            foreach (var e in FadeAnimation(-1.0f, 2.0f, _fadeTime))
            {
                yield return e;
            }
            UnityEngine.Object.Destroy(_destructionObj);
        }

        public IEnumerable Exit()
        {
            foreach(var e in FadeAnimation(2.0f, -1.0f, _fadeTime))
            {
                yield return e;
            }
        }

        private IEnumerable FadeAnimation(float fromVal, float toVal, float duration)
        {
            var startTime = Time.time;
            var endTime = startTime + duration;
            while (Time.time < endTime)
            {
                var sinceStart = Time.time - startTime;
                var percent = sinceStart / duration;
                for(int i = 0; i < _fadeMaterialList.Count; i++)
                {
                    _fadeMaterialList[i].SetFloat("_Threshold", Mathf.Lerp(fromVal, toVal, percent));
                }
                yield return null;
            }
        }
    }
}