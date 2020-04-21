﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Windows.Input;
using Xamarin.Forms;

namespace XamarinReactorUI
{
    public interface IRxMenuItem : IRxBaseMenuItem
    {
        bool IsDestructive { get; set; }
        ImageSource IconImageSource { get; set; }
        string Text { get; set; }
    }

    public class RxMenuItem<T> : RxBaseMenuItem<T>, IRxMenuItem where T : MenuItem, new()
    {
        public RxMenuItem()
        {
        }

        public RxMenuItem(Action<T> componentRefAction)
            : base(componentRefAction)
        {
        }

        public bool IsDestructive { get; set; } = (bool)MenuItem.IsDestructiveProperty.DefaultValue;
        public ImageSource IconImageSource { get; set; } = (ImageSource)MenuItem.IconImageSourceProperty.DefaultValue;
        public string Text { get; set; } = (string)MenuItem.TextProperty.DefaultValue;

        protected override void OnUpdate()
        {
            NativeControl.IsDestructive = IsDestructive;
            NativeControl.IconImageSource = IconImageSource;
            NativeControl.Text = Text;

            base.OnUpdate();
        }

        protected override IEnumerable<VisualNode> RenderChildren()
        {
            yield break;
        }
    }

    public class RxMenuItem : RxMenuItem<MenuItem>
    {
        public RxMenuItem()
        {
        }

        public RxMenuItem(Action<MenuItem> componentRefAction)
            : base(componentRefAction)
        {
        }

    }

    public static class RxMenuItemExtensions
    {
        public static T IsDestructive<T>(this T menuitem, bool isDestructive) where T : IRxMenuItem
        {
            menuitem.IsDestructive = isDestructive;
            return menuitem;
        }

        public static T IconImageSource<T>(this T menuitem, ImageSource iconImageSource) where T : IRxMenuItem
        {
            menuitem.IconImageSource = iconImageSource;
            return menuitem;
        }

        public static T IconImage<T>(this T menuitem, string file) where T : IRxMenuItem
        {
            menuitem.IconImageSource = ImageSource.FromFile(file);
            return menuitem;
        }

        public static T IconImage<T>(this T menuitem, string fileAndroid, string fileiOS) where T : IRxMenuItem
        {
            menuitem.IconImageSource = Device.RuntimePlatform == Device.Android ? ImageSource.FromFile(fileAndroid) : ImageSource.FromFile(fileiOS);
            return menuitem;
        }

        public static T IconImage<T>(this T menuitem, string resourceName, Assembly sourceAssembly) where T : IRxMenuItem
        {
            menuitem.IconImageSource = ImageSource.FromResource(resourceName, sourceAssembly);
            return menuitem;
        }

        public static T IconImage<T>(this T menuitem, Uri imageUri) where T : IRxMenuItem
        {
            menuitem.IconImageSource = ImageSource.FromUri(imageUri);
            return menuitem;
        }

        public static T IconImage<T>(this T menuitem, Uri imageUri, bool cachingEnabled, TimeSpan cacheValidity) where T : IRxMenuItem
        {
            menuitem.IconImageSource = new UriImageSource
            {
                Uri = imageUri,
                CachingEnabled = cachingEnabled,
                CacheValidity = cacheValidity
            };
            return menuitem;
        }

        public static T IconImage<T>(this T menuitem, Func<Stream> imageStream) where T : IRxMenuItem
        {
            menuitem.IconImageSource = ImageSource.FromStream(imageStream);
            return menuitem;
        }

        public static T Text<T>(this T menuitem, string text) where T : IRxMenuItem
        {
            menuitem.Text = text;
            return menuitem;
        }
    }
}