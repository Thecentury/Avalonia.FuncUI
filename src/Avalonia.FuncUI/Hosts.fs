namespace Avalonia.FuncUI.Hosts

open Avalonia.Controls
open Avalonia.FuncUI
open Avalonia.Styling
open Avalonia.FuncUI.Types
open Avalonia.FuncUI.VirtualDom

type IViewHost =
    abstract member Update: IView option -> unit

type HostWindow() as this =
    inherit Window()

    let mutable lastViewElement : IView option = None

    let update (nextViewElement : IView option) : unit =
        use _ = Performance.step "Diff and patch"
        VirtualDom.updateRoot (this, lastViewElement, nextViewElement)
        lastViewElement <- nextViewElement

    interface IViewHost with
        member this.Update next =
            update next

type HostControl() as this =
    inherit ContentControl()

    let mutable lastViewElement : IView option = None

    let update (nextViewElement : IView option) : unit =
        use _ = Performance.step "Diff and patch"
        VirtualDom.updateRoot (this, lastViewElement, nextViewElement)
        lastViewElement <- nextViewElement

    override this.StyleKeyOverride = typeof<ContentControl>

    interface IViewHost with
        member this.Update next =
            update next