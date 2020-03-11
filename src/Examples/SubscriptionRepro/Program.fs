module Examples.CounterApp

open Avalonia
open Avalonia.Controls
open Elmish
open Avalonia.FuncUI.Components.Hosts
open Avalonia.FuncUI
open Avalonia.FuncUI.Elmish
open Avalonia.Controls.ApplicationLifetimes
open Avalonia.FuncUI.DSL

module Counter =
    type State = { count : int }

    let init = { count = 0 }

    type Add =
        | Add of State

    let update (msg : Add) (_state: State) : State =
        match msg with
        | Add state' ->
            { state' with count = state'.count + 1 }

    let view (state : State) dispatch =
        StackPanel.create [
            StackPanel.children [
                Grid.create [
                    Grid.children [
                        Button.create [
                            Button.content "+"
                            Button.onClick (fun _ -> dispatch (Add state))
                        ]
                    ]
                ]

                TextBlock.create [
                    TextBlock.text (string state.count)
                ]
            ]
        ]

    ()

type MainWindow() as this =
    inherit HostWindow()
    do
        base.Title <- "Counter Example"
        base.Height <- 400.0
        base.Width <- 400.0

        Elmish.Program.mkSimple (fun () -> Counter.init) Counter.update Counter.view
        |> Program.withHost this
        |> Program.withConsoleTrace
        |> Program.run

type App() =
    inherit Application()

    override this.Initialize() =
        this.Styles.Load "resm:Avalonia.Themes.Default.DefaultTheme.xaml?assembly=Avalonia.Themes.Default"
        this.Styles.Load "resm:Avalonia.Themes.Default.Accents.BaseDark.xaml?assembly=Avalonia.Themes.Default"

    override this.OnFrameworkInitializationCompleted() =
        match this.ApplicationLifetime with
        | :? IClassicDesktopStyleApplicationLifetime as desktopLifetime ->
            let mainWindow = MainWindow()
            desktopLifetime.MainWindow <- mainWindow
        | _ -> ()

module Program =

    [<EntryPoint>]
    let main(args: string[]) =
        AppBuilder
            .Configure<App>()
            .UsePlatformDetect()
            .UseSkia()
            .StartWithClassicDesktopLifetime(args)