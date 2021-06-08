using System;
using System.Collections.Generic;
using System.Text.Json;
using Sandbox;

namespace PermissionSystem
{
  public class JsonFileParser : FileParserBase
  {
    private PermissionBundle _permissionBundle;
    public PermissionBundle PermissionBundle
    {
      get
      {
        if (_permissionBundle == null)
        {
          _permissionBundle = LoadEverything();
        }
        return _permissionBundle;
      }
    }
    private static JsonRoot ReadPermissionsFile()
    {
      return FileSystem.Mounted.ReadJson<JsonRoot>("data/permissions/permissions.json");
    }

    private static Dictionary<string, string> Convert(List<JsonMetadata> metadataList)
    {
      Dictionary<string, string> metadata = new();
      if (metadataList == null)
      {
        return metadata;
      }

      foreach (JsonMetadata jsonMetadata in metadataList)
      {
        metadata.Add(jsonMetadata.key, jsonMetadata.value);
      }
      return metadata;
    }
    private static List<Permission> Convert(List<string> jsonPermissions)
    {
      List<Permission> permissionList = new();

      if (jsonPermissions == null)
      {
        return permissionList;
      }

      jsonPermissions.ForEach(x =>
      {
        permissionList.Add(new Permission()
        {
          Enabled = !x.Trim().StartsWith('!'),
          Pattern = x.Trim().TrimStart('!')
        });
      });
      return permissionList;
    }

    private static Dictionary<string, Group> Convert(List<JsonGroup> jsonGroups)
    {
      Dictionary<string, Group> groups = new();

      if (jsonGroups?.Count > 0)
      {
        foreach (JsonGroup jsonGroup in jsonGroups)
        {
          Group group = new();

          group.Name = jsonGroup.name;
          group.Weight = jsonGroup.weight;
          group.Immunity = jsonGroup.immunity ?? jsonGroup.weight + 1;
          group.Roles = jsonGroup.roles;
          group.Metadata = Convert(jsonGroup.metadata);

          groups.Add(group.Name, group);
        }
      }
      else
      {
        Log.Error("No groups found in permissions file");
      }

      return groups;
    }
    private static Dictionary<string, User> Convert(List<JsonUser> jsonUsers, Dictionary<string, Group> groups, Group defaultGroup)
    {
      Dictionary<string, User> users = new();

      if (jsonUsers?.Count > 0)
      {
        // Get list of users
        foreach (JsonUser jsonUser in jsonUsers)
        {
          User user = new();
          user.SteamId = jsonUser.steamId;
          user.Weight = jsonUser.overrides.weight;
          user.Immunity = jsonUser.overrides.immunity;
          user.Permissions = Convert(jsonUser.overrides.permissions);
          user.Roles = jsonUser.roles;
          user.Metadata = Convert(jsonUser.metadata);

          if (groups.ContainsKey(jsonUser.group))
          {
            user.Group = groups[jsonUser.group];
          }
          else
          {
            user.Group = defaultGroup;
          }

          users.Add(user.SteamId, user);
        }
      }
      else
      {
        Log.Warning("No users found in permissions file");
      }
      return users;
    }
    private static Options Convert(JsonOptions jsonOptions, Dictionary<string, Group> groups)
    {
      // Get options
      Options options = new();

      if (groups.ContainsKey(jsonOptions.defaultGroup))
      {
        options.DefaultGroup = groups[jsonOptions.defaultGroup];
      }
      else
      {
        throw new Exception($"Default group not found: {jsonOptions.defaultGroup}");
      }
      return options;
    }
    private static PermissionBundle Convert(JsonRoot jsonDoc)
    {
      Dictionary<string, Group> groups = Convert(jsonDoc.groups);
      Options options = Convert(jsonDoc.options, groups);
      Dictionary<string, User> users = Convert(jsonDoc.users, groups, options.DefaultGroup);

      // Get list of permissions per group
      if (jsonDoc.permissions?.Count > 0)
      {
        foreach (JsonPermissions jsonPermission in jsonDoc.permissions)
        {
          if (groups.ContainsKey(jsonPermission.group))
          {
            groups[jsonPermission.group].Permissions = Convert(jsonPermission.permissions);
          }
          else
          {
            Log.Error($"Permission group not found: {jsonPermission.group}");
          }
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
      return Convert(parsedJson);
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

    public override bool SaveEveryhing(PermissionBundle bundle)
    {
      throw new System.NotImplementedException();
    }

    public override bool SaveGroups(List<Group> groups)
    {
      throw new System.NotImplementedException();
    }

    public override bool SaveOptions(Options options)
    {
      throw new System.NotImplementedException();
    }

    public override bool SaveUsers(List<User> users)
    {
      throw new System.NotImplementedException();
    }
  }
}