namespace NET6.Demo.IdentitySer
{
    public class CurrentUser
    {
        //用于在身份验证和授权过程中标识用户。
        public int Id { get; set; }
        
        //用于显示用户的标识信息。
        public string? Name { get; set; }

        //表示当前用户的昵称。昵称是用户自定义的、可选的替代名字，通常用于非正式场景或用户个性化展示。
        public string? NikeName { get; set; }

        //表示当前用户的年龄。
        public int Age { get; set; }

        //表示当前用户所属的角色列表。角色用于定义用户在系统中具有的权限和访问级别。
        //通过该属性，可以将一个或多个角色分配给用户。
        public List<string>? RoleList { get; set; }

        //表示当前用户的描述或简介。通常用于提供关于用户的额外信息或介绍。
        public string? Description { get; set; }
    }
}
