# EmbeddedResourceVirtualPathProvider #

A custom VirtualPathProvider for IIS - load views and assets from Embedded Resources in referenced assemblies . To get started, install into your ASP.NET web application via nuget:

> Install-Package Escc.EmbeddedResourceVirtualPathProvider

Move views and assets into other assemblies, maintaining folder structure. e.g.

`/MyAspNetApp/Views/Thing/Thing.cshtml -> /ThingComponent/Views/Thing/Thing.cshtml`

And set the the files BuildAction as EmbbeddedResource. Make sure your assembly is referenced, register the provider, and you're done!

## Registering the provider

You need to register the virtual path provider when your application starts. There are several ways to do this, such as using `global.asax.cs`, the `App_Start` folder or using [WebActivator](https://github.com/davidebbo/WebActivator). For example, placing this file in `App_Start` will register all loaded assemblies except those starting with `System`.

	public class RegisterVirtualPathProvider
    {
        public static void AppInitialize()
        {
	        var assemblies = System.Web.Compilation.BuildManager.GetReferencedAssemblies()
	            .Cast<Assembly>()
	            .Where(a => a.GetName().Name.StartsWith("System") == false);
	        System.Web.Hosting.HostingEnvironment.RegisterVirtualPathProvider(new EmbeddedResourceVirtualPathProvider.Vpp(assemblies.ToArray()));
		}
	}

You can also look for embedded resources in a specific assembly, and map it to its location on disk, so that it can be refreshed when you edit the files during development.

	public class RegisterVirtualPathProvider
    {
        public static void AppInitialize()
        {
 			System.Web.Hosting.HostingEnvironment.RegisterVirtualPathProvider(new EmbeddedResourceVirtualPathProvider.Vpp()
            {
                {typeof (SomeAssembly.SomeClass).Assembly, @"..\LocationOfSomeAssemblyProject"}
            });
		}
	}

You can set up rules determining the order to check assemblies for resources, letting you (for example) have different view assemblies for different hostnames.

There is some help at https://github.com/mcintyre321/EmbeddedResourceVirtualPathProvider/wiki/Help

Please check out my other projects! 

Cheers, Harry

@mcintyre321

MIT Licenced

## NuGet package ##

The NuGet package is built using the [NuBuild](https://github.com/bspell1/NuBuild) extension for Visual Studio.