# XDT-Extensions
Package extending the Xml Document Transform XDT.

![.NET Core](https://github.com/akarzazi/xdt-extensions/workflows/.NET%20Core/badge.svg)

More about the XDT format and syntax :

https://docs.microsoft.com/en-us/previous-versions/aspnet/dd465326(v=vs.110)?redirectedfrom=MSDN#transform-attribute-syntax


This package overcomes some limitations of XDT around comments insertion and formatting. 

It brings new transform functions that enables injecting comments and spaces while inserting elements.

# Try it online

You can try and browse the XML document transformations on the [Playground](https://akarzazi.github.io/xdt-playground).

# Nuget Package
.Net Standard 2.1

https://www.nuget.org/packages/XDT.Extensions

## Table of content
- [Try it online](#try-it-online)
- [Nuget Package](#nuget-package)
- [Use cases](#use-cases)
  * [Add a section with comments above](#add-a-section-with-comments-above)
  * [Insert a section with comments above and below](#insert-a-section-with-comments-above-and-below)
  * [Remove specific comments with XPath](#remove-specific-comments-with-xpath)
- [Usage](#usage)
  * [Notes](#notes)
  * [Troubleshooting](#troubleshooting)
- [Extension Transforms Reference](#extension-transforms-reference)
  * [Insert / Replace](#insert---replace)
  * [Remove](#remove)
- [Contributing](#contributing)
  * [How can I contribute ?](#how-can-i-contribute--)



# Use cases

## Add a section with comments above

Source XML 
```xml
<?xml version="1.0"?>
<configuration>
  <appSettings>
    <add key="IsStaging" value="True" />
    <add key="Environment" value="Dev" />
  </appSettings>
</configuration>
```

Result XML 
```xml
<?xml version="1.0"?>
<configuration>
  <appSettings>
    <add key="IsStaging" value="True" />
    <add key="Environment" value="Dev" />
    <!--   Storage Section   -->
    <add key="storagePath" value="d:/temp/" />
  </appSettings>
</configuration>
```

Transform XDT 
```xml
<?xml version="1.0"?>
<configuration xmlns:xdt="http://schemas.microsoft.com/XML-Document-Transform" xmlns:xdtExt="xdt-extensions" xml:space="preserve" >
  <xdt:Import assembly="XdtExtensions"
             namespace="XdtExtensions" />

  <appSettings>

    <add key="storagePath" value="d:/temp/" xdt:Transform="InsertIfMissingExt" xdt:Locator="Match(key)">
    <xdtExt:before>
    <!--   Storage Section   -->
    </xdtExt:before>
    </add>
    
  </appSettings>
</configuration>
```

## Insert a section with comments above and below

Source XML 
```xml
<?xml version="1.0"?>
<configuration>
  <appSettings>
    <add key="BatchSize" value="100" />
    <add key="IsSandboxed" value="True" />
    <add key="Environment" value="Dev" />
  </appSettings>
</configuration>
```

Result XML 
```xml
<?xml version="1.0"?>
<configuration>
  <appSettings>
    <add key="BatchSize" value="100" />
    <!--   Comments above  -->
    <add key="newKey" value="newValue" />
    <!--   Comments below  -->
    <add key="IsSandboxed" value="True" />
    <add key="Environment" value="Dev" />
  </appSettings>
</configuration>
```

Transform XDT 
```xml
<?xml version="1.0"?>
<configuration xmlns:xdt="http://schemas.microsoft.com/XML-Document-Transform" xmlns:xdtExt="xdt-extensions" xml:space="preserve" >
  <xdt:Import assembly="XdtExtensions"
             namespace="XdtExtensions" />

  <appSettings>

    <add key="newKey" value="newValue" xdt:Transform="InsertBeforeExt(/configuration/appSettings/add[@key='IsSandboxed'])" >
    <xdtExt:before>
    <!--   Comments above  -->
    </xdtExt:before>
    <xdtExt:after>
    <!--   Comments below  -->
    </xdtExt:after>
    </add>
    
  </appSettings>
</configuration>
```

## Remove specific comments with XPath

Source XML 
```xml
<?xml version="1.0"?>
<configuration>
  <appSettings>
    <!-- existing comments -->
    <add key="IsProduction" value="True" />
    <add key="IsStaging" value="True" />

    <!-- this unwanted comment will be removed -->

    <add key="Environment" value="Dev" />

    <!-- this unwanted comment will also be removed -->
  </appSettings>
</configuration>
```

Result XML 
```xml
<?xml version="1.0"?>
<configuration>
  <appSettings>
    <!-- existing comments -->
    <add key="IsProduction" value="True" />
    <add key="IsStaging" value="True" />

    <add key="Environment" value="Dev" />
  </appSettings>
</configuration>
```

Transform XDT 
```xml
<?xml version="1.0"?>
<configuration xmlns:xdt="http://schemas.microsoft.com/XML-Document-Transform" xmlns:xdtExt="xdt-extensions" xml:space="preserve" >
  <xdt:Import assembly="XdtExtensions"
             namespace="XdtExtensions" />

  <appSettings>
   <does_not_matter xdt:Transform="RemoveAllExt(/configuration/appSettings/comment()[contains(.,'unwanted comment')])" />
  </appSettings>
</configuration>
```

# Usage

Check the [demo project](demo) for a complete sample.

```csharp
// using XdtExtensions.Microsoft.Web.XmlTransform

var xml = File.ReadAllText("samples/source.xml");
var xdt = File.ReadAllText("samples/transform.xml");

using (XmlTransformableDocument document = new XmlTransformableDocument() { PreserveWhitespace = true })
using (XmlTransformation transformation = new XmlTransformation(xdt, isTransformAFile: false, null))
{
    document.LoadXml(xml);

    var success = transformation.Apply(document);
    if (!success)
    {
        throw new Exception($"An error has occurred on apply transform, use IXmlTransformationLogger for more details.");
    }

    document.Save(new MemoryStream());

    Console.WriteLine("Result: \n" + document.OuterXml);
}
```
## Notes

A fork of the [XDT package](https://github.com/dotnet/xdt) is bundled with the package to better manage spacing and formatting under the namespace `XdtExtensions.Microsoft.Web.XmlTransform`.

## Troubleshooting

If you have an `assembly not found error` when using the extended transforms, make sure the `XdtExtensions` assembly is loaded by adding the following in the calling assembly.

```csharp
private string _loadXdtExtensionsAssembly = XdtExtensions.DefaultNamespace.Namespace;
```

# Extension Transforms Reference

## Insert / Replace

All the following transform operations that create content can leverage  the meta tags `:before` and `:after` to inject content before and/or after the `TagElement` as shown below.

```XML
    <TagElement  xdt:Transform="TransformOperation" />
    <xdtExt:before>
    <!--   Content injected before the tag  -->
    </xdtExt:before>
    <xdtExt:after>
    <!--   Content injected before   -->
    </xdtExt:after>
    </TagElement>
    
```

| Transform Operation  | Description | 
|----------|-------------|
| InsertExt | Same as Insert | 
| InsertIfMissingExt | Same as InsertIfMissing   |
| InsertAllExt | Insert in all matched locations |
| InsertBeforeExt | Same as InsertBefore |
| InsertAfterExt | Same as InsertAfter |
| ReplaceExt | Same as Replace |
| ReplaceAllExt | Replaces at all matched locations |

## Remove

The Remove operations can target any node type using XPath as an argument, thus it can be used to remove comments.

Note that the tag and the location do not matter.
```XML
<does_not_matter 
     xdt:Transform="RemoveExt(//appSettings/comment()[contains(.,'unwanted comment')])" />

```

| Transform Operation  | Description | 
|----------|-------------|
| RemoveExt | Removes the first matched node | 
| RemoveAllExt | Removes all matched nodes |


# Contributing

We're glad to know you're interested in the project.

Your contributions are welcome !

## How can I contribute ?

You can contribute in the following ways : 

* Report an issue / Suggest a feature.
* Create a pull request.
