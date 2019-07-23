namespace ZendeskApi.Client.Resources.Interfaces
{
    public interface IHelpCenterResource
    {
        IHelpCenterCategoriesResource Categories { get; }
        IHelpCenterSectionsResource Sections { get; }
        IHelpCenterArticlesResource Articles { get; }
    }
}
