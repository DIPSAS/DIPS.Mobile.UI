/*********************************************************************************
    The MIT License (MIT)
    Copyright (c) 2018 bernhard.richter@gmail.com
    Permission is hereby granted, free of charge, to any person obtaining a copy
    of this software and associated documentation files (the "Software"), to deal
    in the Software without restriction, including without limitation the rights
    to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
    copies of the Software, and to permit persons to whom the Software is
    furnished to do so, subject to the following conditions:
    The above copyright notice and this permission notice shall be included in all
    copies or substantial portions of the Software.
    THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
    IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
    FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
    AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
    LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
    OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
    SOFTWARE.
******************************************************************************
    dotnet-steps version 0.0.1
    https://github.com/seesharper/dotnet-steps
    http://twitter.com/bernhardrichter
******************************************************************************/
using System.ComponentModel;
using System.Reflection;

/// <summary>
/// Represents a synchronous step.
/// </summary>
public delegate void Step();

/// <summary>
/// Represents an asynchronous step.
/// </summary>
/// <returns></returns>
public delegate Task AsyncStep();

/// <summary>
/// Represents a function that displays a summary report.
/// </summary>
/// <param name="results"></param>
public delegate void SummaryStep(IEnumerable<StepResult> results);

// Dummy lambda to obtain the submission instance.
Action stepsDummyaction = () => StepsDummy();

void StepsDummy() { }


StepRunner.Initialize(stepsDummyaction.Target);

public static async Task ExecuteSteps(IList<string> args)
{
    await StepRunner.Execute(args);
}


public static void ShowHelp(this StepInfo[] steps)
{
    WriteLine("---------------------------------------------------------------------");
    WriteLine("Available Steps");
    WriteLine("---------------------------------------------------------------------");
    var stepMaxWidth = steps.Select(s => $"{s.Name}".Length).OrderBy(l => l).Last() + 15;
    WriteLine($"{"Step".PadRight(stepMaxWidth)}Description");
    WriteLine($"{"".PadRight(stepMaxWidth - 15, '-')}{"".PadLeft(15)}{"".PadRight(18, '-')}");
    foreach (var step in steps)
    {
        var name = step.Name + (step.IsDefault ? " (default)" : string.Empty);
        Write(name.PadRight(stepMaxWidth, ' '));
        WriteLine(step.Description);
    }
}

public static void ShowSummary(this StepResult[] results)
{
    if (results.Length == 0)
    {
        return;
    }

    WriteLine("---------------------------------------------------------------------");
    WriteLine("Steps Summary");
    WriteLine("---------------------------------------------------------------------");
    var stepMaxWidth = results.Select(s => $"{s.Name}".Length).OrderBy(l => l).Last() + 15;
    WriteLine($"{"Step".PadRight(stepMaxWidth)}Duration{"".PadLeft(10)} Total");

    WriteLine($"{"".PadRight(stepMaxWidth - 15, '-')}{"".PadLeft(15)}{"".PadRight(16, '-')}{"".PadLeft(3)}{"".PadRight(16, '-')}");
    TimeSpan total = TimeSpan.Zero;
    foreach (var result in results)
    {
        total = total.Add(result.Duration);
        WriteLine($"{result.Name.PadRight(stepMaxWidth)}{result.Duration.ToString()}{"".PadLeft(3)}{result.TotalDuration.ToString()}");
    }
    WriteLine("---------------------------------------------------------------------");
    WriteLine($"{"Total".PadRight(stepMaxWidth)}{total.ToString()}");
}

[DebuggerStepThrough]
private static class StepRunner
{
    private static object _submission;
    private static Type _submissionType;

    private static Stack<StepResult> _callStack = new Stack<StepResult>();

    private static List<StepResult> _results = new List<StepResult>();

    private static bool HasWrappedFields;


    [EditorBrowsable(EditorBrowsableState.Never)]
    internal static void Initialize(object submission)
    {
        _submission = submission;
        _submissionType = submission.GetType();
    }

    public async static Task Execute(IList<string> stepNames)
    {
        if (!HasWrappedFields)
        {
            WrapFields(_results);
            HasWrappedFields = true;
        }

        var stepDelegates = GetStepDelegates();

        if (stepNames.Contains("help", StringComparer.OrdinalIgnoreCase))
        {
            ShowHelp(stepDelegates.Values.ToArray());
            return;
        }

        if (stepDelegates.Keys.Intersect(stepNames).Count() == 0)
        {
            await GetDefaultDelegate(stepDelegates)();
        }

        foreach (var stepName in stepNames)
        {
            _callStack.Clear();

            // if (stepName.Equals("help", StringComparison.OrdinalIgnoreCase))
            // {
            //     stepDelegates.Values.ToArray().ShowHelp();
            //     break;
            // }

            if (stepDelegates.TryGetValue(stepName, out var stepDelegate))
            {
                await stepDelegate.Invoke();
                continue;
            }
        }

        GetSummaryStepDelegate()(_results);
        _results.Clear();
    }

    private static void WrapFields(List<StepResult> results)
    {
        WrapStepFields();
        WrapAsyncStepFields(results);
    }

    private static void WrapStepFields()
    {
        var stepFields = GetStepFields<Step>();
        foreach (var stepField in stepFields)
        {
            var step = GetStepDelegate<Step>(stepField);
            Step wrappedStep = () =>
            {
                StepResult stepresult = PushStepResultOntoCallStack(stepField);
                var stopWatch = Stopwatch.StartNew();
                step();
                stopWatch.Stop();
                PopCallStackAndUpdateDurations(stepresult, stopWatch);
            };
            stepField.SetValue(stepField.IsStatic ? null : _submission, wrappedStep);
        }
    }

    private static void WrapAsyncStepFields(List<StepResult> results)
    {
        var stepFields = GetStepFields<AsyncStep>();
        foreach (var stepField in stepFields)
        {
            var step = GetStepDelegate<AsyncStep>(stepField);
            AsyncStep wrappedStep = async () =>
            {
                StepResult stepresult = PushStepResultOntoCallStack(stepField);
                var stopWatch = Stopwatch.StartNew();
                await step();
                stopWatch.Stop();
                PopCallStackAndUpdateDurations(stepresult, stopWatch);
            };
            stepField.SetValue(stepField.IsStatic ? null : _submission, wrappedStep);
        }
    }

    private static StepResult PushStepResultOntoCallStack(FieldInfo stepField)
    {
        var stepresult = new StepResult(stepField.Name, TimeSpan.Zero, TimeSpan.Zero);
        _callStack.Push(stepresult);
        return stepresult;
    }

    private static void PopCallStackAndUpdateDurations(StepResult stepresult, Stopwatch stopWatch)
    {
        var durationForThisStep = stopWatch.Elapsed;
        stepresult.TotalDuration = durationForThisStep;
        _results.Add(_callStack.Pop());

        if (_callStack.Count > 0)
        {
            var callingStep = _callStack.Peek();
            callingStep.Duration = callingStep.Duration.Subtract(durationForThisStep);
        }
        stepresult.Duration = stepresult.Duration.Add(durationForThisStep);
    }

    private static SummaryStep GetSummaryStepDelegate()
    {
        var summarySteps = GetStepDelegates<SummaryStep>();
        if (summarySteps.Length > 1)
        {
            throw new InvalidOperationException("Found multiple summary steps");
        }

        if (summarySteps.Length == 1)
        {
            return summarySteps[0];
        }

        return results => results.ToArray().ShowSummary();
    }

    private static Func<Task> GetDefaultDelegate(Dictionary<string, StepInfo> stepDelegates)
    {
        if (stepDelegates.Count == 1)
        {
            return stepDelegates.First().Value.Invoke;
        }

        var defaultStepDelegate = stepDelegates.Values.Where(si => si.IsDefault).SingleOrDefault();
        if (defaultStepDelegate != null)
        {
            return defaultStepDelegate.Invoke;
        }

        return () =>
        {
            stepDelegates.Values.ToArray().ShowHelp();
            return Task.CompletedTask;
        };

    }

    private static Dictionary<string, StepInfo> GetStepDelegates()
    {
        var stepFields = GetStepFields<Step>();
        List<StepInfo> results = new List<StepInfo>();
        foreach (var stepField in stepFields)
        {
            StepInfo stepInfo = new StepInfo(stepField.Name, GetStepDescription(stepField), RepresentsDefaultStep(stepField), () => { GetStepDelegate<Step>(stepField)(); return Task.CompletedTask; });
            results.Add(stepInfo);
        }

        var asyncStepFields = GetStepFields<AsyncStep>();
        foreach (var asyncStepField in asyncStepFields)
        {
            StepInfo stepInfo = new StepInfo(asyncStepField.Name, GetStepDescription(asyncStepField), RepresentsDefaultStep(asyncStepField), () => GetStepDelegate<AsyncStep>(asyncStepField)());
            results.Add(stepInfo);
        }

        return results.ToDictionary(si => si.Name, si => si, StringComparer.OrdinalIgnoreCase);
    }

    private static TStep GetStepDelegate<TStep>(FieldInfo stepField)
    {
        return (TStep)(stepField.IsStatic ? stepField.GetValue(null) : stepField.GetValue(_submission));
    }

    private static TStep GetStepDelegate<TStep>(PropertyInfo property)
    {
        return (TStep)(property.GetMethod.IsStatic ? property.GetValue(null) : property.GetValue(_submission));
    }

    private static TStep[] GetStepDelegates<TStep>()
    {
        var fieldSteps = GetStepFields<TStep>().Select(f => GetStepDelegate<TStep>(f));
        var propertySteps = GetStepProperties<TStep>().Select(p => GetStepDelegate<TStep>(p));
        return fieldSteps.Concat(propertySteps).ToArray();
    }

    private static string GetStepDescription(MemberInfo stepFieldInfo)
    {
        return stepFieldInfo.GetCustomAttribute<StepDescriptionAttribute>()?.Description ?? string.Empty;
    }

    private static bool RepresentsDefaultStep(MemberInfo memberInfo)
    {
        return memberInfo.IsDefined(typeof(DefaultStepAttribute)) || memberInfo.Name.Equals("defaultstep", StringComparison.OrdinalIgnoreCase);
    }

    private static FieldInfo[] GetStepFields<TStep>()
    {
        return _submissionType.GetFields().Where(f => f.FieldType == typeof(TStep)).ToArray();
    }

    private static PropertyInfo[] GetStepProperties<TStep>()
    {
        return _submissionType.GetProperties().Where(f => f.PropertyType == typeof(TStep)).ToArray();
    }
}

[AttributeUsage(System.AttributeTargets.Field | System.AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
public sealed class StepDescriptionAttribute : Attribute
{
    public StepDescriptionAttribute(string description)
    {
        Description = description;
    }

    public string Description { get; }
}

[AttributeUsage(System.AttributeTargets.Field | System.AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
public sealed class DefaultStepAttribute : Attribute
{
    public DefaultStepAttribute()
    {
    }
}

public class StepResult
{
    public StepResult(string name, TimeSpan duration, TimeSpan totalDuration)
    {
        Name = name;
        Duration = duration;
        TotalDuration = totalDuration;
    }

    public string Name { get; }
    public TimeSpan Duration { get; set; }
    public TimeSpan TotalDuration { get; set; }
}


public class StepInfo
{
    private readonly Func<Task> _step;

    public StepInfo(string name, string description, bool isDefault, Func<Task> step)
    {
        Name = name;
        Description = description;
        IsDefault = isDefault;
        _step = step;
    }

    public string Name { get; }
    public string Description { get; }
    public bool IsDefault { get; }

    public async Task Invoke()
    {
        await _step();
    }
}