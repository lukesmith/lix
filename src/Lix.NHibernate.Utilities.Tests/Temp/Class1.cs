using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lix.Commons.Specifications;
using Lix.Commons.Tests.Examples;
using MbUnit.Framework;
using NHibernate;
using NHibernate.Criterion;

namespace Lix.NHibernate.Utilities.Tests.Temp
{
    //[TestFixture]
    //public class Class1 : NHibernate.Utilities.Tests.using_a_nhibernate_specification_executor<Lix.Commons.Specifications.DefaultNHibernateCriteriaSpecificationExecutor<Fish>, S>
    //{
    //    protected override DefaultNHibernateCriteriaSpecificationExecutor<Fish> GetExecutor(S specification)
    //    {
    //        return new DefaultNHibernateCriteriaSpecificationExecutor<Fish>(specification, this.Session);
    //    }

    //    protected override S GetSpecificationForMultipleUniqueResult()
    //    {
    //        return new S(string.Empty);
    //    }

    //    protected override S GetSpecificationForUniqueResult(string description)
    //    {
    //        return new S(description);
    //    }

        
    //}
    //public class Person
    //{
        
    //}

    //public class S : Lix.Commons.Specifications.INHibernateCriteriaSpecification
    //{
    //    private readonly string description;

    //    public S(string description)
    //    {
    //        this.description = description;
    //    }

    //    public ICriteria Build(ISession context)
    //    {
    //        var per = context.CreateCriteria<Fish>();
    //        var qw = CriteriaTransformer.Clone(per);
    //        qw = qw.SetProjection(Projections.Count("Description"));

    //        per = per.Add(Expression.Like("Description", description));
    //        per = per.SetProjection(Projections.Count("Description"));
    //        var d = per.FutureValue<int>();

            
    //        qw = qw.Add(Expression.Not(Expression.Like("Description", description)));
    //        var jj = qw.FutureValue<int>();

    //        var t = d.Value;
    //        var q = jj.Value;

    //        var aad = context.CreateMultiCriteria();
    //        aad = aad.Add(per);
    //        aad = aad.Add(qw);

    //        var daa = aad.List();
            
    //        return per;
    //    }

    //    public object Build(object context)
    //    {
    //        throw new NotImplementedException();
    //    }
    //}
}
