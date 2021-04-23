﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace U.Reactor
{
    public class ImageSetter
    {
        public virtual Sprite sprite { get; set; } = null;
        public virtual Color color { get; set; } = Color.white;
        public virtual Material material { get; set; } = null;
        public virtual bool raycastTarget { get; set; } = true;
        public virtual Vector4 raycastPadding { get; set; } = Vector4.zero;
        public virtual bool maskable { get; set; } = true;
        public virtual bool useSpriteMesh { get; set; } = false;
        public virtual bool preserveAspect { get; set; } = false;
        //public Image.FillMethod fillMethod;


        public Image Set(Image c)
        {
            c.sprite = sprite;
            c.color = color;
            c.material = material;
            c.raycastTarget = raycastTarget;
            c.raycastPadding = raycastPadding;
            c.maskable = maskable;

            if(sprite != null)
            {
                //c.fillMethod = fillMethod;
                c.useSpriteMesh = useSpriteMesh;
                c.preserveAspect = preserveAspect;
            }

            return c;
        }

        public Image Set(GameObject gameObject)
        {
            return Set(gameObject.AddComponent<Image>());
        }

    }

}
