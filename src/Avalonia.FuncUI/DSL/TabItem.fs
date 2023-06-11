namespace Avalonia.FuncUI.DSL

open System

[<AutoOpen>]
module TabItem =
    open Avalonia.Controls
    open Avalonia.FuncUI.Types
    open Avalonia.FuncUI.Builder

    let create (attrs: IAttr<TabItem> list): IView<TabItem> =
        ViewBuilder.Create<TabItem>(attrs)

    type TabItem with

        static member tabStripPlacement<'t when 't :> TabItem>(placement: Nullable<Dock>) : IAttr<'t> =
            AttrBuilder<'t>.CreateProperty<Nullable<Dock>>(TabItem.TabStripPlacementProperty, placement, ValueNone)

        static member tabStripPlacement<'t when 't :> TabItem>(placement: Dock) : IAttr<'t> =
            AttrBuilder<'t>.CreateProperty<Nullable<Dock>>(TabItem.TabStripPlacementProperty, Nullable placement, ValueNone)

        static member isSelected<'t when 't :> TabItem>(value: bool) : IAttr<'t> =
            AttrBuilder<'t>.CreateProperty<bool>(TabItem.IsSelectedProperty, value, ValueNone)

        static member onIsSelectedChanged<'t when 't :> TabItem>(func: bool -> unit, ?subPatchOptions) =
            AttrBuilder<'t>.CreateSubscription<bool>(TabItem.IsSelectedProperty, func, ?subPatchOptions = subPatchOptions)