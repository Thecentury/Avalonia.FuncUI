namespace Avalonia.FuncUI

open System.Diagnostics.CodeAnalysis
open Avalonia.FuncUI
open Avalonia.FuncUI.Types

[<AllowNullLiteral>]
[<DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)>]
type Component (render: IComponentContext -> IView) =
    inherit ComponentBase ()

    override _.Render ctx =
        render ctx

type Component with

    static member create(key: string, render: IComponentContext -> IView) : IView<Component> =
        { View.ViewType = typeof<Component>
          View.ViewKey = ValueSome key
          View.Attrs = list.Empty
          View.Outlet = ValueNone
          View.ConstructorArgs = [| render :> obj |] }
        :> IView<Component>