using FluentNHibernate.Mapping;
using Lix.Commons.Tests.Examples;

namespace Lix.NHibernate.Utilities.Tests.Repositories.Examples
{
    public sealed class FishMap : ClassMap<Fish>
    {
        public FishMap()
        {
            this.Id(x => x.Id)
                .GeneratedBy.Native();

            this.Map(x => x.IsDeleted);

            this.Map(x => x.Description);
        }
    }
}