using System.Collections.Immutable;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PM.Gallery.AuthServer.Models;

namespace PM.Gallery.AuthServer.Pages.Account.Admin
{
	[Authorize(Roles = "Admin")]
	public class IndexModel : PageModel
    {
		public UserManager<ApplicationUser> _userManager { get; set; }
		public RoleManager<IdentityRole> _roleManager { get; set; }
		[BindProperty]
		public CreateInputModel Input { get; set; }

		public ImmutableArray<ApplicationUser> IdsUsers { get; set; }
		public IndexModel(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
		{
			_userManager = userManager;
			_roleManager = roleManager;
		}

		public void OnGet()
        { 
			IdsUsers= _userManager.Users.AsNoTracking().OrderBy(d => d.UserName).ToImmutableArray();
        }
		public async Task<IActionResult> OnPostDelete(string id)
		{
			var user = await _userManager.FindByIdAsync(id);
			await _userManager.DeleteAsync(user);
			return RedirectToPage("/Account/Admin/Index");
        }
        public async Task<IActionResult> OnPostUpdate()
        {
			if (TryValidateModel(Input, nameof(Input)))
			{
				var user = await _userManager.FindByIdAsync(Input.Id);
				user.UserName=Input.Username;
				var result = await _userManager.UpdateAsync(user);
				if(result.Succeeded)
				{	
					return RedirectToPage("/Account/Admin/Index");
				}
				else
				{
					result.Errors.ToList().ForEach(e => ModelState.AddModelError(e.Code, e.Description));
					return Page();
				}
			}
			return Page();
		}
		public async Task<IActionResult> OnPostPwdReset(string id_, [Required] string old_pwd, [Required]string new_pwd)
		{
			if (old_pwd is null)
			{
				ModelState.AddModelError(string.Empty, "Please provide correct old pwd.");
				return Page();
			}
			if (new_pwd is null)
			{
				ModelState.AddModelError(string.Empty, "Please provide correct new pwd.");
				return Page();
			}
			var result = await _userManager.ChangePasswordAsync(await _userManager.FindByIdAsync(id_),old_pwd,new_pwd);
			if (result.Succeeded)
			{
				return RedirectToPage("/Account/Admin/Index");
			}
			else
			{
				result.Errors.ToList().ForEach(e => ModelState.AddModelError(e.Code, e.Description));
				return Page();
			}
		}
		public async Task<IActionResult> OnPostRoleUpdate(string id_,string role,string Button)
		{
			if (role is null)
			{
				ModelState.AddModelError(string.Empty, "Please provide a correct role.");
				return Page();
			}
			else
			{
				if (Button == "Remove")
				{
					var result = await _userManager.RemoveFromRoleAsync(await _userManager.FindByIdAsync(id_), role);
					if (result.Succeeded)
					{
						return RedirectToPage("/Account/Admin/Index");
					}
					else
					{
						result.Errors.ToList().ForEach(e => ModelState.AddModelError(e.Code, e.Description));
						return Page();
					}
				}
				else if (Button == "Add")
				{
					var result = await _userManager.AddToRoleAsync(await _userManager.FindByIdAsync(id_), role);
					Console.WriteLine(123456);
					if (result.Succeeded)
					{
						return RedirectToPage("/Account/Admin/Index");
					}
					else
					{
						result.Errors.ToList().ForEach(e => ModelState.AddModelError(e.Code, e.Description));
						return Page();
					}
				}
				ModelState.AddModelError(string.Empty, "invalid Button.");
				return Page();
			}
		}
	}
}
