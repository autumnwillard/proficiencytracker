using Microsoft.AspNetCore.Mvc;

public class UserInfoViewComponent : ViewComponent
{
    public IViewComponentResult Invoke()
    {
        var isAuthenticated = User.Identity.IsAuthenticated;
        var username = isAuthenticated ? User.Identity.Name : null;

        var model = new UserInfoViewModel
        {
            IsAuthenticated = isAuthenticated,
            Username = username
        };

        return View(model);
    }
}

public class UserInfoViewModel
{
    public bool IsAuthenticated { get; set; }
    public string Username { get; set; }
}
