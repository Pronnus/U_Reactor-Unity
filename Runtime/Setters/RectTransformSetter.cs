﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace U.Reactor
{
    public class RectTransformSetter
    {
        // RectTransform
        public virtual Vector2 pivot { get; set; } = new Vector2(.5f, .5f);
        public virtual Vector2 scale { get; set; } = new Vector2(1, 1);
        public virtual Quaternion rotation { get; set; } = new Quaternion(0, 0, 0, 0);
        public virtual Vector2 anchorMin { get; set; } = new Vector2(.5f, .5f);
        public virtual Vector2 anchorMax { get; set; } = new Vector2(.5f, .5f);
        public virtual Vector3 localPosition { get; set; } = new Vector3(0, 0, 0);
        public virtual Vector2 sizeDelta { get; set; } = Vector2.zero;
        public virtual Vector2 offsetMin { get; set; } = Vector2.zero;
        public virtual Vector2 offsetMax { get; set; } = Vector2.zero;
        public virtual float width { get; set; } = 100f;
        public virtual float height { get; set; } = 100f;
        public virtual int flexibleWidth { get; set; } = 1;
        public virtual int flexibleHeight { get; set; } = 1;


        public RectTransform Set(RectTransform c)
        {
            c.pivot = pivot;
            c.localPosition = localPosition;
            c.anchorMin = anchorMin;
            c.anchorMax = anchorMax;
            c.sizeDelta = new Vector2(width, height);
            c.localScale = new Vector3(scale.x, scale.y, 1f);
            c.rotation = rotation;

            return c;
        }

        public RectTransform SetByAnchors(RectTransform c)
        {
            c.anchorMin = anchorMin;
            c.anchorMax = anchorMax;
            c.sizeDelta = sizeDelta;
            c.offsetMin = offsetMin;
            c.offsetMax = offsetMax;

            return c;
        }

        public RectTransform Set(GameObject gameObject)
        {
            return Set(gameObject.AddComponent<RectTransform>());
        }

        public RectTransform SetByAnchors(GameObject gameObject)
        {
            return SetByAnchors(gameObject.AddComponent<RectTransform>());
        }

    }

}