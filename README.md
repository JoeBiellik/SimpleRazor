SimpleRazor
===========
[![License](http://img.shields.io/badge/license-MIT-brightgreen.svg?style=flat-square)](http://opensource.org/licenses/MIT)
[![NuGet version](https://img.shields.io/nuget/v/SimpleRazor.svg?label=nuget%20version&style=flat-square)](https://www.nuget.org/packages/SimpleRazor)
[![NuGet downloads](https://img.shields.io/nuget/dt/SimpleRazor.svg?label=nuget%20downloads&style=flat-square)](https://www.nuget.org/stats/packages/SimpleRazor?groupby=Version)
[![CI Build Status](https://www.myget.org/BuildSource/Badge/joebiellik?identifier=edeba8ce-7e39-4b43-afef-02c6015486bb)](https://www.myget.org/gallery/joebiellik)

Simple Razor template rendering engine built on top of Microsoft's Razor template system and based on [RazorEngine](https://github.com/Antaris/RazorEngine) but designed to be very simple and lightweight with minimal dependencies. Ideal for non-MVC environments where you just want to render a Razor template without any fuss.

##Dependencies
* Runs under .NET 4.0/4.5 or Mono
* [Microsoft.CSharp](http://msdn.microsoft.com/en-us/library/microsoft.csharp.aspx)
* [Microsoft.AspNet.Razor](https://www.nuget.org/packages/Microsoft.AspNet.Razor)

##Installation
Download and install with [NuGet](https://www.nuget.org/):
```bash
PM> Install-Package SimpleRazor
```

##Quickstart
For more examples see the demo application project.
```csharp
using SimpleRazor;

var template = "@Model.Name says @Model.Message!";
var model = new {
	Name = "Joe",
	Message = "Hello World"
};

string result = Razor.Render(template, model); // "Joe says Hello World!"
```

## Licence
The MIT License (MIT)

Copyright (c) 2014 Joe Biellik

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