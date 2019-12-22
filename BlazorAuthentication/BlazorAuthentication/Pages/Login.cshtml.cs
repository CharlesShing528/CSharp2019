using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

#region �o�̱N�|�O�s�[�J���R�W�Ŷ��ŧi
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
#endregion

namespace BlazorAuthentication.Pages
{
    [AllowAnonymous]
    public class LoginModel : PageModel
    {
        public string ReturnUrl { get; set; }
        public async Task<IActionResult> OnGetAsync(string paramUsername, string paramPassword)
        {
            string returnUrl = Url.Content("~/");
            try
            {
                // �M���w�g�s�b���n�J Cookie ���e
                await HttpContext
                    .SignOutAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme);
            }
            catch { }

            #region �o�̱N�|�n�w��ǤJ���ϥΪ̱b���P�K�X�i������

            #region ���m��²�ƱN�����������ҡA���L�A���m�߱N²�Ƥ�������]�p

            #endregion

            #region �[�J�o�ӨϥΪ̻ݭn�Ψ쪺 �ŧi���� Claim Type
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, paramUsername),
                new Claim(ClaimTypes.Role, "Administrator"),
            };
            #endregion

            #region �إ� �ŧi�������ѧO
            // ClaimsIdentity���O�O�ŧi�������ѧO���������, �]�N�O�ŧi���X�Ҵy�z�������ѧO
            var claimsIdentity = new ClaimsIdentity(
                claims, CookieAuthenticationDefaults.AuthenticationScheme);
            #endregion

            #region �إ�����{�Ҷ��q�ݭn�x�s�����A
            var authProperties = new AuthenticationProperties
            {
                IsPersistent = true,
                RedirectUri = this.Request.Host.Value
            };
            #endregion

            #region �i��ϥεn�J
            try
            {
                await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);
            }
            catch (Exception ex)
            {
                string error = ex.Message;
            }
            #endregion
          
            #endregion

            return LocalRedirect(returnUrl);
        }
    }
}
