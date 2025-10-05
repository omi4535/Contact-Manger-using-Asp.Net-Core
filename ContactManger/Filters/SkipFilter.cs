using Microsoft.AspNetCore.Mvc.Filters;

namespace ContactManger.Filters
{
    public class SkipFilter :Attribute , IFilterMetadata
    {
    }
}
