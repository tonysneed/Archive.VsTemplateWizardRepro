# VsTemplateWizard Repro

This solution reproduces a problem with VS2015 RC in which a VSIX installer
that includes NuGet packages with the installer, but the packages are not installed.

This is based on the following article:
[Authoring Visual Studio Templates with NuGet Dependencies](https://docs.nuget.org/create/packages-in-visual-studio-templates)

Here are the steps I took to reproduct the issue:

1. Created a VS project template that includes a NuGet package
  - I am using Json.Net for this purpose

2. Create a VSIX installer which includes NuGet packages in a Packages folder
  - The Json.Net package is included with a Build action of Content.
  - Include in VSIX is set to True
  - Set target version range for VS 2013 or greater

3. Modified the vstemplate file in the VS template zip file to add Json.Net
  - Added <WizardExtension> and <WizardData> sections to the file
  - These indicate location of Json.Net package in the VSIX

4. Added the project template to a ProjectTemplates folder
  - Included the template as an Asset in the VSIX project

5. Build the Deploy project to create the VSIX installer
  - Ran the installer to install on both VS2013 and VS2015RC
    + Using Community Edition for both 2013 and 2015RC
  - Unchecked option in VS to restore packages on build
  - Created a new app using the Some App template
    + Newly created app DID have packages installed for VS2013.
    + Newly created app DID NOT have packages installed for VS2015RC.

6. To correct this problem, it is necessary to remove packages.config
   from the project template, and from csproj and vstemplate files.
   - Remove project.config file from the csproj file.
     + References to DLL's from packages may remain in the csproj file.
   - Re-zipped SomeApp-NuGetWizard.zip and replaced it in the ProjectTemplates
     folder of the Deploy project, then built to regenerate VSIX file.
	 + Uninstalled prior version then reinstalled VSIX, retarting Visual Studio.
   - Packages.config and package references will be added to new projects
     created using the project template.
