# Why this program was written

The Nordic SDK folder is structured so that many include files which are needed for a project are in different folders. All of the folder paths need to be added to the c_user_include_directories definition for the project to compile. 

It is time consuming to search for each include file that a compile will complain about being missing so that it's path can be added to the project c_user_include_directories. It is easy to end up with duplicate paths and unnecessary paths.

 
# Installation
The [executable] is a single file which runs on Windows 10 assuming the .NET Framework 4.7.2 is installed, so it does not need an installer.  
