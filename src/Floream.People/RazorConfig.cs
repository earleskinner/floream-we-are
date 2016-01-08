using Nancy.ViewEngines.Razor;
using System.Collections.Generic;
public class RazorConfig : IRazorConfiguration
{
    public IEnumerable<string> GetAssemblyNames()
    {
        yield return "Floream.People";
        yield return "Floream.People.Datasources";
    }

    public IEnumerable<string> GetDefaultNamespaces()
    {
        yield return "Floream.People";
        yield return "Floream.People.DataSources.Entities";
    }

    public bool AutoIncludeModelNamespace
    {
        get { return true; }
    }
}