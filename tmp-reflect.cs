using System;
using System.Linq;
using System.Reflection;

var assemblyPath = Environment.GetCommandLineArgs()[1];
var assembly = Assembly.LoadFrom(assemblyPath);
foreach (var type in assembly.GetTypes().Where(t => t.FullName?.Contains("Handlers.Items") == true && (t.Name.Contains("ItemsViewSource") || t.Name.Contains("ItemsViewAdapter"))))
{
    Console.WriteLine(type.FullName);
    foreach (var member in type.GetMembers(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static | BindingFlags.DeclaredOnly))
    {
        Console.WriteLine("  " + member.MemberType + " " + member.Name);
    }
}
