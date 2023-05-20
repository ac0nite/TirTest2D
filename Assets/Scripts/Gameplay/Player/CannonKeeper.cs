namespace Gameplay.Player
{
    public interface ICannonKeeper
    {
        Cannon Cannon { get; }
    }
    
    public class CannonKeeper : ICannonKeeper
    {
        private readonly Cannon.Factory _cannonFactory;
        private Cannon _cannon;

        public CannonKeeper(Cannon.Factory cannonFactory)
        {
            _cannonFactory = cannonFactory;
        }

        public Cannon Cannon
        {
            get
            {
                if (_cannon == null)
                    _cannon = _cannonFactory.Create();

                return _cannon;
            } 
        }
    }
}