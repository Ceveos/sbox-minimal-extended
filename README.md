# sbox-minimal-extended

![Screenshot](https://files.facepunch.com/garry/8fc638dc-2c62-4ed6-b20a-69c2c5342a9c.jpg)

 Minimal-extended gamemode for s&box.
 
 # Installing
 
 Download as a zip and put the `minimum-extended` folder in your `addons/` folder. It should show up in the menu allowing you to make a new game.
 
 # Installing Addons

Under `minimal-extended/code/addons` you can simply drag-and-drop your addon modules here. If the addon requires any assets (such as models), they have to be manually placed in the correct directory (for example, `minimal-extended/models`) 

# Included Addons

There are a few modules included by default in Minimal-Extended. These modules are considered essential for most servers, and the inclusion of it 

## 1. Permission System

The permissions system is a flexible module that enables developers to restrict commands and actions to clients who have sufficient permission. It is abstracted to where nearly all logic can be overriden to fit any scenario. Additionally, it's simple enough that any developer can use without having to create their own system. Ideally, this will create an ecosystem where all administration systems can be inter-changed as the underlying API is the same (think CAMI for gmod).

This is not, however, an admin suite on its own. Due to game modes having their own unique needs, it is impossible to create an admin suite that would satisfy everyones use case. In addition to the permission system, you will need an administration module to take advantage of it. In the future, there may be a simple admin mod that can be optionally installed.

## 2. Save System

Gone are the days of needing to write your SQL information in every addon. Now, a common storage API can be used and customized to the servers needs.

The save system allows any addon to store data (globally, or on a per-client level). At the moment, the Save System only has a module to store data in RAM. Once S&box allows writing to files, this can be expanded to solutions such as JSON / SQLite / etc..

## 3. Logger

With so many addons, it can be a mess to understand the console output. The logger function prefixes every message with the addons name. Additionally, you can pass in a list of objects, and it'll automatically convert it into a human-readable string.

If you use the following code, the Logger utility can take over the default `Log` function:

```cs
using Logger = AddonLogger.Logger;

...

private static readonly Logger Log = new( AddonInfo.Instance );
```

# FAQ

## Do addons have to be placed under /code/addons?

Addons can be placed anywhere under the `/code/` directory (even nested). However, for consistency, it is recommended to place addons under the `/code/addons/` directory.

## Does this have dependency checking?

Yes, this template will check all the dependencies of your addons and ensure that they:

1. Exist
2. Have the required minimum version

While most issues with dependencies will be caught at compile time due to the nature of using said dependencies, this is a fallback to catch errors early on in runtime. 