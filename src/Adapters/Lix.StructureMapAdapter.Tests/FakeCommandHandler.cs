using Lix.Commands;

namespace Lix.StructureMapAdapter.Tests
{
    public class FakeCommandHandler : AbstractCommandHandler<FakeCommand>
    {
        public FakeCommandHandler(ICurrentCommandProvider currentCommandProvider)
        {
            CurrentCommandProvider = currentCommandProvider;
        }

        public ICurrentCommandProvider CurrentCommandProvider 
        {
            get; private set;
        }

        public override void Execute(FakeCommand command)
        {
        }
    }
}