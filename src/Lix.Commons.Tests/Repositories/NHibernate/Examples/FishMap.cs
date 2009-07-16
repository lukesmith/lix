using FluentNHibernate.Mapping;
using Lix.Commons.Tests.Examples;

namespace Lix.Commons.Tests.Repositories.NHibernate.Examples
{
    public sealed class FishMap : ClassMap<Fish>
    {
        public FishMap()
        {
            Id(x => x.Id)
                .GeneratedBy.Native();

            Map(x => x.IsDeleted);
        }
    }
}