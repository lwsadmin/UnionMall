using System.Threading.Tasks;
using Abp.Configuration;
using Abp.Runtime.Session;
using Abp.Zero.Configuration;
using Microsoft.AspNetCore.Identity;
using UnionMall.Authorization.Accounts.Dto;
using UnionMall.Authorization.Users;
namespace UnionMall.Authorization.Accounts
{
    public class AccountAppService : UnionMallAppServiceBase, IAccountAppService
    {
        private readonly UserRegistrationManager _userRegistrationManager;
        public readonly IAbpSession _AbpSession;
        private readonly UserManager _userManager;
        private readonly IPasswordHasher<User> _passwordHasher;
        public AccountAppService(IPasswordHasher<User> passwordHasher,
            UserRegistrationManager userRegistrationManager, IAbpSession AbpSession, UserManager userManager)
        {
            _userRegistrationManager = userRegistrationManager;
            _AbpSession = AbpSession;
            _userManager = userManager;
            _passwordHasher = passwordHasher;
        }
        //[RemoteService(IsEnabled = false)]
        public async Task<IsTenantAvailableOutput> IsTenantAvailable(IsTenantAvailableInput input)
        {
            var tenant = await TenantManager.FindByTenancyNameAsync(input.TenancyName);
            if (tenant == null)
            {
                return new IsTenantAvailableOutput(TenantAvailabilityState.NotFound);
            }

            if (!tenant.IsActive)
            {
                return new IsTenantAvailableOutput(TenantAvailabilityState.InActive);
            }

            return new IsTenantAvailableOutput(TenantAvailabilityState.Available, tenant.Id);
        }

        public async Task<RegisterOutput> Register(RegisterInput input)
        {
            var user = await _userRegistrationManager.RegisterAsync(
                input.Name,
                input.Surname,
                input.EmailAddress,
                input.UserName,
                input.Password,
                true // Assumed email address is always confirmed. Change this if you want to implement email confirmation.
            );

            var isEmailConfirmationRequiredForLogin = await SettingManager.GetSettingValueAsync<bool>(AbpZeroSettingNames.UserManagement.IsEmailConfirmationRequiredForLogin);

            return new RegisterOutput
            {
                CanLogin = user.IsActive && (user.IsEmailConfirmed || !isEmailConfirmationRequiredForLogin)
            };
        }

        public bool Unlock(string pwd)
        {
            if (_AbpSession.UserId != null)
            {
                var user = _userManager.GetUserByIdAsync((long)_AbpSession.UserId);
                PasswordVerificationResult emuePwd = _passwordHasher.VerifyHashedPassword(user.Result, user.Result.Password, pwd);

                if (emuePwd == PasswordVerificationResult.Success)
                    return true;
                else
                    return false;

            }
            else
            {

                return false;
            }

        }
    }
}
