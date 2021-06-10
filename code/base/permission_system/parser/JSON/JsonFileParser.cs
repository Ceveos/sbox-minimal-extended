using System.Linq;
using System;
using System.Collections.Generic;
using System.Text.Json;
using Sandbox;
using Logger = AddonLogger.Logger;

namespace PermissionSystem
{
  public class JsonFileParser : FileParserBase
  {
    private static readonly Logger Log = new( AddonInfo.Instance );
    private PermissionBundle _permissionBundle;
    public PermissionBundle PermissionBundle
    {
      get
      {
        if ( _permissionBundle == null )
        {
          _permissionBundle = LoadEverything();
        }
        return _permissionBundle;
      }
    }
    private static JsonRoot ReadPermissionsFile()
    {
      return FileSystem.Mounted.ReadJson<JsonRoot>( "data/permissions/permissions.json" );
    }

    private static Dictionary<string, string> Convert( List<JsonMetadata> metadataList )
    {
      Dictionary<string, string> metadata = new();
      if ( metadataList == null )
      {
        return metadata;
      }

      foreach ( JsonMetadata jsonMetadata in metadataList )
      {
        metadata.Add( jsonMetadata.key, jsonMetadata.value );
      }
      return metadata;
    }
    private static List<Permission> Convert( List<string> jsonPermissions )
    {
      List<Permission> permissionList = new();

      if ( jsonPermissions == null )
      {
        return permissionList;
      }

      jsonPermissions.ForEach( x =>
       {
         permissionList.Add( new Permission()
         {
           Enabled = !x.Trim().StartsWith( '!' ),
           Pattern = x.Trim().TrimStart( '!' )
         } );
       } );
      return permissionList;
    }

    private static Dictionary<string, Group> Convert( List<JsonGroup> jsonGroups )
    {
      Dictionary<string, Group> groups = new();

      if ( jsonGroups?.Count == 0 )
      {
        Log.Error( "No groups found in permissions file" );
        throw new Exception( "No groups found in permissions file" );
      }

      foreach ( JsonGroup jsonGroup in jsonGroups )
      {
        Group group = new();

        group.Name = jsonGroup.name;
        group.Weight = jsonGroup.weight ?? Int32.MinValue;
        group.Immunity = jsonGroup.immunity ?? Int32.MinValue;
        group.Roles = jsonGroup.roles;
        group.Metadata = Convert( jsonGroup.metadata );

        groups.Add( group.Name, group );
      }

      // Link inheritance once all groups established
      foreach ( JsonGroup jsonGroup in jsonGroups )
      {
        if ( jsonGroup.inherits != null && groups.ContainsKey( jsonGroup.inherits ) )
        {
          if ( jsonGroup.inherits == jsonGroup.name )
          {
            Log.Error( $"{jsonGroup.name} Group cannot inherit from itself" );
          }
          else
          {
            groups[jsonGroup.name].InheritsFrom = groups[jsonGroup.inherits];
          }
        }
      }

      // Check for circular dependency
      foreach ( Group group in groups.Values )
      {
        if ( group.InheritsFrom == null )
        {
          continue;
        }

        Group mergedGroup = group;
        Group currentGroup = group.InheritsFrom;
        List<string> groupsVisited = new();
        groupsVisited.Add( mergedGroup.Name );

        while ( currentGroup != null )
        {
          if ( groupsVisited.Contains( currentGroup.Name ) )
          {
            Log.Error( $"Circular inheritance detected for group {currentGroup.Name}" );
            break;
          }

          groupsVisited.Add( currentGroup.Name );

          // Set weight / immunity if applicable
          if ( mergedGroup.Weight == Int32.MinValue && currentGroup.Weight != Int32.MinValue )
          {
            mergedGroup.Weight = currentGroup.Weight;
          }

          if ( mergedGroup.Immunity == Int32.MinValue && currentGroup.Immunity != Int32.MinValue )
          {
            mergedGroup.Immunity = currentGroup.Immunity;
          }

          // Copy over new metadata
          if ( currentGroup.Metadata != null )
          {
            mergedGroup.Metadata ??= new();
            foreach ( (string key, string value) in currentGroup.Metadata )
            {
              if ( !mergedGroup.Metadata.ContainsKey( key ) )
              {
                mergedGroup.Metadata.Add( key, value );
              }
            }
          }

          // Copy over roles
          if ( currentGroup.Roles != null )
          {
            mergedGroup.Roles ??= new();
            foreach ( string role in currentGroup.Roles )
            {
              if ( !mergedGroup.Roles.Contains( role ) )
              {
                mergedGroup.Roles.Add( role );
              }
            }
          }

          currentGroup = currentGroup.InheritsFrom;
        }

        // Give weight / immunity a valid value if they were not set
        if ( mergedGroup.Weight == Int32.MinValue )
        {
          Log.Warning( $"{mergedGroup.Name} group has no valid weight; defaulting to 0" );
          mergedGroup.Weight = 0;
        }
        if ( mergedGroup.Immunity == Int32.MinValue )
        {
          mergedGroup.Immunity = mergedGroup.Weight + 1;
        }
      }

      return groups;
    }
    private static Dictionary<string, User> Convert( List<JsonUser> jsonUsers, Dictionary<string, Group> groups, Group defaultGroup )
    {
      Dictionary<string, User> users = new();

      if ( jsonUsers?.Count > 0 )
      {
        // Get list of users
        foreach ( JsonUser jsonUser in jsonUsers )
        {
          User user = new();
          user.SteamId = jsonUser.steamId;
          user.Weight = jsonUser.overrides.weight;
          user.Immunity = jsonUser.overrides.immunity;
          user.Permissions = Convert( jsonUser.overrides.permissions );
          user.Roles = jsonUser.roles;
          user.Metadata = Convert( jsonUser.metadata );

          if ( groups.ContainsKey( jsonUser.group ) )
          {
            user.Group = groups[jsonUser.group];
          }
          else
          {
            user.Group = defaultGroup;
          }

          users.Add( user.SteamId, user );
        }
      }
      else
      {
        Log.Warning( "No users found in permissions file" );
      }
      return users;
    }
    private static Options Convert( JsonOptions jsonOptions, Dictionary<string, Group> groups )
    {
      // Get options
      Options options = new();

      options.ReloadOnHotload = jsonOptions.reloadOnHotload;

      if ( groups.ContainsKey( jsonOptions.defaultGroup ) )
      {
        options.DefaultGroup = groups[jsonOptions.defaultGroup];
      }
      else
      {
        throw new Exception( $"Default group not found: {jsonOptions.defaultGroup}" );
      }
      return options;
    }
    private static PermissionBundle Convert( JsonRoot jsonDoc )
    {
      Dictionary<string, Group> groups = Convert( jsonDoc.groups );
      Options options = Convert( jsonDoc.options, groups );
      Dictionary<string, User> users = Convert( jsonDoc.users, groups, options.DefaultGroup );

      // Get list of permissions per group
      if ( jsonDoc.permissions?.Count > 0 )
      {
        foreach ( JsonPermissions jsonPermission in jsonDoc.permissions )
        {
          if ( groups.ContainsKey( jsonPermission.group ) )
          {
            groups[jsonPermission.group].Permissions = Convert( jsonPermission.permissions );
          }
          else
          {
            Log.Error( $"Permissions for group not found: {jsonPermission.group}" );
          }
        }
      }

      // Update group permissions/immunity based on inheritance
      foreach ( Group group in groups.Values )
      {
        if ( group.InheritsFrom == null )
        {
          continue;
        }

        Group mergedGroup = group;
        Group currentGroup = group.InheritsFrom;

        while ( currentGroup != null )
        {
          // Copy over new permissions
          if ( currentGroup.Permissions != null )
          {
            mergedGroup.Permissions ??= new();
            foreach ( Permission permission in currentGroup.Permissions )
            {
              if ( !mergedGroup.Permissions.Any( x => x.Pattern == permission.Pattern ) )
              {
                mergedGroup.Permissions.Add( permission );
              }
            }
          }

          // Circular dependency already checked
          currentGroup = currentGroup.InheritsFrom;
        }
      }

      // Return combined bundle
      return new PermissionBundle()
      {
        Groups = groups,
        Users = users,
        Options = options
      };
    }

    public override PermissionBundle LoadEverything()
    {
      var parsedJson = ReadPermissionsFile();
      return Convert( parsedJson );
    }
    public override Dictionary<string, Group> LoadGroups()
    {
      return LoadEverything().Groups;
    }
    public override Options LoadOptions()
    {
      return LoadEverything().Options;
    }
    public override Dictionary<string, User> LoadUsers()
    {
      return LoadEverything().Users;
    }

    public override bool SaveEveryhing( PermissionBundle bundle )
    {
      throw new System.NotImplementedException();
    }

    public override bool SaveGroups( List<Group> groups )
    {
      throw new System.NotImplementedException();
    }

    public override bool SaveOptions( Options options )
    {
      throw new System.NotImplementedException();
    }

    public override bool SaveUsers( List<User> users )
    {
      throw new System.NotImplementedException();
    }
  }
}