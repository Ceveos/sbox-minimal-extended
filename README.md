# sbox-minimal-extended

Modular gamemode with addon support for s&box.
 
# Installing

1. Clone/Download this repo to `common/sbox/workspace`
2. Run `.\watcher.ps1 your-new-gamemode -build` to get started, which will create an `sbox/workspace/your-new-gamemode/` folder you can drop addons into
3. Run `.\watcher.ps1 your-new-gamemode -build` again to **erase** and copy fresh files from `sbox/workspace/your-new-gamemode/` into `sbox/addons/your-new-gamemode/`,
which will then watch for file changes in your workspace, auto copying them to `sbox/addons` where the game will hot reload from.  
Note: `-build`'s copy will likely overwhelm the hotreloader, freezing the game.
4. Launch s&box and your new gamemode will show in the menu, allowing you to start a new game.
5. `.\watcher.ps1 your-new-gamemode` will skip the erase/refresh step, and simply watch for new changes

 
# Installing Addons

Under `sbox/workspace/your-new-gamemode/` you can simply drag-and-drop your addon modules here. Any assets (eg. models) will be copied from each addon to the resulting gamemode's root `models/`, where the game expects. It is recommended (to avoid collisions) to namespace addon assets, eg. `sbox/workspace/my-gamemode/wirebox/models/wirebox/gate.vmdl`, which will be accessible in-game as `models/wirebox/gate.vmdl`.

## List of Optional Addons

Here's some example addons that are compatible:

- [sandbox-plus](https://github.com/nebual/sandbox-plus) - A fork of the official [sandbox](https://github.com/facepunch/sandbox) gamemode,
intended as a possible base for minimal-extended, with an emphasis on extendability
- [Wirebox](https://github.com/wiremod/wirebox) - Wiremod for s&box
- [napkins-chat](https://github.com/Nebual/napkins-chat) - A very small addon that makes the vanilla chat have history

# Builtin Addons

There are a few modules included by default in Minimal-Extended. These modules are considered essential for most servers.

## 1. Permission System

The permissions system is a flexible module that enables developers to restrict commands and actions to clients who have sufficient permission. It is abstracted to where nearly all logic can be overriden to fit any scenario. Additionally, it's simple enough that any developer can use without having to create their own system. Ideally, this will create an ecosystem where all administration systems can be inter-changed as the underlying API is the same (think CAMI for gmod).

This is not, however, an admin suite on its own. Due to game modes having their own unique needs, it is impossible to create an admin suite that would satisfy everyones use case. In addition to the permission system, you will need an administration module to take advantage of it. In the future, there may be a simple admin mod that can be optionally installed.

Look at the following subsections for examples on how to interact with the permission system:

### Defining Commands for Addons

Before we interact with the permission system, it's arguably more important to take a second and ensure you have good design when using commands in your addon. 

Addon commands should follow a node structure in such a way where it's easy to group commands together. Lets take a utility addon as an example. The utility addon will have the following methods: `say`, `trade`, `kill`, `teleport`, `noclip`

One of the ways we can name these commands in a useful way could be the following:
```
utility.normal.say
utility.normal.trade
utility.mod.kill
utility.mod.teleport
utility.admin.noclip
```

The way our structure is built, we can easily give a group access to just the normal commands (`utility.normal.*`) without granting access to the more destructive commands. Additionally, if the utility addon comes out with a new update that introduces new commands, you're already covered.

### Checking Permission for Client

When you want to check for permission, there are two main methods to be aware about: `HasCustomPermission`, and `CanTarget`

#### HasCustomPermission

`HasCustomPermission()` is a simple method that exists under the Client type. This returns a boolean on whether or not the client has permission to run the command. A command is simply a string and can be anything. However, it is recommended to use a node system to make permissions easier to manage (read above for example)

Example code:
```cs
if (client.HasCustomPermission("utility.admin.noclip")) { 
  // perform noclip
} else {
  Log.Warning("Insufficient permission for noclip");
}
```

#### CanTarget

`CanTarget()` is a simple method that exists under the Client type. This returns a boolean on whether or not a client has permission to use a command against another client. For example, if a moderator wanted to use a `kill` command against an admin, we would first check if the mod can target the admin client. If not, the request is denied. The ability to target another client is purely determined by the users `Weight` vs `Immunity`.

`Weight` can be considered how much power a users command has. For example, in the previous example, if the mod had a `Weight` of 50, then the moderator can use the `kill` command on any client who has `Immunity` of 50 or less. Following the previous example, the admin may have had `Immunity` of 100. Therefore the Moderator would not have had sufficient weight to run the command against the admin.

Example code:
```cs
if (client.CanTarget(enemy)) {
  // Perform  command against enemy
} else { 
  Log.Warning("Insufficient permission to run command against target");
}
```

## 2. Save System

Gone are the days of needing to write your SQL information in every addon. Now, a common storage API can be used and customized to the servers needs.

The save system allows any addon to store data (globally, or on a per-client level). At the moment, the Save System only has a module to store data in RAM. Once S&box allows writing to files, this can be expanded to solutions such as JSON / SQLite / etc..

The following code shows an example on how to count the number of times a "hotload" has occured in a S&box session:

```cs
[Event( "hotloaded" )]
public static void OnHotLoad()
{
  if ( IsServer )
  {
    // You create a save module by initiating an instance of one
    // In this case, we're using a RamSaveModule which stores everything in RAM
    // The "Default" instance is what 'database' you want to access. You can put
    // a clients Steam ID, for example, to get a unique data store for that client
    Save.SaveModule db = Save.RamSaveModule.Instance( "Default" ); 

    // You have access to Load and LoadClass. Use Load<T> for primatives,
    // and LoadClass<T> for any non-primative objects.
    int count = db.Load<int>( "hotload_count" );
    Log.Warning( $"[Server] Hotloaded {++count} times" );
    db.Save( "hotload_count", count );
  }
}
```

## 3. Logger

With so many addons, it can be a mess to understand the console output. The logger function prefixes every message with the addons name. Additionally, you can pass in a list of objects, and it'll automatically convert it into a human-readable string.

If your class inherits `AddonClass<T>`, then the logging utility will be overriden by default. 

If your class does not inherit `AddonClass<T>`, then use the following code to ensure the Logger utility takes over the default `Log` function:

```cs
using Logger = AddonLogger.Logger;

...

private static readonly Logger Log = new( AddonInfo.Instance );
```

# FAQ

## Why do I need this Watcher script?

Addons are usually individual git repositories, each with their own models/code, but this causes a few problems for s&box:
- assets like models need to be in the root `sbox/addons/your-gamemode/models` directory to be correctly loaded by connecting players
- on connect, players seem to download almost everything in the gamemode, including `.git` folders, `.blend`'s, etc

The Watcher automates merging these addons' assets together, skipping unused files.

## I'm confused, what file structure am I supposed to have?

- sbox/workspace/watcher.ps1
- sbox/workspace/prop-hunt/better-chat/.git (etc)
- sbox/workspace/prop-hunt/better-chat/code/better-chat/AddonInfo.cs
- sbox/workspace/prop-hunt/better-chat/code/better-chat/ChatWindow.cs
- sbox/workspace/prop-hunt/better-chat/code/prop-hunt-core/AddonInfo.cs
- sbox/workspace/prop-hunt/prop-hunt-core/code/prop-hunt-core/Hunter.cs
- sbox/workspace/prop-hunt/prop-hunt-core/models/prop-hunt-core/box.vmdl
- sbox/workspace/prop-hunt/prop-hunt-core/material/prop-hunt-core/wood.vmdl

## Does this have dependency checking?

Yes, this template will check all the dependencies of your addons and ensure that they:

1. Exist
2. Have the required minimum version

While most issues with dependencies will be caught at compile time due to the nature of using said dependencies, this is a fallback to catch errors early on in runtime. 
