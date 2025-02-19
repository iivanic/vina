using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace vina.Server.Identity.Interfaces
{
    public interface IUserStore<TUser> where TUser : IdentityUser
    {
        Task<IdentityResult> CreateAsync(TUser user, CancellationToken cancellationToken);
        Task<IdentityResult> UpdateAsync(TUser user, CancellationToken cancellationToken);
        Task<IdentityResult> DeleteAsync(TUser user, CancellationToken cancellationToken);
        Task<TUser> FindByIdAsync(string userId, CancellationToken cancellationToken);
        Task<TUser> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken);
    }

    public interface IRoleStore<TRole> where TRole : IdentityRole
    {
        Task<IdentityResult> CreateAsync(TRole role, CancellationToken cancellationToken);
        Task<IdentityResult> UpdateAsync(TRole role, CancellationToken cancellationToken);
        Task<IdentityResult> DeleteAsync(TRole role, CancellationToken cancellationToken);
        Task<TRole> FindByIdAsync(string roleId, CancellationToken cancellationToken);
        Task<TRole> FindByNameAsync(string normalizedRoleName, CancellationToken cancellationToken);
    }

    public interface IUserPasswordStore<TUser> where TUser : IdentityUser
    {
        Task<string> GetPasswordHashAsync(TUser user, CancellationToken cancellationToken);
        Task<bool> HasPasswordAsync(TUser user, CancellationToken cancellationToken);
        Task SetPasswordHashAsync(TUser user, string passwordHash, CancellationToken cancellationToken);
    }

    public interface IUserRoleStore<TUser> where TUser : IdentityUser
    {
        Task AddToRoleAsync(TUser user, string roleName, CancellationToken cancellationToken);
        Task RemoveFromRoleAsync(TUser user, string roleName, CancellationToken cancellationToken);
        Task<IList<string>> GetRolesAsync(TUser user, CancellationToken cancellationToken);
        Task<bool> IsInRoleAsync(TUser user, string roleName, CancellationToken cancellationToken);
        Task<IList<TUser>> GetUsersInRoleAsync(string roleName, CancellationToken cancellationToken);
    }

    public interface IUserClaimStore<TUser> where TUser : IdentityUser
    {
        Task<IList<Claim>> GetClaimsAsync(TUser user, CancellationToken cancellationToken);
        Task AddClaimsAsync(TUser user, IEnumerable<Claim> claims, CancellationToken cancellationToken);
        Task ReplaceClaimAsync(TUser user, Claim claim, Claim newClaim, CancellationToken cancellationToken);
        Task RemoveClaimsAsync(TUser user, IEnumerable<Claim> claims, CancellationToken cancellationToken);
        Task<IList<TUser>> GetUsersForClaimAsync(Claim claim, CancellationToken cancellationToken);
    }

    public interface IUserLoginStore<TUser> where TUser : IdentityUser
    {
        Task AddLoginAsync(TUser user, UserLoginInfo login, CancellationToken cancellationToken);
        Task RemoveLoginAsync(TUser user, string loginProvider, string providerKey, CancellationToken cancellationToken);
        Task<IList<UserLoginInfo>> GetLoginsAsync(TUser user, CancellationToken cancellationToken);
        Task<TUser> FindByLoginAsync(string loginProvider, string providerKey, CancellationToken cancellationToken);
    }

    public interface IUserTokenStore<TUser> where TUser : IdentityUser
    {
        Task SetTokenAsync(TUser user, string loginProvider, string name, string value, CancellationToken cancellationToken);
        Task RemoveTokenAsync(TUser user, string loginProvider, string name, CancellationToken cancellationToken);
        Task<string> GetTokenAsync(TUser user, string loginProvider, string name, CancellationToken cancellationToken);
    }

    public interface IUserEmailStore<TUser> where TUser : IdentityUser
    {
        Task SetEmailAsync(TUser user, string email, CancellationToken cancellationToken);
        Task<string> GetEmailAsync(TUser user, CancellationToken cancellationToken);
        Task<bool> GetEmailConfirmedAsync(TUser user, CancellationToken cancellationToken);
        Task SetEmailConfirmedAsync(TUser user, bool confirmed, CancellationToken cancellationToken);
        Task<TUser> FindByEmailAsync(string normalizedEmail, CancellationToken cancellationToken);
        Task<string> GetNormalizedEmailAsync(TUser user, CancellationToken cancellationToken);
        Task SetNormalizedEmailAsync(TUser user, string normalizedEmail, CancellationToken cancellationToken);
    }

    public interface IUserPhoneNumberStore<TUser> where TUser : IdentityUser
    {
        Task SetPhoneNumberAsync(TUser user, string phoneNumber, CancellationToken cancellationToken);
        Task<string> GetPhoneNumberAsync(TUser user, CancellationToken cancellationToken);
        Task<bool> GetPhoneNumberConfirmedAsync(TUser user, CancellationToken cancellationToken);
        Task SetPhoneNumberConfirmedAsync(TUser user, bool confirmed, CancellationToken cancellationToken);
    }

    public interface IUserTwoFactorStore<TUser> where TUser : IdentityUser
    {
        Task SetTwoFactorEnabledAsync(TUser user, bool enabled, CancellationToken cancellationToken);
        Task<bool> GetTwoFactorEnabledAsync(TUser user, CancellationToken cancellationToken);
    }

    public interface IUserLockoutStore<TUser> where TUser : IdentityUser
    {
        Task<DateTimeOffset?> GetLockoutEndDateAsync(TUser user, CancellationToken cancellationToken);
        Task SetLockoutEndDateAsync(TUser user, DateTimeOffset? lockoutEnd, CancellationToken cancellationToken);
        Task<int> IncrementAccessFailedCountAsync(TUser user, CancellationToken cancellationToken);
        Task ResetAccessFailedCountAsync(TUser user, CancellationToken cancellationToken);
        Task<int> GetAccessFailedCountAsync(TUser user, CancellationToken cancellationToken);
        Task<bool> GetLockoutEnabledAsync(TUser user, CancellationToken cancellationToken);
        Task SetLockoutEnabledAsync(TUser user, bool enabled, CancellationToken cancellationToken);
    }
}