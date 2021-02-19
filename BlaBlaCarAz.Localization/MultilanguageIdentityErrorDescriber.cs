using BlaBlaCarAz.Localization.Resources;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace BlaBlaCarAz.Localization
{
    public class MultilanguageIdentityErrorDescriber : IdentityErrorDescriber
    {
        private readonly IStringLocalizer _localizer;
        private readonly IStringLocalizer<Resource1> _stringLocalizer;

        public MultilanguageIdentityErrorDescriber(IStringLocalizerFactory factory, IStringLocalizer<Resource1> stringLocalizer)
        {
            var type = typeof(IdentityErrors);
            var assemblyName = new AssemblyName(type.GetTypeInfo().Assembly.FullName);
            _localizer = factory.Create("IdentityErrors", assemblyName.Name);
            _stringLocalizer = stringLocalizer;
        }


        /// <inheritdoc />
        public override IdentityError DefaultError() => new IdentityError { Code = nameof(DefaultError), Description = _localizer[nameof(DefaultError)] };

        /// <inheritdoc />
        public override IdentityError ConcurrencyFailure() => new IdentityError { Code = nameof(ConcurrencyFailure), Description =_localizer[nameof(ConcurrencyFailure)] };

        /// <inheritdoc />
        public override IdentityError PasswordMismatch() => new IdentityError { Code = nameof(PasswordMismatch), Description = _localizer[nameof(PasswordMismatch)] };

        /// <inheritdoc />
        public override IdentityError InvalidToken() => new IdentityError { Code = nameof(InvalidToken), Description = _localizer[nameof(InvalidToken)] };

        /// <inheritdoc />
        public override IdentityError LoginAlreadyAssociated() => new IdentityError { Code = nameof(LoginAlreadyAssociated), Description = _localizer[nameof(LoginAlreadyAssociated)] };

        /// <inheritdoc />
        public override IdentityError InvalidUserName(string userName) => new IdentityError { Code = nameof(InvalidUserName), Description =string.Format(_localizer[nameof(InvalidUserName)],userName) };

        /// <inheritdoc />
        public override IdentityError InvalidEmail(string email) => new IdentityError { Code = nameof(InvalidEmail), Description = string.Format(_localizer[nameof(InvalidEmail)], email) };

        /// <inheritdoc />
        public override IdentityError DuplicateUserName(string userName) => new IdentityError { Code = nameof(DuplicateUserName), Description = String.Format(_localizer[nameof(DuplicateUserName)], userName) };

        /// <inheritdoc />
        public override IdentityError DuplicateEmail(string email) => new IdentityError { Code = nameof(DuplicateEmail), Description = string.Format(_localizer[nameof(DuplicateEmail)], email) };

        /// <inheritdoc />
        public override IdentityError InvalidRoleName(string role) => new IdentityError { Code = nameof(InvalidRoleName), Description = string.Format(_localizer[nameof(InvalidRoleName)], role) };

        /// <inheritdoc />
        public override IdentityError DuplicateRoleName(string role) => new IdentityError { Code = nameof(DuplicateRoleName), Description = string.Format(_localizer[nameof(DuplicateRoleName)], role) };

        /// <inheritdoc />
        public override IdentityError UserAlreadyHasPassword() => new IdentityError { Code = nameof(UserAlreadyHasPassword), Description = _localizer[nameof(UserAlreadyHasPassword)] };

        /// <inheritdoc />
        public override IdentityError UserLockoutNotEnabled() => new IdentityError { Code = nameof(UserLockoutNotEnabled), Description = _localizer[nameof(UserLockoutNotEnabled)] };

        /// <inheritdoc />
        public override IdentityError UserAlreadyInRole(string role) => new IdentityError { Code = nameof(UserAlreadyInRole), Description = string.Format(_localizer[nameof(UserAlreadyInRole)], role) };

        /// <inheritdoc />
        public override IdentityError UserNotInRole(string role) => new IdentityError { Code = nameof(UserNotInRole), Description = string.Format(_localizer[nameof(UserNotInRole)], role) };

        /// <inheritdoc />
        public override IdentityError PasswordTooShort(int length) => new IdentityError { Code = nameof(PasswordTooShort), Description = string.Format(_localizer[nameof(PasswordTooShort)], length) };

        /// <inheritdoc />
        public override IdentityError PasswordRequiresNonAlphanumeric() => new IdentityError { Code = nameof(PasswordRequiresNonAlphanumeric), Description = _localizer[nameof(PasswordRequiresNonAlphanumeric)] };

        /// <inheritdoc />
        public override IdentityError PasswordRequiresDigit() => new IdentityError { Code = nameof(PasswordRequiresDigit), Description = _localizer[nameof(PasswordRequiresDigit)] };

        /// <inheritdoc />
        public override IdentityError PasswordRequiresLower() => new IdentityError { Code = nameof(PasswordRequiresLower), Description = _localizer[nameof(PasswordRequiresLower)] };

        /// <inheritdoc />
        public override IdentityError PasswordRequiresUpper() => new IdentityError { Code = nameof(PasswordRequiresUpper), Description = _localizer[nameof(PasswordRequiresUpper)] };
    }
}
