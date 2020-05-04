﻿using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using XamarinReactorUI.Internals;

namespace XamarinReactorUI
{
    public interface IRxView : IRxVisualElement
    {
        LayoutOptions HorizontalOptions { get; set; }
        LayoutOptions VerticalOptions { get; set; }
        Thickness Margin { get; set; }

        Action OnTappedAction { get; set; }
        Action OnDoubleTappedAction { get; set; }
    }

    public abstract class RxView<T> : RxVisualElement<T>, IRxView where T : Xamarin.Forms.View, new()
    {
        private TapGestureRecognizer _tapGestureRecognizer;
        private TapGestureRecognizer _doubleTapGestureRecognizer;

        protected RxView()
        {
        }

        protected RxView(Action<T> componentRefAction)
            : base(componentRefAction)
        { 
        
        }

        public LayoutOptions HorizontalOptions { get; set; } = (LayoutOptions)View.HorizontalOptionsProperty.DefaultValue;
        
        public LayoutOptions VerticalOptions { get; set; } = (LayoutOptions)View.VerticalOptionsProperty.DefaultValue;
        
        public Thickness Margin { get; set; } = (Thickness)View.MarginProperty.DefaultValue;

        public Action OnTappedAction { get; set; }
        public Action OnDoubleTappedAction { get; set; }

        protected override void OnUpdate()
        {
            NativeControl.VerticalOptions = VerticalOptions;
            NativeControl.HorizontalOptions = HorizontalOptions;
            NativeControl.Margin = Margin;

            NativeControl.GestureRecognizers.Clear();

            if (OnTappedAction != null)
            {
                NativeControl.GestureRecognizers.Add(_tapGestureRecognizer = new TapGestureRecognizer()
                {
                    Command = new ActionCommand(OnTappedAction)
                });
            }

            if (OnDoubleTappedAction != null)
            {
                NativeControl.GestureRecognizers.Add(_doubleTapGestureRecognizer = new TapGestureRecognizer()
                {
                    Command = new ActionCommand(OnDoubleTappedAction),
                    NumberOfTapsRequired = 2
                });
            }

            base.OnUpdate();
        }

        protected override void OnMigrated(VisualNode newNode)
        {
            if (NativeControl != null && _tapGestureRecognizer != null)
                NativeControl.GestureRecognizers.Remove(_tapGestureRecognizer);
            if (NativeControl != null && _doubleTapGestureRecognizer != null)
                NativeControl.GestureRecognizers.Remove(_doubleTapGestureRecognizer);

            base.OnMigrated(newNode);
        }

        protected override void OnUnmount()
        {
            if (NativeControl != null && _tapGestureRecognizer != null)
                NativeControl.GestureRecognizers.Remove(_tapGestureRecognizer);
            if (NativeControl != null && _doubleTapGestureRecognizer != null)
                NativeControl.GestureRecognizers.Remove(_doubleTapGestureRecognizer);

            base.OnUnmount();
        }
    }

    public static class RxViewExtensions
    {
        public static T OnTapped<T>(this T view, Action action) where T : IRxView
        {
            view.OnTappedAction = action;
            return view;
        }

        public static T OnDoubleTapped<T>(this T view, Action action) where T : IRxView
        {
            view.OnDoubleTappedAction = action;
            return view;
        }

        public static T Margin<T>(this T view, Thickness margin) where T : IRxView
        {
            view.Margin = margin;
            return view;
        }

        public static T Margin<T>(this T view, double left, double top, double right, double bottom) where T : IRxView
        {
            view.Margin = new Thickness(left, top, right, bottom);
            return view;
        }

        public static T Margin<T>(this T view, double uniform) where T : IRxView
        {
            view.Margin = new Thickness(uniform);
            return view;
        }

        public static T Margin<T>(this T view, double leftRight, double topBottom) where T : IRxView
        {
            view.Margin = new Thickness(leftRight, topBottom);
            return view;
        }

        public static T HorizontalOptions<T>(this T view, LayoutOptions layoutOptions) where T : IRxView
        {
            view.HorizontalOptions = layoutOptions;
            return view;
        }

        public static T HStart<T>(this T view) where T : IRxView
        {
            view.HorizontalOptions = LayoutOptions.Start;
            return view;
        }

        public static T HCenter<T>(this T view) where T : IRxView
        {
            view.HorizontalOptions = LayoutOptions.Center;
            return view;
        }

        public static T HEnd<T>(this T view) where T : IRxView
        {
            view.HorizontalOptions = LayoutOptions.End;
            return view;
        }

        public static T HFill<T>(this T view) where T : IRxView
        {
            view.HorizontalOptions = LayoutOptions.Fill;
            return view;
        }
        public static T HStartAndExpand<T>(this T view) where T : IRxView
        {
            view.HorizontalOptions = LayoutOptions.StartAndExpand;
            return view;
        }
        public static T HCenterAndExpand<T>(this T view) where T : IRxView
        {
            view.HorizontalOptions = LayoutOptions.CenterAndExpand;
            return view;
        }
        public static T HEndAndExpand<T>(this T view) where T : IRxView
        {
            view.HorizontalOptions = LayoutOptions.EndAndExpand;
            return view;
        }
        public static T HFillAndExpand<T>(this T view) where T : IRxView
        {
            view.HorizontalOptions = LayoutOptions.FillAndExpand;
            return view;
        }

        public static T VerticalOptions<T>(this T view, LayoutOptions layoutOptions) where T : IRxView
        {
            view.VerticalOptions = layoutOptions;
            return view;
        }

        public static T VStart<T>(this T view) where T : IRxView
        {
            view.VerticalOptions = LayoutOptions.Start;
            return view;
        }

        public static T VCenter<T>(this T view) where T : IRxView
        {
            view.VerticalOptions = LayoutOptions.Center;
            return view;
        }

        public static T VEnd<T>(this T view) where T : IRxView
        {
            view.VerticalOptions = LayoutOptions.End;
            return view;
        }

        public static T VFill<T>(this T view) where T : IRxView
        {
            view.VerticalOptions = LayoutOptions.Fill;
            return view;
        }
        public static T VStartAndExpand<T>(this T view) where T : IRxView
        {
            view.VerticalOptions = LayoutOptions.StartAndExpand;
            return view;
        }
        public static T VCenterAndExpand<T>(this T view) where T : IRxView
        {
            view.VerticalOptions = LayoutOptions.CenterAndExpand;
            return view;
        }
        public static T VEndAndExpand<T>(this T view) where T : IRxView
        {
            view.VerticalOptions = LayoutOptions.EndAndExpand;
            return view;
        }
        public static T VFillAndExpand<T>(this T view) where T : IRxView
        {
            view.VerticalOptions = LayoutOptions.FillAndExpand;
            return view;
        }

    }
}
