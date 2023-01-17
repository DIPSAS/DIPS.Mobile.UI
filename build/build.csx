#r "nuget:dotnet-steps, 0.0.2"

Step step1 = () => WriteLine("Build ran!");

Step step2 = () => WriteLine("Build ran!");

var args = Args;
if(args.Count() == 0){
    WriteLine("Please select steps to run:");
    var input = ReadLine();
    args = input.Split(' ');
}

await ExecuteSteps(args);
