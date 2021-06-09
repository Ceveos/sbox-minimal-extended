using System.Text.RegularExpressions;
using System;
using Sandbox;
using System.Collections.Generic;

namespace PermissionSystem
{
  public static partial class Permissions
  {
    public static event Func<ClientHasPermissionEventArgs, ClientHasPermissionEventArgs> ClientHasPermissionEvent;
    public static event Func<ClientCanTargetEventArgs, ClientCanTargetEventArgs> ClientCanTargetEvent;

    // Client Extension methods

    /// <summary>
    /// Does this client have the required permission 
    /// </summary>
    /// <param name="client">Client whose permissions needs to be checked</param>
    /// <param name="command">Command that you want to run</param>
    /// <returns>True if the client has permission</returns>
    public static bool HasCustomPermission( this Client client, string command )
    {
      if ( client?.IsValid() != true )
      {
        return false;
      }
      var args = new ClientHasPermissionEventArgs( client, command );
      ClientHasPermissionEvent?.Invoke( args );
      return args.HasPermission;
    }

    /// <summary>
    /// Does this client have permission to target the specified client
    /// </summary>
    /// <param name="client">Client who is running the command</param>
    /// <param name="target">Client who is being targetted by the command</param>
    /// <returns>True if the client can target</returns>
    public static bool CanTarget( this Client client, Client target )
    {
      if ( client?.IsValid() != true || target?.IsValid() != true )
      {
        return false;
      }
      var args = new ClientCanTargetEventArgs( client, target );
      ClientCanTargetEvent?.Invoke( args );
      return args.HasPermission;
    }

    /// <summary>
    /// Get the group of a client
    /// </summary>
    /// <param name="client">Client that you want the group of</param>
    /// <returns>Group that the client is in</returns>
    public static Group CurrentGroup( this Client client )
    {
      if ( client?.IsValid() != true )
      {
        return null;
      }

      User user = GetUser( client.SteamId );
      Group group = user?.Group ?? Bundle.Options.DefaultGroup;
      return group;
    }

    /// <summary>
    /// Get the User of a client
    /// </summary>
    /// <param name="client">Client that you want the user of</param>
    /// <returns>User that the client is</returns>
    public static User CurrentUser( this Client client )
    {
      if ( client?.IsValid() != true )
      {
        return null;
      }

      return GetUser( client.SteamId );
    }

    /// <summary>
    /// Get the roles that a client is in
    /// </summary>
    /// <param name="client">Client that you want the roles of</param>
    /// <returns>Roles the client is</returns>
    public static List<string> GetRoles( this Client client )
    {
      List<string> roles = new();
      if ( client?.IsValid() != true )
      {
        return roles;
      }

      User user = GetUser( client.SteamId );
      Group group = user?.Group ?? Bundle.Options.DefaultGroup;

      // Add user roles
      if ( user?.Roles != null )
      {
        user.Roles.ForEach( role =>
         {
           if ( !roles.Contains( role ) )
           {
             roles.Add( role );
           }
         } );
      }

      // Add group roles
      if ( group?.Roles != null )
      {
        group.Roles.ForEach( role =>
         {
           if ( !roles.Contains( role ) )
           {
             roles.Add( role );
           }
         } );
      }

      return roles;
    }

    /// <summary>
    /// Get the metadata of a client. Contains user-specific and group-specific metadata.
    /// </summary>
    /// <param name="client">Client that you want the metadata of</param>
    /// <returns>Metadata of the client</returns>
    public static Dictionary<string, string> GetMetadata( this Client client )
    {
      Dictionary<string, string> metadata = new();
      if ( client?.IsValid() != true )
      {
        return metadata;
      }

      User user = GetUser( client.SteamId );
      Group group = user?.Group ?? Bundle.Options.DefaultGroup;

      // Add group metadata
      if ( group?.Metadata != null )
      {
        foreach ( (string key, string value) in group.Metadata )
        {
          metadata[key] = value;
        }
      }

      // Add group metadata
      if ( user?.Metadata != null )
      {
        foreach ( (string key, string value) in user.Metadata )
        {
          metadata[key] = value;
        }
      }

      return metadata;
    }

    // TODO: Can you get client from Player?
    // private static PlayerHasPermissionEventArgs DoPlayerHasPermissionEvent(PlayerHasPermissionEventArgs args)
    // {
    //   // 
    // }
    private static ClientHasPermissionEventArgs DoClientHasPermissionEvent( ClientHasPermissionEventArgs args )
    {
      if ( args.Handled )
      {
        return args;
      }

      // Check if client is null / invalid
      if ( args.Client?.IsValid() != true )
      {
        args.Handled = true;
        args.HasPermission = false;
        return args;
      }

      User user = GetUser( args.Client.SteamId );
      Group group = user?.Group ?? Bundle.Options.DefaultGroup;

      // Check if permission for this command exists in the user overrides
      if ( user != null )
      {
        foreach ( Permission permission in user.Permissions )
        {
          if ( Regex.IsMatch( args.Command, WildCardToRegular( permission.Pattern ) ) )
          {
            args.Handled = true;
            args.HasPermission = permission.Enabled ?? true;
            return args;
          }
        }
      }

      // Check if permission for this command exists in the group
      if ( group != null )
      {
        foreach ( Permission permission in group.Permissions )
        {
          if ( Regex.IsMatch( args.Command, WildCardToRegular( permission.Pattern ) ) )
          {
            args.Handled = true;
            args.HasPermission = permission.Enabled ?? true;
            return args;
          }
        }
      }
      return args;
    }
    private static ClientCanTargetEventArgs DoClientCanTargetEvent( ClientCanTargetEventArgs args )
    {
      if ( args.Handled )
      {
        return args;
      }

      // We're guaranteed to be able to handle this
      args.Handled = true;

      // Check if client is null / invalid
      if ( args.Client?.IsValid() != true || args.Target?.IsValid() != true )
      {
        args.HasPermission = false;
        return args;
      }

      args.HasPermission = GetWeight( args.Client ) >= GetImmunity( args.Client );

      // args.HasPermission = args.Command.ToLower() == "noclip";
      return args;
    }

  }
}