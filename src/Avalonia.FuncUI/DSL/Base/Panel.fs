namespace Avalonia.FuncUI.DSL

[<AutoOpen>]
module Panel =
    open Avalonia.Controls
    open Avalonia.FuncUI.Types
    open Avalonia.FuncUI.Builder
    open Avalonia.Media.Immutable
    open Avalonia.Media

    let create (attrs: IAttr<Panel> list): IView<Panel> =
        ViewBuilder.Create<Panel>(attrs)

    type Panel with
        static member children<'t when 't :> Panel>(value: IView list) : IAttr<'t> =
            let getter : ('t -> obj) = (fun control -> control.Children :> obj)

            AttrBuilder<'t>.CreateContentMultiple("Children", ValueSome getter, ValueNone, value)

        static member background<'t when 't :> Panel>(value: IBrush) : IAttr<'t> =
            AttrBuilder<'t>.CreateProperty<IBrush>(Panel.BackgroundProperty, value, ValueNone)

        static member background<'t when 't :> Panel>(color: string) : IAttr<'t> =
            color |> Color.Parse |> ImmutableSolidColorBrush |> Panel.background

        static member background<'t when 't :> Panel>(color: Color) : IAttr<'t> =
            color |> ImmutableSolidColorBrush |> Panel.background