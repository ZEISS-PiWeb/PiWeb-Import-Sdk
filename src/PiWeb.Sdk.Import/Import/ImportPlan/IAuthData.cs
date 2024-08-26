#region copyright

/* * * * * * * * * * * * * * * * * * * * * * * * * */
/* Carl Zeiss Industrielle Messtechnik GmbH        */
/* Softwaresystem PiWeb                            */
/* (c) Carl Zeiss 2023                             */
/* * * * * * * * * * * * * * * * * * * * * * * * * */

#endregion

using System;
using System.Threading;
using System.Threading.Tasks;

namespace Zeiss.PiWeb.Sdk.Import.ImportPlan;

/// <summary>
/// Represents authentication data required to connect to an import target.
/// </summary>
public interface IAuthData
{
	#region properties

	/// <summary>
	/// The authentication type.
	/// </summary>
	AuthType AuthType { get; }

	/// <summary>
	/// The username used for basic authentication. Only valid when <see cref="AuthType"/> is <see cref="ImportPlan.AuthType.Basic"/>.
	/// </summary>
	string Username { get; }

	/// <summary>
	/// The password used for basic authentication. Only valid when <see cref="AuthType"/> is <see cref="ImportPlan.AuthType.Basic"/>.
	/// </summary>
	string Password { get; }

	/// <summary>
	/// The thumbprint of the client certificate to use. Only valid when <see cref="AuthType"/> is
	/// <see cref="ImportPlan.AuthType.Certificate"/>.
	/// </summary>
	string CertificateThumbprint { get; }

	#endregion

	#region methods

	/// <summary>
	/// Runs the given <paramref name="tokenConsumer"/> with the current refresh token. The <paramref name="tokenConsumer"/> may return
	/// a new refresh token which then replaces the current refresh token for any future calls. This is necessary to correctly handle
	/// servers with refresh token rotation.
	/// When no refresh token is available in the authentication data, <paramref name="tokenConsumer"/> is not run at all and <c>false</c>
	/// will be returned.
	/// </summary>
	/// <param name="tokenConsumer">
	/// A token handling function. This function will be called with the current refresh token as argument. It is expected to use the
	/// refresh token and then return a new refresh token that is usable for a future call. If the current refresh token is still valid
	/// after it was used (no refresh token rotation necessary), the current refresh token can be returned instead of a new token.
	/// </param>
	/// <param name="cancellationToken">A cancellation token to cancel the operation.</param>
	/// <returns>
	/// True if a refresh token was available in the authentication data and <paramref name="tokenConsumer"/> was called;
	/// otherwise, false.
	/// </returns>
	Task<bool> ReadAndUpdateRefreshTokenAsync(Func<string, Task<string>> tokenConsumer, CancellationToken cancellationToken = default);

	/// <summary>
	/// Checks whether the refresh token can be accessed.
	/// </summary>
	public bool CanAccessRefreshToken();

	#endregion
}