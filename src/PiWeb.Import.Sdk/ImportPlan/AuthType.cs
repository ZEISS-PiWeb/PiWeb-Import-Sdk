#region copyright

/* * * * * * * * * * * * * * * * * * * * * * * * * */
/* Carl Zeiss Industrielle Messtechnik GmbH        */
/* Softwaresystem PiWeb                            */
/* (c) Carl Zeiss 2023                             */
/* * * * * * * * * * * * * * * * * * * * * * * * * */

#endregion

namespace Zeiss.PiWeb.Import.Sdk.ImportPlan;

/// <summary>
/// A list of possible authentication types.
/// </summary>
public enum AuthType
{
	/// <summary>
	/// No authentication required.
	/// </summary>
	None,

	/// <summary>
	/// Basic authentication using username and password.
	/// </summary>
	Basic,

	/// <summary>
	/// Single Sign-on authentication based on Kerberos or NTLM authentication.
	/// </summary>
	WindowsSSO,

	/// <summary>
	/// Authentication via client certificate.
	/// </summary>
	Certificate,

	/// <summary>
	/// Authentication via OIDC (piweb cloud)
	/// </summary>
	OIDC
}