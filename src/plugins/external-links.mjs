import { defineHastPlugin } from "satteri";

const ROOT_PATH_PREFIX = "/";
const FRAGMENT_IDENTIFIER_PREFIX = "#";
const EMAIL_LINK_PREFIX = "mailto:";
const MS_LEARN_TRACKING_PARAM = "wt.mc_id=MVP_367508";

function isInternalLink(url) {
  return (
    url.startsWith(ROOT_PATH_PREFIX) ||
    url.startsWith(FRAGMENT_IDENTIFIER_PREFIX) ||
    url.startsWith(EMAIL_LINK_PREFIX)
  );
}

function isRelativeLink(url) {
  return !url.includes("://");
}

export const externalLinks = defineHastPlugin({
  name: "external-links",
  element: {
    filter: ["a"],
    visit(node, ctx) {
      const url = node.properties.href;

      if (typeof url !== "string" || isInternalLink(url) || isRelativeLink(url)) {
        return;
      }

      if (url.includes("learn.microsoft.com") && !url.includes("wt.mc_id")) {
        const separator = url.includes("?") ? "&" : "?";
        ctx.setProperty(node, "href", `${url}${separator}${MS_LEARN_TRACKING_PARAM}`);
      }

      ctx.setProperty(node, "target", "_blank");
      ctx.setProperty(node, "rel", "noopener noreferrer");
    },
  },
});
