# MvcInteropX

[![Build status](https://img.shields.io/appveyor/ci/EricNewton/MvcInterop.svg)](https://ci.appveyor.com/project/EricNewton/mvcinterop)
[![NuGet](https://img.shields.io/nuget/v/MvcInteropX.svg)](https://www.nuget.org/packages/MvcInteropX)

This is a fork of Zeus MvcInterop to provide MVC capabilities to WebForms pages.

## Install using Nuget:
```
nuget install MvcInteropX
```

## Quick Examples

The MvcInteropX library helps you integrate newer MVC with an older webforms based web application.  In many cases, simply changing the base class for the webform to an MvcInterop.InteropPage type is enough.  User Controls get the same treatment, via MvcInterop.InteropUserControl.

Normally MVC views provide a typed Model property, and MvcInteropX supports this too via ```MvcInterop.InteropPage<Tmodel>``` and ```MvcInterop.InteropControl<Tmodel>```.

If you desire creating reusable controllers and views, and have to work with an existing WebForms application, MvcInteropX helps you achieve that goal!
