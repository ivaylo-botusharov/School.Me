namespace School.Web.Extensions
{
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Routing;
    using School.Web.Infrastructure;
    
    public static class ActionLinkExtensions
    {
        public static MvcHtmlString ActionLinkWithHtml(
            this HtmlHelper htmlHelper, 
            string linkText, 
            string actionName, 
            string controllerName,
            object routeDict,
            object htmlAttributes,
            string htmlElement,
            bool htmlElementBeforeText)
        {
            TagBuilder linkTag = new TagBuilder("a");

            UrlHelper url = new UrlHelper(htmlHelper.ViewContext.RequestContext);

            string actionUrlString = url.Action(actionName, controllerName, routeDict);

            linkTag.Attributes.Add("href", actionUrlString);

            string linkTagInnerHtml;

            if (htmlElementBeforeText)
            {
                linkTagInnerHtml = htmlElement + linkText;
            }
            else
            {
                linkTagInnerHtml = linkText + htmlElement;
            }

            linkTag.InnerHtml = linkTagInnerHtml;

            linkTag.MergeAttributes(new RouteValueDictionary(htmlAttributes));

            return new MvcHtmlString(linkTag.ToString(TagRenderMode.Normal));
        }

        public static MvcHtmlString ActionLinkWithSpan(
            this HtmlHelper htmlHelper,
            string linkText,
            string actionName,
            string controllerName,
            object routeDict,
            object htmlAttributes,
            object spanAttributesDict,
            bool spanBeforeInnerText)
        {
            TagBuilder linkTag = new TagBuilder("a");
            TagBuilder spanTag = new TagBuilder("span");

            if (spanAttributesDict != null)
            {
                spanTag.MergeAttributes(new RouteValueDictionary(spanAttributesDict));
            }

            UrlHelper url = new UrlHelper(htmlHelper.ViewContext.RequestContext);

            string actionUrlString = url.Action(actionName, controllerName, routeDict);

            linkTag.Attributes.Add("href", actionUrlString);

            string linkTagInnerHtml;

            if (spanBeforeInnerText)
            {
                linkTagInnerHtml = spanTag.ToString(TagRenderMode.Normal) + linkText;
            }
            else
            {
                linkTagInnerHtml = linkText + spanTag.ToString(TagRenderMode.Normal);
            }

            linkTag.InnerHtml = linkTagInnerHtml;

            linkTag.MergeAttributes(new RouteValueDictionary(htmlAttributes));

            return new MvcHtmlString(linkTag.ToString(TagRenderMode.Normal));
        }

        public static MvcHtmlString ActionLinkWithSpan(
            this HtmlHelper htmlHelper,
            string linkText,
            string actionName,
            string controllerName,
            object htmlAttributes,
            object spanAttributesDict,
            bool spanBeforeInnerText)
        {
            return ActionLinkWithSpan(
                htmlHelper, linkText, actionName, controllerName, null, htmlAttributes, spanAttributesDict, spanBeforeInnerText);
        }

        public static MvcHtmlString ActionLinkWithSpan(
            this HtmlHelper htmlHelper,
            string linkText,
            string actionName,
            string controllerName,
            object routeDict,
            object htmlAttributes,
            object spanAttributesDict)
        {
            return ActionLinkWithSpan(
                htmlHelper, linkText, actionName, controllerName, routeDict, htmlAttributes, spanAttributesDict, true);
        }

        public static MvcHtmlString ActionLinkWithSpan(
            this HtmlHelper htmlHelper,
            string linkText,
            string actionName,
            string controllerName,
            object htmlAttributes,
            object spanAttributesDict)
        {
            return ActionLinkWithSpan(
                htmlHelper, linkText, actionName, controllerName, null, htmlAttributes, spanAttributesDict, true);
        }

        public static MvcHtmlString ActionLinkWithSpan(
            this HtmlHelper htmlHelper,
            string linkText,
            string actionName,
            object routeDict,
            object htmlAttributes,
            object spanAttributesDict,
            bool spanBeforeInnerText)
        {
            return ActionLinkWithSpan(
                htmlHelper, linkText, actionName, null, routeDict, htmlAttributes, spanAttributesDict, spanBeforeInnerText);
        }

        public static MvcHtmlString ActionLinkWithSpan(
            this HtmlHelper htmlHelper,
            string linkText,
            string actionName,
            object routeDict,
            object htmlAttributes,
            object spanAttributesDict)
        {
            return ActionLinkWithSpan(
                htmlHelper, linkText, actionName, null, routeDict, htmlAttributes, spanAttributesDict, true);
        }

        public static MvcHtmlString ActionLinkWithSpan(
           this HtmlHelper htmlHelper,
           string actionName,
           object routeDict,
           object htmlAttributes,
           object spanAttributesDict)
        {
            return ActionLinkWithSpan(
                htmlHelper, string.Empty, actionName, null, routeDict, htmlAttributes, spanAttributesDict, true);
        }

        public static MvcHtmlString ActionLinkWithSpan(
            this HtmlHelper htmlHelper,
            string linkText,
            string actionName,
            object htmlAttributes,
            object spanAttributesDict,
            bool spanBeforeInnerText)
        {
            return ActionLinkWithSpan(
                htmlHelper, linkText, actionName, null, null, htmlAttributes, spanAttributesDict, spanBeforeInnerText);
        }

        public static MvcHtmlString ActionLinkWithSpan(
            this HtmlHelper htmlHelper,
            string actionName,
            object htmlAttributes,
            object spanAttributesDict)
        {
            return ActionLinkWithSpan(
                htmlHelper, string.Empty, actionName, null, null, htmlAttributes, spanAttributesDict, true);
        }

        public static MvcHtmlString ActionLinkWithSpan(
            this HtmlHelper htmlHelper,
            string linkText,
            string actionName,
            object htmlAttributes,
            object spanAttributesDict)
        {
            return ActionLinkWithSpan(
                htmlHelper, linkText, actionName, null, null, htmlAttributes, spanAttributesDict, true);
        }

        public static MvcHtmlString ActionLinkWithSpanUsingSession(
            this HtmlHelper htmlHelper,
            string linkText,
            string defaultActionName,
            string defaultControllerName,
            string sessionVariableName,
            object htmlAttributes,
            object spanAttributesDict,
            bool spanBeforeInnerText)
        {
            TagBuilder linkTag = new TagBuilder("a");
            TagBuilder spanTag = new TagBuilder("span");

            if (spanAttributesDict != null)
            {
                spanTag.MergeAttributes(new RouteValueDictionary(spanAttributesDict));
            }

            UrlHelper url = new UrlHelper(htmlHelper.ViewContext.RequestContext);

            HttpContext context = HttpContext.Current;

            RedirectUrl redirectUrl = (RedirectUrl)context.Session[sessionVariableName];

            string actionUrlString;

            if (redirectUrl == null || string.IsNullOrEmpty(redirectUrl.RedirectControllerName))
            {
                actionUrlString = url.Action(defaultActionName, defaultControllerName);
            }
            else
            {
                actionUrlString = url.Action(
                    redirectUrl.RedirectActionName, 
                    redirectUrl.RedirectControllerName, 
                    redirectUrl.RedirectParameters);
            }

            linkTag.Attributes.Add("href", actionUrlString);

            string linkTagInnerHtml;

            if (spanBeforeInnerText)
            {
                linkTagInnerHtml = spanTag.ToString(TagRenderMode.Normal) + linkText;
            }
            else
            {
                linkTagInnerHtml = linkText + spanTag.ToString(TagRenderMode.Normal);
            }

            linkTag.InnerHtml = linkTagInnerHtml;

            linkTag.MergeAttributes(new RouteValueDictionary(htmlAttributes));

            return new MvcHtmlString(linkTag.ToString(TagRenderMode.Normal));
        }

        public static MvcHtmlString ActionLinkWithSpanUsingSession(
            this HtmlHelper htmlHelper,
            string linkText,
            string defaultActionName,
            string defaultControllerName,
            string sessionVariableName,
            object htmlAttributes,
            object spanAttributesDict)
        {
            return ActionLinkWithSpanUsingSession(
                htmlHelper, linkText, defaultActionName, defaultControllerName, sessionVariableName, htmlAttributes, spanAttributesDict, true);
        }

        public static MvcHtmlString ActionLinkWithSpanUsingSession(
            this HtmlHelper htmlHelper,
            string defaultActionName,
            string defaultControllerName,
            string sessionVariableName,
            object htmlAttributes,
            object spanAttributesDict)
        {
            return ActionLinkWithSpanUsingSession(
                htmlHelper, null, defaultActionName, defaultControllerName, sessionVariableName, htmlAttributes, spanAttributesDict, true);
        }
    }
}