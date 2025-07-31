module Avalonia.FuncUI.Performance

open System
open System.Collections.Concurrent
open System.Diagnostics
open System.Timers

// todo mikbri regular dictionary?
let private measurements = ConcurrentDictionary<string, float>()

let addMeasurement stepName value =
  measurements.AddOrUpdate (stepName, value, Func<_, _, _>(fun _ current -> current + value)) |> ignore

let step stepName =
  let time = Stopwatch.StartNew ()
  { new IDisposable with
      member _.Dispose () = addMeasurement stepName (float time.ElapsedMilliseconds) }

let private timer = new Timer(TimeSpan.FromSeconds 1)

let private report log =
  let report = measurements.ToArray()
  measurements.Clear()

  if report.Length > 0 then
    let report =
      report
      |> Seq.sortByDescending _.Value
      |> Seq.map (fun kvp -> $"{kvp.Key}: {kvp.Value}")
      |> String.concat Environment.NewLine
    log $"Profiling:\n{report}"

let start log =
  timer.AutoReset <- true
  timer.Enabled <- true
  timer.Elapsed.Add (fun _ -> report log)
  timer.Start ()