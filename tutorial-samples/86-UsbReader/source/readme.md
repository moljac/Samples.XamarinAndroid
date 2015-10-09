# Xamarin SDK Bindings

## Steps

1.	generate bindings projects
	*	Brightcove.Bindings.XamarinAndroid[.csproj]
	*	Brightcove.Bindings.XamarinIOS[.csproj]
	*	Brightcove.Bindings.XamarinIOS-Classic[.csproj]
	

## XamarinAndroid

### Enable Intellisense/Autocompletion in Generated code

MCWs Managed Callable Wrappers are generated in

	obj\Debug\generated\src\*.cs
	obj\Release\generated\src\*.cs
	
	obj\'$(Configuration)'\generated\src\*.cs

	
Add following xml snippet into Android Bindings project file (csproj)

	<ItemGroup>
	   <Compile Include="obj\'$(Configuration)'\generated\src\*.cs" />
	 </ItemGroup>
 