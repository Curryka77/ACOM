﻿#pragma checksum "C:\Users\80520\source\repos\ACOM\ACOMv2\Views\MainPage.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "18AC5FC5D083946E0E264EC79BBF333F6DB7B4A96FC8CBEC58E654714D28478F"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ACOMv2.Views
{
    partial class MainPage : 
        global::Microsoft.UI.Xaml.Controls.Page, 
        global::Microsoft.UI.Xaml.Markup.IComponentConnector
    {
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.UI.Xaml.Markup.Compiler"," 3.0.0.2411")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        private static class XamlBindingSetters
        {
            public static void Set_WinUICommunity_TitleBar_Title(global::WinUICommunity.TitleBar obj, global::System.String value, string targetNullValue)
            {
                if (value == null && targetNullValue != null)
                {
                    value = targetNullValue;
                }
                obj.Title = value ?? global::System.String.Empty;
            }
            public static void Set_WinUICommunity_TitleBar_Subtitle(global::WinUICommunity.TitleBar obj, global::System.String value, string targetNullValue)
            {
                if (value == null && targetNullValue != null)
                {
                    value = targetNullValue;
                }
                obj.Subtitle = value ?? global::System.String.Empty;
            }
            public static void Set_CommunityToolkit_WinUI_Controls_PropertySizer_Binding(global::CommunityToolkit.WinUI.Controls.PropertySizer obj, global::System.Double value)
            {
                obj.Binding = value;
            }
            public static void Set_Microsoft_UI_Xaml_UIElement_Visibility(global::Microsoft.UI.Xaml.UIElement obj, global::Microsoft.UI.Xaml.Visibility value)
            {
                obj.Visibility = value;
            }
        };

        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.UI.Xaml.Markup.Compiler"," 3.0.0.2411")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        private partial class MainPage_obj1_Bindings :
            global::Microsoft.UI.Xaml.Markup.IDataTemplateComponent,
            global::Microsoft.UI.Xaml.Markup.IXamlBindScopeDiagnostics,
            global::Microsoft.UI.Xaml.Markup.IComponentConnector,
            IMainPage_Bindings
        {
            private global::ACOMv2.Views.MainPage dataRoot;
            private bool initialized = false;
            private const int NOT_PHASED = (1 << 31);
            private const int DATA_CHANGED = (1 << 30);

            // Fields for each control that has bindings.
            private global::WinUICommunity.TitleBar obj2;
            private global::CommunityToolkit.WinUI.Controls.PropertySizer obj5;

            // Static fields for each binding's enabled/disabled state
            private static bool isobj2TitleDisabled = false;
            private static bool isobj2SubtitleDisabled = false;
            private static bool isobj5BindingDisabled = false;
            private static bool isobj5VisibilityDisabled = false;

            private MainPage_obj1_BindingsTracking bindingsTracking;

            public MainPage_obj1_Bindings()
            {
                this.bindingsTracking = new MainPage_obj1_BindingsTracking(this);
            }

            public void Disable(int lineNumber, int columnNumber)
            {
                if (lineNumber == 16 && columnNumber == 23)
                {
                    isobj2TitleDisabled = true;
                }
                else if (lineNumber == 22 && columnNumber == 23)
                {
                    isobj2SubtitleDisabled = true;
                }
                else if (lineNumber == 58 && columnNumber == 29)
                {
                    isobj5BindingDisabled = true;
                }
                else if (lineNumber == 60 && columnNumber == 29)
                {
                    isobj5VisibilityDisabled = true;
                }
            }

            // IComponentConnector

            public void Connect(int connectionId, global::System.Object target)
            {
                switch(connectionId)
                {
                    case 2: // Views\MainPage.xaml line 15
                        this.obj2 = global::WinRT.CastExtensions.As<global::WinUICommunity.TitleBar>(target);
                        break;
                    case 5: // Views\MainPage.xaml line 57
                        this.obj5 = global::WinRT.CastExtensions.As<global::CommunityToolkit.WinUI.Controls.PropertySizer>(target);
                        this.bindingsTracking.RegisterTwoWayListener_5(this.obj5);
                        break;
                    default:
                        break;
                }
            }
                        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.UI.Xaml.Markup.Compiler"," 3.0.0.2411")]
                        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
                        public global::Microsoft.UI.Xaml.Markup.IComponentConnector GetBindingConnector(int connectionId, object target) 
                        {
                            return null;
                        }

            // IDataTemplateComponent

            public void ProcessBindings(global::System.Object item, int itemIndex, int phase, out int nextPhase)
            {
                nextPhase = -1;
            }

            public void Recycle()
            {
                return;
            }

            // IMainPage_Bindings

            public void Initialize()
            {
                if (!this.initialized)
                {
                    this.Update();
                }
            }
            
            public void Update()
            {
                this.Update_(this.dataRoot, NOT_PHASED);
                this.initialized = true;
            }

            public void StopTracking()
            {
                this.bindingsTracking.ReleaseAllListeners();
                this.initialized = false;
            }

            public void DisconnectUnloadedObject(int connectionId)
            {
                throw new global::System.ArgumentException("No unloadable elements to disconnect.");
            }

            public bool SetDataRoot(global::System.Object newDataRoot)
            {
                this.bindingsTracking.ReleaseAllListeners();
                if (newDataRoot != null)
                {
                    this.dataRoot = global::WinRT.CastExtensions.As<global::ACOMv2.Views.MainPage>(newDataRoot);
                    return true;
                }
                return false;
            }

            public void Activated(object obj, global::Microsoft.UI.Xaml.WindowActivatedEventArgs data)
            {
                this.Initialize();
            }

            public void Loading(global::Microsoft.UI.Xaml.FrameworkElement src, object data)
            {
                this.Initialize();
            }

            // Update methods for each path node used in binding steps.
            private void Update_(global::ACOMv2.Views.MainPage obj, int phase)
            {
                if (obj != null)
                {
                    if ((phase & (NOT_PHASED | DATA_CHANGED | (1 << 0))) != 0)
                    {
                        this.Update_NavView(obj.NavView, phase);
                    }
                }
                this.Update_WinUICommunity_ProcessInfoHelper_ProductName(global::WinUICommunity.ProcessInfoHelper.ProductName, phase);
                this.Update_WinUICommunity_ProcessInfoHelper_VersionWithPrefix(global::WinUICommunity.ProcessInfoHelper.VersionWithPrefix, phase);
            }
            private void Update_WinUICommunity_ProcessInfoHelper_ProductName(global::System.String obj, int phase)
            {
                if ((phase & ((1 << 0) | NOT_PHASED )) != 0)
                {
                    // Views\MainPage.xaml line 15
                    if (!isobj2TitleDisabled)
                    {
                        XamlBindingSetters.Set_WinUICommunity_TitleBar_Title(this.obj2, obj, null);
                    }
                }
            }
            private void Update_WinUICommunity_ProcessInfoHelper_VersionWithPrefix(global::System.String obj, int phase)
            {
                if ((phase & ((1 << 0) | NOT_PHASED )) != 0)
                {
                    // Views\MainPage.xaml line 15
                    if (!isobj2SubtitleDisabled)
                    {
                        XamlBindingSetters.Set_WinUICommunity_TitleBar_Subtitle(this.obj2, obj, null);
                    }
                }
            }
            private void Update_NavView(global::Microsoft.UI.Xaml.Controls.NavigationView obj, int phase)
            {
                this.bindingsTracking.UpdateChildListeners_NavView(obj);
                if (obj != null)
                {
                    if ((phase & (NOT_PHASED | DATA_CHANGED | (1 << 0))) != 0)
                    {
                        this.Update_NavView_OpenPaneLength(obj.OpenPaneLength, phase);
                        this.Update_NavView_IsPaneOpen(obj.IsPaneOpen, phase);
                    }
                }
            }
            private void Update_NavView_OpenPaneLength(global::System.Double obj, int phase)
            {
                if ((phase & ((1 << 0) | NOT_PHASED | DATA_CHANGED)) != 0)
                {
                    // Views\MainPage.xaml line 57
                    if (!isobj5BindingDisabled)
                    {
                        XamlBindingSetters.Set_CommunityToolkit_WinUI_Controls_PropertySizer_Binding(this.obj5, obj);
                    }
                }
            }
            private void Update_NavView_IsPaneOpen(global::System.Boolean obj, int phase)
            {
                if ((phase & (NOT_PHASED | DATA_CHANGED | (1 << 0))) != 0)
                {
                    this.Update_NavView_IsPaneOpen_Cast_IsPaneOpen_To_Visibility(obj ? global::Microsoft.UI.Xaml.Visibility.Visible : global::Microsoft.UI.Xaml.Visibility.Collapsed, phase);
                }
            }
            private void Update_NavView_IsPaneOpen_Cast_IsPaneOpen_To_Visibility(global::Microsoft.UI.Xaml.Visibility obj, int phase)
            {
                if ((phase & ((1 << 0) | NOT_PHASED | DATA_CHANGED)) != 0)
                {
                    // Views\MainPage.xaml line 57
                    if (!isobj5VisibilityDisabled)
                    {
                        XamlBindingSetters.Set_Microsoft_UI_Xaml_UIElement_Visibility(this.obj5, obj);
                    }
                }
            }
            private void UpdateTwoWay_5_Binding()
            {
                if (this.initialized)
                {
                    if (this.dataRoot != null)
                    {
                        if (this.dataRoot.NavView != null)
                        {
                            this.dataRoot.NavView.OpenPaneLength = this.obj5.Binding;
                        }
                    }
                }
            }

            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.UI.Xaml.Markup.Compiler"," 3.0.0.2411")]
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            private class MainPage_obj1_BindingsTracking
            {
                private global::System.WeakReference<MainPage_obj1_Bindings> weakRefToBindingObj; 

                public MainPage_obj1_BindingsTracking(MainPage_obj1_Bindings obj)
                {
                    weakRefToBindingObj = new global::System.WeakReference<MainPage_obj1_Bindings>(obj);
                }

                public MainPage_obj1_Bindings TryGetBindingObject()
                {
                    MainPage_obj1_Bindings bindingObject = null;
                    if (weakRefToBindingObj != null)
                    {
                        weakRefToBindingObj.TryGetTarget(out bindingObject);
                        if (bindingObject == null)
                        {
                            weakRefToBindingObj = null;
                            ReleaseAllListeners();
                        }
                    }
                    return bindingObject;
                }

                public void ReleaseAllListeners()
                {
                    UpdateChildListeners_NavView(null);
                }

                public void DependencyPropertyChanged_NavView_OpenPaneLength(global::Microsoft.UI.Xaml.DependencyObject sender, global::Microsoft.UI.Xaml.DependencyProperty prop)
                {
                    MainPage_obj1_Bindings bindings = TryGetBindingObject();
                    if (bindings != null)
                    {
                        global::Microsoft.UI.Xaml.Controls.NavigationView obj = sender as global::Microsoft.UI.Xaml.Controls.NavigationView;
                        if (obj != null)
                        {
                            bindings.Update_NavView_OpenPaneLength(obj.OpenPaneLength, DATA_CHANGED);
                        }
                    }
                }
                public void DependencyPropertyChanged_NavView_IsPaneOpen(global::Microsoft.UI.Xaml.DependencyObject sender, global::Microsoft.UI.Xaml.DependencyProperty prop)
                {
                    MainPage_obj1_Bindings bindings = TryGetBindingObject();
                    if (bindings != null)
                    {
                        global::Microsoft.UI.Xaml.Controls.NavigationView obj = sender as global::Microsoft.UI.Xaml.Controls.NavigationView;
                        if (obj != null)
                        {
                            bindings.Update_NavView_IsPaneOpen(obj.IsPaneOpen, DATA_CHANGED);
                        }
                    }
                }
                private global::Microsoft.UI.Xaml.Controls.NavigationView cache_NavView = null;
                private long tokenDPC_NavView_OpenPaneLength = 0;
                private long tokenDPC_NavView_IsPaneOpen = 0;
                public void UpdateChildListeners_NavView(global::Microsoft.UI.Xaml.Controls.NavigationView obj)
                {
                    if (obj != cache_NavView)
                    {
                        if (cache_NavView != null)
                        {
                            cache_NavView.UnregisterPropertyChangedCallback(global::Microsoft.UI.Xaml.Controls.NavigationView.OpenPaneLengthProperty, tokenDPC_NavView_OpenPaneLength);
                            cache_NavView.UnregisterPropertyChangedCallback(global::Microsoft.UI.Xaml.Controls.NavigationView.IsPaneOpenProperty, tokenDPC_NavView_IsPaneOpen);
                            cache_NavView = null;
                        }
                        if (obj != null)
                        {
                            cache_NavView = obj;
                            tokenDPC_NavView_OpenPaneLength = obj.RegisterPropertyChangedCallback(global::Microsoft.UI.Xaml.Controls.NavigationView.OpenPaneLengthProperty, DependencyPropertyChanged_NavView_OpenPaneLength);
                            tokenDPC_NavView_IsPaneOpen = obj.RegisterPropertyChangedCallback(global::Microsoft.UI.Xaml.Controls.NavigationView.IsPaneOpenProperty, DependencyPropertyChanged_NavView_IsPaneOpen);
                        }
                    }
                }
                public void RegisterTwoWayListener_5(global::CommunityToolkit.WinUI.Controls.PropertySizer sourceObject)
                {
                    sourceObject.RegisterPropertyChangedCallback(global::CommunityToolkit.WinUI.Controls.PropertySizer.BindingProperty, (sender, prop) =>
                    {
                        var bindingObj = this.TryGetBindingObject();
                        if (bindingObj != null)
                        {
                            bindingObj.UpdateTwoWay_5_Binding();
                        }
                    });
                }
            }
        }

        /// <summary>
        /// Connect()
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.UI.Xaml.Markup.Compiler"," 3.0.0.2411")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 2: // Views\MainPage.xaml line 15
#pragma warning disable 0618  //   Warning on Deprecated usage
                {
                    this.AppTitleBar = global::WinRT.CastExtensions.As<global::WinUICommunity.TitleBar>(target);
                    ((global::WinUICommunity.TitleBar)this.AppTitleBar).BackRequested += this.AppTitleBar_BackRequested;
                    ((global::WinUICommunity.TitleBar)this.AppTitleBar).PaneToggleRequested += this.AppTitleBar_PaneToggleRequested;
#pragma warning restore 0618
                }
                break;
            case 3: // Views\MainPage.xaml line 44
                {
                    this.NavView = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.NavigationView>(target);
                }
                break;
            case 4: // Views\MainPage.xaml line 49
                {
                    this.JsonBreadCrumbNavigator = global::WinRT.CastExtensions.As<global::WinUICommunity.BreadcrumbNavigator>(target);
                }
                break;
            case 6: // Views\MainPage.xaml line 61
                {
                    this.NavFrame = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.Frame>(target);
                    ((global::Microsoft.UI.Xaml.Controls.Frame)this.NavFrame).Navigated += this.NavFrame_Navigated;
                }
                break;
            case 7: // Views\MainPage.xaml line 24
                {
                    global::Microsoft.UI.Xaml.Controls.AutoSuggestBox element7 = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.AutoSuggestBox>(target);
                    ((global::Microsoft.UI.Xaml.Controls.AutoSuggestBox)element7).QuerySubmitted += this.OnQuerySubmitted;
                    ((global::Microsoft.UI.Xaml.Controls.AutoSuggestBox)element7).TextChanged += this.OnTextChanged;
                }
                break;
            case 8: // Views\MainPage.xaml line 32
                {
                    this.ThemeButton = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.Button>(target);
                    ((global::Microsoft.UI.Xaml.Controls.Button)this.ThemeButton).Click += this.ThemeButton_Click;
                }
                break;
            default:
                break;
            }
            this._contentLoaded = true;
        }


        /// <summary>
        /// GetBindingConnector(int connectionId, object target)
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.UI.Xaml.Markup.Compiler"," 3.0.0.2411")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public global::Microsoft.UI.Xaml.Markup.IComponentConnector GetBindingConnector(int connectionId, object target)
        {
            global::Microsoft.UI.Xaml.Markup.IComponentConnector returnValue = null;
            switch(connectionId)
            {
            case 1: // Views\MainPage.xaml line 2
                {                    
                    global::Microsoft.UI.Xaml.Controls.Page element1 = (global::Microsoft.UI.Xaml.Controls.Page)target;
                    MainPage_obj1_Bindings bindings = new MainPage_obj1_Bindings();
                    returnValue = bindings;
                    bindings.SetDataRoot(this);
                    this.Bindings = bindings;
                    element1.Loading += bindings.Loading;
                    global::Microsoft.UI.Xaml.Markup.XamlBindingHelper.SetDataTemplateComponent(element1, bindings);
                }
                break;
            }
            return returnValue;
        }
    }
}

