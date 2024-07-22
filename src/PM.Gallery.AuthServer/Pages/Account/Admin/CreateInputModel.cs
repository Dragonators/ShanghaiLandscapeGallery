// Copyright (c) Duende Software. All rights reserved.
// See LICENSE in the project root for license information.


using System.ComponentModel.DataAnnotations;

namespace PM.Gallery.AuthServer.Pages.Account.Admin;
public class CreateInputModel
{
    [Required(ErrorMessage = "用户名是必填的")]
	[RegularExpression(@"^[\w\-]{2,16}$", ErrorMessage = "用户名只能包含字母、数字和-_，长度不短于2不超过16")]
    public string Username { get; set; }
	public string Id { get; set; }
	
}