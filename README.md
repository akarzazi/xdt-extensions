# XDT-Extensions
Package extending the Xml Document Transform (XDT).

![.NET Core](https://github.com/akarzazi/XdtExtensions/workflows/.NET%20Core/badge.svg)

# Nuget Package
.Net Standard 2.1

https://www.nuget.org/packages/XdtExtensions/

# Why
This package overcomes some limitations of XDT mostly around comments and formatting. 

It brings new transform functions that enables injecting comments and spaces while inserting tags.

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

# Contributing

We're glad to know you're interested in the project.

Your contributions are welcome !

## How can I contribute ?

You can contribute in the following ways : 

* Report an issue / Suggest a feature.
* Create a pull request.