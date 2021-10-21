﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace U.Reactor
{
    public abstract class REbaseSelector
    {
        internal Func<LayoutElementBSetter> layoutElementSetter { get; set; } // // Inline selector.layoutElementSetter = xxx
        internal bool isLayoutElement { get; set; } = false; // Inline selector.isLayoutElement = xxx
        public bool isDisposed { get; private set; } = false; // Interal

        public HC.ReactorId elementId { get; private set; } // By constructor new Selector(xxx)
        public RectTransform rectTransform { get; private set; } // By constructor new Selector(xxx)
        public GameObject gameObject { get; private set; } // By constructor new Selector(xxx)
        public REbaseSelector parent { get; private set; } // By Func selector.SetParent()
        public REbaseSelector[] childs { get; private set; } // By Func selector.SetChilds()


        // Get the root canvas
        public REcanvas.Selector rootCanvasSelector
        { 
            get 
            {
                REbaseSelector root = this;
                int i = 0;
                while(root.parent != null && i < 200)
                {
                    root = root.parent;
                    i++;
                }

                try
                {
                    return (REcanvas.Selector)root;
                }
                catch (Exception)
                {
                    return null;
                }
            } 
        }

        // Get the first parent canvas
        public REcanvas.Selector parentCanvasSelector
        {
            get
            {
                if (parent == null)
                {
                    try
                    {
                        return (REcanvas.Selector)this;
                    }
                    catch (Exception)
                    {
                        return null;
                    }
                }

                REbaseSelector parentCanvas = parent;

                int i = 0;
                while (parentCanvas.parent != null && parentCanvas.gameObject.GetComponent<Canvas>() == null && i < 200)
                {
                    parentCanvas = parentCanvas.parent;
                    i++;
                }

                try
                {
                    return (REcanvas.Selector)parentCanvas;
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        
        public  REbaseSelector[] brothersSelector { get
            {
                if (parent == null)
                    return null;
                else
                    return parent.childs;
            }
        } 

        internal REbaseSelector(GameObject gameObject, HC.ReactorId pieceId, RectTransform rectTransform)
        {
            this.gameObject = gameObject;
            this.elementId = pieceId;
            this.rectTransform = rectTransform;
        }


        internal void SetParent(REbaseSelector parent)
        {
            this.parent = parent;
        }

        internal void SetChilds(List<REbase> childs)
        {
            this.childs = childs.Select(c => c.selector).ToArray();
        }


        internal virtual void Destroy()
        {
            isDisposed = true;
            layoutElementSetter = null;
            elementId = null;
            rectTransform = null;
            gameObject = null;
            parent = null;
            childs = null;
        }

    }
}
