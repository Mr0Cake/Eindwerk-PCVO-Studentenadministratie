﻿#pragma checksum "..\..\..\..\Pages\Ticketing\Tickets.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "08B1D8C1773363B00D9768D58185D87F"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using MahApps.Metro.Controls;
using StudentenAdministratieApp.Pages.Ticketing;
using StudentenAdministratieApp.ViewModel.Ticketing;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace StudentenAdministratieApp.Pages.Ticketing {
    
    
    /// <summary>
    /// Tickets
    /// </summary>
    public partial class Tickets : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 93 "..\..\..\..\Pages\Ticketing\Tickets.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid tInschrijving;
        
        #line default
        #line hidden
        
        
        #line 116 "..\..\..\..\Pages\Ticketing\Tickets.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid tAanwezigheid;
        
        #line default
        #line hidden
        
        
        #line 196 "..\..\..\..\Pages\Ticketing\Tickets.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock inschrijving;
        
        #line default
        #line hidden
        
        
        #line 241 "..\..\..\..\Pages\Ticketing\Tickets.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid tLocked;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/StudentenAdministratieApp;component/pages/ticketing/tickets.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\Pages\Ticketing\Tickets.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.tInschrijving = ((System.Windows.Controls.Grid)(target));
            return;
            case 2:
            this.tAanwezigheid = ((System.Windows.Controls.Grid)(target));
            return;
            case 3:
            this.inschrijving = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 4:
            this.tLocked = ((System.Windows.Controls.Grid)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

