﻿#pragma checksum "C:\Users\80520\source\repos\ACOM\ACOMv2\Views\Settings\ThemeSettingPage.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "4FA5DF0C8F7FEB2D8D1EC7CEDFAE782EC1B5976C4BD4B77986E7353402E366A4"
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
    partial class ThemeSettingPage : 
        global::Microsoft.UI.Xaml.Controls.Page, 
        global::Microsoft.UI.Xaml.Markup.IComponentConnector
    {
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.UI.Xaml.Markup.Compiler"," 3.0.0.2411")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        private static class XamlBindingSetters
        {
            public static void Set_WinUICommunity_ThemeServiceAttach_ThemeService(global::Microsoft.UI.Xaml.DependencyObject obj, global::WinUICommunity.IThemeService value, string targetNullValue)
            {
                if (value == null && targetNullValue != null)
                {
                    value = (global::WinUICommunity.IThemeService) global::Microsoft.UI.Xaml.Markup.XamlBindingHelper.ConvertValue(typeof(global::WinUICommunity.IThemeService), targetNullValue);
                }
                global::WinUICommunity.ThemeServiceAttach.SetThemeService(obj, value);
            }
        };

        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.UI.Xaml.Markup.Compiler"," 3.0.0.2411")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        private partial class ThemeSettingPage_obj1_Bindings :
            global::Microsoft.UI.Xaml.Markup.IDataTemplateComponent,
            global::Microsoft.UI.Xaml.Markup.IXamlBindScopeDiagnostics,
            global::Microsoft.UI.Xaml.Markup.IComponentConnector,
            IThemeSettingPage_Bindings
        {
            private global::ACOMv2.Views.ThemeSettingPage dataRoot;
            private bool initialized = false;
            private const int NOT_PHASED = (1 << 31);
            private const int DATA_CHANGED = (1 << 30);

            // Fields for each control that has bindings.
            private global::Microsoft.UI.Xaml.Controls.ComboBox obj2;
            private global::Microsoft.UI.Xaml.Controls.ComboBox obj3;

            // Static fields for each binding's enabled/disabled state
            private static bool isobj2ThemeServiceDisabled = false;
            private static bool isobj3ThemeServiceDisabled = false;

            private ThemeSettingPage_obj1_BindingsTracking bindingsTracking;

            public ThemeSettingPage_obj1_Bindings()
            {
                this.bindingsTracking = new ThemeSettingPage_obj1_BindingsTracking(this);
            }

            public void Disable(int lineNumber, int columnNumber)
            {
                if (lineNumber == 34 && columnNumber == 27)
                {
                    isobj2ThemeServiceDisabled = true;
                }
                else if (lineNumber == 22 && columnNumber == 27)
                {
                    isobj3ThemeServiceDisabled = true;
                }
            }

            // IComponentConnector

            public void Connect(int connectionId, global::System.Object target)
            {
                switch(connectionId)
                {
                    case 2: // Views\Settings\ThemeSettingPage.xaml line 34
                        this.obj2 = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.ComboBox>(target);
                        break;
                    case 3: // Views\Settings\ThemeSettingPage.xaml line 22
                        this.obj3 = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.ComboBox>(target);
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

            // IThemeSettingPage_Bindings

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
                    this.dataRoot = global::WinRT.CastExtensions.As<global::ACOMv2.Views.ThemeSettingPage>(newDataRoot);
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
            private void Update_(global::ACOMv2.Views.ThemeSettingPage obj, int phase)
            {
                this.Update_ACOMv2_App_Current(global::ACOMv2.App.Current, phase);
            }
            private void Update_ACOMv2_App_Current(global::ACOMv2.App obj, int phase)
            {
                if (obj != null)
                {
                    if ((phase & (NOT_PHASED | (1 << 0))) != 0)
                    {
                        this.Update_ACOMv2_App_Current_GetThemeService(obj.GetThemeService, phase);
                    }
                }
            }
            private void Update_ACOMv2_App_Current_GetThemeService(global::WinUICommunity.IThemeService obj, int phase)
            {
                if ((phase & ((1 << 0) | NOT_PHASED )) != 0)
                {
                    // Views\Settings\ThemeSettingPage.xaml line 34
                    if (!isobj2ThemeServiceDisabled)
                    {
                        XamlBindingSetters.Set_WinUICommunity_ThemeServiceAttach_ThemeService(this.obj2, obj, null);
                    }
                    // Views\Settings\ThemeSettingPage.xaml line 22
                    if (!isobj3ThemeServiceDisabled)
                    {
                        XamlBindingSetters.Set_WinUICommunity_ThemeServiceAttach_ThemeService(this.obj3, obj, null);
                    }
                }
            }

            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.UI.Xaml.Markup.Compiler"," 3.0.0.2411")]
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            private class ThemeSettingPage_obj1_BindingsTracking
            {
                private global::System.WeakReference<ThemeSettingPage_obj1_Bindings> weakRefToBindingObj; 

                public ThemeSettingPage_obj1_BindingsTracking(ThemeSettingPage_obj1_Bindings obj)
                {
                    weakRefToBindingObj = new global::System.WeakReference<ThemeSettingPage_obj1_Bindings>(obj);
                }

                public ThemeSettingPage_obj1_Bindings TryGetBindingObject()
                {
                    ThemeSettingPage_obj1_Bindings bindingObject = null;
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
            case 1: // Views\Settings\ThemeSettingPage.xaml line 2
                {                    
                    global::Microsoft.UI.Xaml.Controls.Page element1 = (global::Microsoft.UI.Xaml.Controls.Page)target;
                    ThemeSettingPage_obj1_Bindings bindings = new ThemeSettingPage_obj1_Bindings();
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

