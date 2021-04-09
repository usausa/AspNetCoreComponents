namespace AspNetCoreComponents.Bootstrap.TagHelpers
{
    using System.Text.Encodings.Web;

    using Microsoft.AspNetCore.Mvc.TagHelpers;
    using Microsoft.AspNetCore.Razor.TagHelpers;

    [HtmlTargetElement(Attributes = ConditionAttributeName)]
    public sealed class DisabledIfTagHelper : TagHelper
    {
        private const string ConditionAttributeName = "disabled-if";

        [HtmlAttributeName(ConditionAttributeName)]
        public bool Condition { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (Condition)
            {
                output.AddClass("disabled", HtmlEncoder.Default);
            }
        }
    }
}
