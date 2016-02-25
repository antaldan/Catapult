#r "tools/FAKE/tools/FakeLib.dll" // include Fake lib
open Fake 

let buildDir = "./artifacts"
let now = System.DateTime.UtcNow
let buildNumber = sprintf "1.%i.%s.%s" now.Year (now.ToString("MMdd")) (now.ToString("HHmm"))

Target "Clean" (fun _ ->
    CleanDirs [ buildDir ]
)

Target "Build" (fun _ ->
    MSBuildReleaseExt null [("ApplicationVersion", buildNumber)] "Build" ["Source/AlphaLaunch.App.sln"]
        |> Log "AppBuild-Output: "
)

Target "Publish" (fun _ ->
    MSBuildReleaseExt buildDir [("ApplicationVersion", buildNumber); ("PublishUrl", "http://alphalaunch.rasmuskl.dk/setup/")] "Publish" ["Source/AlphaLaunch.App.sln"]
        |> Log "AppPublish-Output: "
)

Target "Deploy" (fun _ ->
    trace "Heavy deploy action"
)

"Clean"
==> "Build"
==> "Publish"
==> "Deploy"

Run "Deploy"