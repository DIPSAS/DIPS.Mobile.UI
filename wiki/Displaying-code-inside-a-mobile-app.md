You might want to display code inside your mobile app for people to see. To do this, you can use our syntax highlighting components.

# Inspiration
[highlight.js](http://highlightjs.org).

# Usage

To display code for people, use the `<CodeViewer/>`. This will display a web view with a javascript library for people to interact with.


```xml
<dui:CodeViewer Code="{Binding MyJson}" Language="json" />
```

> This is JSON example, but you can use any of the supported languages of highlight.js that does not require a additional package.

# Properties
Inspect the [components properties class](https://github.com/DIPSAS/DIPS.Mobile.UI/blob/main/src/library/DIPS.Mobile.UI/Components/SyntaxHighlighting/Json/JsonViewer.Properties.cs) to further customize and use it.