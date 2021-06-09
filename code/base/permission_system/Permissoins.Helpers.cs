using System;
using System.Text.RegularExpressions;
using Sandbox;

namespace PermissionSystem
{
  public static partial class Permissions
  {
    private static string WildCardToRegular(string value)
    {
      return "^" + Regex.Escape(value).Replace("\\?", ".").Replace("\\*", ".*") + "$";
    }

    public static int GetImmunity(Client client)
    {
      User user = GetUser(client.SteamId);

      Group userGroup = user?.Group ?? Bundle.Options.DefaultGroup;

      int? userWeight = null;
      if (user != null)
      {
        if (user.Immunity != null)
        {
          return (int)user.Immunity;
        }
        userWeight = user.Weight;
      }

      if (userGroup?.Immunity != null)
      {
        return (int)userGroup.Immunity;
      }
      return userWeight ?? 0;
    }
    public static int GetWeight(Client client)
    {
      User user = GetUser(client.SteamId);

      Group userGroup = user?.Group ?? Bundle.Options.DefaultGroup;

      if (user != null && user.Weight != null)
      {
        return (int)user.Weight;
      }

      if (userGroup?.Weight != null)
      {
        return (int)userGroup.Weight;
      }
      return 0;
    }
    public static Group GetGroup(string group)
    {
      return GroupExists(group) ? Bundle.Groups[group] : null;
    }
    public static bool GroupExists(string group)
    {
      return Bundle.Groups.ContainsKey(group);
    }

    public static User GetUser(ulong steamId)
    {
      return GetUser(steamId.ToString());
    }
    public static User GetUser(string steamId)
    {
      return UserExists(steamId) ? Bundle.Users[steamId] : null;
    }

    public static bool UserExists(ulong steamId)
    {
      return UserExists(steamId.ToString());
    }
    public static bool UserExists(string steamId)
    {
      return Bundle.Users.ContainsKey(steamId.ToString());
    }
  }
}