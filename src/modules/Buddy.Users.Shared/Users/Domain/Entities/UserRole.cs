using System.Collections.Generic;
using Buddy.Domain.Entities;

namespace Buddy.Users.Domain.Entities;

/// <summary>
///     Represents a User role
/// </summary>
public class UserRole : Entity
{
    private ICollection<PermissionRecordUserRoleMapping> _permissionRecordUserRoleMappings;

    /// <summary>
    ///     Gets or sets the User role name
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    ///     Gets or sets a value indicating whether the User role is active
    /// </summary>
    public bool Active { get; set; }

    /// <summary>
    ///     Gets or sets a value indicating whether the User role is system
    /// </summary>
    public bool IsSystemRole { get; set; }

    /// <summary>
    ///     Gets or sets the User role system name
    /// </summary>
    public string SystemName { get; set; }

    /// <summary>
    ///     Gets or sets a value indicating whether the Users must change passwords after a specified time
    /// </summary>
    public bool EnablePasswordLifetime { get; set; }

    /// <summary>
    ///     Gets or sets the permission record-User role mappings
    /// </summary>
    public virtual ICollection<PermissionRecordUserRoleMapping> PermissionRecordUserRoleMappings
    {
        get => _permissionRecordUserRoleMappings ??= new List<PermissionRecordUserRoleMapping>();
        protected set => _permissionRecordUserRoleMappings = value;
    }
}