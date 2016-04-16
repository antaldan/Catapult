#r "tools/FAKE/tools/FakeLib.dll" // include Fake lib
open Fake 

let artifactsDir = "./artifacts/"
let publishBuildDir = artifactsDir + "publish-build/"
let publishUploadDir = FullName (artifactsDir) + "publish-upload/"

let now = System.DateTime.UtcNow
let buildNumber = sprintf "1.%i.%s.%s" now.Year (now.ToString("MMdd")) (now.ToString("HHmm"))

Target "Clean" (fun _ ->
    CleanDirs [ artifactsDir ]
)

Target "Build" (fun _ ->
    MSBuildReleaseExt null [("ApplicationVersion", buildNumber)] "Build" ["Source/Catapult.sln"]
        |> Log "AppBuild-Output: "
)

Target "Publish" (fun _ ->
    MSBuildReleaseExt publishBuildDir [("ApplicationVersion", buildNumber); ("PublishUrl", "http://catapult.rasmuskl.dk/setup/"); ("PublishDir", publishUploadDir)] "Publish" ["Source/Catapult.sln"]
        |> Log "AppPublish-Output: "
)

let replacePublish = replace (FullName publishUploadDir) ""
let replaceBackslashes = replace "\\" "/"

let curlUpload(path:string) = (execProcess (fun info ->
    info.FileName <- "./tools/curl/curl.exe"
    info.Arguments <- "\"ftp://catapult.rasmuskl.dk/catapult/setup/" +  (replaceBackslashes (replacePublish path)) + "\" --ssl --insecure --ftp-create-dirs --user " + (environVarOrFail "CATAPULT_FTP")+ " -T \"" + path + "\""
)(System.TimeSpan.FromMinutes 1.))

Target "Deploy" (fun _ ->
    !! "**\*.*" |> SetBaseDir publishUploadDir |> Seq.map curlUpload |> Seq.toArray |> ignore
)

"Clean"
==> "Build"
==> "Publish"
==> "Deploy"

Run "Deploy"