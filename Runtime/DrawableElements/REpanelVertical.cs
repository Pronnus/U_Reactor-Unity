﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace U.Reactor
{
    public class REpanelVertical : RErenderer
    {
        public override Type elementType => this.GetType();
        protected override Func<RectTransformBSetter> PropsRectTransform => propsRectTransform;
        protected override Func<GameObjectBSetter> PropsGameObject => propsGameObject;
        protected override Func<IdBSetter> PropsReactorId => propsReactorId;
        protected override Func<CanvasRendererBSetter> PropsCanvasRenderer => propsCanvasRenderer;
        protected override Func<LayoutElementBSetter> PropsLayoutElement => null;
        public override bool isLayoutElement => false;


        #region Components

        protected Image backImageCmp;
        protected ScrollRect scrollRectCmp;
        protected RectMask2D rectMask2Cmp;
        protected VerticalLayoutGroup verticalLayoutCmp;
        protected ContentSizeFitter contentSizeCmp;

        protected Scrollbar vScrollbarCmp;
        protected Image vScrollbarImageCmp;
        protected Image vScrollbarHandleImageCmp;

        protected Scrollbar hScrollbarCmp;
        protected Image hScrollbarImageCmp;
        protected Image hScrollbarHandleImageCmp;

        #endregion Components


        #region Setters

        // Base
        public Func<RectTransformSetter> propsRectTransform = () => new RectTransformSetter();
        public Func<GameObjectSetter> propsGameObject = () => new GameObjectSetter();
        public Func<ReactorIdSetter> propsReactorId = () => new ReactorIdSetter();
        // Child
        public Func<CanvasRendererSetter> propsCanvasRenderer = () => new CanvasRendererSetter();

        public Func<BackImageSetter> propsBackImage = () => new BackImageSetter();
        public Func<ScrollRectSetter> propsScrollRect = () => new ScrollRectSetter();
        public Func<RectMask2DSetter> propsRectMask2D = () => new RectMask2DSetter { };
        public Func<VerticalLayoutGroupSetter> propsVerticalLayoutGroup = () => new VerticalLayoutGroupSetter();
        public Func<ContenSizeFilterSetter> propsContentSizeFilter = () => new ContenSizeFilterSetter();
        public Func<VScrollbarBackImageSetter> propsVScrollbarImage = () => new VScrollbarBackImageSetter();
        public Func<VScrollbarSetter> propsVScrollbar = () => new VScrollbarSetter();
        public Func<VScrollbarHandleImageSetter> propsVScrollbarHandleImageCmp = () => new VScrollbarHandleImageSetter();
        public Func<HScrollbarBackImageSetter> propsHScrollbarImage = () => new HScrollbarBackImageSetter();
        public Func<HScrollbarSetter> propsHScrollbar = () => new HScrollbarSetter();
        public Func<HScrollbarHandleImageSetter> propsHScrollbarHandleImageCmp = () => new HScrollbarHandleImageSetter();

        #endregion Setters


        #region Hooks

        public UseEffect.Hook[] useEffect;

        public UseApplicationEvents.Hook useApplicationEvents;
        public UseCanvasEvents.Hook useCanvasEvents;
        public UseDrag.Hook useDrag;
        public UseLateUpdate.Hook useLateUpdate;
        public UseObjectEvents.Hook useObjectEvents;
        public UsePointer.Hook usePointer;
        public UseScroll.Hook useScroll;
        public UseSelectEvents.Hook useSelectEvents;
        public UseSubmitEvents.Hook useSubmitEvents;
        public UseUpdate.Hook useUpdate;

        #endregion Hooks


        #region Drawers

        protected override void AddComponents()
        {

            // Add the gameObjects
            var viewportGO = InstanciateObject("Viewport", gameObject);
            var containerGO = InstanciateObject("Container", viewportGO);
            var vScrollbarGO = InstanciateScrollbar("Vertical Scrollbar", gameObject, out vScrollbarCmp, out vScrollbarImageCmp, out vScrollbarHandleImageCmp);
            var hScrollbarGO = InstanciateScrollbar("Horizontal Scrollbar", gameObject, out hScrollbarCmp, out hScrollbarImageCmp, out hScrollbarHandleImageCmp);

            // Set virtual parent
            virtualChildContainer = containerGO;

            // Add Components
            backImageCmp = propsBackImage().Set(gameObject);
            scrollRectCmp = propsScrollRect().Set(gameObject);
            rectMask2Cmp = propsRectMask2D().Set(viewportGO);
            verticalLayoutCmp = propsVerticalLayoutGroup().Set(containerGO);
            contentSizeCmp = propsContentSizeFilter().Set(containerGO);

            propsVScrollbarImage().SetAllExceptType(vScrollbarImageCmp);
            propsVScrollbar().Set(vScrollbarCmp);
            propsVScrollbarHandleImageCmp().SetAllExceptType(vScrollbarHandleImageCmp);

            propsHScrollbarImage().SetAllExceptType(hScrollbarImageCmp);
            propsHScrollbar().Set(hScrollbarCmp);
            propsHScrollbarHandleImageCmp().SetAllExceptType(hScrollbarHandleImageCmp);

            // Relations
            scrollRectCmp.content = containerGO.GetComponent<RectTransform>();
            scrollRectCmp.viewport = viewportGO.GetComponent<RectTransform>();
            scrollRectCmp.horizontalScrollbar = hScrollbarCmp;
            scrollRectCmp.verticalScrollbar = vScrollbarCmp;

            // Obtain percentage size
            SetReferenceSize(new RectTransformSetter());


            new RectTransformBSetter()
            {
                pivot = new Vector2(0f, 1f),
                localPosition = new Vector2(0f, 0f),
                anchorMin = new Vector2(0f, 0f),
                anchorMax = new Vector2(1, 1f),
                sizeDelta = new Vector2(0, 0),
            }.SetOrSearchBySizeDelta(viewportGO);

            new RectTransformBSetter()
            {
                pivot = new Vector2(0f, 1f),
                localPosition = new Vector2(0, 0f),
                anchorMin = new Vector2(0, 0f),
                anchorMax = new Vector2(1, 1f),
                offsetMin = new Vector2(0, 0f),
                offsetMax = new Vector2(0, 0f),
            }.SetOrSearchByAnchors(containerGO);

            new RectTransformBSetter()
            {
                pivot = new Vector2(1f, 1f),
                localPosition = new Vector2(0f, 0),
                anchorMin = new Vector2(1, 0f),
                anchorMax = new Vector2(1, 1f),
                sizeDelta = new Vector2(20, 0f),  // 20,0
            }.SetOrSearchBySizeDelta(vScrollbarGO);

            new RectTransformBSetter()
            {
                pivot = new Vector2(0f, 0f),
                localPosition = new Vector2(0f, 0),
                anchorMin = new Vector2(0f, 0f),
                anchorMax = new Vector2(1f, 0f),
                sizeDelta = new Vector2(-20, 20f),   // -20,20
            }.SetOrSearchBySizeDelta(hScrollbarGO);

        }

        protected override void AddHooks()
        {
            UseEffect.AddHook(gameObject, (Selector)selector, useEffect);
            UseApplicationEvents.AddHook(gameObject, (Selector)selector, useApplicationEvents);
            UseCanvasEvents.AddHook(gameObject, (Selector)selector, useCanvasEvents);
            UseDrag.AddHook(gameObject, (Selector)selector, useDrag);
            UseLateUpdate.AddHook(gameObject, (Selector)selector, useLateUpdate);
            UseObjectEvents.AddHook(gameObject, (Selector)selector, useObjectEvents);
            UsePointer.AddHook(gameObject, (Selector)selector, usePointer);
            UseScroll.AddHook(gameObject, (Selector)selector, useScroll);
            UseSelectEvents.AddHook(gameObject, (Selector)selector, useSelectEvents);
            UseSubmitEvents.AddHook(gameObject, (Selector)selector, useSubmitEvents);
            UseUpdate.AddHook(gameObject, (Selector)selector, useUpdate);
        }

        protected override REbaseSelector AddSelector() => new Selector(gameObject, reactorIdCmp, rectTransformCmp, canvasRendererCmp, backImageCmp, scrollRectCmp, rectMask2Cmp, verticalLayoutCmp, contentSizeCmp,
                vScrollbarImageCmp, vScrollbarCmp, vScrollbarHandleImageCmp, hScrollbarImageCmp, hScrollbarCmp, hScrollbarHandleImageCmp);

        protected override void AfterCreateComponent()
        {
            propsScrollRect().SetListeners(scrollRectCmp, (Selector)selector);
            propsVScrollbar().SetListeners(vScrollbarCmp, (Selector)selector);
            propsHScrollbar().SetListeners(hScrollbarCmp, (Selector)selector);
        }

        protected override void AfterCreateChild(REbaseSelector child)
        {
            // Call function to add layout to childs elements
            try
            {
                child.layoutElementSetter?.Invoke().Set(child.gameObject);
            }
            catch (Exception)
            {
            }
        }

        protected override bool CrateChildCondition(REbase child)
        {
            if (child.isLayoutElement) return true;

            Debug.LogError("REverticalLayout: You are trying to create a " + child.elementType.Name + " inside a layout. Layouts only can have layout elements as childs, put any other element inside a REbox to create it");
            return false;
        }

        #endregion Drawers


        #region Subclasses

        public class Selector : RErendererSelector
        {

            public Image backImage { get; private set; }
            public ScrollRect scrollRect { get; private set; }
            public RectMask2D rectMask2 { get; private set; }
            public VerticalLayoutGroup verticalLayout { get; private set; }
            public ContentSizeFitter contentSize { get; private set; }

            public Image vScrollbarImage { get; private set; }
            public Scrollbar vScrollbar { get; private set; }
            public Image vScrollbarHandleImage { get; private set; }

            public Image hScrollbarImage { get; private set; }
            public Scrollbar hScrollbar { get; private set; }
            public Image hScrollbarHandleImage { get; private set; }


            internal Selector(
                GameObject gameObject,
                HC.ReactorId pieceId,
                RectTransform rectTransform,
                CanvasRenderer canvasRenderer,

                Image backImage,
                ScrollRect scrollRect,
                RectMask2D rectMask2,
                VerticalLayoutGroup verticalLayout,
                ContentSizeFitter contentSize,

                Image vScrollbarImage,
                Scrollbar vScrollbar,
                Image vScrollbarHandleImage,

                Image hScrollbarImage,
                Scrollbar hScrollbar,
                Image hScrollbarHandleImage

                ) : base(gameObject, pieceId, rectTransform, canvasRenderer)
            {
                this.backImage = backImage;
                this.scrollRect = scrollRect;
                this.rectMask2 = rectMask2;
                this.verticalLayout = verticalLayout;
                this.contentSize = contentSize;

                this.vScrollbarImage = vScrollbarImage;
                this.vScrollbar = vScrollbar;
                this.vScrollbarHandleImage = vScrollbarHandleImage;

                this.hScrollbarImage = hScrollbarImage;
                this.hScrollbar = hScrollbar;
                this.hScrollbarHandleImage = hScrollbarHandleImage;
            }

            internal override void Destroy()
            {
                base.Destroy();

                this.backImage = null;
                this.scrollRect = null;
                this.rectMask2 = null;
                this.verticalLayout = null;
                this.contentSize = null;

                this.vScrollbarImage = null;
                this.vScrollbar = null;
                this.vScrollbarHandleImage = null;

                this.hScrollbarImage = null;
                this.hScrollbar = null;
                this.hScrollbarHandleImage = null;

            }
        }

        public class UseEffect : UseEffect<Selector, UseEffect> { }
        public class UseApplicationEvents : UseApplicationEvents<Selector, UseApplicationEvents> { }
        public class UseCanvasEvents : UseCanvasEvents<Selector, UseCanvasEvents> { }
        public class UseDrag : UseDrag<Selector, UseDrag> { }
        public class UseLateUpdate : UseLateUpdate<Selector, UseLateUpdate> { }
        public class UseObjectEvents : UseObjectEvents<Selector, UseObjectEvents> { }
        public class UsePointer : UsePointer<Selector, UsePointer> { }
        public class UseScroll : UseScroll<Selector, UseScroll> { }
        public class UseSelectEvents : UseSelectEvents<Selector, UseSelectEvents> { }
        public class UseSubmitEvents : UseSubtitEvents<Selector, UseSubmitEvents> { }
        public class UseUpdate : UseUpdate<Selector, UseUpdate> { }


        #endregion Subclasses


        #region Subsetters

        public class CanvasRendererSetter : CanvasRendererBSetter
        {

        }

        public class ReactorIdSetter : IdBSetter
        {

        }

        public class GameObjectSetter : GameObjectBSetter
        {
            public override string name { get; set; } = "Panel Vertical";
        }

        public class RectTransformSetter : RectTransformBSetter
        {
            public override float width { get; set; } = 0;
            public override float height { get; set; } = 0;
            public override Vector2 anchorMin { get; set; } = Vector2.zero;
            public override Vector2 anchorMax { get; set; } = Vector2.one;
        }

        public class BackImageSetter : ImageBSetter<Selector>
        {
            public override Color color { get; set; } = new Color(255, 255, 255, .4f);
        }

        public class ScrollRectSetter : ScrollRectBSetter<Selector>
        {
            public override bool horizontal { get; set; } = false;
            public override ScrollRect.ScrollbarVisibility verticalScrollbarVisibility { get; set; } = ScrollRect.ScrollbarVisibility.AutoHideAndExpandViewport;
            public override ScrollRect.ScrollbarVisibility horizontalScrollbarVisibility { get; set; } = ScrollRect.ScrollbarVisibility.AutoHideAndExpandViewport;
        }

        public class RectMask2DSetter : RectMask2DBSetter
        {

        }

        public class VerticalLayoutGroupSetter : VerticalLayoutGroupBSetter
        {
            public override float spacing { get; set; } = 10;
            public override RectOffset padding { get; set; } = new RectOffset(10, 10, 10, 10);
        }

        public class ContenSizeFilterSetter : ContentSizeFilterBSetter
        {
            public override ContentSizeFitter.FitMode horizontalFit { get; set; } = ContentSizeFitter.FitMode.Unconstrained;
            public override ContentSizeFitter.FitMode verticalFit { get; set; } = ContentSizeFitter.FitMode.PreferredSize;
        }

        public class VScrollbarSetter : ScrollbarBSetter<Selector>
        {
            public override Scrollbar.Direction direction { get; set; } = Scrollbar.Direction.BottomToTop;
        }

        public class VScrollbarBackImageSetter : ImageBSetter<Selector>
        {
            public override Color color { get; set; } = Color.gray;
        }

        public class VScrollbarHandleImageSetter : ImageBSetter<Selector>
        {
            public override Color color { get; set; } = new Color(0.3f, 0.3f, 0.3f, 1f);
        }

        public class HScrollbarSetter : ScrollbarBSetter<Selector>
        {

        }

        public class HScrollbarBackImageSetter : ImageBSetter<Selector>
        {
            public override Color color { get; set; } = Color.gray;
        }

        public class HScrollbarHandleImageSetter : ImageBSetter<Selector>
        {
            public override Color color { get; set; } = new Color(0.3f, 0.3f, 0.3f, 1f);
        }

        #endregion


        #region Static Funcs


        public static RectTransformSetter TableRectTransform(int xPad, int xCell, int yPad, int yCell)
        {
            // Validate values
            if ((xCell < 1) && (xCell > 100)) throw new FormatException("REpanel.TableRectTransform(): xCell(" + xCell + ") must be between 0 and 100");
            if ((yCell < 1) && (yCell > 100)) throw new FormatException("REpanel.TableRectTransform(): yCell(" + yCell + ") must be between 0 and 100");
            if ((xPad < 0) && (xPad > 99)) throw new FormatException("REpanel.TableRectTransform(): xPad(" + xPad + ") must be between 0 and 99");
            if ((yPad < 0) && (xPad > 99)) throw new FormatException("REpanel.TableRectTransform(): yPad(" + yPad + ") must be between 0 and 99");
            if (xCell < xPad) throw new FormatException("REpanel.TableRectTransform(): xCell(" + xCell + ") must be greater than xPad(" + xPad + ")");
            if (yCell < yPad) throw new FormatException("REpanel.TableRectTransform(): yCell(" + yCell + ") must be greater than yPad(" + yPad + ")");

            return new RectTransformSetter
            {
                anchorMin = new Vector2(xPad / 100f, yPad / 100f),
                anchorMax = new Vector2(xCell / 100f, yCell / 100f),
            };
        }

        public static Selector CastSelector(REbaseSelector selector)
        {
            try
            {
                return (Selector)selector;
            }
            catch (Exception)
            {
                Debug.Log("REpanelVertical: Cant cast selector");
                return null;
            }
        }

        public new static Selector[] Find(string pattern) => Find<Selector>(pattern);

        public new static Selector[] Find() => Find<Selector>();

        public new static Selector FindOne(string pattern) => FindOne<Selector>(pattern);


        #endregion Static Funcs


    }
}

